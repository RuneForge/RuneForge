using System;
using System.Linq;

using Microsoft.Extensions.DependencyInjection;

using RuneForge.Game.Components.Attributes;
using RuneForge.Game.Components.Interfaces;

namespace RuneForge.Game.Components.Extensions
{
    internal static class ServiceProviderExtensions
    {
        public static IComponentFactory GetComponentFactory(this IServiceProvider serviceProvider, Type componentPrototypeType)
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
    }
}
