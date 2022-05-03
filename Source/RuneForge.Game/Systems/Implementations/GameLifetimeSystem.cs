using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.Xna.Framework;

using RuneForge.Game.GameSessions.Interfaces;
using RuneForge.Game.Players;

namespace RuneForge.Game.Systems.Implementations
{
    public class GameLifetimeSystem : System
    {
        private static readonly TimeSpan s_checkPeriod = TimeSpan.FromSeconds(2);

        private readonly IGameSessionContext m_gameSessionContext;
        private readonly List<Player> m_activePlayers;
        private TimeSpan m_timeSincePreviousCheck;

        public GameLifetimeSystem(IGameSessionContext gameSessionContext)
        {
            m_gameSessionContext = gameSessionContext;
            m_activePlayers = new List<Player>();
            m_timeSincePreviousCheck = TimeSpan.Zero;
        }

        public override void Update(GameTime gameTime)
        {
            m_timeSincePreviousCheck += gameTime.ElapsedGameTime;
            if (m_timeSincePreviousCheck >= s_checkPeriod)
            {
                m_activePlayers.Clear();
                foreach (Player player in m_gameSessionContext.Players)
                {
                    if (player.Id == m_gameSessionContext.Map.NeutralPassivePlayerId)
                        continue;

                    if (m_gameSessionContext.Buildings.Any(building => building.Owner == player))
                        m_activePlayers.Add(player);
                }
                if (m_activePlayers.Count < 2)
                    m_gameSessionContext.Complete();
                m_timeSincePreviousCheck = TimeSpan.Zero;
            }

            base.Update(gameTime);
        }
    }
}
