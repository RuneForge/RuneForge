using AutoMapper;

using RuneForge.Data.Buildings;
using RuneForge.Game.Buildings;

namespace RuneForge.Game.AutoMapper
{
    public class BuildingProfile : Profile
    {
        public BuildingProfile()
        {
            SourceMemberNamingConvention = new ExactMatchNamingConvention();
            DestinationMemberNamingConvention = new ExactMatchNamingConvention();

            CreateMap<Building, BuildingDto>()
                .ForMember(unit => unit.OwnerId, options => options.MapFrom(unit => unit.Owner.Id));
        }
    }
}
