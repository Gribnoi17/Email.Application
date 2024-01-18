using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Email.Application.AppServices.Contracts.Emails.Enums;

namespace Email.Application.DataAccess.Emails.Models
{
    /// <summary>
    /// Представляет запись в базе данных.
    /// </summary>
    [Table("message", Schema = "email_logging")]
    internal class EmailEntity
    {
        /// <summary>
        /// Уникальный идентификатор.
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public long Id { get; set; }
        
        /// <summary>
        /// Тема письма.
        /// </summary>
        [Column("subject")]
        [Required]
        public string Subject { get; set; }
        
        /// <summary>
        /// Текст письма.
        /// </summary>
        [Column("body")]
        [Required]
        public string Body { get; set; }
        
        /// <summary>
        /// Список адресатов.
        /// </summary>
        [Column("recipients")]
        [Required]
        public List<string> Recipients { get; set; }
        
        /// <summary>
        /// Дата отправки.
        /// </summary>
        [Column("send_time")]
        [Required]
        public DateTime SendTime { get; set; }
        
        /// <summary>
        /// Статус отправки сообщения.
        /// </summary>
        [Column("send_result")]
        [Required]
        public EmailSendStatus Result { get; set; }
        
        /// <summary>
        /// Ошибка при отправки сообщения.
        /// </summary>
        [Column("failed_message")]
        public string? FailedMessage { get; set; }
    }
}