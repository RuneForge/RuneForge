using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using RuneForge.Core.Controllers;
using RuneForge.Core.Controllers.Interfaces;
using RuneForge.Core.Input;
using RuneForge.Core.Input.EventProviders.Interfaces;
using RuneForge.Core.Rendering;
using RuneForge.Core.Rendering.Interfaces;
using RuneForge.Game.Entities;
using RuneForge.Game.GameSessions;
using RuneForge.Game.GameSessions.Interfaces;
using RuneForge.Game.Maps;
using RuneForge.Game.Orders.Implementations;
using RuneForge.Game.Systems.Interfaces;
using RuneForge.Game.Units;

namespace RuneForge.Core.GameStates.Implementations
{
    public class GameplayGameState : GameState
    {
        private static readonly string s_defaultMapAssetName = Path.Combine("Maps", "Southshore");

        private readonly IGameSessionContext m_gameSessionContext;
        private readonly IKeyboardEventProvider m_keyboardEventProvider;
        private readonly IMouseEventProvider m_mouseEventProvider;
        private readonly ISpriteBatchProvider m_spriteBatchProvider;
        private readonly IEntitySelectionContext m_entitySelectionContext;
        private readonly IEntitySelector m_entitySelector;
        private readonly IOrderTypeResolver m_orderTypeResolver;
        private readonly IEnumerable<ISystem> m_systems;
        private readonly IEnumerable<IRenderer> m_renderers;
        private readonly Camera2D m_camera;
        private readonly Camera2DParameters m_cameraParameters;
        private readonly CameraController m_cameraController;
        private readonly UnitController m_unitController;
        private readonly Lazy<GraphicsDevice> m_graphicsDeviceProvider;

        public GameplayGameState(
            IGameSessionContext gameSessionContext,
            IKeyboardEventProvider keyboardEventProvider,
            IMouseEventProvider mouseEventProvider,
            ISpriteBatchProvider spriteBatchProvider,
            IEntitySelectionContext entitySelectionContext,
            IEntitySelector entitySelector,
            IOrderTypeResolver orderTypeResolver,
            IEnumerable<ISystem> systems,
            IEnumerable<IRenderer> renderers,
            Camera2D camera,
            Camera2DParameters cameraParameters,
            CameraController cameraController,
            UnitController unitController,
            Lazy<GraphicsDevice> graphicsDeviceProvider
            )
        {
            m_gameSessionContext = gameSessionContext;
            m_keyboardEventProvider = keyboardEventProvider;
            m_mouseEventProvider = mouseEventProvider;
            m_spriteBatchProvider = spriteBatchProvider;
            m_entitySelectionContext = entitySelectionContext;
            m_entitySelector = entitySelector;
            m_orderTypeResolver = orderTypeResolver;
            m_systems = systems;
            m_renderers = renderers;
            m_camera = camera;
            m_cameraParameters = cameraParameters;
            m_cameraController = cameraController;
            m_unitController = unitController;
            m_graphicsDeviceProvider = graphicsDeviceProvider;
        }

        public override void Run()
        {
            SubscribeToKeyboardEvents();
            SubscribeToMouseEvents();
            base.Run();
        }
        public override void Stop()
        {
            UnsubscribeFromKeyboardEvents();
            UnsubscribeFromMouseEvents();
            base.Stop();
        }

        public override void Update(GameTime gameTime)
        {
            foreach (ISystem system in m_systems.Where(system => system.Enabled))
                system.Update(gameTime);

            base.Update(gameTime);
        }
        public override void Draw(GameTime gameTime)
        {
            SpriteBatch worldSpriteBatch = m_spriteBatchProvider.WorldSpriteBatch;
            SpriteBatch onDisplayInterfaceSpriteBatch = m_spriteBatchProvider.OnDisplayInterfaceSpriteBatch;

            worldSpriteBatch.Begin(samplerState: SamplerState.PointClamp, transformMatrix: m_camera.GetWorldToScreenTransformationMatrix());
            onDisplayInterfaceSpriteBatch.Begin(transformMatrix: m_camera.GetWorldToScreenTransformationMatrix());

            foreach (IRenderer renderer in m_renderers.Where(renderer => renderer.Visible))
                renderer.Draw(gameTime);

            worldSpriteBatch.End();
            onDisplayInterfaceSpriteBatch.End();

            base.Draw(gameTime);
        }

        public override void LoadContent()
        {
            GameSessionParameters parameters = new GameSessionParameters() { MapAssetName = s_defaultMapAssetName };
            m_gameSessionContext.Initialize(parameters);

            GraphicsDevice graphicsDevice = m_graphicsDeviceProvider.Value;

            SpriteBatch worldSpriteBatch = new SpriteBatch(graphicsDevice);
            SpriteBatch onDisplayInterfaceSpriteBatch = new SpriteBatch(graphicsDevice);
            m_spriteBatchProvider.WorldSpriteBatch = worldSpriteBatch;
            m_spriteBatchProvider.OnDisplayInterfaceSpriteBatch = onDisplayInterfaceSpriteBatch;

            m_cameraParameters.Viewport = graphicsDevice.Viewport;

            foreach (IRenderer renderer in m_renderers)
                renderer.LoadContent();

            base.LoadContent();
        }

        private void SubscribeToKeyboardEvents()
        {
            m_keyboardEventProvider.KeyDown += HandleCameraMovement;
            m_keyboardEventProvider.KeyPressed += HandleCameraScaling;
            m_keyboardEventProvider.KeyPressed += HandleEntitySelectionDrop;
        }
        private void UnsubscribeFromKeyboardEvents()
        {
            m_keyboardEventProvider.KeyDown -= HandleCameraMovement;
            m_keyboardEventProvider.KeyPressed -= HandleCameraScaling;
            m_keyboardEventProvider.KeyPressed -= HandleEntitySelectionDrop;
        }
        private void SubscribeToMouseEvents()
        {
            m_mouseEventProvider.MouseButtonClicked += HandleEntitySelection;
            m_mouseEventProvider.MouseButtonClicked += HandleOrderOnRightClick;
        }
        private void UnsubscribeFromMouseEvents()
        {
            m_mouseEventProvider.MouseButtonClicked -= HandleEntitySelection;
            m_mouseEventProvider.MouseButtonClicked -= HandleOrderOnRightClick;
        }

        private void HandleCameraMovement(object sender, KeyboardEventArgs e)
        {
            if (!e.Handled && (e.Key == Key.Up || e.Key == Key.Down || e.Key == Key.Left || e.Key == Key.Right))
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
            if (!e.Handled && (e.Key == Key.Add || e.Key == Key.Subtract))
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
        private void HandleEntitySelection(object sender, MouseEventArgs e)
        {
            if (!e.Handled && e.Button == MouseButtons.LeftButton)
            {
                e.Handle();
                if (m_entitySelector.TrySelectEntity(e.X, e.Y, out Entity entity))
                    m_entitySelectionContext.Entity = entity;
            }
        }
        private void HandleEntitySelectionDrop(object sender, KeyboardEventArgs e)
        {
            if (!e.Handled && e.Key == Key.Escape)
            {
                e.Handle();
                m_entitySelectionContext.Entity = null;
            }
        }
        private void HandleOrderOnRightClick(object sender, MouseEventArgs e)
        {
            Entity entity = m_entitySelectionContext.Entity;
            if (!e.Handled && entity != null && e.Button == MouseButtons.RightButton)
            {
                e.Handle();
                if (m_orderTypeResolver.TryResolveOrderType(entity, e.X, e.Y, out Type orderType))
                {
                    bool shiftPressed = (m_keyboardEventProvider.GetState().GetModifierKeys() & ModifierKeys.Shift) == ModifierKeys.Shift;
                    (int targetCellX, int targetCellY) = GetCellByScreenPoint(e.Location);
                    if (entity is Unit unit && orderType == typeof(MoveOrder))
                        m_unitController.Move(unit, targetCellX, targetCellY, shiftPressed);
                }
            }
        }

        private Point GetCellByScreenPoint(Point screenPoint)
        {
            Point worldPoint = m_camera.TranslateScreenToWorld(screenPoint.ToVector2()).ToPoint();
            return new Point(worldPoint.X / Map.CellWidth, worldPoint.Y / Map.CellHeight);
        }
    }
}
