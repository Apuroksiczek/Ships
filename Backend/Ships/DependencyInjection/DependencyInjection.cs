using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ships.Models;

namespace Ships.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddGameDependencyInjection
            (
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.Configure<GameSettings>(configuration.GetSection(GameSettings.SectionName));
            services.AddSingleton<IGame, Game>();
            return services;
        }
    }
}