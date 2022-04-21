using RuneForge.Game.Components.Implementations;

namespace RuneForge.Game.Components.Factories
{
    public class MovementComponentFactory : ComponentFactory<MovementComponent, MovementComponentPrototype>
    {
        public override MovementComponent CreateComponentFromPrototype(MovementComponentPrototype componentPrototype, MovementComponentPrototype componentPrototypeOverride)
        {
            return new MovementComponent(componentPrototype.MovementSpeed, componentPrototype.MovementType);
        }
    }
}
