namespace Email.Application.AppServices.Contracts.Emails.Models
{
    /// <summary>
    /// Модель запроса слоя бизнес лоигки для отправки сообщения.
    /// </summary>
    public class EmailInternalRequest
    {
        /// <summary>
        /// Тема письма.
        /// </summary>
        public string Subject { get; set; }
        
        /// <summary>
        /// Текст письма.
        /// </summary>
        public string Body { get; set; }
        
        /// <summary>
        /// Список адресатов.
        /// </summary>
        public List<string> Recipients { get; set; }
    }
}