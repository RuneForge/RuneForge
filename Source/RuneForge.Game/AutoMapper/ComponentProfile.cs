using AutoMapper;

using RuneForge.Data.Components;
using RuneForge.Game.Components.Implementations;
using RuneForge.Game.Components.Interfaces;

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
                .Include<AnimationStateComponent, AnimationStateComponentDto>()
                .Include<OrderQueueComponent, OrderQueueComponentDto>()
                .Include<LocationComponent, LocationComponentDto>()
                .Include<DirectionComponent, DirectionComponentDto>()
                .Include<MovementComponent, MovementComponentDto>();

            CreateMap<TextureAtlasComponent, TextureAtlasComponentDto>();
            CreateMap<AnimationAtlasComponent, AnimationAtlasComponentDto>();
            CreateMap<AnimationStateComponent, AnimationStateComponentDto>();
            CreateMap<OrderQueueComponent, OrderQueueComponentDto>();
            CreateMap<LocationComponent, LocationComponentDto>();
            CreateMap<DirectionComponent, DirectionComponentDto>();
            CreateMap<MovementComponent, MovementComponentDto>();
        }
    }
}
