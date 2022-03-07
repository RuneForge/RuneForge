using System;
using System.Collections.ObjectModel;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using RuneForge.Core.Interface.Controls;
using RuneForge.Core.Interface.Controls.Helpers;
using RuneForge.Core.Interface.Interfaces;

namespace RuneForge.Core.Interface.Components
{
    public class GraphicsInterfaceComponent : DrawableGameComponent
    {
        private readonly Lazy<GraphicsDevice> m_graphicsDeviceProvider;

        private readonly IGraphicsInterfaceService m_graphicsInterfaceService;

        public GraphicsInterfaceComponent(Lazy<GraphicsDevice> graphicsDeviceProvider, IGraphicsInterfaceService graphicsInterfaceService)
        {
            m_graphicsDeviceProvider = graphicsDeviceProvider;
            m_graphicsInterfaceService = graphicsInterfaceService;
        }

        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice value = m_graphicsDeviceProvider.Value;
            Viewport viewport = value.Viewport;
            Viewport viewport3 = value.Viewport = m_graphicsInterfaceService.Viewport;
            base.Draw(gameTime);
            ReadOnlyCollection<Control> controlsByDrawOrder = m_graphicsInterfaceService.GetControlsByDrawOrder();
            foreach (Control item in controlsByDrawOrder)
            {
                value.Viewport = GraphicsControlGeometryHelpers.CreateChildViewport(in viewport3, item.Bounds);
                item.Draw(gameTime);
            }
            value.Viewport = viewport;
        }
    }
}
