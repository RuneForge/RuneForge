using System;

using AutoMapper;

using RuneForge.Data.Components;
using RuneForge.Game.Buildings;
using RuneForge.Game.Components.Implementations;
using RuneForge.Game.Units;

namespace RuneForge.Game.AutoMapper.Resolvers
{
    public class MeleeCombatComponentTargetEntityIdValueResolver : IValueResolver<MeleeCombatComponent, MeleeCombatComponentDto, string>
    {
        public string Resolve(MeleeCombatComponent source, MeleeCombatComponentDto destination, string destMember, ResolutionContext context)
        {
            if (source.TargetEntity == null)
                return null;
            else
            {
                string typeName = source.TargetEntity.GetType().Name;
                int id = source.TargetEntity switch
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
