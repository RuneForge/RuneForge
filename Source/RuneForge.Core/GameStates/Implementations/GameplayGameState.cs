using System;
using System.IO;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using RuneForge.Core.Input;
using RuneForge.Core.Input.EventProviders.Interfaces;
using RuneForge.Core.Rendering;
using RuneForge.Game.Maps;
using RuneForge.Game.Maps.Interfaces;

namespace RuneForge.Core.GameStates.Implementations
{
    public class GameplayGameState : GameState
    {
        private static readonly string s_defaultMapAssetName = Path.Combine("Maps", "Southshore");

        private readonly IServiceProvider m_serviceProvider;
        private readonly Lazy<ContentManager> m_contentManagerProvider;
        private readonly Lazy<GraphicsDevice> m_graphicsDeviceProvider;
        private readonly IKeyboardEventProvider m_keyboardEventProvider;
        private Map m_map;
        private SpriteBatch m_spriteBatch;
        private Camera2D m_camera;
        private MapRenderer m_mapRenderer;

        public GameplayGameState(
            IServiceProvider serviceProvider,
            Lazy<ContentManager> contentManagerProvider,
            Lazy<GraphicsDevice> graphicsDeviceProvider,
            IKeyboardEventProvider keyboardEventProvider
            )
        {
            m_serviceProvider = serviceProvider;
            m_contentManagerProvider = contentManagerProvider;
            m_graphicsDeviceProvider = graphicsDeviceProvider;
            m_keyboardEventProvider = keyboardEventProvider;
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
            ContentManager contentManager = m_contentManagerProvider.Value;
            GraphicsDevice graphicsDevice = m_graphicsDeviceProvider.Value;

            m_map = contentManager.Load<Map>(s_defaultMapAssetName);

            IMapCellTypeResolver mapCellTypeResolver = m_serviceProvider.GetRequiredService<IMapCellTypeResolver>();
            m_map.ResolveMapCellTypes(mapCellTypeResolver);

            m_spriteBatch = new SpriteBatch(graphicsDevice);
            m_camera = new Camera2D(new Point(graphicsDevice.Viewport.Width, graphicsDevice.Viewport.Height));
            m_mapRenderer = new MapRenderer(m_map, m_camera, m_spriteBatch, contentManager, graphicsDevice.Viewport);

            m_mapRenderer.LoadContent();

            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
        public override void Draw(GameTime gameTime)
        {
            m_spriteBatch.Begin(samplerState: SamplerState.PointClamp, transformMatrix: m_camera.GetWorldToScreenTransformationMatrix());
            m_mapRenderer.Draw();
            m_spriteBatch.End();

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
                        m_camera.Location += new Point(0, -8);
                        break;
                    case Key.Down:
                        m_camera.Location += new Point(0, +8);
                        break;
                    case Key.Left:
                        m_camera.Location += new Point(-8, 0);
                        break;
                    case Key.Right:
                        m_camera.Location += new Point(+8, 0);
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
                        m_camera.Scale *= 2.0f;
                        break;
                    case Key.Subtract:
                        m_camera.Scale *= 0.5f;
                        break;
                }
            }
        }
    }
}
