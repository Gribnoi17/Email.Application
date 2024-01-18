using Email.Application.AppServices.Contracts.Emails.Handlers;
using Email.Application.AppServices.Contracts.Emails.Models;
using Email.Application.AppServices.Contracts.Emails.Repository;

namespace Email.Application.AppServices.Emails.Handlers
{
    /// <inheritdoc cref="IGetEmailsHandler"/>
    internal class GetEmailsHandler : IGetEmailsHandler
    {
        private readonly IEmailRepository _repository;

        public GetEmailsHandler(IEmailRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<EmailMessage>> Handle(CancellationToken token)
        {
            var emailMessages = await _repository.GetEmails(token);

            return emailMessages;
        }
    }
}