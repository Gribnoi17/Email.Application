using Email.Application.AppServices.Contracts.Emails.Configuration;
using Email.Application.AppServices.Contracts.Emails.Enums;
using Email.Application.AppServices.Contracts.Emails.Handlers;
using Email.Application.AppServices.Contracts.Emails.Models;
using Email.Application.AppServices.Contracts.Emails.Repository;
using Email.Application.AppServices.Emails.Handlers;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using Xunit;

namespace Email.Application.AppServices.UnitTests.Emails.Handlers
{
    public class SendEmailHandlerTests
    {
        private readonly SmtpClient _smtpClient;
        private readonly IOptionsMonitor<SmtpSettings> _smtpSettings;
        private readonly IEmailRepository _repository;
        private readonly ISendEmailHandler _sendEmailHandler;

        public SendEmailHandlerTests()
        {
            _smtpClient = Substitute.For<SmtpClient>();
            _repository = Substitute.For<IEmailRepository>();
            
            var mockSmtpSettingsOptions = Substitute.For<IOptionsMonitor<SmtpSettings>>();
            mockSmtpSettingsOptions.CurrentValue.Returns(new SmtpSettings()
            {
                Host = "test-host",
                Port = 123,
                Username = "test-username",
                Password = "test-password"
            });
            _smtpSettings = mockSmtpSettingsOptions;
            
            _sendEmailHandler = new SendEmailHandler(_repository, _smtpSettings, _smtpClient);
        }

        [Fact]
        public async Task Handle_Successfully_SendEmail()
        {
            // Arrange
            var request = new EmailInternalRequest
            {
                Subject = "Test Subject",
                Body = "Test Body",
                Recipients = new List<string> { "test@example.com" }
            };

            var token = CancellationToken.None;
    
            _smtpClient.ConnectAsync(Arg.Any<string>(), Arg.Any<int>(), Arg.Any<SecureSocketOptions>(), Arg.Any<CancellationToken>()).Returns(Task.CompletedTask);
            _smtpClient.AuthenticateAsync(_smtpSettings.CurrentValue.Username, _smtpSettings.CurrentValue.Password, Arg.Any<CancellationToken>()).Returns(Task.CompletedTask);
            _smtpClient.SendAsync(Arg.Any<MimeMessage>(), Arg.Any<CancellationToken>()).Returns(Task.FromResult(string.Empty));
            _smtpClient.DisconnectAsync(true, Arg.Any<CancellationToken>()).Returns(Task.CompletedTask);

            // Act
            await _sendEmailHandler.Handle(request, token);

            // Assert
            await _repository.Received().AddEmail(Arg.Is<EmailMessage>(em => em.Result == EmailSendStatus.Ok), token);
        }
        
        [Fact]
        public async Task Handle_FailsToSendEmail_ThrowsException()
        {
            // Arrange

            var request = new EmailInternalRequest
            {
                Subject = "Test Subject",
                Body = "Test Body",
                Recipients = new List<string> { "testexample.com" }
            };

            var token = CancellationToken.None;
            
            _smtpClient.ConnectAsync(Arg.Any<string>(), Arg.Any<int>(), Arg.Any<SecureSocketOptions>(), Arg.Any<CancellationToken>()).Returns(Task.CompletedTask);
            _smtpClient.AuthenticateAsync(_smtpSettings.CurrentValue.Username, _smtpSettings.CurrentValue.Password, Arg.Any<CancellationToken>()).Returns(Task.CompletedTask);
            _smtpClient.SendAsync(Arg.Any<MimeMessage>(), Arg.Any<CancellationToken>()).ThrowsAsync(new InvalidOperationException());
            _smtpClient.DisconnectAsync(true, Arg.Any<CancellationToken>()).Returns(Task.CompletedTask);

            // Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() => _sendEmailHandler.Handle(request, token));

            await _repository.Received().AddEmail(Arg.Is<EmailMessage>(em => em.Result == EmailSendStatus.Failed), token);
        }
    }
}