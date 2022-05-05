using RuneForge.Data.Components;
using RuneForge.Game.Components.Interfaces;

namespace RuneForge.Game.Components
{
    public abstract class ComponentFactory<TComponent, TComponentPrototype, TComponentDto> : IComponentFactory<TComponent, TComponentPrototype, TComponentDto>
        where TComponent : IComponent
        where TComponentPrototype : ComponentPrototype
        where TComponentDto : ComponentDto
    {
        protected ComponentFactory()
        {
        }

        public abstract TComponent CreateComponentFromPrototype(TComponentPrototype componentPrototype, TComponentPrototype componentPrototypeOverride);

        public abstract TComponent CreateComponentFromDto(TComponentDto componentDto);

        public IComponent CreateComponentFromPrototype(ComponentPrototype componentPrototype, ComponentPrototype componentPrototypeOverride)
        {
            return CreateComponentFromPrototype((TComponentPrototype)componentPrototype, (TComponentPrototype)componentPrototypeOverride);
        }

        public IComponent CreateComponentFromDto(ComponentDto componentDto)
        {
            return CreateComponentFromDto((TComponentDto)componentDto);
        }
    }
}
