using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using RuneForge.Core.Interface.Controls.Helpers;
using RuneForge.Core.Interface.Controls.ImageSources;

namespace RuneForge.Core.Interface.Controls
{
    public class PictureBox : GraphicsControl
    {
        private ImageSource m_imageSource;

        private Alignment m_imageAlignment;

        public ImageSource ImageSource
        {
            get
            {
                return m_imageSource;
            }
            set
            {
                if (m_imageSource != value)
                {
                    m_imageSource = value;
                    OnImageSourceChanged(EventArgs.Empty);
                }
            }
        }

        public Alignment ImageAlignment
        {
            get
            {
                return m_imageAlignment;
            }
            set
            {
                if (m_imageAlignment != value)
                {
                    m_imageAlignment = value;
                    OnImageAlignmentChanged(EventArgs.Empty);
                }
            }
        }

        public event EventHandler<EventArgs> ImageSourceChanged;

        public event EventHandler<EventArgs> ImageAlignmentChanged;

        public PictureBox(ControlEventSource eventSource, ContentManager contentManager, GraphicsDevice graphicsDevice, SpriteBatch spriteBatch)
            : this(eventSource, contentManager, graphicsDevice, spriteBatch, DefaultEnabledValue, DefaultVisibleValue, DefaultDrawOrder)
        {
        }

        public PictureBox(ControlEventSource eventSource, ContentManager contentManager, GraphicsDevice graphicsDevice, SpriteBatch spriteBatch, bool enabled, bool visible, int drawOrder)
            : base(eventSource, contentManager, graphicsDevice, spriteBatch, enabled, visible, drawOrder)
        {
            m_imageSource = null;
            m_imageAlignment = Alignment.TopLeft;
        }

        protected virtual void OnImageSourceChanged(EventArgs e)
        {
            InvalidateRenderCache();
            ImageSourceChanged?.Invoke(this, e);
        }

        protected virtual void OnImageAlignmentChanged(EventArgs e)
        {
            InvalidateRenderCache();
            ImageAlignmentChanged?.Invoke(this, e);
        }

        protected override void Render(GameTime gameTime, SpriteBatch spriteBatch)
        {
            //IL_0048: Unknown result type (might be due to invalid IL or missing references)
            //IL_004d: Unknown result type (might be due to invalid IL or missing references)
            //IL_004f: Unknown result type (might be due to invalid IL or missing references)
            //IL_0050: Unknown result type (might be due to invalid IL or missing references)
            //IL_0055: Unknown result type (might be due to invalid IL or missing references)
            //IL_005d: Unknown result type (might be due to invalid IL or missing references)
            //IL_005e: Unknown result type (might be due to invalid IL or missing references)
            //IL_005f: Unknown result type (might be due to invalid IL or missing references)
            //IL_0064: Unknown result type (might be due to invalid IL or missing references)
            base.Render(gameTime, spriteBatch);
            if (m_imageSource == null)
            {
                throw new InvalidOperationException("It is not possible to draw an image with no ImageSource set to it.");
            }
            if (m_imageSource.Width * m_imageSource.Height != 0)
            {
                Point size = m_imageSource.Size;
                Point imageLocation = GetImageLocation(size);
                spriteBatch.Draw(m_imageSource, new Rectangle(imageLocation, size), Color.White);
            }
        }

        private Point GetImageLocation(Point imageSize)
        {
            //IL_0007: Unknown result type (might be due to invalid IL or missing references)
            //IL_0009: Unknown result type (might be due to invalid IL or missing references)
            //IL_000e: Unknown result type (might be due to invalid IL or missing references)
            //IL_0019: Unknown result type (might be due to invalid IL or missing references)
            //IL_001a: Unknown result type (might be due to invalid IL or missing references)
            //IL_001d: Unknown result type (might be due to invalid IL or missing references)
            Alignment imageAlignment = m_imageAlignment;
            Rectangle rectangle = base.Bounds;
            GraphicsControlGeometryHelpers.CalculateLocationInsideRectangle(imageAlignment, imageSize, in rectangle, out Point location);
            return location;
        }
    }
}
