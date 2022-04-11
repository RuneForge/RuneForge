using System;
using System.IO;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using RuneForge.Core.Controllers;
using RuneForge.Core.Input;
using RuneForge.Core.Input.EventProviders.Interfaces;
using RuneForge.Core.Rendering;
using RuneForge.Core.Rendering.Interfaces;
using RuneForge.Game.GameSessions;
using RuneForge.Game.GameSessions.Interfaces;

namespace RuneForge.Core.GameStates.Implementations
{
    public class GameplayGameState : GameState
    {
        private static readonly string s_defaultMapAssetName = Path.Combine("Maps", "Southshore");

        private readonly IGameSessionContext m_gameSessionContext;
        private readonly ISpriteBatchProvider m_spriteBatchProvider;
        private readonly IKeyboardEventProvider m_keyboardEventProvider;
        private readonly Camera2D m_camera;
        private readonly Camera2DParameters m_cameraParameters;
        private readonly MapRenderer m_mapRenderer;
        private readonly CameraController m_cameraController;
        private readonly Lazy<GraphicsDevice> m_graphicsDeviceProvider;

        public GameplayGameState(
            IGameSessionContext gameSessionContext,
            ISpriteBatchProvider spriteBatchProvider,
            IKeyboardEventProvider keyboardEventProvider,
            Camera2D camera,
            Camera2DParameters cameraParameters,
            MapRenderer mapRenderer,
            CameraController cameraController,
            Lazy<GraphicsDevice> graphicsDeviceProvider
            )
        {
            m_gameSessionContext = gameSessionContext;
            m_spriteBatchProvider = spriteBatchProvider;
            m_keyboardEventProvider = keyboardEventProvider;
            m_camera = camera;
            m_cameraParameters = cameraParameters;
            m_mapRenderer = mapRenderer;
            m_cameraController = cameraController;
            m_graphicsDeviceProvider = graphicsDeviceProvider;
        }

        public override void Run()
        {
            SubscribeToKeyboardEvents();
            base.Run();
        }
        public override void Stop()
        {
            UnsubscribeFromKeyboardEvents();
            base.Stop();
        }

        public override void LoadContent()
        {
            GameSessionParameters parameters = new GameSessionParameters() { MapAssetName = s_defaultMapAssetName };
            m_gameSessionContext.Initialize(parameters);

            GraphicsDevice graphicsDevice = m_graphicsDeviceProvider.Value;

            SpriteBatch worldSpriteBatch = new SpriteBatch(graphicsDevice);
            m_spriteBatchProvider.WorldSpriteBatch = worldSpriteBatch;

            m_cameraParameters.Viewport = graphicsDevice.Viewport;

            m_mapRenderer.LoadContent();

            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
        public override void Draw(GameTime gameTime)
        {
            SpriteBatch worldSpriteBatch = m_spriteBatchProvider.WorldSpriteBatch;
            worldSpriteBatch.Begin(samplerState: SamplerState.PointClamp, transformMatrix: m_camera.GetWorldToScreenTransformationMatrix());
            m_mapRenderer.Draw();
            worldSpriteBatch.End();

            base.Draw(gameTime);
        }

        private void SubscribeToKeyboardEvents()
        {
            m_keyboardEventProvider.KeyDown += HandleCameraMovement;
            m_keyboardEventProvider.KeyPressed += HandleCameraScaling;
        }
        private void UnsubscribeFromKeyboardEvents()
        {
            m_keyboardEventProvider.KeyDown -= HandleCameraMovement;
            m_keyboardEventProvider.KeyPressed -= HandleCameraScaling;
        }

        private void HandleCameraMovement(object sender, KeyboardEventArgs e)
        {
            if (e.Key == Key.Up || e.Key == Key.Down || e.Key == Key.Left || e.Key == Key.Right)
            {
                e.Handle();
                switch (e.Key)
                {
                    case Key.Up:
                        m_cameraController.MoveCamera(new Point(0, -8));
                        break;
                    case Key.Down:
                        m_cameraController.MoveCamera(new Point(0, +8));
                        break;
                    case Key.Left:
                        m_cameraController.MoveCamera(new Point(-8, 0));
                        break;
                    case Key.Right:
                        m_cameraController.MoveCamera(new Point(+8, 0));
                        break;
                }
            }
        }
        private void HandleCameraScaling(object sender, KeyboardEventArgs e)
        {
            if (e.Key == Key.Add || e.Key == Key.Subtract)
            {
                e.Handle();
                switch (e.Key)
                {
                    case Key.Add:
                        m_cameraController.SetCameraScale(m_camera.Scale * 2.0f);
                        break;
                    case Key.Subtract:
                        m_cameraController.SetCameraScale(m_camera.Scale * 0.5f);
                        break;
                }
            }
        }
    }
}
