﻿using System;
using System.Collections.ObjectModel;

using Microsoft.Xna.Framework.Content;

using RuneForge.Game.Buildings;
using RuneForge.Game.GameSessions.Interfaces;
using RuneForge.Game.Maps;
using RuneForge.Game.Maps.Interfaces;
using RuneForge.Game.Players;
using RuneForge.Game.Players.Interfaces;
using RuneForge.Game.Units;

namespace RuneForge.Game.GameSessions
{
    public class GameSessionContext : IGameSessionContext
    {
        private readonly IMapLandscapeCellTypeResolver m_landscapeCellTypeResolver;
        private readonly IMapDecorationCellTypeResolver m_decorationCellTypeResolver;
        private readonly IMapDecorationFactory m_mapDecorationFactory;
        private readonly IPlayerFactory m_playerFactory;
        private readonly Lazy<ContentManager> m_contentManagerProvider;
        private readonly Lazy<IMapDecorationService> m_mapDecorationServiceProvider;
        private readonly Lazy<IPlayerService> m_playerServiceProvider;

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
            IPlayerFactory playerFactory,
            Lazy<ContentManager> contentManagerProvider,
            Lazy<IMapDecorationService> mapDecorationServiceProvider,
            Lazy<IPlayerService> playerServiceProvider
            )
        {
            m_landscapeCellTypeResolver = landscapeCellTypeResolver;
            m_decorationCellTypeResolver = decorationCellTypeResolver;
            m_mapDecorationFactory = mapDecorationFactory;
            m_playerFactory = playerFactory;
            m_contentManagerProvider = contentManagerProvider;
            m_mapDecorationServiceProvider = mapDecorationServiceProvider;
            m_playerServiceProvider = playerServiceProvider;

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
                CreatePlayers(map);

                Map = map;
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
    }
}
