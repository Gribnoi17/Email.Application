using Email.Application.AppServices.Contracts.Emails.Models;

namespace Email.Application.AppServices.Contracts.Emails.Handlers
{
    /// <summary>
    /// Обработчик для отправки email сообщений.
    /// </summary>
    public interface ISendEmailHandler
    {
        /// <summary>
        /// Отправляет email сообщение.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="token">Токен отмены операции.</param>
        /// <returns></returns>
        Task Handle(EmailInternalRequest request, CancellationToken token);
    }
}