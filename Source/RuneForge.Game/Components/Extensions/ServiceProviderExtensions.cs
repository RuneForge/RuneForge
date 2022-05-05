using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using Microsoft.Extensions.DependencyInjection;

using RuneForge.Game.Components.Attributes;
using RuneForge.Game.Components.Interfaces;

namespace RuneForge.Game.Components.Extensions
{
    internal static class ServiceProviderExtensions
    {
        public static IComponentFactory GetComponentFactoryByPrototypeType(this IServiceProvider serviceProvider, Type componentPrototypeType)
        {
            ComponentFactoryAttribute[] attributes = componentPrototypeType
                .GetCustomAttributes(typeof(ComponentFactoryAttribute), false)
                .Cast<ComponentFactoryAttribute>()
                .ToArray();

            if (attributes.Length == 0)
                throw new InvalidOperationException($"Unable to find the {nameof(ComponentFactoryAttribute)} attribute on the {componentPrototypeType.Name} type.");
            else
            {
                ComponentFactoryAttribute attribute = attributes.Single();

                Type componentFactoryType = attribute.ComponentFactoryType;
                return (IComponentFactory)serviceProvider.GetRequiredService(componentFactoryType);
            }
        }

        public static IComponentFactory GetComponentFactoryByDtoType(this IServiceProvider serviceProvider, Type componentDtoType)
        {
            IEnumerable<IComponentFactory> componentFactories = serviceProvider.GetRequiredService<IEnumerable<IComponentFactory>>();
            foreach (IComponentFactory componentFactory in componentFactories)
            {
                Type componentFactoryType = componentFactory.GetType();

                ComponentDtoAttribute[] attributes = componentFactoryType
                    .GetCustomAttributes(typeof(ComponentDtoAttribute), false)
                    .Cast<ComponentDtoAttribute>()
                    .ToArray();

                if (attributes.Length == 0)
                    throw new InvalidOperationException($"Unable to find the {nameof(ComponentDtoAttribute)} attribute on the {componentFactoryType.Name} type.");
                else
                {
                    ComponentDtoAttribute attribute = attributes.Single();

                    if (componentDtoType == attribute.ComponentDtoType)
                        return componentFactory;
                }
            }

            throw new InvalidOperationException($"Unable to find a factory for the {componentDtoType.Name} type.");
        }
    }
}
