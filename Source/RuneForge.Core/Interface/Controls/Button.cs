using System;
using System.IO;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using RuneForge.Core.Input;
using RuneForge.Core.Interface.Controls.Helpers;
using RuneForge.Core.TextureAtlases;

namespace RuneForge.Core.Interface.Controls
{
    public class Button : ButtonBase
    {
        internal static class Constants
        {
            public const int FrameWidth = 4;

            public const int ButtonReleasedOffset = 0;

            public const int ButtonPressedOffset = 1;

            public const int LabelReleasedOffset = 2;

            public const int LabelPressedOffset = 3;

            public const Alignment LabelTextAlignment = Alignment.Center;

            public const string FrameBackgroundTextureRegionName = "Frame-Background";

            public const string FrameSideTextureRegionNameTemplate = "Frame-Side-{0}";

            public const string FrameCornerTextureRegionNameTemplate = "Frame-Corner-{0}";

            public const string ButtonReleasedBackgroundTextureRegionName = "Button-Released-Background";

            public const string ButtonReleasedSideTextureRegionNameTemplate = "Button-Released-Side-{0}";

            public const string ButtonReleasedCornerTextureRegionNameTemplate = "Button-Released-Corner-{0}";

            public const string ButtonPressedBackgroundTextureRegionName = "Button-Pressed-Background";

            public const string ButtonPressedSideTextureRegionNameTemplate = "Button-Pressed-Side-{0}";

            public const string ButtonPressedCornerTextureRegionNameTemplate = "Button-Pressed-Corner-{0}";

            public const string ButtonDisabledBackgroundTextureRegionName = "Button-Disabled-Background";

            public const string ButtonDisabledSideTextureRegionNameTemplate = "Button-Disabled-Side-{0}";

            public const string ButtonDisabledCornerTextureRegionNameTemplate = "Button-Disabled-Corner-{0}";

            public static readonly Color LabelTextColor = Color.LightGoldenrodYellow;

            public static readonly Color LabelShadowColor = Color.Transparent;

            public static readonly string TextureAtlasAssetName = Path.Combine("TextureAtlases", "Interface", "Controls", "Button");

            public static readonly RectangularFrameValidationData FrameValidationData = new RectangularFrameValidationData
            {
                SideTextureRegionNameTemplate = "Frame-Side-{0}",
                CornerTextureRegionNameTemplate = "Frame-Corner-{0}",
                ValidationPairs = new (Alignment, Alignment)[8]
                {
                (Alignment.Top, Alignment.TopLeft),
                (Alignment.Top, Alignment.TopRight),
                (Alignment.Bottom, Alignment.BottomLeft),
                (Alignment.Bottom, Alignment.BottomRight),
                (Alignment.Left, Alignment.TopLeft),
                (Alignment.Left, Alignment.BottomLeft),
                (Alignment.Right, Alignment.TopRight),
                (Alignment.Right, Alignment.BottomRight)
                }
            };

            public static readonly RectangularFrameValidationData ButtonReleasedValidationData = new RectangularFrameValidationData
            {
                SideTextureRegionNameTemplate = "Button-Released-Side-{0}",
                CornerTextureRegionNameTemplate = "Button-Released-Corner-{0}",
                ValidationPairs = new (Alignment, Alignment)[8]
                {
                (Alignment.Top, Alignment.TopLeft),
                (Alignment.Top, Alignment.TopRight),
                (Alignment.Bottom, Alignment.BottomLeft),
                (Alignment.Bottom, Alignment.BottomRight),
                (Alignment.Left, Alignment.TopLeft),
                (Alignment.Left, Alignment.BottomLeft),
                (Alignment.Right, Alignment.TopRight),
                (Alignment.Right, Alignment.BottomRight)
                }
            };

            public static readonly RectangularFrameValidationData ButtonPressedValidationData = new RectangularFrameValidationData
            {
                SideTextureRegionNameTemplate = "Button-Pressed-Side-{0}",
                CornerTextureRegionNameTemplate = "Button-Pressed-Corner-{0}",
                ValidationPairs = new (Alignment, Alignment)[8]
                {
                (Alignment.Top, Alignment.TopLeft),
                (Alignment.Top, Alignment.TopRight),
                (Alignment.Bottom, Alignment.BottomLeft),
                (Alignment.Bottom, Alignment.BottomRight),
                (Alignment.Left, Alignment.TopLeft),
                (Alignment.Left, Alignment.BottomLeft),
                (Alignment.Right, Alignment.TopRight),
                (Alignment.Right, Alignment.BottomRight)
                }
            };

            public static readonly RectangularFrameValidationData ButtonDisabledValidationData = new RectangularFrameValidationData
            {
                SideTextureRegionNameTemplate = "Button-Disabled-Side-{0}",
                CornerTextureRegionNameTemplate = "Button-Disabled-Corner-{0}",
                ValidationPairs = new (Alignment, Alignment)[8]
                {
                (Alignment.Top, Alignment.TopLeft),
                (Alignment.Top, Alignment.TopRight),
                (Alignment.Bottom, Alignment.BottomLeft),
                (Alignment.Bottom, Alignment.BottomRight),
                (Alignment.Left, Alignment.TopLeft),
                (Alignment.Left, Alignment.BottomLeft),
                (Alignment.Right, Alignment.TopRight),
                (Alignment.Right, Alignment.BottomRight)
                }
            };

            public static TextureRegion2D GetFrameTextureRegion(TextureAtlas textureAtlas, Alignment alignment)
            {
                string sideTextureRegionNameTemplate = "Frame-Side-{0}";
                string cornerTextureRegionNameTemplate = "Frame-Corner-{0}";
                return textureAtlas.GetFrameTextureRegion(sideTextureRegionNameTemplate, cornerTextureRegionNameTemplate, alignment);
            }

            public static TextureRegion2D GetButtonReleasedTextureRegion(TextureAtlas textureAtlas, Alignment alignment)
            {
                string sideTextureRegionNameTemplate = "Button-Released-Side-{0}";
                string cornerTextureRegionNameTemplate = "Button-Released-Corner-{0}";
                return textureAtlas.GetFrameTextureRegion(sideTextureRegionNameTemplate, cornerTextureRegionNameTemplate, alignment);
            }

            public static TextureRegion2D GetButtonPressedTextureRegion(TextureAtlas textureAtlas, Alignment alignment)
            {
                string sideTextureRegionNameTemplate = "Button-Pressed-Side-{0}";
                string cornerTextureRegionNameTemplate = "Button-Pressed-Corner-{0}";
                return textureAtlas.GetFrameTextureRegion(sideTextureRegionNameTemplate, cornerTextureRegionNameTemplate, alignment);
            }

            public static TextureRegion2D GetButtonDisabledTextureRegion(TextureAtlas textureAtlas, Alignment alignment)
            {
                string sideTextureRegionNameTemplate = "Button-Disabled-Side-{0}";
                string cornerTextureRegionNameTemplate = "Button-Disabled-Corner-{0}";
                return textureAtlas.GetFrameTextureRegion(sideTextureRegionNameTemplate, cornerTextureRegionNameTemplate, alignment);
            }
        }

        private readonly Label m_buttonTextLabel;

        private TextureAtlas m_textureAtlas;

        private bool m_pressed;

        public string Text
        {
            get
            {
                return m_buttonTextLabel.Text;
            }
            set
            {
                if (m_buttonTextLabel.Text != value)
                {
                    m_buttonTextLabel.Text = value;
                    OnTextChanged(EventArgs.Empty);
                }
            }
        }

        public SpriteFont SpriteFont
        {
            get
            {
                return m_buttonTextLabel.SpriteFont;
            }
            set
            {
                if (m_buttonTextLabel.SpriteFont != value)
                {
                    m_buttonTextLabel.SpriteFont = value;
                    OnSpriteFontChanged(EventArgs.Empty);
                }
            }
        }

        public event EventHandler<EventArgs> TextChanged;

        public event EventHandler<EventArgs> SpriteFontChanged;

        public Button(ControlEventSource eventSource, ContentManager contentManager, GraphicsDevice graphicsDevice, SpriteBatch spriteBatch)
            : this(eventSource, contentManager, graphicsDevice, spriteBatch, DefaultEnabledValue, DefaultVisibleValue, DefaultDrawOrder)
        {
        }

        public Button(ControlEventSource eventSource, ContentManager contentManager, GraphicsDevice graphicsDevice, SpriteBatch spriteBatch, bool enabled, bool visible, int drawOrder)
            : base(eventSource, contentManager, graphicsDevice, spriteBatch, enabled, visible, drawOrder)
        {
            m_buttonTextLabel = new Label(null, contentManager, graphicsDevice, spriteBatch)
            {
                X = 2,
                Y = 2,
                Width = 0,
                Height = 0,
                TextAlignment = Alignment.Center,
                TextColor = Constants.LabelTextColor,
                ShadowColor = Constants.LabelShadowColor
            };
            m_buttonTextLabel.Rendered += delegate
            {
                InvalidateRenderCache();
            };
            BuiltInControls.Add(m_buttonTextLabel);
            m_pressed = false;
        }

        public override void LoadContent()
        {
            m_textureAtlas = LoadButtonTextureAtlas();
            base.LoadContent();
        }

        protected virtual void OnTextChanged(EventArgs e)
        {
            TextChanged?.Invoke(this, e);
        }

        protected virtual void OnSpriteFontChanged(EventArgs e)
        {
            SpriteFontChanged?.Invoke(this, e);
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            m_buttonTextLabel.Size = new Point(base.Size.X - 2, base.Size.Y - 2);
            base.OnSizeChanged(e);
        }

        protected override void Render(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Render(gameTime, spriteBatch);
            spriteBatch.DrawFilledRectangle((Alignment alignment) => Constants.GetFrameTextureRegion(m_textureAtlas, alignment), m_textureAtlas.TextureRegions["Frame-Background"], new Rectangle(0, 0, Bounds.Width, Bounds.Height), Color.White);
            int num = 4 + (m_pressed ? 1 : 0);
            if (Enabled)
            {
                if (m_pressed)
                {
                    spriteBatch.DrawFilledRectangle((Alignment alignment) => Constants.GetButtonPressedTextureRegion(m_textureAtlas, alignment), m_textureAtlas.TextureRegions["Button-Pressed-Background"], new Rectangle(num, num, Bounds.Width - 8, Bounds.Height - 8), Color.White);
                }
                else
                {
                    spriteBatch.DrawFilledRectangle((Alignment alignment) => Constants.GetButtonReleasedTextureRegion(m_textureAtlas, alignment), m_textureAtlas.TextureRegions["Button-Released-Background"], new Rectangle(num, num, Bounds.Width - 8, Bounds.Height - 8), Color.White);
                }
            }
            else
            {
                spriteBatch.DrawFilledRectangle((Alignment alignment) => Constants.GetButtonDisabledTextureRegion(m_textureAtlas, alignment), m_textureAtlas.TextureRegions["Button-Disabled-Background"], new Rectangle(num, num, Bounds.Width - 8, Bounds.Height - 8), Color.White);
            }
        }

        protected override void OnPressed(MouseEventArgs e)
        {
            base.OnPressed(e);
            m_pressed = true;
            m_buttonTextLabel.Location = new Point(3, 3);
            InvalidateRenderCache();
        }

        protected override void OnReleased(MouseEventArgs e)
        {
            base.OnReleased(e);
            m_pressed = false;
            m_buttonTextLabel.Location = new Point(2, 2);
            InvalidateRenderCache();
        }

        private TextureAtlas LoadButtonTextureAtlas()
        {
            TextureAtlas textureAtlas = ContentManager.Load<TextureAtlas>(Constants.TextureAtlasAssetName);
            textureAtlas.ValidateGeometry(Constants.FrameValidationData);
            textureAtlas.ValidateGeometry(Constants.ButtonReleasedValidationData);
            textureAtlas.ValidateGeometry(Constants.ButtonPressedValidationData);
            textureAtlas.ValidateGeometry(Constants.ButtonDisabledValidationData);
            return textureAtlas;
        }
    }
}
