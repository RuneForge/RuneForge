using System;

using RuneForge.Game.Entities;

namespace RuneForge.Core.Controllers.Interfaces
{
    public interface IEntitySelectionContext
    {
        public Entity Entity { get; set; }

        public event EventHandler<EventArgs> EntitySelected;
        public event EventHandler<EventArgs> EntitySelectionDropped;
    }
}
