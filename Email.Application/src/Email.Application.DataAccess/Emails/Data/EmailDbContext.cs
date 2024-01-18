using Email.Application.AppServices.Contracts.Emails.Models;
using Email.Application.DataAccess.Emails.Models;
using Microsoft.EntityFrameworkCore;

namespace Email.Application.DataAccess.Emails.Data
{
    /// <summary>
    /// Контекст для работы с базой данных.
    /// </summary>
    internal class EmailDbContext : DbContext
    {
        /// <summary>
        /// DbSet email записей.
        /// </summary>
        public DbSet<EmailEntity> Emails { get; set; }

        /// <summary>
        /// Инициализирует объект класса EmailDbContext.
        /// </summary>
        /// <param name="options">Настройки для контекста базы данных.</param>
        public EmailDbContext(DbContextOptions options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EmailEntity>()
                .Property(e => e.Recipients)
                .HasConversion(
                    v => string.Join(',', v),
                    v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList()
                );

            base.OnModelCreating(modelBuilder);
        }
    }
}