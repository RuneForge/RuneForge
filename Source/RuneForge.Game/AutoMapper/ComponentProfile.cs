using AutoMapper;

using RuneForge.Data.Components;
using RuneForge.Game.Entities.Components;
using RuneForge.Game.Entities.Interfaces;

namespace RuneForge.Game.AutoMapper
{
    public class ComponentProfile : Profile
    {
        public ComponentProfile()
        {
            SourceMemberNamingConvention = new ExactMatchNamingConvention();
            DestinationMemberNamingConvention = new ExactMatchNamingConvention();

            CreateMap<IComponent, ComponentDto>()
                .Include<TextureAtlasComponent, TextureAtlasComponentDto>()
                .Include<AnimationAtlasComponent, AnimationAtlasComponentDto>()
                .Include<LocationComponent, LocationComponentDto>()
                .Include<DirectionComponent, DirectionComponentDto>();

            CreateMap<TextureAtlasComponent, TextureAtlasComponentDto>();
            CreateMap<AnimationAtlasComponent, AnimationAtlasComponentDto>();
            CreateMap<LocationComponent, LocationComponentDto>();
            CreateMap<DirectionComponent, DirectionComponentDto>();
        }
    }
}
