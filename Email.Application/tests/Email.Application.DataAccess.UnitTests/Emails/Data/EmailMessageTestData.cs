using System.Collections;
using Email.Application.AppServices.Contracts.Emails.Enums;
using Email.Application.AppServices.Contracts.Emails.Models;

namespace Email.Application.DataAccess.UnitTests.Emails.Data
{
    internal class EmailMessageTestData : IEnumerable<object[]>
    {
        private readonly EmailMessage _emailMessageOne;
        private readonly EmailMessage _emailMessageTwo; 
        private readonly EmailMessage _emailMessageThree;

        public EmailMessageTestData()
        {
            _emailMessageOne = new EmailMessage
            {
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
            
            _emailMessageTwo = new EmailMessage
            {
                Subject = "Письмо",
                Body = "Что-то",
                Recipients = new List<string>()
                {
                    "example@gmail.com",
                    "yyyyyyy@yandex.ru"
                },
                SendTime = new DateTime(2024, 01, 01, 12, 00, 00),
                Result = EmailSendStatus.Ok
            };
            
            _emailMessageThree = new EmailMessage
            {
                Subject = "Почти",
                Body = "Привет привет!",
                Recipients = new List<string>()
                {
                    "qweq21312@gmail.com",
                    "yrt2342342@yandex.ru"
                },
                SendTime = new DateTime(2024, 01, 02, 11, 00, 00),
                Result = EmailSendStatus.Failed
            };
        }

        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { _emailMessageOne};
            yield return new object[] { _emailMessageTwo };
            yield return new object[] { _emailMessageThree};
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public List<EmailMessage> GetEmails()
        {
            var emails = new List<EmailMessage>()
            {
                _emailMessageOne,
                _emailMessageTwo,
                _emailMessageThree
            };

            return emails;
        }
    }
}