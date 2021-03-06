using System.Linq;

using AutoMapper;

using RuneForge.Data.Components;
using RuneForge.Game.AutoMapper.Resolvers;
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
                .Include<MovementComponent, MovementComponentDto>()
                .Include<ResourceContainerComponent, ResourceContainerComponentDto>()
                .Include<ResourceSourceComponent, ResourceSourceComponentDto>()
                .Include<ResourceStorageComponent, ResourceStorageComponentDto>()
                .Include<UnitShelterComponent, UnitShelterComponentDto>()
                .Include<UnitShelterOccupantComponent, UnitShelterOccupantComponentDto>()
                .Include<HealthComponent, HealthComponentDto>()
                .Include<DurabilityComponent, DurabilityComponentDto>()
                .Include<MeleeCombatComponent, MeleeCombatComponentDto>()
                .Include<ProductionCostComponent, ProductionCostComponentDto>()
                .Include<ProductionFacilityComponent, ProductionFacilityComponentDto>();

            CreateMap<TextureAtlasComponent, TextureAtlasComponentDto>();
            CreateMap<AnimationAtlasComponent, AnimationAtlasComponentDto>();
            CreateMap<AnimationStateComponent, AnimationStateComponentDto>();
            CreateMap<OrderQueueComponent, OrderQueueComponentDto>();
            CreateMap<LocationComponent, LocationComponentDto>();
            CreateMap<DirectionComponent, DirectionComponentDto>();
            CreateMap<MovementComponent, MovementComponentDto>();
            CreateMap<ResourceContainerComponent, ResourceContainerComponentDto>();
            CreateMap<ResourceSourceComponent, ResourceSourceComponentDto>();
            CreateMap<ResourceStorageComponent, ResourceStorageComponentDto>();
            CreateMap<UnitShelterComponent, UnitShelterComponentDto>()
                .ForMember(component => component.OccupantIds, options => options.MapFrom(component => component.Occupants.Select(unit => unit.Id).ToArray()));
            CreateMap<UnitShelterOccupantComponent, UnitShelterOccupantComponentDto>();
            CreateMap<HealthComponent, HealthComponentDto>();
            CreateMap<DurabilityComponent, DurabilityComponentDto>();
            CreateMap<MeleeCombatComponent, MeleeCombatComponentDto>()
                .ForMember(component => component.TargetEntityId, options => options.MapFrom<MeleeCombatComponentTargetEntityIdValueResolver>());
            CreateMap<ProductionCostComponent, ProductionCostComponentDto>();
            CreateMap<ProductionFacilityComponent, ProductionFacilityComponentDto>()
                .ForMember(component => component.UnitCodesProduced, options => options.MapFrom(component => component.UnitsProduced.Select(unitPrototype => unitPrototype.Code).ToArray()))
                .ForMember(component => component.UnitCodeCurrentlyProduced, options => options.MapFrom(component => component.UnitCurrentlyProduced.Code));
        }
    }
}
