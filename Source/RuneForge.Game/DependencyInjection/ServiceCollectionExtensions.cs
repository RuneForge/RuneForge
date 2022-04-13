using Microsoft.Extensions.DependencyInjection;

using RuneForge.Data.Buildings;
using RuneForge.Data.Buildings.Interfaces;
using RuneForge.Data.Players;
using RuneForge.Data.Players.Interfaces;
using RuneForge.Data.Units;
using RuneForge.Data.Units.Interfaces;
using RuneForge.Game.Buildings;
using RuneForge.Game.Buildings.Interfaces;
using RuneForge.Game.GameSessions;
using RuneForge.Game.GameSessions.Interfaces;
using RuneForge.Game.Maps;
using RuneForge.Game.Maps.Interfaces;
using RuneForge.Game.Players;
using RuneForge.Game.Players.Interfaces;
using RuneForge.Game.Units;
using RuneForge.Game.Units.Interfaces;

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
            services.AddScoped<IMapLandscapeCellTypeResolver, MapLandscapeCellTypeResolver>();
            services.AddScoped<IMapDecorationCellTypeResolver, MapDecorationCellTypeResolver>();
            return services;
        }

        public static IServiceCollection AddPlayerServices(this IServiceCollection services)
        {
            services.AddScoped<IPlayerRepository, InMemoryPlayerRepository>();
            services.AddScoped<IPlayerService, PlayerService>();
            return services;
        }

        public static IServiceCollection AddUnitServices(this IServiceCollection services)
        {
            services.AddScoped<IUnitRepository, InMemoryUnitRepository>();
            services.AddScoped<IUnitService, UnitService>();
            services.AddScoped<IUnitFactory, UnitFactory>();
            return services;
        }

        public static IServiceCollection AddBuildingServices(this IServiceCollection services)
        {
            services.AddScoped<IBuildingRepository, InMemoryBuildingRepository>();
            services.AddScoped<IBuildingService, BuildingService>();
            services.AddScoped<IBuildingFactory, BuildingFactory>();
            return services;
        }
    }
}
