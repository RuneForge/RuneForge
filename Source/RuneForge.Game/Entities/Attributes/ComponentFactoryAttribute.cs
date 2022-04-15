using System;

namespace RuneForge.Game.Entities.Attributes
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public sealed class ComponentFactoryAttribute : Attribute
    {
        public Type ComponentFactoryType { get; }

        public ComponentFactoryAttribute(Type componentFactoryType)
        {
            ComponentFactoryType = componentFactoryType;
        }
    }
}
