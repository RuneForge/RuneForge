using Microsoft.Xna.Framework.Graphics;

namespace RuneForge.Core.Rendering
{
    public class Camera2DParameters
    {
        private const float c_minScale = 0.25f;
        private const float c_maxScale = 4.0f;
        private const float c_defaultScale = 1.0f;

        public Viewport Viewport { get; set; }

        public float MinScale { get; set; } = c_minScale;
        public float MaxScale { get; set; } = c_maxScale;
        public float DefaultScale { get; set; } = c_defaultScale;
    }
}
