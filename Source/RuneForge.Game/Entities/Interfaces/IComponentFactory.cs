namespace RuneForge.Game.Entities.Interfaces
{
    public interface IComponentFactory
    {
        public IComponent CreateComponentFromPrototype(ComponentPrototype componentPrototype);
    }

    public interface IComponentFactory<TComponent, TComponentPrototype> : IComponentFactory
        where TComponent : IComponent
        where TComponentPrototype : ComponentPrototype
    {
        public TComponent CreateComponentFromPrototype(TComponentPrototype componentPrototype);
    }
}
