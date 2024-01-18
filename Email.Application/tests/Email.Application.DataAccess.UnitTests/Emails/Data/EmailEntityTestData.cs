using System.Collections;
using Email.Application.AppServices.Contracts.Emails.Enums;
using Email.Application.DataAccess.Emails.Models;

namespace Email.Application.DataAccess.UnitTests.Emails.Data
{
    internal class EmailEntityTestData : IEnumerable<object[]>
    {
        private readonly EmailEntity _emailEntityOne;
        private readonly EmailEntity _emailEntityTwo; 
        private readonly EmailEntity _emailEntityThree;

        public EmailEntityTestData()
        {
            _emailEntityOne = new EmailEntity
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
            
            _emailEntityTwo = new EmailEntity
            {
                Id = 2,
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
            
            _emailEntityThree = new EmailEntity
            {
                Id = 3,
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
            yield return new object[] { _emailEntityOne};
            yield return new object[] { _emailEntityTwo };
            yield return new object[] { _emailEntityThree};
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public List<EmailEntity> GetEmailEntities()
        {
            var emails = new List<EmailEntity>()
            {
                _emailEntityOne,
                _emailEntityTwo,
                _emailEntityThree
            };

            return emails;
        }
    }
}