using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using Microsoft.Xna.Framework.Graphics;

namespace RuneForge.Core.TextureAtlases
{
    public class TextureAtlas
    {
        public string Name { get; }

        public Texture2D Texture { get; }

        public IReadOnlyDictionary<string, TextureRegion2D> TextureRegions { get; }

        public TextureAtlas(string name, Texture2D texture, IDictionary<string, TextureRegion2D> textureRegions)
        {
            Name = !string.IsNullOrEmpty(name)
                ? name : throw new ArgumentException("Name of the texture atlas can not be null or empty.", nameof(name));
            Texture = texture ?? throw new ArgumentNullException(nameof(texture));
            TextureRegions = textureRegions != null
                ? new ReadOnlyDictionary<string, TextureRegion2D>(textureRegions) : throw new ArgumentNullException(nameof(textureRegions));
        }
    }
}
