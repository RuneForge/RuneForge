using System;
using System.IO;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using RuneForge.Core.Input;
using RuneForge.Core.Interface.Controls.Helpers;
using RuneForge.Core.TextureAtlases;

namespace RuneForge.Core.Interface.Controls
{
    public class Window : GraphicsControl
    {
        internal class WindowButton : ButtonBase
        {
            private readonly Window m_window;

            private Point m_initialLocation;

            private bool m_pressed;

            public Point DeltaLocation { get; private set; }

            public string ButtonReleasedTextureRegionName { get; set; }

            public string ButtonPressedTextureRegionName { get; set; }

            public event EventHandler<EventArgs> Dragged;

            public WindowButton(ControlEventSource eventSource, ContentManager contentManager, GraphicsDevice graphicsDevice, SpriteBatch spriteBatch, Window window, bool enabled, bool visible, int drawOrder)
                : base(eventSource, contentManager, graphicsDevice, spriteBatch, enabled, visible, drawOrder)
            {
                m_window = window;
            }

            protected void OnDragged(EventArgs e)
            {
                Dragged?.Invoke(this, e);
            }

            protected override void Render(GameTime gameTime, SpriteBatch spriteBatch)
            {
                base.Render(gameTime, spriteBatch);
                string textureRegionName = m_pressed ? ButtonPressedTextureRegionName : ButtonReleasedTextureRegionName;
                TextureRegion2D textureRegion = m_window.m_textureAtlas.GetTextureRegion(textureRegionName);
                spriteBatch.Draw(textureRegion, new Rectangle(0, 0, Width, Height), Color.White);
            }

            protected override void OnMouseDragStarted(object sender, MouseEventArgs e)
            {
                base.OnMouseDragStarted(sender, e);
            }

            protected override void OnMouseDragFinished(object sender, MouseEventArgs e)
            {
                base.OnMouseDragFinished(sender, e);
            }

            protected override void OnMouseDragMoved(object sender, MouseEventArgs e)
            {
                base.OnMouseDragMoved(sender, e);
                if (m_pressed && e.Button == (e.Button & MouseButtons.LeftButton))
                {
                    Point locationInsideViewport = e.GetLocationInsideViewport();
                    DeltaLocation = locationInsideViewport - m_initialLocation;
                    OnDragged(e);
                }
            }

            protected override void OnPressed(MouseEventArgs e)
            {
                base.OnPressed(e);
                m_initialLocation = e.GetLocationInsideViewport();
                m_pressed = true;
                InvalidateRenderCache();
            }

            protected override void OnReleased(MouseEventArgs e)
            {
                base.OnReleased(e);
                m_pressed = false;
                InvalidateRenderCache();
            }
        }

        internal static class Constants
        {
            public const int WindowButtonSize = 20;

            public const int RegularWindowButtonWidth = 22;

            public const int MostRightWindowButtonWidth = 23;

            public const int WindowHeaderHeight = 23;

            public const int WindowHeaderHeightOverlapped = 24;

            public const int WindowContentFrameWidth = 7;

            public const Alignment WindowTitleLabelTextAlignment = Alignment.Center;

            public const string FrameBackgroundTextureRegionName = "Frame-Background";

            public const string FrameSideTextureRegionNameTemplate = "Frame-Side-{0}";

            public const string FrameCornerTextureRegionNameTemplate = "Frame-Corner-{0}";

            public const string FrameHorizontalSplitterSideTextureRegionNameTemplate = "Frame-HorizontalSplitter-Side-{0}";

            public const string FrameHorizontalSplitterCornerTextureRegionNameTemplate = "Frame-HorizontalSplitter-Corner-{0}";

            public const string FrameVerticalSplitterSideTextureRegionNameTemplate = "Frame-VerticalSplitter-Side-{0}";

            public const string FrameVerticalSplitterCornerTextureRegionNameTemplate = "Frame-VerticalSplitter-Corner-{0}";

            public const string CloseButtonReleasedTextureRegionName = "CloseButton-Released";

            public const string CloseButtonPressedTextureRegionName = "CloseButton-Pressed";

            public const string MoveButtonReleasedTextureRegionName = "MoveButton-Released";

            public const string MoveButtonPressedTextureRegionName = "MoveButton-Pressed";

            public static readonly Point WindowButtonOffset = new Point(1, 2);

            public static readonly Point WindowTitleLabelOffset = new Point(2, 2);

            public static readonly Color WindowTitleLabelTextColor = Color.LightGoldenrodYellow;

            public static readonly Color WindowTitleLabelShadowColor = Color.Transparent;

            public static readonly string TextureAtlasAssetName = Path.Combine("TextureAtlases", "Interface", "Controls", "Window");

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

            public static readonly RectangularFrameValidationData FrameHorizontalSplitterCornerValidationData = new RectangularFrameValidationData
            {
                SideTextureRegionNameTemplate = "Frame-HorizontalSplitter-Side-{0}",
                CornerTextureRegionNameTemplate = "Frame-HorizontalSplitter-Corner-{0}",
                ValidationPairs = new (Alignment, Alignment)[4]
                {
                (Alignment.Top, Alignment.TopLeft),
                (Alignment.Top, Alignment.TopRight),
                (Alignment.Bottom, Alignment.BottomLeft),
                (Alignment.Bottom, Alignment.BottomRight)
                }
            };

            public static readonly RectangularFrameValidationData FrameVerticalSplitterCornerValidationData = new RectangularFrameValidationData
            {
                SideTextureRegionNameTemplate = "Frame-VerticalSplitter-Side-{0}",
                CornerTextureRegionNameTemplate = "Frame-VerticalSplitter-Corner-{0}",
                ValidationPairs = new (Alignment, Alignment)[4]
                {
                (Alignment.Left, Alignment.TopLeft),
                (Alignment.Left, Alignment.BottomLeft),
                (Alignment.Right, Alignment.TopRight),
                (Alignment.Right, Alignment.BottomRight)
                }
            };

            public static TextureRegion2D GetTitleFrameTextureRegion(TextureAtlas textureAtlas, Alignment alignment, bool hasButtons)
            {
                string empty = string.Empty;
                string empty2 = string.Empty;
                switch (alignment)
                {
                    case Alignment.Top:
                    case Alignment.Bottom:
                    case Alignment.Left:
                        empty = "Frame-Side-{0}";
                        return textureAtlas.GetFrameTextureRegion(empty, empty2, alignment);
                    case Alignment.Right:
                        empty = hasButtons ? "Frame-VerticalSplitter-Side-{0}" : "Frame-Side-{0}";
                        return textureAtlas.GetFrameTextureRegion(empty, empty2, alignment);
                    case Alignment.TopLeft:
                    case Alignment.BottomLeft:
                        empty2 = "Frame-Corner-{0}";
                        return textureAtlas.GetFrameTextureRegion(empty, empty2, alignment);
                    case Alignment.TopRight:
                    case Alignment.BottomRight:
                        empty2 = hasButtons ? "Frame-VerticalSplitter-Corner-{0}" : "Frame-Corner-{0}";
                        return textureAtlas.GetFrameTextureRegion(empty, empty2, alignment);
                    default:
                        return null;
                }
            }

            public static TextureRegion2D GetContentFrameTextureRegion(TextureAtlas textureAtlas, Alignment alignment)
            {
                string empty = string.Empty;
                string empty2 = string.Empty;
                switch (alignment)
                {
                    case Alignment.Top:
                        empty = "Frame-HorizontalSplitter-Side-{0}";
                        return textureAtlas.GetFrameTextureRegion(empty, empty2, alignment);
                    case Alignment.Bottom:
                    case Alignment.Left:
                    case Alignment.Right:
                        empty = "Frame-Side-{0}";
                        return textureAtlas.GetFrameTextureRegion(empty, empty2, alignment);
                    case Alignment.TopLeft:
                    case Alignment.TopRight:
                        empty2 = "Frame-HorizontalSplitter-Corner-{0}";
                        return textureAtlas.GetFrameTextureRegion(empty, empty2, alignment);
                    case Alignment.BottomLeft:
                    case Alignment.BottomRight:
                        empty2 = "Frame-Corner-{0}";
                        return textureAtlas.GetFrameTextureRegion(empty, empty2, alignment);
                    default:
                        return null;
                }
            }

            public static TextureRegion2D GetButtonFrameTextureRegion(TextureAtlas textureAtlas, Alignment alignment, bool mostRightButton)
            {
                string empty = string.Empty;
                string empty2 = string.Empty;
                switch (alignment)
                {
                    case Alignment.Top:
                    case Alignment.Bottom:
                        empty = "Frame-Side-{0}";
                        return textureAtlas.GetFrameTextureRegion(empty, empty2, alignment);
                    case Alignment.Left:
                        empty = "Frame-VerticalSplitter-Side-{0}";
                        return textureAtlas.GetFrameTextureRegion(empty, empty2, alignment);
                    case Alignment.Right:
                        empty = mostRightButton ? "Frame-Side-{0}" : "Frame-VerticalSplitter-Side-{0}";
                        return textureAtlas.GetFrameTextureRegion(empty, empty2, alignment);
                    case Alignment.TopLeft:
                    case Alignment.BottomLeft:
                        empty2 = "Frame-VerticalSplitter-Corner-{0}";
                        return textureAtlas.GetFrameTextureRegion(empty, empty2, alignment);
                    case Alignment.TopRight:
                    case Alignment.BottomRight:
                        empty2 = mostRightButton ? "Frame-Corner-{0}" : "Frame-VerticalSplitter-Corner-{0}";
                        return textureAtlas.GetFrameTextureRegion(empty, empty2, alignment);
                    default:
                        return null;
                }
            }
        }

        private readonly Label m_windowTitleLabel;

        private readonly WindowButton m_closeWindowButton;

        private readonly WindowButton m_moveWindowButton;

        private bool m_canBeClosed;

        private bool m_canBeMoved;

        private TextureAtlas m_textureAtlas;

        public string Title
        {
            get
            {
                return m_windowTitleLabel.Text;
            }
            set
            {
                if (m_windowTitleLabel.Text != value)
                {
                    m_windowTitleLabel.Text = value;
                    OnTitleChanged(EventArgs.Empty);
                }
            }
        }

        public SpriteFont SpriteFont
        {
            get
            {
                return m_windowTitleLabel.SpriteFont;
            }
            set
            {
                if (m_windowTitleLabel.SpriteFont != value)
                {
                    m_windowTitleLabel.SpriteFont = value;
                    OnSpriteFontChanged(EventArgs.Empty);
                }
            }
        }

        public bool CanBeClosed
        {
            get
            {
                return m_canBeClosed;
            }
            set
            {
                if (m_canBeClosed != value)
                {
                    m_canBeClosed = value;
                    OnCanBeClosedChanged(EventArgs.Empty);
                }
            }
        }

        public bool CanBeMoved
        {
            get
            {
                return m_canBeMoved;
            }
            set
            {
                if (m_canBeMoved != value)
                {
                    m_canBeMoved = value;
                    OnCanBeMovedChanged(EventArgs.Empty);
                }
            }
        }

        public event EventHandler<EventArgs> TitleChanged;

        public event EventHandler<EventArgs> SpriteFontChanged;

        public event EventHandler<EventArgs> CanBeClosedChanged;

        public event EventHandler<EventArgs> CanBeMovedChanged;

        public event EventHandler<EventArgs> Closing;

        public event EventHandler<EventArgs> Closed;

        public Window(ControlEventSource eventSource, ContentManager contentManager, GraphicsDevice graphicsDevice, SpriteBatch spriteBatch)
            : this(eventSource, contentManager, graphicsDevice, spriteBatch, Control.DefaultEnabledValue, Control.DefaultVisibleValue, Control.DefaultDrawOrder)
        {
        }

        public Window(ControlEventSource eventSource, ContentManager contentManager, GraphicsDevice graphicsDevice, SpriteBatch spriteBatch, bool enabled, bool visible, int drawOrder)
            : base(eventSource, contentManager, graphicsDevice, spriteBatch, enabled, visible, drawOrder)
        {
            m_windowTitleLabel = new Label(null, contentManager, graphicsDevice, spriteBatch)
            {
                Width = 0,
                Height = 0,
                TextAlignment = Alignment.Center,
                TextColor = Constants.WindowTitleLabelTextColor,
                ShadowColor = Constants.WindowTitleLabelShadowColor
            };
            m_windowTitleLabel.Rendered += delegate
            {
                InvalidateRenderCache();
            };
            BuiltInControls.Add(m_windowTitleLabel);
            m_closeWindowButton = new WindowButton(null, contentManager, graphicsDevice, spriteBatch, this, enabled: false, visible: false, 0)
            {
                X = 0,
                Y = 0,
                Width = 20,
                Height = 20,
                ButtonReleasedTextureRegionName = "CloseButton-Released",
                ButtonPressedTextureRegionName = "CloseButton-Pressed"
            };
            m_closeWindowButton.Rendered += delegate
            {
                InvalidateRenderCache();
            };
            m_closeWindowButton.Clicked += delegate
            {
                Close();
            };
            BuiltInControls.Add(m_closeWindowButton);
            m_moveWindowButton = new WindowButton(null, contentManager, graphicsDevice, spriteBatch, this, enabled: false, visible: false, 0)
            {
                X = 0,
                Y = 0,
                Width = 20,
                Height = 20,
                ButtonReleasedTextureRegionName = "MoveButton-Released",
                ButtonPressedTextureRegionName = "MoveButton-Pressed"
            };
            m_moveWindowButton.Rendered += delegate
            {
                InvalidateRenderCache();
            };
            m_moveWindowButton.Dragged += delegate
            {
                Location += m_moveWindowButton.DeltaLocation;
            };
            BuiltInControls.Add(m_moveWindowButton);
        }

        public void Close()
        {
            OnClosing(EventArgs.Empty);
            OnClosed(EventArgs.Empty);
        }

        public override void LoadContent()
        {
            m_textureAtlas = LoadWindowTextureAtlas();
            base.LoadContent();
        }

        protected virtual void OnTitleChanged(EventArgs e)
        {
            TitleChanged?.Invoke(this, e);
        }

        protected virtual void OnSpriteFontChanged(EventArgs e)
        {
            SpriteFontChanged?.Invoke(this, e);
        }

        protected virtual void OnCanBeClosedChanged(EventArgs e)
        {
            ReorderWindowButtons();
            CanBeClosedChanged?.Invoke(this, e);
        }

        protected virtual void OnCanBeMovedChanged(EventArgs e)
        {
            ReorderWindowButtons();
            CanBeMovedChanged?.Invoke(this, e);
        }

        protected virtual void OnClosing(EventArgs e)
        {
            Closing?.Invoke(this, e);
        }

        protected virtual void OnClosed(EventArgs e)
        {
            Closed?.Invoke(this, e);
        }

        protected override Rectangle GetContainerBounds()
        {
            int x = 7;
            int y = 30;
            int width = Width - 14;
            int height = Height - 37;
            return new Rectangle(x, y, width, height);
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            int windowButtonsCount = GetWindowButtonsCount();
            int titleFrameWidth = GetTitleFrameWidth(windowButtonsCount);
            m_windowTitleLabel.Size = new Point(titleFrameWidth, 23);
            ReorderWindowButtons();
            base.OnSizeChanged(e);
        }

        protected override void Render(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Render(gameTime, spriteBatch);
            int windowButtonsCount = GetWindowButtonsCount();
            int titleFrameWidth = GetTitleFrameWidth(windowButtonsCount);
            spriteBatch.DrawFilledRectangle((Alignment alignment) => Constants.GetTitleFrameTextureRegion(m_textureAtlas, alignment, windowButtonsCount > 0), m_textureAtlas.TextureRegions["Frame-Background"], new Rectangle(0, 0, titleFrameWidth, 24), Color.White);
            int i;
            for (i = 0; i < windowButtonsCount; i++)
            {
                bool flag = i == 0;
                Point location = new Point(titleFrameWidth + ((windowButtonsCount - i - 1) * 22), 0);
                Point size = new Point(flag ? 23 : 22, 24);
                spriteBatch.DrawFilledRectangle((Alignment alignment) => Constants.GetButtonFrameTextureRegion(m_textureAtlas, alignment, i == 0), m_textureAtlas.TextureRegions["Frame-Background"], new Rectangle(location, size), Color.White);
            }
            spriteBatch.DrawFilledRectangle((Alignment alignment) => Constants.GetContentFrameTextureRegion(m_textureAtlas, alignment), m_textureAtlas.TextureRegions["Frame-Background"], new Rectangle(0, 23, Width, Height - 23), Color.White);
        }

        private TextureAtlas LoadWindowTextureAtlas()
        {
            TextureAtlas textureAtlas = ContentManager.Load<TextureAtlas>(Constants.TextureAtlasAssetName);
            textureAtlas.ValidateGeometry(Constants.FrameValidationData);
            textureAtlas.ValidateGeometry(Constants.FrameHorizontalSplitterCornerValidationData);
            textureAtlas.ValidateGeometry(Constants.FrameVerticalSplitterCornerValidationData);
            return textureAtlas;
        }

        private void ReorderWindowButtons()
        {
            BuiltInControls.Remove(m_closeWindowButton);
            BuiltInControls.Remove(m_moveWindowButton);
            int windowButtonsCount = GetWindowButtonsCount();
            int titleFrameWidth = GetTitleFrameWidth(windowButtonsCount);
            int currentButtonIndex2 = 0;
            ProcessButton(m_closeWindowButton, m_canBeClosed, ref currentButtonIndex2);
            ProcessButton(m_moveWindowButton, m_canBeMoved, ref currentButtonIndex2);
            void ProcessButton(WindowButton button, bool active, ref int currentButtonIndex)
            {
                if (active)
                {
                    Point point = new Point(titleFrameWidth + ((windowButtonsCount - currentButtonIndex++ - 1) * 22), 0);
                    button.Enabled = true;
                    button.Visible = true;
                    button.Location = point + Constants.WindowButtonOffset;
                    BuiltInControls.Add(button);
                }
                else
                {
                    button.Enabled = false;
                    button.Visible = false;
                }
            }
        }

        private int GetWindowButtonsCount()
        {
            int num = 0;
            if (CanBeClosed)
            {
                num++;
            }
            if (CanBeMoved)
            {
                num++;
            }
            return num;
        }

        private int GetTitleFrameWidth(int windowButtonsCount)
        {
            int num = Width;
            if (windowButtonsCount > 0)
            {
                num -= 23 + ((windowButtonsCount - 1) * 22);
            }
            return num;
        }
    }
}
