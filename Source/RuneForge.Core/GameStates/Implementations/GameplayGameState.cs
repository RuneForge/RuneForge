using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using RuneForge.Core.Controllers;
using RuneForge.Core.Controllers.Interfaces;
using RuneForge.Core.GameStates.Interfaces;
using RuneForge.Core.Input;
using RuneForge.Core.Input.EventProviders.Interfaces;
using RuneForge.Core.Interface.Controls;
using RuneForge.Core.Interface.Interfaces;
using RuneForge.Core.Interface.Windows;
using RuneForge.Core.Rendering;
using RuneForge.Core.Rendering.Interfaces;
using RuneForge.Game.Buildings;
using RuneForge.Game.Components.Implementations;
using RuneForge.Game.Entities;
using RuneForge.Game.GameSessions;
using RuneForge.Game.GameSessions.Interfaces;
using RuneForge.Game.Maps;
using RuneForge.Game.Orders.Implementations;
using RuneForge.Game.Players.Interfaces;
using RuneForge.Game.Systems.Interfaces;
using RuneForge.Game.Units;

namespace RuneForge.Core.GameStates.Implementations
{
    public class GameplayGameState : GameState
    {
        private const int c_interfaceWindowsOffset = 4;

        private static readonly string s_defaultMapAssetName = Path.Combine("Maps", "Southshore");

        private readonly IGameSessionContext m_gameSessionContext;
        private readonly IGraphicsInterfaceService m_graphicsInterfaceService;
        private readonly IGameStateService m_gameStateService;
        private readonly IKeyboardEventProvider m_keyboardEventProvider;
        private readonly IMouseEventProvider m_mouseEventProvider;
        private readonly ISpriteBatchProvider m_spriteBatchProvider;
        private readonly ISpriteFontProvider m_spriteFontProvider;
        private readonly IPlayerService m_playerService;
        private readonly IEntitySelectionContext m_entitySelectionContext;
        private readonly IEntitySelector m_entitySelector;
        private readonly IOrderTypeResolver m_orderTypeResolver;
        private readonly IEnumerable<ISystem> m_systems;
        private readonly IEnumerable<IRenderer> m_renderers;
        private readonly Camera2D m_camera;
        private readonly Camera2DParameters m_cameraParameters;
        private readonly CameraController m_cameraController;
        private readonly UnitController m_unitController;
        private readonly Lazy<ContentManager> m_contentManagerProvider;
        private readonly Lazy<GraphicsDevice> m_graphicsDeviceProvider;
        private SpriteBatch m_interfaceSpriteBatch;
        private EntityDetailsWindow m_entityDetailsWindow;
        private OrderConfirmationDialogWindow m_orderConfirmationDialogWindow;
        private TargetBasedOrderScheduledEventArgs m_activeTargetBasedOrderEventArgs;
        private PlayerResourceStatisticsWindow m_playerResourceStatisticsWindow;

        public GameplayGameState(
            IGameSessionContext gameSessionContext,
            IGraphicsInterfaceService graphicsInterfaceService,
            IGameStateService gameStateService,
            IKeyboardEventProvider keyboardEventProvider,
            IMouseEventProvider mouseEventProvider,
            ISpriteBatchProvider spriteBatchProvider,
            ISpriteFontProvider spriteFontProvider,
            IPlayerService playerService,
            IEntitySelectionContext entitySelectionContext,
            IEntitySelector entitySelector,
            IOrderTypeResolver orderTypeResolver,
            IEnumerable<ISystem> systems,
            IEnumerable<IRenderer> renderers,
            Camera2D camera,
            Camera2DParameters cameraParameters,
            CameraController cameraController,
            UnitController unitController,
            Lazy<ContentManager> contentManagerProvider,
            Lazy<GraphicsDevice> graphicsDeviceProvider
            )
        {
            m_gameSessionContext = gameSessionContext;
            m_graphicsInterfaceService = graphicsInterfaceService;
            m_gameStateService = gameStateService;
            m_keyboardEventProvider = keyboardEventProvider;
            m_mouseEventProvider = mouseEventProvider;
            m_spriteBatchProvider = spriteBatchProvider;
            m_spriteFontProvider = spriteFontProvider;
            m_playerService = playerService;
            m_entitySelectionContext = entitySelectionContext;
            m_entitySelector = entitySelector;
            m_orderTypeResolver = orderTypeResolver;
            m_systems = systems;
            m_renderers = renderers;
            m_camera = camera;
            m_cameraParameters = cameraParameters;
            m_cameraController = cameraController;
            m_unitController = unitController;
            m_contentManagerProvider = contentManagerProvider;
            m_graphicsDeviceProvider = graphicsDeviceProvider;
            m_interfaceSpriteBatch = null;
            m_entityDetailsWindow = null;
            m_orderConfirmationDialogWindow = null;
            m_activeTargetBasedOrderEventArgs = null;
            m_playerResourceStatisticsWindow = null;
        }

        public override void Run()
        {
            GraphicsDevice graphicsDevice = m_graphicsDeviceProvider.Value;
            m_graphicsInterfaceService.Viewport = new Viewport(0, 0, graphicsDevice.PresentationParameters.BackBufferWidth, graphicsDevice.PresentationParameters.BackBufferHeight);
            m_graphicsInterfaceService.RegisterControl(m_entityDetailsWindow);
            m_graphicsInterfaceService.RegisterControl(m_orderConfirmationDialogWindow);
            m_graphicsInterfaceService.RegisterControl(m_playerResourceStatisticsWindow);
            SubscribeToKeyboardEvents();
            SubscribeToMouseEvents();
            base.Run();
        }
        public override void Stop()
        {
            m_graphicsInterfaceService.UnregisterControl(m_entityDetailsWindow);
            m_graphicsInterfaceService.UnregisterControl(m_orderConfirmationDialogWindow);
            m_graphicsInterfaceService.UnregisterControl(m_playerResourceStatisticsWindow);
            UnsubscribeFromKeyboardEvents();
            UnsubscribeFromMouseEvents();
            base.Stop();
        }

        public override void Update(GameTime gameTime)
        {
            foreach (ISystem system in m_systems.Where(system => system.Enabled))
                system.Update(gameTime);

            m_entityDetailsWindow.UpdateEntityDetails();
            m_playerResourceStatisticsWindow.UpdateStatistics();

            if (m_gameSessionContext.Completed)
                CompleteState();

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

            m_interfaceSpriteBatch = new SpriteBatch(graphicsDevice);
            SpriteBatch worldSpriteBatch = new SpriteBatch(graphicsDevice);
            SpriteBatch onDisplayInterfaceSpriteBatch = new SpriteBatch(graphicsDevice);
            m_spriteBatchProvider.WorldSpriteBatch = worldSpriteBatch;
            m_spriteBatchProvider.OnDisplayInterfaceSpriteBatch = onDisplayInterfaceSpriteBatch;

            m_cameraParameters.Viewport = graphicsDevice.Viewport;

            foreach (IRenderer renderer in m_renderers)
                renderer.LoadContent();

            base.LoadContent();

            CreateInterfaceWindows();
            m_entityDetailsWindow.LoadContent();
            m_orderConfirmationDialogWindow.LoadContent();
            m_playerResourceStatisticsWindow.LoadContent();
        }

        private void CompleteState()
        {
            m_gameStateService.RunGameState<MainMenuGameState>();
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
            m_mouseEventProvider.MouseButtonClicked += HandleOrderConfirmation;
            m_mouseEventProvider.MouseButtonClicked += HandleOrderOnRightClick;
            m_mouseEventProvider.MouseButtonClicked += HandleEntitySelection;
        }
        private void UnsubscribeFromMouseEvents()
        {
            m_mouseEventProvider.MouseButtonClicked -= HandleOrderConfirmation;
            m_mouseEventProvider.MouseButtonClicked -= HandleOrderOnRightClick;
            m_mouseEventProvider.MouseButtonClicked -= HandleEntitySelection;
        }

        private void CreateInterfaceWindows()
        {
            m_entityDetailsWindow = new EntityDetailsWindow(
                new ControlEventSource(),
                m_contentManagerProvider.Value,
                m_graphicsDeviceProvider.Value,
                m_interfaceSpriteBatch,
                m_gameSessionContext,
                m_spriteFontProvider
                )
            {
                X = c_interfaceWindowsOffset,
                Y = c_interfaceWindowsOffset,
                Visible = false,
            };
            m_entityDetailsWindow.InstantOrderScheduled += (sender, e) => HandleInstantOrder(sender, e);
            m_entityDetailsWindow.TargetBasedOrderScheduled += (sender, e) => HandleTargetBasedOrder(sender, e);
            m_entityDetailsWindow.OrderQueueClearingScheduled += (sender, e) => HandleOrderQueueClearing(sender, e);

            m_orderConfirmationDialogWindow = new OrderConfirmationDialogWindow(
                new ControlEventSource(),
                m_contentManagerProvider.Value,
                m_graphicsDeviceProvider.Value,
                m_interfaceSpriteBatch,
                m_spriteFontProvider
                )
            {
                X = m_entityDetailsWindow.X + m_entityDetailsWindow.Width + c_interfaceWindowsOffset,
                Y = c_interfaceWindowsOffset,
                Visible = false,
            };
            m_orderConfirmationDialogWindow.OrderCancelled += (sender, e) => HandleOrderCancellation(sender, e);

            m_playerResourceStatisticsWindow = new PlayerResourceStatisticsWindow(
                new ControlEventSource(),
                m_contentManagerProvider.Value,
                m_graphicsDeviceProvider.Value,
                m_interfaceSpriteBatch,
                m_spriteFontProvider
                )
            {
                Player = m_playerService.GetPlayer(m_gameSessionContext.Map.HumanPlayerId),
            };
            m_playerResourceStatisticsWindow.X = m_graphicsInterfaceService.Viewport.Width - (m_playerResourceStatisticsWindow.Width + c_interfaceWindowsOffset);
            m_playerResourceStatisticsWindow.Y = c_interfaceWindowsOffset;

            m_entitySelectionContext.EntitySelected += (sender, e) =>
            {
                m_entityDetailsWindow.Entity = m_entitySelectionContext.Entity;
                m_entityDetailsWindow.Visible = true;
            };
            m_entitySelectionContext.EntitySelectionDropped += (sender, e) =>
            {
                m_entityDetailsWindow.Visible = false;
                if (m_activeTargetBasedOrderEventArgs != null)
                {
                    m_activeTargetBasedOrderEventArgs.Cancel();
                    m_activeTargetBasedOrderEventArgs = null;
                }    
            };
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
                if (m_entitySelector.TrySelectEntity(e.X, e.Y, out Entity entity))
                {
                    e.Handle();
                    m_entitySelectionContext.Entity = entity;
                }
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
        private void HandleInstantOrder(object sender, InstantOrderScheduledEventArgs e)
        {
            ExecuteOrder(m_entityDetailsWindow.Entity, 0, 0, e.OrderType);
            if (m_activeTargetBasedOrderEventArgs != null)
            {
                m_activeTargetBasedOrderEventArgs.Cancel();
                m_activeTargetBasedOrderEventArgs = null;
            }
        }
        private void HandleTargetBasedOrder(object sender, TargetBasedOrderScheduledEventArgs e)
        {
            m_orderConfirmationDialogWindow.Visible = true;
            m_activeTargetBasedOrderEventArgs = e;
            m_activeTargetBasedOrderEventArgs.Completed += (sender, e) => m_orderConfirmationDialogWindow.Visible = false;
            m_activeTargetBasedOrderEventArgs.Cancelled += (sender, e) => m_orderConfirmationDialogWindow.Visible = false;
        }
        private void HandleOrderQueueClearing(object sender, EventArgs e)
        {
            if (m_activeTargetBasedOrderEventArgs != null)
            {
                m_activeTargetBasedOrderEventArgs.Cancel();
                m_activeTargetBasedOrderEventArgs = null;
            }
            switch (m_entityDetailsWindow.Entity)
            {
                case Unit unit:
                    m_unitController.ClearOrderQueue(unit);
                    break;
                case Building _:
                    throw new NotImplementedException();
            }
        }
        private void HandleOrderConfirmation(object sender, MouseEventArgs e)
        {
            if (!e.Handled && e.Button == MouseButtons.LeftButton && m_activeTargetBasedOrderEventArgs != null)
            {
                e.Handle();
                (int worldX, int worldY) = GetWorldPointByScreenPoint(e.Location, out _);
                ExecuteOrder(m_entityDetailsWindow.Entity, worldX, worldY, m_activeTargetBasedOrderEventArgs.OrderType);
                m_activeTargetBasedOrderEventArgs.Complete();
                m_activeTargetBasedOrderEventArgs = null;
            }
        }
        private void HandleOrderCancellation(object sender, EventArgs e)
        {
            if (m_activeTargetBasedOrderEventArgs != null)
            {
                m_activeTargetBasedOrderEventArgs.Cancel();
                m_activeTargetBasedOrderEventArgs = null;
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
                    (int worldX, int worldY) = GetWorldPointByScreenPoint(e.Location, out _);
                    ExecuteOrder(entity, worldX, worldY, orderType);
                }
                if (m_activeTargetBasedOrderEventArgs != null)
                {
                    m_activeTargetBasedOrderEventArgs.Cancel();
                    m_activeTargetBasedOrderEventArgs = null;
                }
            }
        }

        private void ExecuteOrder(Entity entity, int worldX, int worldY, Type orderType)
        {
            bool shiftPressed = (m_keyboardEventProvider.GetState().GetModifierKeys() & ModifierKeys.Shift) == ModifierKeys.Shift;
            if (entity is Unit unit)
                ExecuteUnitOrder(unit, worldX, worldY, orderType, shiftPressed);
            else if (m_entityDetailsWindow.Entity is Building)
                throw new NotImplementedException();
        }
        private void ExecuteUnitOrder(Unit unit, int worldX, int worldY, Type orderType, bool addToQueue)
        {
            if (orderType == typeof(MoveOrder))
            {
                Point destinationCell = new Point(worldX / Map.CellWidth, worldY / Map.CellHeight);
                m_unitController.Move(unit, destinationCell.X, destinationCell.Y, addToQueue);
            }
            if (orderType == typeof(AttackOrder))
            {
                Entity targetEntity = m_gameSessionContext.Units.Concat<Entity>(m_gameSessionContext.Buildings)
                    .Where(entity => entity.TryGetComponentOfType(out LocationComponent locationComponent)
                        && new Rectangle((int)locationComponent.X, (int)locationComponent.Y, locationComponent.Width, locationComponent.Height).Contains(new Point(worldX, worldY)))
                    .FirstOrDefault();
                if (targetEntity != null)
                    m_unitController.Attack(unit, targetEntity, addToQueue);
            }
            if (orderType == typeof(GatherResourcesOrder))
            {
                Building resourceSourceBuilding = m_gameSessionContext.Buildings
                    .Where(entity => entity is Building
                        && entity.TryGetComponentOfType(out LocationComponent locationComponent)
                        && new Rectangle((int)locationComponent.X, (int)locationComponent.Y, locationComponent.Width, locationComponent.Height).Contains(new Point(worldX, worldY))
                        && (!entity.TryGetComponentOfType(out UnitShelterOccupantComponent shelterOccupantComponent)
                        || !shelterOccupantComponent.InsideShelter)
                        && entity.TryGetComponentOfType(out ResourceSourceComponent resourceSourceComponent)
                        && entity.TryGetComponentOfType(out ResourceContainerComponent resourceContainerComponent)
                        && resourceContainerComponent.GetResourceAmount(resourceSourceComponent.ResourceType) > 0)
                    .FirstOrDefault();
                if (resourceSourceBuilding != null)
                    m_unitController.GatherResources(unit, resourceSourceBuilding, addToQueue);
            }
        }

        private Point GetWorldPointByScreenPoint(Point screenPoint, out Point cell)
        {
            Point worldPoint = m_camera.TranslateScreenToWorld(screenPoint.ToVector2()).ToPoint();
            cell = new Point(worldPoint.X / Map.CellWidth, worldPoint.Y / Map.CellHeight);
            return worldPoint;
        }
    }
}
