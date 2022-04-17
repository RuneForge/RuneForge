using AutoMapper;

using RuneForge.Data.Components;
using RuneForge.Game.Entities.Interfaces;

namespace RuneForge.Game.AutoMapper
{
    public class ComponentProfile : Profile
    {
        public ComponentProfile()
        {
            SourceMemberNamingConvention = new ExactMatchNamingConvention();
            DestinationMemberNamingConvention = new ExactMatchNamingConvention();

            CreateMap<IComponent, ComponentDto>();
        }
    }
}
