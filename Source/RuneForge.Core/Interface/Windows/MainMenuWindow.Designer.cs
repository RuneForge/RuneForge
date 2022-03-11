using System;

using RuneForge.Core.Interface.Controls;

namespace RuneForge.Core.Interface.Windows
{
    public partial class MainMenuWindow
    {
        private Button m_newGameButton;
        private Button m_exitGameButton;

        private void CreateLayout()
        {
            Width = 384;
            Height = 256;

            Title = "Main Menu";
            CanBeMoved = false;
            CanBeClosed = false;

            m_newGameButton = new Button(null, ContentManager, GraphicsDevice, SpriteBatch)
            {
                X = 0,
                Y = 0,
                Width = 384 - 7 * 2,
                Height = 32,

                Text = "Start a New Game",
            };
            m_newGameButton.Clicked += (sender, e) => StartGameButtonClicked?.Invoke(this, EventArgs.Empty);
            Controls.Add(m_newGameButton);

            m_exitGameButton = new Button(null, ContentManager, GraphicsDevice, SpriteBatch)
            {
                X = 0,
                Y = m_newGameButton.Y + m_newGameButton.Height + 4,
                Width = 384 - 7 * 2,
                Height = 32,

                Text = "Exit Game",
            };
            m_exitGameButton.Clicked += (sender, e) => ExitGameButtonClicked?.Invoke(this, EventArgs.Empty);
            Controls.Add(m_exitGameButton);

            Height = 37 + 32 * 2 + 4;
        }
    }
}
