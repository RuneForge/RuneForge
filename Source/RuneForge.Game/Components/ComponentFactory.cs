using RuneForge.Game.Components.Interfaces;

namespace RuneForge.Game.Components
{
    public abstract class ComponentFactory<TComponent, TComponentPrototype> : IComponentFactory<TComponent, TComponentPrototype>
        where TComponent : IComponent
        where TComponentPrototype : ComponentPrototype
    {
        protected ComponentFactory()
        {
        }

        public abstract TComponent CreateComponentFromPrototype(TComponentPrototype componentPrototype, TComponentPrototype componentPrototypeOverride);

        public IComponent CreateComponentFromPrototype(ComponentPrototype componentPrototype, ComponentPrototype componentPrototypeOverride)
        {
            return CreateComponentFromPrototype((TComponentPrototype)componentPrototype, (TComponentPrototype)componentPrototypeOverride);
        }
    }
}
