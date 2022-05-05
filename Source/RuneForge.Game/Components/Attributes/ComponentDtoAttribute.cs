using System;

namespace RuneForge.Game.Components.Attributes
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public sealed class ComponentDtoAttribute : Attribute
    {
        public Type ComponentDtoType { get; }

        public ComponentDtoAttribute(Type componentDtoType)
        {
            ComponentDtoType = componentDtoType;
        }
    }
}
