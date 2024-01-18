using Email.Application.AppServices.Contracts.Emails.Configuration;
using Email.Application.AppServices.Contracts.Emails.Enums;
using Email.Application.AppServices.Contracts.Emails.Handlers;
using Email.Application.AppServices.Contracts.Emails.Models;
using Email.Application.AppServices.Contracts.Emails.Repository;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;

namespace Email.Application.AppServices.Emails.Handlers
{
    /// <inheritdoc cref="ISendEmailHandler"/>
    internal class SendEmailHandler : ISendEmailHandler
    {
        private readonly SmtpSettings _smtpSettings;
        private readonly SmtpClient _smtpClient;
        private readonly IEmailRepository _repository;

        public SendEmailHandler(IEmailRepository repository, IOptionsMonitor<SmtpSettings> smtpSettings, SmtpClient smtpClient)
        {
            _repository = repository;
            _smtpClient = smtpClient;
            _smtpSettings = smtpSettings.CurrentValue;
        }

        public async Task Handle(EmailInternalRequest request, CancellationToken token)
        {
            var message = new EmailMessage()
            {
                Subject = request.Subject,
                Body = request.Body,
                Recipients = request.Recipients,
                SendTime = DateTime.UtcNow
            };
                
            var emailMessage = new MimeMessage();
            
            emailMessage.From.Add(new MailboxAddress("Gribanov", _smtpSettings.Username));
            emailMessage.To.AddRange(message.Recipients.Select(recipient => new MailboxAddress("Recipient", recipient)));
            emailMessage.Subject = message.Subject;

            var bodyBuilder = new BodyBuilder()
            {
                TextBody = message.Body
            };

            emailMessage.Body = bodyBuilder.ToMessageBody();

            try
            {
                await _smtpClient.ConnectAsync(_smtpSettings.Host, _smtpSettings.Port, SecureSocketOptions.StartTls, token);
                await _smtpClient.AuthenticateAsync(_smtpSettings.Username, _smtpSettings.Password, token);
                await _smtpClient.SendAsync(emailMessage, token);
                await _smtpClient.DisconnectAsync(true, token);

                message.Result = EmailSendStatus.Ok;

                await _repository.AddEmail(message, token);
            }
            catch (Exception e)
            {
                message.Result = EmailSendStatus.Failed;
                message.FailedMessage = e.Message;
                
                await _repository.AddEmail(message, token);

                throw new InvalidOperationException($"Отправка сообщения провалилась {e.Message}.");
            }
        }
    }
}