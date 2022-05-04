using Microsoft.Xna.Framework;

namespace RuneForge.Core.Rendering.Interfaces
{
    public interface IRenderer : IUpdateable, IDrawable
    {
        public void LoadContent();
    }
}
