namespace RuneForge.Content.Pipeline.Game.Components
{
    public abstract class ComponentPrototype
    {
        protected ComponentPrototype()
        {
        }

        public abstract string GetRuntimeTypeName();
    }
}
