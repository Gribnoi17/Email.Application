using Email.Application.Api.Contracts.Emails.Requests;
using Email.Application.Api.Contracts.Emails.Responses;

namespace Email.Application.Api.Contracts.Emails.Controllers
{
    /// <summary>
    /// Контроллер для работы с почтовыми сообщениями.
    /// </summary>
    public interface IEmailController
    {
        /// <summary>
        /// Отправка email сообщения.
        /// </summary>
        /// <param name="request">Сообщение.</param>
        /// <param name="token">Токен отмены операции.</param>
        Task SendEmail(EmailRequest request, CancellationToken token);

        /// <summary>
        /// Получение списка всех отправленных email сообщений.
        /// </summary>
        /// <param name="token">Токен отмены операции.</param>
        /// <returns>Список трансляций.</returns>
        Task<EmailResponse[]> GetEmails(CancellationToken token);
    }
}