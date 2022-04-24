using System;

using Microsoft.Xna.Framework;

using RuneForge.Core.Interface.Controls;

namespace RuneForge.Core.Interface.Windows
{
    public partial class OrderConfirmationDialogWindow
    {
        private const int c_windowWidth = 256;
        private const int c_totalFramesWidth = 14;

        private Label m_tooltipLabel;
        private Button m_cancelOrderButton;

        private void CreateLayout()
        {
            Width = c_windowWidth;
            Height = 128;

            Title = "Order Confirmation Dialog";
            CanBeMoved = false;
            CanBeClosed = false;

            m_tooltipLabel = new Label(null, ContentManager, GraphicsDevice, SpriteBatch)
            {
                X = 0,
                Y = 0,
                Width = c_windowWidth - c_totalFramesWidth,
                Height = 24,

                Text = "Select a target using the left button.",
                TextAlignment = Alignment.TopLeft,
                TextColor = Color.White,
                ShadowColor = Color.Transparent,
            };
            Controls.Add(m_tooltipLabel);

            m_cancelOrderButton = new Button(null, ContentManager, GraphicsDevice, SpriteBatch)
            {
                X = c_windowWidth - c_totalFramesWidth - 128,
                Y = m_tooltipLabel.Y + m_tooltipLabel.Height + 4,
                Width = 128,
                Height = 32,

                Text = "Cancel Order",
            };
            m_cancelOrderButton.Clicked += (sender, e) => OnOrderCancelled(EventArgs.Empty);
            Controls.Add(m_cancelOrderButton);

            Height = m_cancelOrderButton.Y + m_cancelOrderButton.Height + 37;
        }
    }
}
