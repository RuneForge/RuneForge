using System;
using System.Collections.ObjectModel;

namespace RuneForge.Game.Entities.Interfaces
{
    public interface IEntity
    {
        public ReadOnlyCollection<IComponent> Components { get; }

        public IComponent GetComponentOfType(Type componentType);
        public TComponent GetComponentOfType<TComponent>() where TComponent : class, IComponent;

        public bool TryGetComponentOfType(Type componentType, out IComponent component);
        public bool TryGetComponentOfType<TComponent>(out TComponent component) where TComponent : class, IComponent;

        public bool HasComponentOfType(Type componentType);
        public bool HasComponentOfType<TComponent>() where TComponent : class, IComponent;
    }
}
