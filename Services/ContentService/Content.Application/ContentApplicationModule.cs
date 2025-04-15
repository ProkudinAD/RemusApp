using Microsoft.Extensions.DependencyInjection;

namespace Content.Application
{
    public static class ContentApplicationModule
    {
        /*         public static IServiceCollection AddContentApplicationServices(this IServiceCollection services)
                {
                    // Регистрируем все обработчики MediatR из текущей сборки
                    services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));

                    // Добавьте регистрацию других сервисов, если необходимо
                    services.AddScoped<IUnitOfWork, IUnitOfWork>();

                    return services;
                } */
        public static IServiceCollection AddContentApplicationServices(this IServiceCollection services)
        {
            return services;
        }
    }
}