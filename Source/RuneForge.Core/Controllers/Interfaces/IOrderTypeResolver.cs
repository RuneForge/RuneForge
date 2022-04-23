using System;

using RuneForge.Game.Entities;

namespace RuneForge.Core.Controllers.Interfaces
{
    public interface IOrderTypeResolver
    {
        public bool TryResolveOrderType(Entity entity, int screenX, int screenY, out Type orderType);
    }
}
