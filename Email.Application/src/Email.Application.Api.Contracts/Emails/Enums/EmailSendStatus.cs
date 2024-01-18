using System.Text.Json.Serialization;

namespace Email.Application.Api.Contracts.Emails.Enums
{
    /// <summary>
    /// Статус отправки email сообщения.
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter))] 
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