using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using Microsoft.Xna.Framework.Graphics;

namespace RuneForge.Core.AnimationAtlases
{
    public class AnimationAtlas
    {
        public string Name { get; }

        public Texture2D Texture { get; }

        public IReadOnlyDictionary<string, Animation2D> Animations { get; }

        public AnimationAtlas(string name, Texture2D texture, IDictionary<string, Animation2D> animations)
        {
            Name = !string.IsNullOrEmpty(name)
                ? name : throw new ArgumentException("Name of the animation atlas can not be null or empty.", nameof(name));
            Texture = texture ?? throw new ArgumentNullException(nameof(texture));
            Animations = animations != null
                ? new ReadOnlyDictionary<string, Animation2D>(animations) : throw new ArgumentNullException(nameof(animations));
        }
    }
}
