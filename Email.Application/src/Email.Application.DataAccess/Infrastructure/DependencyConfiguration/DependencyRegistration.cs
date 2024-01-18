using Email.Application.AppServices.Contracts.Emails.Repository;
using Email.Application.DataAccess.Emails.Data;
using Email.Application.DataAccess.Emails.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Email.Application.DataAccess.Infrastructure.DependencyConfiguration
{
    /// <summary>
    /// Класс, предоставляющий методы для регистрации зависимостей в контейнере DI.
    /// </summary>
    public static class DependencyRegistration
    {
        private const string _databaseConnectionSection = "DatabaseConnectionString";
        private const string _defaultConnection = "DefaultConnection";
        
        /// <summary>
        /// Регистрирует репозитории для операций с email сообщениями.
        /// </summary>
        /// <param name="services">Коллекция сервисов для регистрации.</param>
        /// <returns>Измененная коллекция сервисов.</returns>
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IEmailRepository, EmailRepository>();
            
            return services;
        }
        
        /// <summary>
        /// Регистрирует контекст базы данных.
        /// </summary>
        /// <param name="services">Коллекция сервисов для регистрации.</param>
        /// <param name="configuration">Конфигурация приложения.</param>
        /// <returns>Измененная коллекция сервисов.</returns>
        public static IServiceCollection AddEmailDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            var databaseConnectionString = configuration.GetSection(_databaseConnectionSection)[_defaultConnection];
            
            services.AddDbContext<EmailDbContext>(
                options => options.UseNpgsql(databaseConnectionString));
        
            return services;
        }
    }
}
