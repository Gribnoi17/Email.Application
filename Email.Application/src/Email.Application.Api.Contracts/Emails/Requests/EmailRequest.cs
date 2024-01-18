namespace Email.Application.Api.Contracts.Emails.Requests
{
    /// <summary>
    /// Модель запроса для отправки email сообщений.
    /// </summary>
    public record EmailRequest
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
        public string[] Recipients { get; set; }
    }
}