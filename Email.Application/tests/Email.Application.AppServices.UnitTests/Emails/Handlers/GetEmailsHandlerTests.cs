using Email.Application.AppServices.Contracts.Emails.Enums;
using Email.Application.AppServices.Contracts.Emails.Handlers;
using Email.Application.AppServices.Contracts.Emails.Models;
using Email.Application.AppServices.Contracts.Emails.Repository;
using Email.Application.AppServices.Emails.Handlers;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using Xunit;

namespace Email.Application.AppServices.UnitTests.Emails.Handlers
{
    public class GetEmailsHandlerTests
    {
        private readonly IEmailRepository _repository;
        private readonly IGetEmailsHandler _getEmailsHandler;

        public GetEmailsHandlerTests()
        {
            _repository = Substitute.For<IEmailRepository>();
            _getEmailsHandler = new GetEmailsHandler(_repository);
        }

        [Fact]
        public async Task Handle_Success_ReturnsEmails()
        {
            //Arrange
            var exceptedResult = new List<EmailMessage>()
            {
                new()
                {
                    Subject = "тест",
                    Body = "Тест тест",
                    Recipients = new List<string>()
                    {
                        "qweq@gmail.com",
                        "xcvxcvx@yandex.ru"
                    },
                    SendTime = new DateTime(2024, 01, 01, 12, 00, 00),
                    Result = EmailSendStatus.Ok
                },
                new()
                {
                    Subject = "Задание",
                    Body = "Сделано",
                    Recipients = new List<string>()
                    {
                        "zzzzzz@gmail.com",
                        "tttttt@yandex.ru"
                    },
                    SendTime = new DateTime(2024, 02, 02, 13, 30, 00),
                    Result = EmailSendStatus.Failed,
                    FailedMessage = "Какая то ошибка"
                },
            };

            _repository.GetEmails(CancellationToken.None).Returns(exceptedResult);
            
            //Act
            var result = await _getEmailsHandler.Handle(CancellationToken.None);
            
            //Assert
            Assert.Equal(exceptedResult, result);
        }
        
        [Fact]
        public async Task Handle_NotSuccess_ReturnInvalidOperationException()
        {
            //Arrange
            _repository.GetEmails(CancellationToken.None).ThrowsAsync(new InvalidOperationException("Какая то ошибка"));
            
            //Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() =>  _getEmailsHandler.Handle(CancellationToken.None));
        }
    }
}