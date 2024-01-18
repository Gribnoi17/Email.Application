using Email.Application.AppServices.Contracts.Emails.Enums;
using Email.Application.AppServices.Contracts.Emails.Models;
using Email.Application.AppServices.Contracts.Emails.Repository;
using Email.Application.DataAccess.Emails.Data;
using Email.Application.DataAccess.Emails.Models;
using Email.Application.DataAccess.Emails.Repository;
using Email.Application.DataAccess.UnitTests.Emails.Data;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Email.Application.DataAccess.UnitTests.Emails.Repository
{
    public class EmailRepositoryTests
    {
        private readonly EmailMessageTestData _emailMessageTestData;
        private readonly EmailEntityTestData _emailEntityTestData;
        private readonly DbContextOptions<EmailDbContext> _dbContextOptions;

        public EmailRepositoryTests()
        {
            _dbContextOptions = new DbContextOptionsBuilder<EmailDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase" + Guid.NewGuid())
                .Options;

            _emailMessageTestData = new EmailMessageTestData();
            _emailEntityTestData = new EmailEntityTestData();
        }

        [Fact]
        public async Task AddEmail_WhenSuccessful_AddedEmailInDatabase()
        {
            //Arrange
            var email = new EmailMessage
            {
                Id = 1,
                Subject = "тест",
                Body = "Привет!",
                Recipients = new List<string>()
                {
                    "qweq@gmail.com",
                    "xcvxcvx@yandex.ru"
                },
                SendTime = new DateTime(2024, 02, 01, 12, 30, 00),
                Result = EmailSendStatus.Ok
            };
            
            long resultId;
            
            await using (var context = new EmailDbContext(_dbContextOptions))
            {
                IEmailRepository repository = new EmailRepository(context);
                
                //Act
                resultId = await repository.AddEmail(email, CancellationToken.None);
            }
            
            //Assert
            Assert.Equal(email.Id, resultId);
        }
        
        [Theory]
        [ClassData(typeof(EmailMessageTestData))]
        public async Task AddEmail_WhenNotSuccessful_ThrowsInvalidOperationException(EmailMessage emailMessage)
        {
            //Arrange
            var invalidEntity = new EmailEntity();
            
            await using (var context = new EmailDbContext(_dbContextOptions))
            {
                IEmailRepository repository = new EmailRepository(context);
                
                context.Entry(invalidEntity).State = EntityState.Added;

                //Act & Assert
                await Assert.ThrowsAsync<InvalidOperationException>(() =>
                    repository.AddEmail(emailMessage, CancellationToken.None));
            }
        }
        
        [Fact]
        public void AddEmail_WithCanceledToken_ReturnsCanceledTask()
        {
            //Arrange
            var canceledToken = new CancellationToken(canceled: true);

            Task task;
            
            var email = new EmailMessage
            {
            };

            using (var context = new EmailDbContext(_dbContextOptions))
            {
                IEmailRepository repository = new EmailRepository(context);

                //Act
                task = repository.AddEmail(email, canceledToken);
            }

            //Assert
            Assert.True(task.IsCanceled);
        }
        
        [Fact]
        public async Task GetLoanContractById_WithValidId_ReturnsLoanContract()
        {
            //Arange
            var emails = _emailMessageTestData.GetEmails();
            var emailEntities = _emailEntityTestData.GetEmailEntities();

            await using (var context = new EmailDbContext(_dbContextOptions))
            {
                foreach (var emailEntity in emailEntities)
                {
                    await context.Emails.AddAsync(emailEntity);
                }
                await context.SaveChangesAsync();
            }

            List<EmailMessage> result;

            await using (var context = new EmailDbContext(_dbContextOptions))
            {
                IEmailRepository repository = new EmailRepository(context);
                
                //Act
                result = await repository.GetEmails(CancellationToken.None);
            }
            
            //Assert
            Assert.Equal(emails.Count, result.Count);
            
            for (int i = 0; i < result.Count; i++)
            {
                Assert.Equal(emails[i].Subject, result[i].Subject);
                Assert.Equal(emails[i].Body, result[i].Body);
                Assert.Equal(emails[i].SendTime, result[i].SendTime);
                Assert.Equal(emails[i].Recipients, result[i].Recipients);
                Assert.Equal(emails[i].Result, result[i].Result);
            }
        }
        
        [Fact]
        public async Task GetLoanContractById_WithInvalidId_ThrowsInvalidOperationException()
        {
            //Arange
            long id = -1;
            
            await using (var context = new EmailDbContext(_dbContextOptions))
            {
                IEmailRepository repository = new EmailRepository(context);
                
                //Act & Assert
                await Assert.ThrowsAsync<InvalidOperationException>(() =>
                    repository.GetEmails(CancellationToken.None));
            }
        }
        
        [Fact]
        public void GetLoanContractById_WithCanceledToken_ReturnsCanceledTask()
        {
            // Arrange
            long validId = 1;
            var canceledToken = new CancellationToken(canceled: true);

            Task task;
            
            using (var context = new EmailDbContext(_dbContextOptions))
            {
                IEmailRepository repository = new EmailRepository(context);
                
                //Act
                task = repository.GetEmails(canceledToken);
            }
            
            //Assert
            Assert.True(task.IsCanceled);
        }
    }
}