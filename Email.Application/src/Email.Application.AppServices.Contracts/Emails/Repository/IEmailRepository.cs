using Email.Application.AppServices.Contracts.Emails.Models;

namespace Email.Application.AppServices.Contracts.Emails.Repository
{
    /// <summary>
    /// Предназначен для работы с базой данных.
    /// </summary>
    public interface IEmailRepository
    {
        /// <summary>
        /// Добавляет сообщение отправленное на почту в базу данных.
        /// </summary>
        /// <param name="message">Сообщение.</param>
        /// <param name="token">Токен отмены операции.</param>
        /// <returns>Айди добавленного сообщения.</returns>
        Task<long> AddEmail(EmailMessage message, CancellationToken token);
        
        /// <summary>
        /// Получает все сообщения отправленные на почту из базы данных.
        /// </summary>
        /// <param name="token">Токен отмены операции.</param>
        /// <returns>Список отправленных сообщений.</returns>
        Task<List<EmailMessage>> GetEmails(CancellationToken token);
    }
}