using Email.Application.Api.Contracts.Emails.Enums;
using Email.Application.Api.Contracts.Emails.Requests;
using Email.Application.Api.Contracts.Emails.Responses;
using Email.Application.AppServices.Contracts.Emails.Models;

namespace Email.Application.Host.Infrastructure.MapService
{
    /// <summary>
    /// Маппер моделей.
    /// </summary>
    internal static class MappingService
    {
        /// <summary>
        /// Маппит данные модели слоя бизнес логики в модель ответа клиенту.
        /// </summary>
        /// <param name="emailMessage">Модель слоя бизнес логики.</param>
        /// <returns>Модель ответа клиенту.</returns>
        public static EmailResponse MapToEmailResponse(EmailMessage emailMessage)
        {
            return new EmailResponse()
            {
                Subject = emailMessage.Subject,
                Body = emailMessage.Body,
                SendTime = emailMessage.SendTime.ToLocalTime(),
                FailedMessage = emailMessage.FailedMessage,
                Recipients = emailMessage.Recipients,
                Result = MapToEmailSendStatus(emailMessage.Result)
            };
        }
        
        /// <summary>
        /// Маппит данные модели слоя клиента в модель бизнес лоигки.
        /// </summary>
        /// <param name="request">Модель слоя клиента.</param>
        /// <returns>Модель слоя бизнес логики.</returns>
        public static EmailInternalRequest MapToEmailInternalRequest(EmailRequest request)
        {
            return new EmailInternalRequest()
            {
                Subject = request.Subject,
                Body = request.Body,
                Recipients = request.Recipients.ToList()
            };
        }
        
        /// <summary>
        /// Маппит список моделей слоя бизнс логики в список моделей для ответа клиенту.
        /// </summary>
        /// <param name="emailMessages">Список моделей слоя бизнес логики.</param>
        /// <returns>Список моделей для ответа клиенту</returns>
        public static EmailResponse[] MapToEmailMessages(List<EmailMessage> emailMessages)
        {
            var emailResponses = emailMessages
                .Select(MapToEmailResponse)
                .ToArray();

            return emailResponses;
        }
        
        /// <summary>
        /// Маппит статус email сообщения между моделью слоя бизнес логики и моделью для ответа пользователю.
        /// </summary>
        /// <param name="status">Статус внутренней модели трансляции.</param>
        /// <returns>Статус трансляции для ответа пользователю.</returns>
        public static EmailSendStatus MapToEmailSendStatus(AppServices.Contracts.Emails.Enums.EmailSendStatus status)
        {
            return status switch
            {
                AppServices.Contracts.Emails.Enums.EmailSendStatus.Ok => EmailSendStatus.Ok,
                AppServices.Contracts.Emails.Enums.EmailSendStatus.Failed => EmailSendStatus.Failed,
                _ => throw new InvalidCastException($"Произошла ошибка при маппинге статуса: {status}")
            };
        }
    }
}