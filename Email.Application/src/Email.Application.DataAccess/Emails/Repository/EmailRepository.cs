using Email.Application.AppServices.Contracts.Emails.Models;
using Email.Application.AppServices.Contracts.Emails.Repository;
using Email.Application.DataAccess.Emails.Data;
using Email.Application.DataAccess.Infrastructure.MapService;
using Microsoft.EntityFrameworkCore;

namespace Email.Application.DataAccess.Emails.Repository
{
    internal class EmailRepository : IEmailRepository
    {
        private readonly EmailDbContext _dbContext;

        public EmailRepository(EmailDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<long> AddEmail(EmailMessage message, CancellationToken token)
        {
            if (token.IsCancellationRequested)
            {
                return await Task.FromCanceled<long>(token);
            }

            var emailEntity = MappingService.MapToEmailEntity(message);

            try
            {
                var result = await _dbContext.Emails.AddAsync(emailEntity, token);

                await _dbContext.SaveChangesAsync(token);

                return result.Entity.Id;
            }
            catch (Exception e)
            {
                throw new InvalidOperationException($"При добавлении сообщения что-то пошло не так: {e.Message}");
            }
        }

        public async Task<List<EmailMessage>> GetEmails(CancellationToken token)
        {
            if (token.IsCancellationRequested)
            {
                return await Task.FromCanceled<List<EmailMessage>>(token);
            }

            var emailEntities = await _dbContext.Emails
                .AsNoTracking()
                .ToListAsync(token);

            if (emailEntities.Count == 0)
            {
                throw new InvalidOperationException($"Записей в базе данных не найдено!");
            }

            var emailMessages = MappingService.MapToEmailMessages(emailEntities);

            return emailMessages;
        }
    }
}