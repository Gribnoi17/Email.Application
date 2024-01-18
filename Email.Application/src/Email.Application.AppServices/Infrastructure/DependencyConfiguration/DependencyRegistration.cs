using Email.Application.AppServices.Contracts.Emails.Configuration;
using Email.Application.AppServices.Contracts.Emails.Handlers;
using Email.Application.AppServices.Emails.Handlers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Email.Application.AppServices.Infrastructure.DependencyConfiguration
{
    /// <summary>
    /// Класс, предоставляющий методы для регистрации зависимостей в контейнере DI.
    /// </summary>
    public static class DependencyRegistration
    {
        /// <summary>
        /// Регистрирует обработчики для операций с email сообщениями.
        /// </summary>
        public static IServiceCollection AddHandlers(this IServiceCollection services)
        {
            services.AddScoped<ISendEmailHandler, SendEmailHandler>();
            
            services.AddScoped<IGetEmailsHandler, GetEmailsHandler>();
            
            return services;
        }

        /// <summary>
        /// Регистрирует конфигурацию Mailkit из файловой конфигурации.
        /// </summary>
        public static IServiceCollection AddSmtpSettings(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<SmtpSettings>(configuration.GetSection("SmtpSettings"));
            
            services.AddSingleton<IOptionsMonitor<SmtpSettings>, OptionsMonitor<SmtpSettings>>();

            return services;
        }
    }
}
