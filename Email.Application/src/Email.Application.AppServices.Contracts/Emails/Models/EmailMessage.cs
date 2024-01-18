using Email.Application.AppServices.Contracts.Emails.Enums;

namespace Email.Application.AppServices.Contracts.Emails.Models
{
    public class EmailMessage
    {
        /// <summary>
        /// Уникальный идентификатор.
        /// </summary>
        public long Id { get; set; }
        
        /// <summary>
        /// Тема письма.
        /// </summary>
        public string Subject { get; set; }
        
        /// <summary>
        /// Текст письма.
        /// </summary>
        public string Body { get; set; }
        
        /// <summary>
        /// Дата отправки сообщения.
        /// </summary>
        public DateTime SendTime { get; set; }
        
        /// <summary>
        /// Список адресатов.
        /// </summary>
        public List<string> Recipients { get; set; }
        
        /// <summary>
        /// Статус отправки сообщения.
        /// </summary>
        public EmailSendStatus Result { get; set; }
        
        /// <summary>
        /// Ошибка при отправки сообщения.
        /// </summary>
        public string? FailedMessage { get; set; }
    }
}