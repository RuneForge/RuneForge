using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

using RuneForge.Game.Components.Extensions;
using RuneForge.Game.Components.Interfaces;

namespace RuneForge.Game.Components
{
    public static class ComponentFactoryHelpers
    {
        public static Collection<IComponent> CreateComponentCollection(IServiceProvider serviceProvider, IList<ComponentPrototype> componentPrototypes)
        {
            return CreateComponentCollection(serviceProvider, componentPrototypes, Array.Empty<ComponentPrototype>());
        }

        public static Collection<IComponent> CreateComponentCollection(IServiceProvider serviceProvider, IList<ComponentPrototype> componentPrototypes, IList<ComponentPrototype> componentPrototypeOverrides)
        {
            Dictionary<Type, ComponentPrototype> componentPrototypeOverridesByTypes = componentPrototypeOverrides.ToDictionary(componentPrototypeOverride => componentPrototypeOverride.GetType());

            Collection<IComponent> components = new Collection<IComponent>();
            foreach (ComponentPrototype componentPrototype in componentPrototypes)
            {
                Type componentPrototypeType = componentPrototype.GetType();

                if (!componentPrototypeOverridesByTypes.TryGetValue(componentPrototypeType, out ComponentPrototype componentPrototypeOverride))
                    componentPrototypeOverride = null;

                IComponentFactory componentFactory = serviceProvider.GetComponentFactory(componentPrototypeType);
                IComponent component = componentFactory.CreateComponentFromPrototype(componentPrototype, componentPrototypeOverride);
                components.Add(component);
            }
            return components;
        }
    }
}
