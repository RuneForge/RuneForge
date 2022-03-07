using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using RuneForge.Core.Interface.Controls.Helpers;

namespace RuneForge.Core.Interface.Controls
{
    public abstract class GraphicsControl : Control
    {
        private readonly SpriteBatch m_spriteBatch;

        private RenderTarget2D m_renderTarget;

        private bool m_renderCacheInvalidated;

        private bool m_disposed;

        public ContentManager ContentManager { get; }

        public GraphicsDevice GraphicsDevice { get; }

        protected GraphicsControl(ControlEventSource eventSource, ContentManager contentManager, GraphicsDevice graphicsDevice, SpriteBatch spriteBatch)
            : this(eventSource, contentManager, graphicsDevice, spriteBatch, DefaultEnabledValue, DefaultVisibleValue, DefaultDrawOrder)
        {
        }

        protected GraphicsControl(ControlEventSource eventSource, ContentManager contentManager, GraphicsDevice graphicsDevice, SpriteBatch spriteBatch, bool enabled, bool visible, int drawOrder)
            : base(eventSource, enabled, visible, drawOrder)
        {
            ContentManager = contentManager;
            GraphicsDevice = graphicsDevice;
            m_spriteBatch = spriteBatch;
        }

        public override void Draw(GameTime gameTime)
        {
            //IL_0065: Unknown result type (might be due to invalid IL or missing references)
            //IL_006a: Unknown result type (might be due to invalid IL or missing references)
            base.Draw(gameTime);
            if (m_renderCacheInvalidated)
            {
                RebuildRenderCache(gameTime);
            }
            if (Width * Height > 0)
            {
                m_spriteBatch.Begin(0, null, null, null, null, null, null);
                m_spriteBatch.Draw((Texture2D)(object)m_renderTarget, new Rectangle(0, 0, Width, Height), Color.White);
                m_spriteBatch.End();
            }
        }

        protected virtual void Render(GameTime gameTime, SpriteBatch spriteBatch)
        {
        }

        protected override void InvalidateRenderCache()
        {
            base.InvalidateRenderCache();
            if (!m_renderCacheInvalidated)
            {
                m_renderCacheInvalidated = true;
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (!m_disposed)
            {
                if (disposing)
                {
                    RenderTarget2D renderTarget = m_renderTarget;
                    if (renderTarget != null)
                    {
                        renderTarget.Dispose();
                    }
                }
                m_disposed = true;
            }
            base.Dispose(disposing);
        }

        private void RebuildRenderCache(GameTime gameTime)
        {
            //IL_0087: Unknown result type (might be due to invalid IL or missing references)
            //IL_008c: Unknown result type (might be due to invalid IL or missing references)
            //IL_009c: Unknown result type (might be due to invalid IL or missing references)
            //IL_00a1: Unknown result type (might be due to invalid IL or missing references)
            //IL_00ba: Unknown result type (might be due to invalid IL or missing references)
            //IL_00e7: Unknown result type (might be due to invalid IL or missing references)
            //IL_0179: Unknown result type (might be due to invalid IL or missing references)
            //IL_017e: Unknown result type (might be due to invalid IL or missing references)
            //IL_0183: Unknown result type (might be due to invalid IL or missing references)
            //IL_018a: Unknown result type (might be due to invalid IL or missing references)
            //IL_01e6: Unknown result type (might be due to invalid IL or missing references)
            if (m_renderTarget == null || Width != m_renderTarget.Width || Height != m_renderTarget.Height)
            {
                RenderTarget2D renderTarget = m_renderTarget;
                if (renderTarget != null)
                {
                    renderTarget.Dispose();
                }
                if (Width * Height <= 0)
                {
                    m_renderTarget = null;
                    return;
                }
                m_renderTarget = CreateRenderTarget2D();
            }
            Viewport viewport = GraphicsDevice.Viewport;
            RenderTargetBinding[] renderTargets = GraphicsDevice.GetRenderTargets();
            Viewport viewport2 = CreateRenderTargetViewport(in viewport);
            GraphicsDevice.SetRenderTarget(m_renderTarget);
            GraphicsDevice.Viewport = viewport2;
            m_spriteBatch.Begin(0, BlendState.AlphaBlend, null, null, null, null, null);
            GraphicsDevice.Clear(Color.Transparent);
            m_spriteBatch.End();
            m_spriteBatch.Begin(0, null, null, null, null, null, null);
            Render(gameTime, m_spriteBatch);
            m_spriteBatch.End();
            foreach (Control item in BuiltInControls.GetControlsByDrawOrder())
            {
                DrawControlWithViewport(in viewport2, item, gameTime);
            }
            viewport2 = GraphicsControlGeometryHelpers.CreateChildViewport(in viewport2, GetContainerBounds());
            GraphicsDevice.Viewport = viewport2;
            foreach (Control item2 in Controls.GetControlsByDrawOrder())
            {
                DrawControlWithViewport(in viewport2, item2, gameTime);
            }
            GraphicsDevice.SetRenderTargets(renderTargets);
            GraphicsDevice.Viewport = viewport;
            m_renderCacheInvalidated = false;
            OnRendered(EventArgs.Empty);
        }

        private void DrawControlWithViewport(in Viewport viewport, Control control, GameTime gameTime)
        {
            //IL_0003: Unknown result type (might be due to invalid IL or missing references)
            //IL_0008: Unknown result type (might be due to invalid IL or missing references)
            //IL_000d: Unknown result type (might be due to invalid IL or missing references)
            //IL_0014: Unknown result type (might be due to invalid IL or missing references)
            Viewport viewport2 = GraphicsControlGeometryHelpers.CreateChildViewport(in viewport, control.Bounds);
            GraphicsDevice.Viewport = viewport2;
            control.Draw(gameTime);
        }

        private Viewport CreateRenderTargetViewport(in Viewport viewport)
        {
            //IL_0010: Unknown result type (might be due to invalid IL or missing references)
            //IL_0015: Unknown result type (might be due to invalid IL or missing references)
            //IL_001e: Unknown result type (might be due to invalid IL or missing references)
            //IL_0023: Unknown result type (might be due to invalid IL or missing references)
            //IL_002b: Unknown result type (might be due to invalid IL or missing references)
            //IL_0030: Unknown result type (might be due to invalid IL or missing references)
            //IL_0033: Unknown result type (might be due to invalid IL or missing references)
            int width = Width;
            int height = Height;
            Viewport val = viewport;
            float minDepth = val.MinDepth;
            val = viewport;
            return new Viewport(0, 0, width, height, minDepth, val.MaxDepth);
        }

        private RenderTarget2D CreateRenderTarget2D()
        {
            //IL_0021: Unknown result type (might be due to invalid IL or missing references)
            //IL_0027: Unknown result type (might be due to invalid IL or missing references)
            //IL_002e: Unknown result type (might be due to invalid IL or missing references)
            //IL_0034: Expected O, but got Unknown
            PresentationParameters presentationParameters = GraphicsDevice.PresentationParameters;
            return new RenderTarget2D(GraphicsDevice, Width, Height, false, presentationParameters.BackBufferFormat, presentationParameters.DepthStencilFormat, 0, (RenderTargetUsage)1);
        }
    }
}
