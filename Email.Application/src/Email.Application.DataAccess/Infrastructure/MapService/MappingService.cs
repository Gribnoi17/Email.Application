using Email.Application.AppServices.Contracts.Emails.Models;
using Email.Application.DataAccess.Emails.Models;

namespace Email.Application.DataAccess.Infrastructure.MapService
{
    /// <summary>
    /// Маппер моделей.
    /// </summary>
    internal static class MappingService
    {
        /// <summary>
        /// Маппит данные модели слоя бизнес логики в модель для работы с базой данных.
        /// </summary>
        /// <param name="message">Модель слоя бизнес лоигки.</param>
        /// <returns>Модель слоя базы данных.</returns>
        public static EmailEntity MapToEmailEntity(EmailMessage message)
        {
            return new EmailEntity()
            {
                Id = message.Id,
                Subject = message.Subject,
                Body = message.Body,
                SendTime = message.SendTime,
                FailedMessage = message.FailedMessage,
                Recipients = message.Recipients,
                Result = message.Result
            };
        }
        
        /// <summary>
        /// Маппит данные модели слоя данных в модель для работы с бизнес логикой.
        /// </summary>
        /// <param name="emailEntity">Модель слоя данных.</param>
        /// <returns>Модель слоя бизнес логики.</returns>
        public static EmailMessage MapToEmailMessage(EmailEntity emailEntity)
        {
            return new EmailMessage()
            {
                Id = emailEntity.Id,
                Subject = emailEntity.Subject,
                Body = emailEntity.Body,
                SendTime = emailEntity.SendTime,
                FailedMessage = emailEntity.FailedMessage,
                Recipients = emailEntity.Recipients,
                Result = emailEntity.Result
            };
        }
        
        /// <summary>
        /// Маппит список моделей слоя данных в список моделей для работы с бизнес логикой.
        /// </summary>
        /// <param name="emailEntities">Список моделей слоя данных.</param>
        /// <returns>Список моделей слоя бизнес логики.</returns>
        public static List<EmailMessage> MapToEmailMessages(List<EmailEntity> emailEntities)
        {
            var emailMessages = emailEntities
                .Select(MapToEmailMessage)
                .ToList();

            return emailMessages;
        }
    }
}