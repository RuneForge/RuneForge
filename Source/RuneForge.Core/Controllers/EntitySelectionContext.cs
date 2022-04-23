using System;

using RuneForge.Core.Controllers.Interfaces;
using RuneForge.Game.Entities;

namespace RuneForge.Core.Controllers
{
    public class EntitySelectionContext : IEntitySelectionContext
    {
        private Entity m_entity;

        public EntitySelectionContext()
        {
            m_entity = null;
        }

        public Entity Entity
        {
            get => m_entity;
            set
            {
                if (m_entity != value)
                {
                    if (m_entity != null)
                        OnEntitySelectionDropped(EventArgs.Empty);
                    if (value != null)
                        OnEntitySelected(EventArgs.Empty);
                    m_entity = value;
                }
            }
        }

        public event EventHandler<EventArgs> EntitySelected;
        public event EventHandler<EventArgs> EntitySelectionDropped;

        protected virtual void OnEntitySelected(EventArgs e)
        {
            EntitySelected?.Invoke(this, e);
        }
        protected virtual void OnEntitySelectionDropped(EventArgs e)
        {
            EntitySelectionDropped?.Invoke(this, e);
        }
    }
}
