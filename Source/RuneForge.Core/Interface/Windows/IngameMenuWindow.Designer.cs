using System;

using RuneForge.Core.Interface.Controls;

namespace RuneForge.Core.Interface.Windows
{
    public partial class IngameMenuWindow
    {
        private const int c_windowWidth = 256;
        private const int c_totalFramesWidth = 14;

        private Button m_resumeGameButton;
        private Button m_saveGameButton;
        private Button m_exitToMainMenuButton;
        private Button m_exitToDesktopButton;

        private void CreateLayout()
        {
            Width = c_windowWidth;
            Height = 256;

            Title = "Pause Menu";
            CanBeMoved = false;
            CanBeClosed = false;

            m_resumeGameButton = new Button(null, ContentManager, GraphicsDevice, SpriteBatch)
            {
                X = 0,
                Y = 0,
                Width = c_windowWidth - 7 * 2,
                Height = 32,

                Text = "Resume Game",
            };
            m_resumeGameButton.Clicked += (sender, e) => OnGameResumed(EventArgs.Empty);
            Controls.Add(m_resumeGameButton);

            m_saveGameButton = new Button(null, ContentManager, GraphicsDevice, SpriteBatch)
            {
                X = 0,
                Y = m_resumeGameButton.Y + m_resumeGameButton.Height + 4,
                Width = c_windowWidth - 7 * 2,
                Height = 32,

                Text = "Save Game",
            };
            m_saveGameButton.Clicked += (sender, e) =>
            {
                DateTime currentDateTime = DateTime.Now;
                if (currentDateTime - m_previousClickTime >= s_saveGameButtonThrottleTime)
                {
                    OnGameSaved(EventArgs.Empty);
                    m_previousClickTime = currentDateTime;
                }
            };
            Controls.Add(m_saveGameButton);

            m_exitToMainMenuButton = new Button(null, ContentManager, GraphicsDevice, SpriteBatch)
            {
                X = 0,
                Y = m_saveGameButton.Y + m_saveGameButton.Height + 4,
                Width = c_windowWidth - 7 * 2,
                Height = 32,

                Text = "Exit to Main Menu",
            };
            m_exitToMainMenuButton.Clicked += (sender, e) => OnExitedToMainMenu(EventArgs.Empty);
            Controls.Add(m_exitToMainMenuButton);

            m_exitToDesktopButton = new Button(null, ContentManager, GraphicsDevice, SpriteBatch)
            {
                X = 0,
                Y = m_exitToMainMenuButton.Y + m_exitToMainMenuButton.Height + 4,
                Width = c_windowWidth - 7 * 2,
                Height = 32,

                Text = "Exit to Desktop",
            };
            m_exitToDesktopButton.Clicked += (sender, e) => OnExitedToDesktop(EventArgs.Empty);
            Controls.Add(m_exitToDesktopButton);

            Height = m_exitToDesktopButton.Y + m_exitToDesktopButton.Height + 37;
        }
    }
}
