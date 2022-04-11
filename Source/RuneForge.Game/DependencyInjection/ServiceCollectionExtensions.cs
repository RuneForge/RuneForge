using Microsoft.Extensions.DependencyInjection;

using RuneForge.Game.GameSessions;
using RuneForge.Game.GameSessions.Interfaces;
using RuneForge.Game.Maps;
using RuneForge.Game.Maps.Interfaces;

namespace RuneForge.Game.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddGameSessionContext(this IServiceCollection services)
        {
            services.AddScoped<IGameSessionContext, GameSessionContext>();
            return services;
        }

        public static IServiceCollection AddMapServices(this IServiceCollection services)
        {
            services.AddScoped<IMapCellTypeResolver, MapCellTypeResolver>();
            services.AddScoped<IMapDecorationTypeResolver, MapDecorationTypeResolver>();
            return services;
        }
    }
}
