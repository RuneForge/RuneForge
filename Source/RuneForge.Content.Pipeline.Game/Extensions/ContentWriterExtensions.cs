using System;
using System.Linq;

using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;

using RuneForge.Content.Pipeline.Game.Entities;
using RuneForge.Content.Pipeline.Game.Entities.Attributes;
using RuneForge.Content.Pipeline.Game.Entities.Interfaces;

namespace RuneForge.Content.Pipeline.Game.Extensions
{
    public static class ContentWriterExtensions
    {
        public static void Write(this ContentWriter contentWriter, ComponentPrototype componentPrototype)
        {
            Type componentPrototypeType = componentPrototype.GetType();
            ComponentPrototypeWriterAttribute[] attributes = componentPrototypeType
                .GetCustomAttributes(typeof(ComponentPrototypeWriterAttribute), false)
                .Cast<ComponentPrototypeWriterAttribute>()
                .ToArray();

            if (attributes.Length == 0)
                throw new InvalidOperationException($"Unable to find the {nameof(ComponentPrototypeWriterAttribute)} attribute on the {componentPrototypeType.Name} type.");
            else
            {
                ComponentPrototypeWriterAttribute attribute = attributes.Single();

                Type componentPrototypeWriterType = attribute.ComponentPrototypeWriterType;
                IComponentPrototypeWriter componentPrototypeWriter = (IComponentPrototypeWriter)Activator.CreateInstance(componentPrototypeWriterType);

                contentWriter.Write(componentPrototype.GetRuntimeTypeName());
                componentPrototypeWriter.WriteComponentPrototype(contentWriter, componentPrototype);
            }
        }
    }
}
