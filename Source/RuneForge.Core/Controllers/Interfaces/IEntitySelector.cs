using RuneForge.Game.Entities;

namespace RuneForge.Core.Controllers.Interfaces
{
    public interface IEntitySelector
    {
        public bool TrySelectEntity(int screenX, int screenY, out Entity entity);
    }
}
