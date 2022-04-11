using Microsoft.Extensions.DependencyInjection;

using RuneForge.Data.Players;
using RuneForge.Data.Players.Interfaces;
using RuneForge.Game.GameSessions;
using RuneForge.Game.GameSessions.Interfaces;
using RuneForge.Game.Maps;
using RuneForge.Game.Maps.Interfaces;
using RuneForge.Game.Players;
using RuneForge.Game.Players.Interfaces;

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

        public static IServiceCollection AddPlayerServices(this IServiceCollection services)
        {
            services.AddScoped<IPlayerRepository, InMemoryPlayerRepository>();
            services.AddScoped<IPlayerService, PlayerService>();
            return services;
        }
    }
}
