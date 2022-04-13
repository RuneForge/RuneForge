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
        private readonly IMapDecorationTypeResolver m_mapDecorationTypeResolver;
        private readonly Lazy<ContentManager> m_contentManagerProvider;

        public Map Map { get; private set; }

        public Collection<Player> Players { get; }

        public Collection<Unit> Units { get; }
        public Collection<Building> Buildings { get; }

        public bool Initialized { get; private set; }

        public GameSessionContext(IMapLandscapeCellTypeResolver landscapeCellTypeResolver, IMapDecorationTypeResolver mapDecorationTypeResolver, Lazy<ContentManager> contentManagerProvider)
        {
            m_landscapeCellTypeResolver = landscapeCellTypeResolver;
            m_mapDecorationTypeResolver = mapDecorationTypeResolver;
            m_contentManagerProvider = contentManagerProvider;

            Map = null;
            Players = new Collection<Player>();
            Units = new Collection<Unit>();
            Buildings = new Collection<Building>();
            Initialized = false;
        }

        public void Initialize(GameSessionParameters parameters)
        {
            if (Initialized)
                throw new NotSupportedException("Unable to re-initialize the game session.");
            else
            {
                ContentManager contentManager = m_contentManagerProvider.Value;
                Map map = contentManager.Load<Map>(parameters.MapAssetName);
                map.ResolveLandscapeCellTypes(m_landscapeCellTypeResolver);
                map.ResolveMapDecorationTypes(m_mapDecorationTypeResolver);

                Map = map;
                Initialized = true;
            }
        }
    }
}
