using AutoMapper;

using RuneForge.Data.Maps;
using RuneForge.Game.Maps;

namespace RuneForge.Game.AutoMapper
{
    public class MapDecorationProfile : Profile
    {
        public MapDecorationProfile()
        {
            SourceMemberNamingConvention = new ExactMatchNamingConvention();
            DestinationMemberNamingConvention = new ExactMatchNamingConvention();

            CreateMap<MapDecoration, MapDecorationDto>();
        }
    }
}
