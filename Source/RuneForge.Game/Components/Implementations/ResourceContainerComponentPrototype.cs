namespace RuneForge.Game.Components.Implementations
{
    public class ResourceContainerComponentPrototype : ComponentPrototype
    {
        public decimal GoldAmount { get; }

        public ResourceContainerComponentPrototype(decimal goldAmount)
        {
            GoldAmount = goldAmount;
        }
    }
}
