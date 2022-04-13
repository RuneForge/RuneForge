using System;
using System.Collections.ObjectModel;

using Microsoft.Xna.Framework.Content;

using RuneForge.Game.Buildings;
using RuneForge.Game.GameSessions.Interfaces;
using RuneForge.Game.Maps;
using RuneForge.Game.Maps.Interfaces;
using RuneForge.Game.Players;
using RuneForge.Game.Units;

namespace RuneForge.Game.GameSessions
{
    public class GameSessionContext : IGameSessionContext
    {
        private readonly IMapLandscapeCellTypeResolver m_landscapeCellTypeResolver;
        private readonly IMapDecorationCellTypeResolver m_decorationCellTypeResolver;
        private readonly IMapDecorationFactory m_mapDecorationFactory;
        private readonly Lazy<ContentManager> m_contentManagerProvider;
        private readonly Lazy<IMapDecorationService> m_mapDecorationServiceProvider;

        public Map Map { get; private set; }

        public Collection<MapDecoration> MapDecorations { get; }

        public Collection<Unit> Units { get; }
        public Collection<Building> Buildings { get; }

        public Collection<Player> Players { get; }

        public bool Initialized { get; private set; }

        public GameSessionContext(
            IMapLandscapeCellTypeResolver landscapeCellTypeResolver,
            IMapDecorationCellTypeResolver decorationCellTypeResolver,
            IMapDecorationFactory mapDecorationFactory,
            Lazy<ContentManager> contentManagerProvider,
            Lazy<IMapDecorationService> mapDecorationServiceProvider
            )
        {
            m_landscapeCellTypeResolver = landscapeCellTypeResolver;
            m_decorationCellTypeResolver = decorationCellTypeResolver;
            m_mapDecorationFactory = mapDecorationFactory;
            m_contentManagerProvider = contentManagerProvider;
            m_mapDecorationServiceProvider = mapDecorationServiceProvider;

            Map = null;
            MapDecorations = new Collection<MapDecoration>();
            Units = new Collection<Unit>();
            Buildings = new Collection<Building>();
            Players = new Collection<Player>();
            Initialized = false;
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
                Initialized = true;
            }
        }
    }
}
