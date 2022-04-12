using AutoMapper;

using RuneForge.Data.Units;
using RuneForge.Game.Units;

namespace RuneForge.Game.AutoMapper
{
    public class UnitProfile : Profile
    {
        public UnitProfile()
        {
            SourceMemberNamingConvention = new ExactMatchNamingConvention();
            DestinationMemberNamingConvention = new ExactMatchNamingConvention();

            CreateMap<Unit, UnitDto>()
                .ForMember(unit => unit.OwnerId, options => options.MapFrom(unit => unit.Owner.Id));
        }
    }
}
