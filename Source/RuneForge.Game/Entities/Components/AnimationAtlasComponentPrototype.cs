﻿using RuneForge.Game.Entities.Attributes;
using RuneForge.Game.Entities.ComponentFactories;
using RuneForge.Game.Entities.ComponentReaders;

namespace RuneForge.Game.Entities.Components
{
    [ComponentFactory(typeof(AnimationAtlasComponentFactory))]
    [ComponentPrototypeReader(typeof(AnimationAtlasComponentPrototypeReader))]
    public class AnimationAtlasComponentPrototype : ComponentPrototype
    {
        public string AnimationAtlasAssetName { get; }

        public bool HasPlayerColor { get; }

        public AnimationAtlasComponentPrototype(string animationAtlasAssetName, bool hasPlayerColor)
        {
            AnimationAtlasAssetName = animationAtlasAssetName;
            HasPlayerColor = hasPlayerColor;
        }
    }
}