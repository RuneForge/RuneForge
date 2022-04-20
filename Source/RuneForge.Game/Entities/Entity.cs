using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using RuneForge.Game.Entities.Interfaces;

namespace RuneForge.Game.Entities
{
    public abstract class Entity : IEntity
    {
        private readonly List<IComponent> m_components;
        private readonly Dictionary<Type, IComponent> m_componentsByTypes;
        private ReadOnlyCollection<IComponent> m_componentsCache;
        private bool m_cacheInvalidated;

        protected Entity()
            : this(Array.Empty<IComponent>())
        {
        }
        protected Entity(IList<IComponent> components)
        {
            m_components = new List<IComponent>(components);
            m_componentsByTypes = new Dictionary<Type, IComponent>();
            m_componentsCache = null;
            m_cacheInvalidated = true;

            foreach (IComponent component in components)
            {
                if (!m_componentsByTypes.TryAdd(component.GetType(), component))
                    throw new ArgumentException("Unable to create an entity with components of non-unique types.");
            }
        }

        public ReadOnlyCollection<IComponent> Components
        {
            get
            {
                if (!m_cacheInvalidated)
                    return m_componentsCache;
                else
                    return RebuildCache();
            }
        }

        public IComponent GetComponentOfType(Type componentType)
        {
            return m_componentsByTypes[componentType];
        }
        public TComponent GetComponentOfType<TComponent>()
            where TComponent : class, IComponent
        {
            Type type = typeof(TComponent);
            return (TComponent)GetComponentOfType(type);
        }

        public bool TryGetComponentOfType(Type componentType, out IComponent component)
        {
            return m_componentsByTypes.TryGetValue(componentType, out component);
        }
        public bool TryGetComponentOfType<TComponent>(out TComponent component)
            where TComponent : class, IComponent
        {
            Type type = typeof(TComponent);

            bool returnValue;
            (component, returnValue) = TryGetComponentOfType(type, out IComponent componentToAssign) ? ((TComponent)componentToAssign, true) : ((TComponent)null, false);
            return returnValue;
        }

        public bool HasComponentOfType(Type componentType)
        {
            return m_componentsByTypes.ContainsKey(componentType);
        }
        public bool HasComponentOfType<TComponent>()
            where TComponent : class, IComponent
        {
            Type type = typeof(TComponent);
            return HasComponentOfType(type);
        }

        protected void AddComponent(IComponent component)
        {
            m_components.Add(component);
            m_componentsByTypes.Add(component.GetType(), component);
            InvalidateCache();
        }

        protected void RemoveComponent(IComponent component)
        {
            foreach ((Type type, IComponent componentToCompare) in m_componentsByTypes)
            {
                if (componentToCompare == component)
                {
                    m_components.Remove(component);
                    m_componentsByTypes.Remove(type);
                    InvalidateCache();
                    break;
                }
            }
        }

        protected void RemoveComponentOfType(Type componentType)
        {
            if (m_componentsByTypes.TryGetValue(componentType, out IComponent component))
            {
                m_components.Remove(component);
                m_componentsByTypes.Remove(componentType);
                InvalidateCache();
            }
        }
        protected void RemoveComponentOfType<TComponent>()
            where TComponent : class, IComponent
        {
            Type type = typeof(TComponent);
            RemoveComponentOfType(type);
        }

        protected void ClearComponents()
        {
            m_components.Clear();
            m_componentsByTypes.Clear();
            InvalidateCache();
        }

        private void InvalidateCache()
        {
            m_cacheInvalidated = true;
        }

        private ReadOnlyCollection<IComponent> RebuildCache()
        {
            m_componentsCache = m_components.AsReadOnly();
            return m_componentsCache;
        }
    }
}
