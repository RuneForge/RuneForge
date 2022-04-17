using System;
using System.Linq;

using Microsoft.Xna.Framework.Content;

using RuneForge.Game.Entities.Attributes;
using RuneForge.Game.Entities.Interfaces;

namespace RuneForge.Game.Entities.Extensions
{
    public static class ContentReaderExtensions
    {
        public static ComponentPrototype ReadComponentPrototype(this ContentReader contentReader)
        {
            string componentPrototypeTypeName = contentReader.ReadString();
            Type componentPrototypeType = Type.GetType(componentPrototypeTypeName);
            ComponentPrototypeReaderAttribute[] attributes = componentPrototypeType
                .GetCustomAttributes(typeof(ComponentPrototypeReaderAttribute), false)
                .Cast<ComponentPrototypeReaderAttribute>()
                .ToArray();

            if (attributes.Length == 0)
                throw new InvalidOperationException($"Unable to find the {nameof(ComponentPrototypeReaderAttribute)} attribute on the {componentPrototypeType.Name} type.");
            else
            {
                ComponentPrototypeReaderAttribute attribute = attributes.Single();

                Type componentPrototypeReaderType = attribute.ComponentPrototypeReaderType;
                IComponentPrototypeReader componentPrototypeReader = (IComponentPrototypeReader)Activator.CreateInstance(componentPrototypeReaderType);

                return componentPrototypeReader.ReadComponentPrototype(contentReader);
            }
        }
    }
}
