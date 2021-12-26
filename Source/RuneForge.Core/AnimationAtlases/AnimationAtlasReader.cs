using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace RuneForge.Core.AnimationAtlases
{
    public class AnimationAtlasReader : ContentTypeReader<AnimationAtlas>
    {
        protected override AnimationAtlas Read(ContentReader reader, AnimationAtlas existingInstance)
        {
            string animationAtlasAssetName = reader.AssetName;

            string textureAssetName = reader.ReadString();
            Texture2D texture = reader.ContentManager.Load<Texture2D>(textureAssetName);

            int animationsCount = reader.ReadInt32();
            Dictionary<string, Animation2D> animations = new Dictionary<string, Animation2D>();
            for (int i = 0; i < animationsCount; i++)
            {
                string animationName = reader.ReadString();

                bool animationLooped = reader.ReadBoolean();
                bool animationReversed = reader.ReadBoolean();

                int animationRegionsCount = reader.ReadInt32();
                List<AnimationRegion2D> animationRegions = new List<AnimationRegion2D>();
                for (int j = 0; j < animationRegionsCount; j++)
                {
                    string animationRegionName = reader.ReadString();

                    Rectangle animationRegionBounds = new Rectangle(reader.ReadInt32(), reader.ReadInt32(), reader.ReadInt32(), reader.ReadInt32());

                    TimeSpan animationRegionFrameTime = TimeSpan.FromMilliseconds(reader.ReadInt32());

                    animationRegions.Add(new AnimationRegion2D(animationRegionName, texture, animationRegionBounds, animationRegionFrameTime));
                }

                animations.Add(animationName, new Animation2D(animationName, animationRegions, animationLooped, animationReversed, false));
            }

            return new AnimationAtlas(animationAtlasAssetName, texture, animations);
        }
    }
}
