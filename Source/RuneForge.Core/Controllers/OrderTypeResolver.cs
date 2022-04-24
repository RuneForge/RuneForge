using System;

using Microsoft.Xna.Framework;

using RuneForge.Core.Controllers.Interfaces;
using RuneForge.Core.Rendering;
using RuneForge.Game.Buildings;
using RuneForge.Game.Entities;
using RuneForge.Game.GameSessions.Interfaces;
using RuneForge.Game.Maps;
using RuneForge.Game.Orders.Implementations;
using RuneForge.Game.Players;
using RuneForge.Game.Units;

namespace RuneForge.Core.Controllers
{
    public class OrderTypeResolver : IOrderTypeResolver
    {
        private readonly IGameSessionContext m_gameSessionContext;
        private readonly Camera2D m_camera;

        public OrderTypeResolver(IGameSessionContext gameSessionContext, Camera2D camera)
        {
            m_gameSessionContext = gameSessionContext;
            m_camera = camera;
        }

        public bool TryResolveOrderType(Entity entity, int screenX, int screenY, out Type orderType)
        {
            Point worldPoint = m_camera.TranslateScreenToWorld(new Vector2(screenX, screenY)).ToPoint();

            Player entityOwner = entity switch
            {
                Unit unit => unit.Owner,
                Building building => building.Owner,
                _ => null,
            };

            if (entityOwner == null || entityOwner.Id != m_gameSessionContext.Map.HumanPlayerId)
            {
                orderType = null;
                return false;
            }

            if (entity is Unit)
            {
                if (worldPoint.X >= 0 && worldPoint.Y >= 0 && worldPoint.X < m_gameSessionContext.Map.Width * Map.CellWidth && worldPoint.Y < m_gameSessionContext.Map.Height * Map.CellHeight)
                {
                    orderType = typeof(MoveOrder);
                    return true;
                }

                orderType = typeof(MoveOrder);
                return true;
            }

            orderType = null;
            return false;
        }
    }
}
