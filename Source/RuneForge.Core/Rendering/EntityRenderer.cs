using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using RuneForge.Core.AnimationAtlases;
using RuneForge.Core.Rendering.Extensions;
using RuneForge.Core.Rendering.Interfaces;
using RuneForge.Core.TextureAtlases;
using RuneForge.Game.Buildings;
using RuneForge.Game.Components.Implementations;
using RuneForge.Game.Entities;
using RuneForge.Game.GameSessions.Interfaces;
using RuneForge.Game.Maps;
using RuneForge.Game.Players;
using RuneForge.Game.Units;

namespace RuneForge.Core.Rendering
{
    public class EntityRenderer : Renderer
    {
        private const string c_defaultBuildingTextureName = "DefaultState";

        private static readonly TimeSpan s_animationCacheResetPeriod = TimeSpan.FromSeconds(120);
        private static readonly List<(string, PlayerColorProviderMethod)> s_playerColorShades = new List<(string, PlayerColorProviderMethod)>()
        {
            (nameof(PlayerColor.EntityColorShadeA), playerColor => playerColor.EntityColorShadeA),
            (nameof(PlayerColor.EntityColorShadeB), playerColor => playerColor.EntityColorShadeB),
            (nameof(PlayerColor.EntityColorShadeC), playerColor => playerColor.EntityColorShadeC),
            (nameof(PlayerColor.EntityColorShadeD), playerColor => playerColor.EntityColorShadeD),
        };

        private readonly IGameSessionContext m_gameSessionContext;
        private readonly ISpriteBatchProvider m_spriteBatchProvider;
        private readonly Camera2D m_camera;
        private readonly Camera2DParameters m_cameraParameters;
        private readonly Lazy<ContentManager> m_contentManagerProvider;
        private readonly Dictionary<string, TextureAtlas> m_textureAtlasesByAssetNames;
        private readonly Dictionary<string, AnimationAtlas> m_animationAtlasesByAssetNames;
        private readonly Dictionary<string, Texture2D> m_externalTexturesByAssetNames;
        private readonly Dictionary<Entity, Animation2D> m_animationCache;
        private TimeSpan m_timeSincePreviousCacheReset;

        public EntityRenderer(
            IGameSessionContext gameSessionContext,
            ISpriteBatchProvider spriteBatchProvider,
            Camera2D camera,
            Camera2DParameters cameraParameters,
            Lazy<ContentManager> contentManagerProvider
            )
        {
            m_gameSessionContext = gameSessionContext;
            m_spriteBatchProvider = spriteBatchProvider;
            m_camera = camera;
            m_cameraParameters = cameraParameters;
            m_contentManagerProvider = contentManagerProvider;
            m_textureAtlasesByAssetNames = new Dictionary<string, TextureAtlas>();
            m_animationAtlasesByAssetNames = new Dictionary<string, AnimationAtlas>();
            m_externalTexturesByAssetNames = new Dictionary<string, Texture2D>();
            m_animationCache = new Dictionary<Entity, Animation2D>();
            m_timeSincePreviousCacheReset = TimeSpan.Zero;
        }

        public override void Update(GameTime gameTime)
        {
            foreach (Entity entity in m_gameSessionContext.Buildings.Concat<Entity>(m_gameSessionContext.Units))
            {
                if (!entity.HasComponentOfType<LocationComponent>())
                    continue;
                if (entity.TryGetComponentOfType(out UnitShelterOccupantComponent unitShelterOccupantComponent) && unitShelterOccupantComponent.InsideShelter)
                    continue;

                if (entity.HasComponentOfType<AnimationAtlasComponent>()
                    && entity.TryGetComponentOfType(out AnimationStateComponent animationStateComponent)
                    && !string.IsNullOrEmpty(animationStateComponent.AnimationName))
                {
                    AnimationAtlasComponent animationAtlasComponent = entity.GetComponentOfType<AnimationAtlasComponent>();

                    ContentManager contentManager = m_contentManagerProvider.Value;
                    string animationAtlasAssetName = animationAtlasComponent.AnimationAtlasAssetName;
                    if (!m_animationAtlasesByAssetNames.TryGetValue(animationAtlasAssetName, out AnimationAtlas animationAtlas))
                    {
                        animationAtlas = contentManager.Load<AnimationAtlas>(animationAtlasAssetName);
                        m_animationAtlasesByAssetNames.Add(animationAtlasAssetName, animationAtlas);
                    }

                    string animationName = animationStateComponent.AnimationName;
                    if (entity.TryGetComponentOfType(out DirectionComponent directionComponent))
                        animationName = $"{animationName}-{directionComponent.Direction}";

                    if (!m_animationCache.TryGetValue(entity, out Animation2D animation) || animation.Name != animationName)
                    {
                        animation = animationAtlas.Animations[animationName].CreateUpdateableCopy();
                        m_animationCache[entity] = animation;
                    }

                    if (animationStateComponent.ResetRequested || animation.Completed)
                        animation.Reset();
                    animation.Update(new GameTime(TimeSpan.Zero, animationStateComponent.ElapsedTime));
                }
            }
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            Rectangle viewportBounds = m_cameraParameters.Viewport.Bounds;
            (int minVisibleX, int minVisibleY) = m_camera.TranslateScreenToWorld(new Vector2(viewportBounds.Left, viewportBounds.Top)).ToPoint();
            (int maxVisibleX, int maxVisibleY) = m_camera.TranslateScreenToWorld(new Vector2(viewportBounds.Right, viewportBounds.Bottom)).ToPoint();

            foreach (Entity entity in m_gameSessionContext.Buildings.Concat<Entity>(m_gameSessionContext.Units))
            {
                if (!entity.TryGetComponentOfType(out LocationComponent locationComponent))
                    continue;
                if (entity.TryGetComponentOfType(out UnitShelterOccupantComponent unitShelterOccupantComponent) && unitShelterOccupantComponent.InsideShelter)
                    continue;

                int minX = (int)locationComponent.X;
                int minY = (int)locationComponent.Y;
                int maxX = minX + locationComponent.Width;
                int maxY = minY + locationComponent.Height;

                if (minX >= maxVisibleX || minY >= maxVisibleY || maxX <= minVisibleX || maxY <= minVisibleY)
                    continue;

                if (entity.HasComponentOfType<AnimationAtlasComponent>()
                    && entity.TryGetComponentOfType(out AnimationStateComponent animationStateComponent)
                    && !string.IsNullOrEmpty(animationStateComponent.AnimationName))
                    DrawAnimation(entity);
                else if (entity.HasComponentOfType<TextureAtlasComponent>())
                    DrawTextureRegion(entity);
            }

            m_timeSincePreviousCacheReset += gameTime.ElapsedGameTime;
            if (m_timeSincePreviousCacheReset > s_animationCacheResetPeriod)
            {
                m_timeSincePreviousCacheReset = TimeSpan.Zero;
                ClearAnimationCache();
            }
        }

        public override void LoadContent()
        {
            Map map = m_gameSessionContext.Map;
            ContentManager contentManager = m_contentManagerProvider.Value;

            var componentPrototypeGroups = map.UnitInstancePrototypes.Select(unitInstancePrototype =>
            {
                UnitPrototype unitPrototype = unitInstancePrototype.EntityPrototype;
                return new
                {
                    TextureAtlasComponentPrototype = unitPrototype.ComponentPrototypes.OfType<TextureAtlasComponentPrototype>().FirstOrDefault(),
                    AnimationAtlasComponentPrototype = unitPrototype.ComponentPrototypes.OfType<AnimationAtlasComponentPrototype>().FirstOrDefault(),
                };
            }).Concat(map.BuildingInstancePrototypes.Select(buildingInstancePrototype =>
            {
                BuildingPrototype buildingPrototype = buildingInstancePrototype.EntityPrototype;
                return new
                {
                    TextureAtlasComponentPrototype = buildingPrototype.ComponentPrototypes.OfType<TextureAtlasComponentPrototype>().FirstOrDefault(),
                    AnimationAtlasComponentPrototype = buildingPrototype.ComponentPrototypes.OfType<AnimationAtlasComponentPrototype>().FirstOrDefault(),
                };
            })).ToList();

            foreach (var componentPrototypeGroup in componentPrototypeGroups)
            {
                if (componentPrototypeGroup.TextureAtlasComponentPrototype != null)
                {
                    string textureAtlasAssetName = componentPrototypeGroup.TextureAtlasComponentPrototype.TextureAtlasAssetName;
                    if (!m_textureAtlasesByAssetNames.TryGetValue(textureAtlasAssetName, out TextureAtlas textureAtlas))
                    {
                        textureAtlas = contentManager.Load<TextureAtlas>(textureAtlasAssetName);
                        m_textureAtlasesByAssetNames.Add(textureAtlasAssetName, textureAtlas);
                    }

                    if (componentPrototypeGroup.TextureAtlasComponentPrototype.HasPlayerColor)
                    {
                        foreach ((string externalTextureAssetNameSuffix, PlayerColorProviderMethod playerColorProviderMethod) in s_playerColorShades)
                        {
                            string externalTextureAssetName = $"{textureAtlas.Texture.Name}-{externalTextureAssetNameSuffix}";
                            if (!m_externalTexturesByAssetNames.TryGetValue(externalTextureAssetName, out Texture2D texture))
                            {
                                texture = contentManager.Load<Texture2D>(externalTextureAssetName);
                                m_externalTexturesByAssetNames.Add(externalTextureAssetName, texture);
                            }
                        }
                    }
                }

                if (componentPrototypeGroup.AnimationAtlasComponentPrototype != null)
                {
                    string animationAtlasAssetName = componentPrototypeGroup.AnimationAtlasComponentPrototype.AnimationAtlasAssetName;
                    if (!m_animationAtlasesByAssetNames.TryGetValue(animationAtlasAssetName, out AnimationAtlas animationAtlas))
                    {
                        animationAtlas = contentManager.Load<AnimationAtlas>(animationAtlasAssetName);
                        m_animationAtlasesByAssetNames.Add(animationAtlasAssetName, animationAtlas);
                    }

                    if (componentPrototypeGroup.AnimationAtlasComponentPrototype.HasPlayerColor)
                    {
                        foreach ((string externalTextureAssetNameSuffix, PlayerColorProviderMethod playerColorProviderMethod) in s_playerColorShades)
                        {
                            string externalTextureAssetName = $"{animationAtlas.Texture.Name}-{externalTextureAssetNameSuffix}";
                            if (!m_externalTexturesByAssetNames.TryGetValue(externalTextureAssetName, out Texture2D texture))
                            {
                                texture = contentManager.Load<Texture2D>(externalTextureAssetName);
                                m_externalTexturesByAssetNames.Add(externalTextureAssetName, texture);
                            }
                        }
                    }
                }
            }
        }

        private void DrawAnimation(Entity entity)
        {
            AnimationAtlasComponent animationAtlasComponent = entity.GetComponentOfType<AnimationAtlasComponent>();
            AnimationStateComponent animationStateComponent = entity.GetComponentOfType<AnimationStateComponent>();
            LocationComponent locationComponent = entity.GetComponentOfType<LocationComponent>();

            ContentManager contentManager = m_contentManagerProvider.Value;
            string animationAtlasAssetName = animationAtlasComponent.AnimationAtlasAssetName;
            if (!m_animationAtlasesByAssetNames.TryGetValue(animationAtlasAssetName, out AnimationAtlas animationAtlas))
            {
                animationAtlas = contentManager.Load<AnimationAtlas>(animationAtlasAssetName);
                m_animationAtlasesByAssetNames.Add(animationAtlasAssetName, animationAtlas);
            }

            string animationName = animationStateComponent.AnimationName;
            if (entity.TryGetComponentOfType(out DirectionComponent directionComponent))
                animationName = $"{animationName}-{directionComponent.Direction}";

            if (!m_animationCache.TryGetValue(entity, out Animation2D animation) || animation.Name != animationName)
            {
                animation = animationAtlas.Animations[animationName].CreateUpdateableCopy();
                m_animationCache[entity] = animation;
            }

            AnimationRegion2D mainAnimationRegion = animation.GetCurrentAnimationRegion();
            Rectangle buildingRectange = new Rectangle((int)locationComponent.X, (int)locationComponent.Y, locationComponent.Width, locationComponent.Height);
            Rectangle drawingRectangle = new Rectangle(
                buildingRectange.Center.X - mainAnimationRegion.Width / 2,
                buildingRectange.Center.Y - (mainAnimationRegion.Height / 2),
                mainAnimationRegion.Width,
                mainAnimationRegion.Height
                );

            SpriteBatch spriteBatch = m_spriteBatchProvider.WorldSpriteBatch;
            spriteBatch.Draw(animation, drawingRectangle, Color.White);

            if (!animationAtlasComponent.HasPlayerColor)
                return;

            Player entityOwner = entity switch
            {
                Unit unit => unit.Owner,
                Building building => building.Owner,
                _ => null,
            };

            if (entityOwner == null)
                return;

            PlayerColor playerColor = entityOwner.Color;
            foreach ((string externalTextureAssetNameSuffix, PlayerColorProviderMethod playerColorProviderMethod) in s_playerColorShades)
            {
                string externalTextureAssetName = $"{animationAtlas.Texture.Name}-{externalTextureAssetNameSuffix}";
                if (!m_externalTexturesByAssetNames.TryGetValue(externalTextureAssetName, out Texture2D externalTexture))
                {
                    externalTexture = contentManager.Load<Texture2D>(externalTextureAssetName);
                    m_externalTexturesByAssetNames.Add(externalTextureAssetName, externalTexture);
                }

                Color maskColor = playerColorProviderMethod(playerColor);
                spriteBatch.DrawTextureRegionUsingExternalTexture(mainAnimationRegion, externalTexture, drawingRectangle, maskColor);
            }
        }

        private void DrawTextureRegion(Entity entity)
        {
            LocationComponent locationComponent = entity.GetComponentOfType<LocationComponent>();
            TextureAtlasComponent textureAtlasComponent = entity.GetComponentOfType<TextureAtlasComponent>();

            ContentManager contentManager = m_contentManagerProvider.Value;
            string textureAtlasAssetName = textureAtlasComponent.TextureAtlasAssetName;
            if (!m_textureAtlasesByAssetNames.TryGetValue(textureAtlasAssetName, out TextureAtlas textureAtlas))
            {
                textureAtlas = contentManager.Load<TextureAtlas>(textureAtlasAssetName);
                m_textureAtlasesByAssetNames.Add(textureAtlasAssetName, textureAtlas);
            }

            string textureRegionName = c_defaultBuildingTextureName;
            if (entity.TryGetComponentOfType(out DirectionComponent directionComponent))
                textureRegionName = $"{textureRegionName}-{directionComponent.Direction}";

            TextureRegion2D mainTextureRegion = textureAtlas.TextureRegions[textureRegionName];
            Rectangle buildingRectange = new Rectangle((int)locationComponent.X, (int)locationComponent.Y, locationComponent.Width, locationComponent.Height);
            Rectangle drawingRectangle = new Rectangle(
                buildingRectange.Center.X - (mainTextureRegion.Width / 2),
                buildingRectange.Center.Y - (mainTextureRegion.Height / 2),
                mainTextureRegion.Width,
                mainTextureRegion.Height
                );

            SpriteBatch spriteBatch = m_spriteBatchProvider.WorldSpriteBatch;
            spriteBatch.Draw(mainTextureRegion, drawingRectangle, Color.White);

            if (!textureAtlasComponent.HasPlayerColor)
                return;

            Player entityOwner = entity switch
            {
                Unit unit => unit.Owner,
                Building building => building.Owner,
                _ => null,
            };

            if (entityOwner == null)
                return;

            PlayerColor playerColor = entityOwner.Color;
            foreach ((string externalTextureAssetNameSuffix, PlayerColorProviderMethod playerColorProviderMethod) in s_playerColorShades)
            {
                string externalTextureAssetName = $"{textureAtlas.Texture.Name}-{externalTextureAssetNameSuffix}";
                if (!m_externalTexturesByAssetNames.TryGetValue(externalTextureAssetName, out Texture2D externalTexture))
                {
                    externalTexture = contentManager.Load<Texture2D>(externalTextureAssetName);
                    m_externalTexturesByAssetNames.Add(externalTextureAssetName, externalTexture);
                }

                Color maskColor = playerColorProviderMethod(playerColor);
                spriteBatch.DrawTextureRegionUsingExternalTexture(mainTextureRegion, externalTexture, drawingRectangle, maskColor);
            }
        }

        private void ClearAnimationCache()
        {
            List<Entity> entitiesToRemove = new List<Entity>();
            HashSet<Entity> existingEntities = m_gameSessionContext.Units.Concat<Entity>(m_gameSessionContext.Buildings).ToHashSet();
            foreach (Entity entity in m_animationCache.Keys)
            {
                if (!existingEntities.Contains(entity))
                    entitiesToRemove.Add(entity);
            }
            foreach (Entity entity in entitiesToRemove)
                m_animationCache.Remove(entity);
        }

        private delegate Color PlayerColorProviderMethod(PlayerColor playerColor);
    }
}
