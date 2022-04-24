using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using RuneForge.Core.Controllers.Interfaces;
using RuneForge.Core.Rendering.Interfaces;
using RuneForge.Game.Buildings;
using RuneForge.Game.Components.Implementations;
using RuneForge.Game.Entities;
using RuneForge.Game.GameSessions.Interfaces;
using RuneForge.Game.Players;
using RuneForge.Game.Units;

namespace RuneForge.Core.Rendering
{
    public class OnDisplayInterfaceRenderer : Renderer
    {
        private readonly IGameSessionContext m_gameSessionContext;
        private readonly IEntitySelectionContext m_entitySelectionContext;
        private readonly ISpriteBatchProvider m_spriteBatchProvider;
        private readonly Lazy<GraphicsDevice> m_graphicsDeviceProvider;
        private readonly Dictionary<(int, int, Color), Texture2D> m_selectionFrames;
        private SpriteBatch m_spriteBatch;

        public OnDisplayInterfaceRenderer(IGameSessionContext gameSessionContext, IEntitySelectionContext entitySelectionContext, ISpriteBatchProvider spriteBatchProvider, Lazy<GraphicsDevice> graphicsDeviceProvider)
        {
            m_gameSessionContext = gameSessionContext;
            m_entitySelectionContext = entitySelectionContext;
            m_spriteBatchProvider = spriteBatchProvider;
            m_graphicsDeviceProvider = graphicsDeviceProvider;
            m_selectionFrames = new Dictionary<(int, int, Color), Texture2D>();
        }

        public override void Draw(GameTime gameTime)
        {
            Entity selectedEntity = m_entitySelectionContext.Entity;
            if (selectedEntity != null && selectedEntity.TryGetComponentOfType(out LocationComponent locationComponent))
            {
                Player entityOwner = selectedEntity switch
                {
                    Unit unit => unit.Owner,
                    Building building => building.Owner,
                    _ => null,
                };

                Color selectionFrameColor = Color.Gray;
                if (entityOwner != null)
                {
                    if (entityOwner.Id == m_gameSessionContext.Map.HumanPlayerId)
                        selectionFrameColor = Color.LimeGreen;
                    else if (entityOwner.Id == m_gameSessionContext.Map.NeutralPassivePlayerId)
                        selectionFrameColor = Color.Yellow;
                    else
                        selectionFrameColor = Color.Red;
                }

                Rectangle rectangle = new Rectangle((int)locationComponent.X, (int)locationComponent.Y, locationComponent.Width, locationComponent.Height);
                SpriteBatch spriteBatch = m_spriteBatchProvider.OnDisplayInterfaceSpriteBatch;
                Texture2D texture = GetSelectionFrameTexture(rectangle.Width, rectangle.Height, selectionFrameColor);
                spriteBatch.Draw(texture, rectangle, Color.White);
            }
        }

        public override void LoadContent()
        {
            GraphicsDevice graphicsDevice = m_graphicsDeviceProvider.Value;
            m_spriteBatch = new SpriteBatch(graphicsDevice);
        }

        private Texture2D GetSelectionFrameTexture(int width, int height, Color selectionFrameColor)
        {
            GraphicsDevice graphicsDevice = m_graphicsDeviceProvider.Value;
            RenderTarget2D CreateRenderTarget2D(int width, int height)
            {
                PresentationParameters presentationParameters = graphicsDevice.PresentationParameters;
                return new RenderTarget2D(graphicsDevice, width, height, false, presentationParameters.BackBufferFormat, presentationParameters.DepthStencilFormat, 0, RenderTargetUsage.PreserveContents);
            }

            if (!m_selectionFrames.TryGetValue((width, height, selectionFrameColor), out Texture2D texture))
            {
                RenderTarget2D renderTarget = CreateRenderTarget2D(width, height);

                using Texture2D transparentTexture = new Texture2D(graphicsDevice, 1, 1);
                using Texture2D frameBorderTexture = new Texture2D(graphicsDevice, 1, 1);
                transparentTexture.SetData(new Color[1] { Color.Transparent });
                frameBorderTexture.SetData(new Color[1] { selectionFrameColor });

                RenderTargetBinding[] currentRenderTargets = graphicsDevice.GetRenderTargets();
                graphicsDevice.SetRenderTarget(renderTarget);

                m_spriteBatch.Begin();
                m_spriteBatch.Draw(transparentTexture, new Rectangle(0, 0, width, height), Color.White);
                m_spriteBatch.Draw(frameBorderTexture, new Rectangle(0, 0, width, 1), Color.White);
                m_spriteBatch.Draw(frameBorderTexture, new Rectangle(0, height - 1, width, 1), Color.White);
                m_spriteBatch.Draw(frameBorderTexture, new Rectangle(0, 0, 1, height), Color.White);
                m_spriteBatch.Draw(frameBorderTexture, new Rectangle(width - 1, 0, 1, height), Color.White);
                m_spriteBatch.End();

                graphicsDevice.SetRenderTargets(currentRenderTargets);
                m_selectionFrames.Add((width, height, selectionFrameColor), renderTarget);
                texture = renderTarget;
            }
            return texture;
        }
    }
}
