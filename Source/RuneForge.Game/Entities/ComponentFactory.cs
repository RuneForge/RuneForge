using RuneForge.Game.Entities.Interfaces;

namespace RuneForge.Game.Entities
{
    public abstract class ComponentFactory<TComponent, TComponentPrototype> : IComponentFactory<TComponent, TComponentPrototype>
        where TComponent : IComponent
        where TComponentPrototype : ComponentPrototype
    {
        protected ComponentFactory()
        {
        }

        public abstract TComponent CreateComponentFromPrototype(TComponentPrototype componentPrototype);

        public IComponent CreateComponentFromPrototype(ComponentPrototype componentPrototype)
        {
            return CreateComponentFromPrototype((TComponentPrototype)componentPrototype);
        }
    }
}
