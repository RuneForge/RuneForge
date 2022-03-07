using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using RuneForge.Core.Interface.Controls.Helpers;

namespace RuneForge.Core.Interface.Controls
{
    public class Label : GraphicsControl
    {
        private string m_text;

        private Alignment m_textAlignment;

        private Color m_textColor;

        private Color m_shadowColor;

        private SpriteFont m_spriteFont;

        public string Text
        {
            get
            {
                return m_text;
            }
            set
            {
                if (m_text != value)
                {
                    m_text = value;
                    OnTextChanged(EventArgs.Empty);
                }
            }
        }

        public Alignment TextAlignment
        {
            get
            {
                return m_textAlignment;
            }
            set
            {
                if (m_textAlignment != value)
                {
                    m_textAlignment = value;
                    OnTextAlignmentChanged(EventArgs.Empty);
                }
            }
        }

        public Color TextColor
        {
            get
            {
                //IL_0001: Unknown result type (might be due to invalid IL or missing references)
                return m_textColor;
            }
            set
            {
                //IL_0002: Unknown result type (might be due to invalid IL or missing references)
                //IL_0007: Unknown result type (might be due to invalid IL or missing references)
                //IL_0013: Unknown result type (might be due to invalid IL or missing references)
                //IL_0014: Unknown result type (might be due to invalid IL or missing references)
                if (m_textColor != value)
                {
                    m_textColor = value;
                    OnTextColorChanged(EventArgs.Empty);
                }
            }
        }

        public Color ShadowColor
        {
            get
            {
                //IL_0001: Unknown result type (might be due to invalid IL or missing references)
                return m_shadowColor;
            }
            set
            {
                //IL_0002: Unknown result type (might be due to invalid IL or missing references)
                //IL_0007: Unknown result type (might be due to invalid IL or missing references)
                //IL_0013: Unknown result type (might be due to invalid IL or missing references)
                //IL_0014: Unknown result type (might be due to invalid IL or missing references)
                if (m_shadowColor != value)
                {
                    m_shadowColor = value;
                    OnShadowColorChanged(EventArgs.Empty);
                }
            }
        }

        public SpriteFont SpriteFont
        {
            get
            {
                return m_spriteFont;
            }
            set
            {
                if (m_spriteFont != value)
                {
                    m_spriteFont = value;
                    OnSpriteFontChanged(EventArgs.Empty);
                }
            }
        }

        public event EventHandler<EventArgs> TextChanged;

        public event EventHandler<EventArgs> TextAlignmentChanged;

        public event EventHandler<EventArgs> TextColorChanged;

        public event EventHandler<EventArgs> ShadowColorChanged;

        public event EventHandler<EventArgs> SpriteFontChanged;

        public Label(ControlEventSource eventSource, ContentManager contentManager, GraphicsDevice graphicsDevice, SpriteBatch spriteBatch)
            : this(eventSource, contentManager, graphicsDevice, spriteBatch, DefaultEnabledValue, DefaultVisibleValue, DefaultDrawOrder)
        {
        }

        public Label(ControlEventSource eventSource, ContentManager contentManager, GraphicsDevice graphicsDevice, SpriteBatch spriteBatch, bool enabled, bool visible, int drawOrder)
            : base(eventSource, contentManager, graphicsDevice, spriteBatch, enabled, visible, drawOrder)
        {
            //IL_0026: Unknown result type (might be due to invalid IL or missing references)
            //IL_002b: Unknown result type (might be due to invalid IL or missing references)
            //IL_0031: Unknown result type (might be due to invalid IL or missing references)
            //IL_0036: Unknown result type (might be due to invalid IL or missing references)
            m_text = string.Empty;
            m_textAlignment = Alignment.TopLeft;
            m_textColor = Color.Black;
            m_shadowColor = Color.Transparent;
        }

        protected virtual void OnTextChanged(EventArgs e)
        {
            InvalidateRenderCache();
            TextChanged?.Invoke(this, e);
        }

        protected virtual void OnTextAlignmentChanged(EventArgs e)
        {
            InvalidateRenderCache();
            TextAlignmentChanged?.Invoke(this, e);
        }

        protected virtual void OnTextColorChanged(EventArgs e)
        {
            InvalidateRenderCache();
            TextColorChanged?.Invoke(this, e);
        }

        protected virtual void OnShadowColorChanged(EventArgs e)
        {
            InvalidateRenderCache();
            ShadowColorChanged?.Invoke(this, e);
        }

        protected virtual void OnSpriteFontChanged(EventArgs e)
        {
            InvalidateRenderCache();
            SpriteFontChanged?.Invoke(this, e);
        }

        protected override void Render(GameTime gameTime, SpriteBatch spriteBatch)
        {
            //IL_003b: Unknown result type (might be due to invalid IL or missing references)
            //IL_0040: Unknown result type (might be due to invalid IL or missing references)
            //IL_004d: Unknown result type (might be due to invalid IL or missing references)
            //IL_0052: Unknown result type (might be due to invalid IL or missing references)
            //IL_0073: Unknown result type (might be due to invalid IL or missing references)
            //IL_0078: Unknown result type (might be due to invalid IL or missing references)
            //IL_007a: Unknown result type (might be due to invalid IL or missing references)
            //IL_007b: Unknown result type (might be due to invalid IL or missing references)
            //IL_0080: Unknown result type (might be due to invalid IL or missing references)
            //IL_0082: Unknown result type (might be due to invalid IL or missing references)
            //IL_0083: Unknown result type (might be due to invalid IL or missing references)
            //IL_0086: Unknown result type (might be due to invalid IL or missing references)
            //IL_008b: Unknown result type (might be due to invalid IL or missing references)
            //IL_008d: Unknown result type (might be due to invalid IL or missing references)
            //IL_0092: Unknown result type (might be due to invalid IL or missing references)
            //IL_00a3: Unknown result type (might be due to invalid IL or missing references)
            //IL_00bb: Unknown result type (might be due to invalid IL or missing references)
            //IL_00bd: Unknown result type (might be due to invalid IL or missing references)
            //IL_00d5: Unknown result type (might be due to invalid IL or missing references)
            //IL_00d7: Unknown result type (might be due to invalid IL or missing references)
            base.Render(gameTime, spriteBatch);
            if (m_spriteFont == null)
            {
                throw new InvalidOperationException("It is not possible to draw a label with no SpriteFont set to it.");
            }
            if (!string.IsNullOrEmpty(m_text) && (!(m_textColor == Color.Transparent) || !(m_shadowColor == Color.Transparent)))
            {
                Vector2 textSize = m_spriteFont.MeasureString(m_text);
                Vector2 textLocation = GetTextLocation(textSize);
                Vector2 shadowLocation = GetShadowLocation(textLocation, textSize, out Vector2 shadowOffset);
                if (m_shadowColor != Color.Transparent)
                {
                    RecalculateLocationForShadowToFit(shadowOffset, ref textLocation, ref shadowLocation);
                }
                spriteBatch.DrawString(m_spriteFont, m_text, shadowLocation, m_shadowColor);
                spriteBatch.DrawString(m_spriteFont, m_text, textLocation, m_textColor);
            }
        }

        private Vector2 GetTextLocation(Vector2 textSize)
        {
            //IL_0009: Unknown result type (might be due to invalid IL or missing references)
            //IL_000f: Unknown result type (might be due to invalid IL or missing references)
            //IL_0014: Unknown result type (might be due to invalid IL or missing references)
            //IL_0021: Unknown result type (might be due to invalid IL or missing references)
            //IL_0026: Unknown result type (might be due to invalid IL or missing references)
            //IL_0029: Unknown result type (might be due to invalid IL or missing references)
            Alignment textAlignment = m_textAlignment;
            Point size = textSize.ToPoint();
            Rectangle rectangle = Bounds;
            GraphicsControlGeometryHelpers.CalculateLocationInsideRectangle(textAlignment, size, in rectangle, out Point location);
            return location.ToVector2();
        }

        private Vector2 GetShadowLocation(Vector2 textLocation, Vector2 textSize, out Vector2 shadowOffset)
        {
            //IL_0003: Unknown result type (might be due to invalid IL or missing references)
            //IL_0017: Unknown result type (might be due to invalid IL or missing references)
            //IL_001c: Unknown result type (might be due to invalid IL or missing references)
            //IL_0021: Unknown result type (might be due to invalid IL or missing references)
            //IL_0023: Unknown result type (might be due to invalid IL or missing references)
            //IL_0028: Unknown result type (might be due to invalid IL or missing references)
            //IL_002d: Unknown result type (might be due to invalid IL or missing references)
            //IL_0030: Unknown result type (might be due to invalid IL or missing references)
            shadowOffset = new Vector2(1 + (int)MathF.Floor(textSize.Y / 24f));
            return textLocation + shadowOffset;
        }

        private void RecalculateLocationForShadowToFit(Vector2 shadowOffset, ref Vector2 textLocation, ref Vector2 shadowLocation)
        {
            //IL_0019: Unknown result type (might be due to invalid IL or missing references)
            //IL_0029: Unknown result type (might be due to invalid IL or missing references)
            //IL_004a: Unknown result type (might be due to invalid IL or missing references)
            //IL_005a: Unknown result type (might be due to invalid IL or missing references)
            if ((m_textAlignment & Alignment.Bottom) == Alignment.Bottom)
            {
                textLocation.Y -= shadowOffset.Y;
                shadowLocation.Y -= shadowOffset.Y;
            }
            if ((m_textAlignment & Alignment.Right) == Alignment.Right)
            {
                textLocation.X -= shadowOffset.X;
                shadowLocation.X -= shadowOffset.X;
            }
        }
    }
}
