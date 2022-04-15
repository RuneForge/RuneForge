namespace RuneForge.Content.Pipeline.Game.Entities
{
    public abstract class ComponentPrototype
    {
        protected ComponentPrototype()
        {
        }

        public abstract string GetRuntimeTypeName();
    }
}
