using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using Microsoft.Xna.Framework.Content;

using RuneForge.Data.Buildings;
using RuneForge.Data.Components;
using RuneForge.Data.Maps;
using RuneForge.Data.Players;
using RuneForge.Data.Units;
using RuneForge.Game.Buildings;
using RuneForge.Game.Buildings.Interfaces;
using RuneForge.Game.Components.Extensions;
using RuneForge.Game.Components.Interfaces;
using RuneForge.Game.GameSessions.Interfaces;
using RuneForge.Game.Maps;
using RuneForge.Game.Maps.Interfaces;
using RuneForge.Game.Players;
using RuneForge.Game.Players.Interfaces;
using RuneForge.Game.Units;
using RuneForge.Game.Units.Interfaces;

namespace RuneForge.Game.GameSessions
{
    public class GameSessionContext : IGameSessionContext
    {
        private readonly IServiceProvider m_serviceProvider;
        private readonly IMapLandscapeCellTypeResolver m_landscapeCellTypeResolver;
        private readonly IMapDecorationCellTypeResolver m_decorationCellTypeResolver;
        private readonly IPlayerFactory m_playerFactory;
        private readonly IMapDecorationFactory m_mapDecorationFactory;
        private readonly Lazy<ContentManager> m_contentManagerProvider;
        private readonly Lazy<IUnitFactory> m_unitFactoryProvider;
        private readonly Lazy<IBuildingFactory> m_buildingFactoryProvider;
        private readonly Lazy<IPlayerService> m_playerServiceProvider;
        private readonly Lazy<IMapDecorationService> m_mapDecorationServiceProvider;
        private readonly Lazy<IUnitService> m_unitServiceProvider;
        private readonly Lazy<IBuildingService> m_buildingServiceProvider;

        public Map Map { get; private set; }

        public Collection<MapDecoration> MapDecorations { get; }

        public Collection<Unit> Units { get; }
        public Collection<Building> Buildings { get; }

        public Collection<Player> Players { get; }

        public Random RandomNumbersGenerator { get; }

        public bool Initialized { get; private set; }

        public bool Completed { get; private set; }

        public GameSessionContext(
            IServiceProvider serviceProvider,
            IMapLandscapeCellTypeResolver landscapeCellTypeResolver,
            IMapDecorationCellTypeResolver decorationCellTypeResolver,
            IPlayerFactory playerFactory,
            IMapDecorationFactory mapDecorationFactory,
            Lazy<ContentManager> contentManagerProvider,
            Lazy<IUnitFactory> unitFactoryProvider,
            Lazy<IBuildingFactory> buildingFactoryProvider,
            Lazy<IPlayerService> playerServiceProvider,
            Lazy<IMapDecorationService> mapDecorationServiceProvider,
            Lazy<IUnitService> unitServiceProvider,
            Lazy<IBuildingService> buildingServiceProvider
            )
        {
            m_serviceProvider = serviceProvider;
            m_landscapeCellTypeResolver = landscapeCellTypeResolver;
            m_decorationCellTypeResolver = decorationCellTypeResolver;
            m_playerFactory = playerFactory;
            m_mapDecorationFactory = mapDecorationFactory;
            m_unitFactoryProvider = unitFactoryProvider;
            m_buildingFactoryProvider = buildingFactoryProvider;
            m_contentManagerProvider = contentManagerProvider;
            m_playerServiceProvider = playerServiceProvider;
            m_mapDecorationServiceProvider = mapDecorationServiceProvider;
            m_unitServiceProvider = unitServiceProvider;
            m_buildingServiceProvider = buildingServiceProvider;

            Map = null;
            MapDecorations = new Collection<MapDecoration>();
            Units = new Collection<Unit>();
            Buildings = new Collection<Building>();
            Players = new Collection<Player>();
            RandomNumbersGenerator = new Random();
            Initialized = false;
            Completed = false;
        }

        public void Initialize(GameSessionParameters parameters)
        {
            if (Initialized)
                throw new NotSupportedException("Unable to re-initialize the game session.");
            else
            {
                ContentManager contentManager = m_contentManagerProvider.Value;
                IMapDecorationService mapDecorationService = m_mapDecorationServiceProvider.Value;
                Map map = contentManager.Load<Map>(parameters.MapAssetName);
                map.ResolveLandscapeCellTypes(m_landscapeCellTypeResolver);
                map.ResolveDecorationCellTypes(m_decorationCellTypeResolver);
                Map = map;

                if (parameters.Type == GameSessionType.NewGame)
                {
                    map.CreateMapDecorations(m_mapDecorationFactory, mapDecorationService);

                    CreatePlayers(map);
                    CreateUnits(map);
                    CreateBuildings(map);
                }
                else
                {
                    CreateMapDecorations(parameters.GameSessionContext.MapDecorations);

                    CreatePlayers(parameters.GameSessionContext.Players);

                    CreateUnits(parameters.GameSessionContext.Units);
                    CreateBuildings(parameters.GameSessionContext.Buildings);

                    Dictionary<Type, IComponentFactory> componentFactories = new Dictionary<Type, IComponentFactory>();
                    CreatePlayerComponents(parameters.GameSessionContext.Players, componentFactories);

                    CreateUnitComponents(parameters.GameSessionContext.Units, componentFactories);
                    CreateBuildingComponents(parameters.GameSessionContext.Buildings, componentFactories);

                    //throw new NotImplementedException();
                }

                Initialized = true;
            }
        }

        public void Complete()
        {
            if (Completed)
                throw new NotSupportedException("Unable to complete the game session a second time.");
            else
                Completed = true;
        }

        private void CreateMapDecorations(ReadOnlyCollection<MapDecorationDto> serializedMapDecorations)
        {
            IMapDecorationService mapDecorationService = m_mapDecorationServiceProvider.Value;
            foreach (MapDecorationDto mapDecorationDto in serializedMapDecorations)
            {
                MapDecoration mapDecoration = m_mapDecorationFactory.CreateFromDto(mapDecorationDto);
                mapDecorationService.AddMapDecoration(mapDecoration);
            }
        }

        private void CreatePlayers(Map map)
        {
            IPlayerService playerService = m_playerServiceProvider.Value;
            foreach (PlayerPrototype playerPrototype in map.PlayerPrototypes)
            {
                Player player = m_playerFactory.CreateFromPrototype(playerPrototype);
                playerService.AddPlayer(player);
            }
        }
        private void CreatePlayers(ReadOnlyCollection<PlayerDto> serializedPlayers)
        {
            IPlayerService playerService = m_playerServiceProvider.Value;
            foreach (PlayerDto playerDto in serializedPlayers)
            {
                Player player = m_playerFactory.CreateFromDto(playerDto);
                playerService.AddPlayer(player);
            }
        }
        private void CreatePlayerComponents(ReadOnlyCollection<PlayerDto> serializedPlayers, Dictionary<Type, IComponentFactory> componentFactories)
        {
            IPlayerService playerService = m_playerServiceProvider.Value;
            foreach (PlayerDto playerDto in serializedPlayers)
            {
                Player player = playerService.GetPlayer(playerDto.Id);
                foreach (ComponentDto componentDto in playerDto.Components)
                    player.AddComponent(CreateComponentFromDto(componentDto, componentFactories));
                playerService.RegisterPlayerChanges(player.Id);
            }
        }

        private void CreateUnits(Map map)
        {
            IUnitFactory unitFactory = m_unitFactoryProvider.Value;
            IUnitService unitService = m_unitServiceProvider.Value;
            foreach (UnitInstancePrototype unitInstancePrototype in map.UnitInstancePrototypes)
            {
                Unit unit = unitFactory.CreateFromInstancePrototype(unitInstancePrototype);
                unitService.AddUnit(unit);
            }
        }
        private void CreateUnits(ReadOnlyCollection<UnitDto> serializedUnits)
        {
            IUnitFactory unitFactory = m_unitFactoryProvider.Value;
            IUnitService unitService = m_unitServiceProvider.Value;
            foreach (UnitDto unitDto in serializedUnits)
            {
                Unit unit = unitFactory.CreateFromDto(unitDto);
                unitService.AddUnit(unit);
            }
        }
        private void CreateUnitComponents(ReadOnlyCollection<UnitDto> serializedUnits, Dictionary<Type, IComponentFactory> componentFactories)
        {
            IUnitService unitService = m_unitServiceProvider.Value;
            foreach (UnitDto unitDto in serializedUnits)
            {
                Unit unit = unitService.GetUnit(unitDto.Id);
                foreach (ComponentDto componentDto in unitDto.Components)
                    unit.AddComponent(CreateComponentFromDto(componentDto, componentFactories));
                unitService.RegisterUnitChanges(unit.Id);
            }
        }

        private void CreateBuildings(Map map)
        {
            IBuildingFactory buildingFactory = m_buildingFactoryProvider.Value;
            IBuildingService buildingService = m_buildingServiceProvider.Value;
            foreach (BuildingInstancePrototype buildingInstancePrototype in map.BuildingInstancePrototypes)
            {
                Building building = buildingFactory.CreateFromInstancePrototype(buildingInstancePrototype);
                buildingService.AddBuilding(building);
            }
        }
        private void CreateBuildings(ReadOnlyCollection<BuildingDto> serializedBuildings)
        {
            IBuildingFactory buildingFactory = m_buildingFactoryProvider.Value;
            IBuildingService buildingService = m_buildingServiceProvider.Value;
            foreach (BuildingDto buildingDto in serializedBuildings)
            {
                Building building = buildingFactory.CreateFromDto(buildingDto);
                buildingService.AddBuilding(building);
            }
        }
        private void CreateBuildingComponents(ReadOnlyCollection<BuildingDto> serializedBuildings, Dictionary<Type, IComponentFactory> componentFactories)
        {
            IBuildingService buildingService = m_buildingServiceProvider.Value;
            foreach (BuildingDto buildingDto in serializedBuildings)
            {
                Building building = buildingService.GetBuilding(buildingDto.Id);
                foreach (ComponentDto componentDto in buildingDto.Components)
                    building.AddComponent(CreateComponentFromDto(componentDto, componentFactories));
                buildingService.RegisterBuildingChanges(building.Id);
            }
        }

        private IComponent CreateComponentFromDto(ComponentDto componentDto, Dictionary<Type, IComponentFactory> componentFactories)
        {
            Type componentDtoType = componentDto.GetType();
            if (!componentFactories.TryGetValue(componentDtoType, out IComponentFactory componentFactory))
            {
                componentFactory = m_serviceProvider.GetComponentFactoryByDtoType(componentDtoType);
                componentFactories.Add(componentDtoType, componentFactory);
            }
            return componentFactory.CreateComponentFromDto(componentDto);
        }
    }
}
