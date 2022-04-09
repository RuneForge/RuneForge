using System;
using System.Linq;

using Microsoft.Extensions.Options;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

using RuneForge.Core.Input.EventProviders.Configuration;
using RuneForge.Core.Input.EventProviders.Interfaces;

namespace RuneForge.Core.Input.EventProviders
{
    public sealed class KeyboardEventProvider : IKeyboardEventProvider, IUpdateable, IDisposable
    {
        private const char c_emptyCharacter = '\0';

        private static readonly Key[] s_keys = ((Key[])Enum.GetValues(typeof(Key))).Where(key => key != Key.None).ToArray();

        private readonly KeyboardEventProviderConfiguration m_configuration;

        private KeyboardStateEx m_extendedState;

        private KeyboardState m_previousState;

        private Key m_previousKey;

        private TimeSpan m_previousKeyPressedOn;

        private bool m_initialEvent;

        private bool m_disposed;

        public GameWindow GameWindow { get; }

        public bool Enabled { get; }

        public int UpdateOrder { get; }

        public TimeSpan InitialEventDelay => m_configuration.InitialEventDelay;

        public TimeSpan RepeatedEventDelay => m_configuration.RepeatedEventDelay;

        public event EventHandler<KeyboardEventArgs> KeyDown;

        public event EventHandler<KeyboardEventArgs> KeyPressed;

        public event EventHandler<KeyboardEventArgs> KeyReleased;

        public event EventHandler<KeyboardEventArgs> KeyTyped;

        public event EventHandler<KeyboardEventArgs> TextTyped;

        public event EventHandler<EventArgs> EnabledChanged
        {
            add
            {
                throw new NotSupportedException();
            }
            remove
            {
                throw new NotSupportedException();
            }
        }

        public event EventHandler<EventArgs> UpdateOrderChanged
        {
            add
            {
                throw new NotSupportedException();
            }
            remove
            {
                throw new NotSupportedException();
            }
        }

        public KeyboardEventProvider(GameWindow gameWindow, IOptions<KeyboardEventProviderConfiguration> configurationOptions)
        {
            //IL_0016: Unknown result type (might be due to invalid IL or missing references)
            m_configuration = configurationOptions.Value;
            m_previousKey = 0;
            m_previousKeyPressedOn = TimeSpan.Zero;
            m_initialEvent = true;
            m_disposed = false;
            GameWindow = gameWindow ?? throw new ArgumentNullException("gameWindow");
            Enabled = true;
            UpdateOrder = 0;
            GameWindow.TextTyped += OnTextTyped;
        }

        public KeyboardStateEx GetState()
        {
            return m_extendedState;
        }

        public void Update(GameTime gameTime)
        {
            //IL_0013: Unknown result type (might be due to invalid IL or missing references)
            //IL_0018: Unknown result type (might be due to invalid IL or missing references)
            //IL_001a: Unknown result type (might be due to invalid IL or missing references)
            //IL_001c: Unknown result type (might be due to invalid IL or missing references)
            //IL_002c: Unknown result type (might be due to invalid IL or missing references)
            //IL_002d: Unknown result type (might be due to invalid IL or missing references)
            //IL_005e: Unknown result type (might be due to invalid IL or missing references)
            //IL_0063: Unknown result type (might be due to invalid IL or missing references)
            //IL_0073: Unknown result type (might be due to invalid IL or missing references)
            //IL_0089: Unknown result type (might be due to invalid IL or missing references)
            //IL_008b: Unknown result type (might be due to invalid IL or missing references)
            //IL_00db: Unknown result type (might be due to invalid IL or missing references)
            //IL_00e0: Unknown result type (might be due to invalid IL or missing references)
            //IL_00f0: Unknown result type (might be due to invalid IL or missing references)
            //IL_0105: Unknown result type (might be due to invalid IL or missing references)
            //IL_0108: Unknown result type (might be due to invalid IL or missing references)
            //IL_0117: Unknown result type (might be due to invalid IL or missing references)
            //IL_0136: Unknown result type (might be due to invalid IL or missing references)
            //IL_013c: Invalid comparison between Unknown and I4
            //IL_0184: Unknown result type (might be due to invalid IL or missing references)
            //IL_01f9: Unknown result type (might be due to invalid IL or missing references)
            if (!Enabled)
            {
                return;
            }
            KeyboardState state = Keyboard.GetState();
            m_extendedState = new KeyboardStateEx(state, m_previousState);
            m_previousState = state;
            ModifierKeys modifierKeys = m_extendedState.GetModifierKeys();
            foreach (Key key in s_keys.Where(key => m_extendedState.IsKeyDown(key)))
            {
                KeyDown?.Invoke(this, new KeyboardEventArgs(key, modifierKeys, c_emptyCharacter, m_extendedState));
            }  
            foreach (Key item in s_keys.Where((Key key) => m_extendedState.WasKeyJustPressed(key)))
            {
                KeyPressed?.Invoke(this, new KeyboardEventArgs(item, modifierKeys, c_emptyCharacter, m_extendedState));
                m_previousKey = item;
                m_previousKeyPressedOn = gameTime.TotalGameTime;
                m_initialEvent = true;
            }
            foreach (Key item2 in s_keys.Where((Key key) => m_extendedState.WasKeyJustReleased(key)))
            {
                KeyReleased?.Invoke(this, new KeyboardEventArgs(item2, modifierKeys, c_emptyCharacter, m_extendedState));
                if (item2 == m_previousKey)
                {
                    m_previousKey = 0;
                }
            }
            if ((int)m_previousKey > 0)
            {
                if (m_initialEvent && gameTime.TotalGameTime - m_previousKeyPressedOn >= InitialEventDelay)
                {
                    KeyTyped?.Invoke(this, new KeyboardEventArgs(m_previousKey, modifierKeys, c_emptyCharacter, m_extendedState));
                    m_previousKeyPressedOn += InitialEventDelay;
                    m_initialEvent = false;
                }
                else if (!m_initialEvent && gameTime.TotalGameTime - m_previousKeyPressedOn >= RepeatedEventDelay)
                {
                    KeyTyped?.Invoke(this, new KeyboardEventArgs(m_previousKey, modifierKeys, c_emptyCharacter, m_extendedState));
                    m_previousKeyPressedOn += RepeatedEventDelay;
                }
            }
        }

        public void Dispose()
        {
            if (!m_disposed)
            {
                GameWindow.TextTyped -= OnTextTyped;
                m_disposed = true;
            }
        }

        private void OnTextTyped(object sender, TextTypedEventArgs e)
        {
            //IL_000c: Unknown result type (might be due to invalid IL or missing references)
            //IL_0011: Unknown result type (might be due to invalid IL or missing references)
            //IL_0014: Unknown result type (might be due to invalid IL or missing references)
            //IL_0016: Unknown result type (might be due to invalid IL or missing references)
            //IL_002d: Unknown result type (might be due to invalid IL or missing references)
            //IL_002e: Unknown result type (might be due to invalid IL or missing references)
            //IL_003a: Unknown result type (might be due to invalid IL or missing references)
            if (Enabled)
            {
                KeyboardState state = Keyboard.GetState();
                KeyboardStateEx state2 = new KeyboardStateEx(state, m_previousState);
                TextTyped?.Invoke(this, new KeyboardEventArgs(e.Key, state2.GetModifierKeys(), e.Character, state2));
            }
        }
    }
}
