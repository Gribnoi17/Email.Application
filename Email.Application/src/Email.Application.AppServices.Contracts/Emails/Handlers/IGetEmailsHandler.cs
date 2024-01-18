using Email.Application.AppServices.Contracts.Emails.Models;

namespace Email.Application.AppServices.Contracts.Emails.Handlers
{
    /// <summary>
    /// Обработчик для получения email сообщений.
    /// </summary>
    public interface IGetEmailsHandler
    {
        /// <summary>
        /// Получения всех email сообщений.
        /// </summary>
        /// <param name="token"></param>
        /// <returns>Список email сообщений.</returns>
        Task<List<EmailMessage>> Handle(CancellationToken token);
    }
}