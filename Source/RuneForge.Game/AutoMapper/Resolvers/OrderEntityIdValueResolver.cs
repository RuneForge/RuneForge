using System;

using AutoMapper;

using RuneForge.Data.Orders;
using RuneForge.Game.Buildings;
using RuneForge.Game.Orders;
using RuneForge.Game.Units;

namespace RuneForge.Game.AutoMapper.Resolvers
{
    public class OrderEntityIdValueResolver : IValueResolver<Order, OrderDto, string>
    {
        public string Resolve(Order source, OrderDto destination, string destMember, ResolutionContext context)
        {
            if (source.Entity == null)
                return null;
            else
            {
                string typeName = source.Entity.GetType().Name;
                int id = source.Entity switch
                {
                    Unit unit => unit.Id,
                    Building building => building.Id,
                    _ => throw new NotSupportedException($"Unable to resaolve an integer Id for the entity of type {typeName}"),
                };
                return $"{typeName}:{id}";
            }
        }
    }
}
