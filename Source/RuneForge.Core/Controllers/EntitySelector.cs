using System.Linq;

using Microsoft.Xna.Framework;

using RuneForge.Core.Controllers.Interfaces;
using RuneForge.Core.Rendering;
using RuneForge.Game.Components.Implementations;
using RuneForge.Game.Entities;
using RuneForge.Game.GameSessions.Interfaces;

namespace RuneForge.Core.Controllers
{
    public class EntitySelector : IEntitySelector
    {
        private readonly IGameSessionContext m_gameSessionContext;
        private readonly Camera2D m_camera;

        public EntitySelector(IGameSessionContext gameSessionContext, Camera2D camera)
        {
            m_gameSessionContext = gameSessionContext;
            m_camera = camera;
        }

        public bool TrySelectEntity(int screenX, int screenY, out Entity entity)
        {
            Point worldPoint = m_camera.TranslateScreenToWorld(new Vector2(screenX, screenY)).ToPoint();
            foreach (Entity entityToCheck in m_gameSessionContext.Units.Concat<Entity>(m_gameSessionContext.Buildings))
            {
                if (!entityToCheck.TryGetComponentOfType(out LocationComponent locationComponent))
                    continue;
                if (entityToCheck.TryGetComponentOfType(out UnitShelterOccupantComponent unitShelterOccupantComponent) && unitShelterOccupantComponent.InsideShelter)
                    continue;

                Rectangle entityRectangle = new Rectangle((int)locationComponent.X, (int)locationComponent.Y, locationComponent.Width, locationComponent.Height);
                if (entityRectangle.Contains(worldPoint))
                {
                    entity = entityToCheck;
                    return true;
                }
            }
            entity = null;
            return false;
        }
    }
}
