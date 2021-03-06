using Microsoft.Extensions.DependencyInjection;

using RuneForge.Data.Buildings;
using RuneForge.Data.Buildings.Interfaces;
using RuneForge.Data.Maps;
using RuneForge.Data.Maps.Interfaces;
using RuneForge.Data.Players;
using RuneForge.Data.Players.Interfaces;
using RuneForge.Data.Units;
using RuneForge.Data.Units.Interfaces;
using RuneForge.Game.AutoMapper.Resolvers;
using RuneForge.Game.Buildings;
using RuneForge.Game.Buildings.Interfaces;
using RuneForge.Game.Components.Factories;
using RuneForge.Game.Components.Interfaces;
using RuneForge.Game.GameSessions;
using RuneForge.Game.GameSessions.Interfaces;
using RuneForge.Game.Maps;
using RuneForge.Game.Maps.Interfaces;
using RuneForge.Game.Orders.Factories;
using RuneForge.Game.Orders.Interfaces;
using RuneForge.Game.PathGenerators;
using RuneForge.Game.PathGenerators.Interfaces;
using RuneForge.Game.Players;
using RuneForge.Game.Players.Interfaces;
using RuneForge.Game.Systems.Implementations;
using RuneForge.Game.Systems.Interfaces;
using RuneForge.Game.Units;
using RuneForge.Game.Units.Interfaces;

namespace RuneForge.Game.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddGameSessionContext(this IServiceCollection services)
        {
            services.AddScoped<GameSessionParameters>();
            services.AddScoped<IGameSessionContext, GameSessionContext>();
            return services;
        }

        public static IServiceCollection AddMapServices(this IServiceCollection services)
        {
            services.AddTransient<IPathGenerator, BreadthFirstPathGenerator>();
            services.AddScoped<IMapLandscapeCellTypeResolver, MapLandscapeCellTypeResolver>();
            services.AddScoped<IMapDecorationCellTypeResolver, MapDecorationCellTypeResolver>();
            services.AddScoped<IMapDecorationRepository, InMemoryMapDecorationRepository>();
            services.AddScoped<IMapDecorationService, MapDecorationService>();
            services.AddScoped<IMapDecorationFactory, MapDecorationFactory>();
            services.AddScoped(serviceProvider => serviceProvider.GetLazyInitializedService<IMapDecorationService>());
            return services;
        }

        public static IServiceCollection AddPlayerServices(this IServiceCollection services)
        {
            services.AddScoped<IPlayerRepository, InMemoryPlayerRepository>();
            services.AddScoped<IPlayerService, PlayerService>();
            services.AddScoped<IPlayerFactory, PlayerFactory>();
            services.AddScoped(serviceProvider => serviceProvider.GetLazyInitializedService<IPlayerService>());
            return services;
        }

        public static IServiceCollection AddUnitServices(this IServiceCollection services)
        {
            services.AddScoped<IUnitRepository, InMemoryUnitRepository>();
            services.AddScoped<IUnitService, UnitService>();
            services.AddScoped<IUnitFactory, UnitFactory>();
            services.AddScoped(serviceProvider => serviceProvider.GetLazyInitializedService<IUnitService>());
            services.AddScoped(serviceProvider => serviceProvider.GetLazyInitializedService<IUnitFactory>());
            return services;
        }

        public static IServiceCollection AddBuildingServices(this IServiceCollection services)
        {
            services.AddScoped<IBuildingRepository, InMemoryBuildingRepository>();
            services.AddScoped<IBuildingService, BuildingService>();
            services.AddScoped<IBuildingFactory, BuildingFactory>();
            services.AddScoped(serviceProvider => serviceProvider.GetLazyInitializedService<IBuildingService>());
            services.AddScoped(serviceProvider => serviceProvider.GetLazyInitializedService<IBuildingFactory>());
            return services;
        }

        public static IServiceCollection AddEntityComponentSystemServices(this IServiceCollection services)
        {
            services.AddScoped<TextureAtlasComponentFactory>();
            services.AddScoped<AnimationAtlasComponentFactory>();
            services.AddScoped<AnimationStateComponentFactory>();
            services.AddScoped<OrderQueueComponentFactory>();
            services.AddScoped<LocationComponentFactory>();
            services.AddScoped<DirectionComponentFactory>();
            services.AddScoped<MovementComponentFactory>();
            services.AddScoped<ResourceContainerComponentFactory>();
            services.AddScoped<ResourceSourceComponentFactory>();
            services.AddScoped<ResourceStorageComponentFactory>();
            services.AddScoped<UnitShelterComponentFactory>();
            services.AddScoped<UnitShelterOccupantComponentFactory>();
            services.AddScoped<HealthComponentFactory>();
            services.AddScoped<DurabilityComponentFactory>();
            services.AddScoped<MeleeCombatComponentFactory>();
            services.AddScoped<ProductionCostComponentFactory>();
            services.AddScoped<ProductionFacilityComponentFactory>();
            services.AddScoped<IComponentFactory, TextureAtlasComponentFactory>();
            services.AddScoped<IComponentFactory, AnimationAtlasComponentFactory>();
            services.AddScoped<IComponentFactory, AnimationStateComponentFactory>();
            services.AddScoped<IComponentFactory, OrderQueueComponentFactory>();
            services.AddScoped<IComponentFactory, LocationComponentFactory>();
            services.AddScoped<IComponentFactory, DirectionComponentFactory>();
            services.AddScoped<IComponentFactory, MovementComponentFactory>();
            services.AddScoped<IComponentFactory, ResourceContainerComponentFactory>();
            services.AddScoped<IComponentFactory, ResourceSourceComponentFactory>();
            services.AddScoped<IComponentFactory, ResourceStorageComponentFactory>();
            services.AddScoped<IComponentFactory, UnitShelterComponentFactory>();
            services.AddScoped<IComponentFactory, UnitShelterOccupantComponentFactory>();
            services.AddScoped<IComponentFactory, HealthComponentFactory>();
            services.AddScoped<IComponentFactory, DurabilityComponentFactory>();
            services.AddScoped<IComponentFactory, MeleeCombatComponentFactory>();
            services.AddScoped<IComponentFactory, ProductionCostComponentFactory>();
            services.AddScoped<IComponentFactory, ProductionFacilityComponentFactory>();
            services.AddScoped<IOrderFactory, MoveOrderFactory>();
            services.AddScoped<IOrderFactory, GatherResourcesOrderFactory>();
            services.AddScoped<IOrderFactory, AttackOrderFactory>();
            services.AddScoped<IOrderFactory, ProduceUnitOrderFactory>();
            services.AddScoped<ISystem, OrderSystem>();
            services.AddScoped<ISystem, MovementSystem>();
            services.AddScoped<ISystem, ResourceSystem>();
            services.AddScoped<ISystem, MeleeCombatSystem>();
            services.AddScoped<ISystem, UnitProductionSystem>();
            services.AddScoped<ISystem, ComputerPlayerManagementSystem>();
            services.AddScoped<ISystem, EntityLifetimeSystem>();
            services.AddScoped<ISystem, GameLifetimeSystem>();
            return services;
        }

        public static IServiceCollection AddAutoMapperValueResolvers(this IServiceCollection services)
        {
            services.AddTransient<OrderEntityIdValueResolver>();
            services.AddTransient<MeleeCombatComponentTargetEntityIdValueResolver>();
            services.AddTransient<AttackOrderTargetEntityIdValueResolver>();
            return services;
        }
    }
}
