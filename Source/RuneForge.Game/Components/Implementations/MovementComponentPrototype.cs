namespace RuneForge.Game.Components.Implementations
{
    public class MovementComponentPrototype : ComponentPrototype
    {
        public float MovementSpeed { get; }

        public MovementComponentPrototype(float movementSpeed)
        {
            MovementSpeed = movementSpeed;
        }
    }
}
