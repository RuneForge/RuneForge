using System;

namespace RuneForge.Game.Components.Attributes
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public sealed class ComponentPrototypeReaderAttribute : Attribute
    {
        public Type ComponentPrototypeReaderType { get; }

        public ComponentPrototypeReaderAttribute(Type componentPrototypeReaderType)
        {
            ComponentPrototypeReaderType = componentPrototypeReaderType;
        }
    }
}
