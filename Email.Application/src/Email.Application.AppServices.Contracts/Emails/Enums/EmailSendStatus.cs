namespace Email.Application.AppServices.Contracts.Emails.Enums
{
    /// <summary>
    /// Статус отправки email сообщения.
    /// </summary>
    public enum EmailSendStatus
    {
        /// <summary>
        /// Сообщение успешно отправлено.
        /// </summary>
        Ok,
        
        /// <summary>
        /// Сообщение не отправлено.
        /// </summary>
        Failed
    }
}