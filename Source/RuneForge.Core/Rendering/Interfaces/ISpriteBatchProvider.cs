using System;

using Microsoft.Xna.Framework.Graphics;

namespace RuneForge.Core.Rendering.Interfaces
{
    public interface ISpriteBatchProvider
    {
        public SpriteBatch WorldSpriteBatch { get; set; }
    }
}
