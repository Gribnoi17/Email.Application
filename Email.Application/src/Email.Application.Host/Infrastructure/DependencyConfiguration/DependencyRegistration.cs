using Email.Application.Host.Infrastructure.Middleware;

namespace Email.Application.Host.Infrastructure.DependencyConfiguration
{
    /// <summary>
    /// Класс, предоставляющий методы для регистрации зависимостей в контейнере DI.
    /// </summary>
    public static class DependencyRegistration
    {
        private const string _serviceName = "ServiceName";
        
        /// <summary>
        /// Регистрирует промежуточное Middleware для добавления HTTP-заголовка "ServiceName" с указанным значением из конфигурации.
        /// </summary>
        /// <param name="services">Коллекция сервисов для регистрации Middleware.</param>
        /// <param name="configuration">Конфигурация, содержащая наименование службы.</param>
        /// <returns>Обновленную коллекцию сервисов.</returns>
        public static IServiceCollection AddMiddlewareService(this IServiceCollection services, IConfiguration configuration)
        {
            string serviceName = configuration.GetValue<string>(_serviceName) ??
                                 throw new ArgumentNullException("configuration.GetValue<string>(\"ServiceName\")");

            services.AddTransient(provider => new HeaderMiddleware(serviceName));

            return services;
        }
    }
}
