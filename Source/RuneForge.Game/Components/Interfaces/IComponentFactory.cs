using RuneForge.Data.Components;

namespace RuneForge.Game.Components.Interfaces
{
    public interface IComponentFactory
    {
        public IComponent CreateComponentFromPrototype(ComponentPrototype componentPrototype, ComponentPrototype componentPrototypeOverride);
        public IComponent CreateComponentFromDto(ComponentDto componentDto);
    }

    public interface IComponentFactory<TComponent, TComponentPrototype, TComponentDto> : IComponentFactory
        where TComponent : IComponent
        where TComponentPrototype : ComponentPrototype
        where TComponentDto : ComponentDto
    {
        public TComponent CreateComponentFromPrototype(TComponentPrototype componentPrototype, TComponentPrototype componentPrototypeOverride);

        public TComponent CreateComponentFromDto(TComponentDto componentDto);
    }
}
