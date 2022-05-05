using System;

namespace RuneForge.Game.Orders.Attributes
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public sealed class OrderDtoAttribute : Attribute
    {
        public Type OrderDtoType { get; }

        public OrderDtoAttribute(Type orderDtoType)
        {
            OrderDtoType = orderDtoType;
        }
    }
}
