using System;
using System.Collections.ObjectModel;

using Microsoft.Xna.Framework.Content;

using RuneForge.Game.Buildings;
using RuneForge.Game.Buildings.Interfaces;
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
                map.CreateMapDecorations(m_mapDecorationFactory, mapDecorationService);

                Map = map;
                CreatePlayers(map);
                CreateUnits(map);
                CreateBuildings(map);

                Initialized = true;
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

        public void Complete()
        {
            if (Completed)
                throw new NotSupportedException("Unable to complete the game session a second time.");
            else
                Completed = true;
        }
    }
}
