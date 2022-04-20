using System;

namespace RuneForge.Content.Pipeline.Game.Components.Attributes
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public sealed class ComponentPrototypeWriterAttribute : Attribute
    {
        public Type ComponentPrototypeWriterType { get; }

        public ComponentPrototypeWriterAttribute(Type componentPrototypeWriterType)
        {
            ComponentPrototypeWriterType = componentPrototypeWriterType;
        }
    }
}
