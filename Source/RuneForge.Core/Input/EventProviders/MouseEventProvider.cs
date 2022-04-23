using System;

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

using RuneForge.Core.Input.EventProviders.Configuration;
using RuneForge.Core.Input.EventProviders.Interfaces;

namespace RuneForge.Core.Input.EventProviders
{
    public sealed class MouseEventProvider : IMouseEventProvider, IUpdateable
    {
        private const MouseButtons c_noMouseButtonValue = 0;

        private static readonly MouseButtons[] s_mouseButtons = (MouseButtons[])Enum.GetValues(typeof(MouseButtons));

        private readonly MouseEventProviderConfiguration m_configuration;

        private MouseStateEx m_extendedState;

        private MouseState m_previousState;

        private MouseEventArgs m_previousMouseButtonPressedArgs;

        private MouseEventArgs m_previousMouseButtonClickedArgs;

        private TimeSpan m_previousMouseButtonClickedTime;

        private bool m_doubleClickPerformed;

        private bool m_draggingInProgress;

        public bool Enabled { get; }

        public int UpdateOrder { get; }

        public TimeSpan DoubleClickTimeSpan => m_configuration.DoubleClickTimeSpan;

        public int DoubleClickDistanceThreshold => m_configuration.DoubleClickDistanceThreshold;

        public int DragDistanceThreshold => m_configuration.DragDistanceThreshold;

        public event EventHandler<MouseEventArgs> MouseMoved;

        public event EventHandler<MouseEventArgs> MouseButtonDown;

        public event EventHandler<MouseEventArgs> MouseButtonPressed;

        public event EventHandler<MouseEventArgs> MouseButtonReleased;

        public event EventHandler<MouseEventArgs> MouseButtonClicked;

        public event EventHandler<MouseEventArgs> MouseButtonDoubleClicked;

        public event EventHandler<MouseEventArgs> MouseScrollWheelMoved;

        public event EventHandler<MouseEventArgs> MouseHorizontalScrollWheelMoved;

        public event EventHandler<MouseEventArgs> MouseDragStarted;

        public event EventHandler<MouseEventArgs> MouseDragFinished;

        public event EventHandler<MouseEventArgs> MouseDragMoved;

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

        public MouseEventProvider(IOptions<MouseEventProviderConfiguration> configurationOptions)
        {
            m_configuration = configurationOptions.Value;
            m_previousMouseButtonPressedArgs = null;
            m_previousMouseButtonClickedArgs = null;
            m_previousMouseButtonClickedTime = TimeSpan.Zero;
            m_doubleClickPerformed = false;
            m_draggingInProgress = false;
            Enabled = true;
            UpdateOrder = 0;
        }

        public MouseStateEx GetState()
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
            //IL_0040: Unknown result type (might be due to invalid IL or missing references)
            //IL_0045: Unknown result type (might be due to invalid IL or missing references)
            //IL_004f: Unknown result type (might be due to invalid IL or missing references)
            //IL_0058: Unknown result type (might be due to invalid IL or missing references)
            if (Enabled)
            {
                MouseState state = Mouse.GetState();
                m_extendedState = new MouseStateEx(state, m_previousState);
                m_previousState = state;
                MouseButtons[] array = s_mouseButtons;
                foreach (MouseButtons mouseButton in array)
                {
                    ProcessMouseButtonPressedEvents(gameTime, mouseButton);
                    ProcessMouseButtonReleasedEvents(gameTime, mouseButton);
                    ProcessMouseDragEvents(mouseButton);
                }
                if (m_extendedState.DeltaX != 0 || m_extendedState.DeltaY != 0)
                {
                    MouseMoved?.Invoke(this, CreateMouseEventArgs(m_extendedState));
                }
                if (m_extendedState.DeltaScrollWheelValue != 0)
                {
                    MouseScrollWheelMoved?.Invoke(this, CreateMouseEventArgs(m_extendedState));
                }
                if (m_extendedState.DeltaHorizontalScrollWheelValue != 0)
                {
                    MouseHorizontalScrollWheelMoved?.Invoke(this, CreateMouseEventArgs(m_extendedState));
                }
            }
        }

        private void ProcessMouseButtonPressedEvents(GameTime gameTime, MouseButtons mouseButton)
        {
            //IL_0007: Unknown result type (might be due to invalid IL or missing references)
            //IL_001c: Unknown result type (might be due to invalid IL or missing references)
            //IL_0029: Unknown result type (might be due to invalid IL or missing references)
            //IL_0070: Unknown result type (might be due to invalid IL or missing references)
            //IL_0090: Unknown result type (might be due to invalid IL or missing references)
            //IL_0097: Unknown result type (might be due to invalid IL or missing references)
            if (!m_extendedState.IsButtonPressed(mouseButton))
            {
                return;
            }
            MouseEventArgs mouseEventArgs = CreateMouseEventArgs(m_extendedState, mouseButton);
            if (m_extendedState.WasButtonJustPressed(mouseButton))
            {
                MouseButtonPressed?.Invoke(this, mouseEventArgs);
                if (m_draggingInProgress)
                {
                    MouseDragFinished?.Invoke(this, CreateMouseEventArgs(m_extendedState, m_previousMouseButtonPressedArgs.Button));
                    m_draggingInProgress = false;
                }
                if (m_previousMouseButtonClickedArgs != null
                    && mouseButton == m_previousMouseButtonClickedArgs.Button
                    && gameTime.TotalGameTime - m_previousMouseButtonClickedTime <= DoubleClickTimeSpan
                    && CalculateManhattanDistance(mouseEventArgs.Location, m_previousMouseButtonClickedArgs.Location) <= DoubleClickDistanceThreshold)
                {
                    MouseButtonDoubleClicked?.Invoke(this, mouseEventArgs);
                    m_doubleClickPerformed = true;
                }
                m_previousMouseButtonPressedArgs = mouseEventArgs;
                m_previousMouseButtonClickedArgs = null;
                m_previousMouseButtonClickedTime = TimeSpan.Zero;
            }
            MouseButtonDown?.Invoke(this, mouseEventArgs);
        }

        private void ProcessMouseButtonReleasedEvents(GameTime gameTime, MouseButtons mouseButton)
        {
            //IL_0007: Unknown result type (might be due to invalid IL or missing references)
            //IL_001c: Unknown result type (might be due to invalid IL or missing references)
            //IL_002b: Unknown result type (might be due to invalid IL or missing references)
            //IL_0032: Unknown result type (might be due to invalid IL or missing references)
            //IL_006d: Unknown result type (might be due to invalid IL or missing references)
            //IL_0074: Unknown result type (might be due to invalid IL or missing references)
            if (m_extendedState.WasButtonJustReleased(mouseButton))
            {
                MouseEventArgs mouseEventArgs = CreateMouseEventArgs(m_extendedState, mouseButton);
                if (m_draggingInProgress && mouseButton == m_previousMouseButtonPressedArgs.Button)
                {
                    MouseDragFinished?.Invoke(this, mouseEventArgs);
                    m_draggingInProgress = false;
                }
                if (!m_doubleClickPerformed && m_previousMouseButtonPressedArgs != null && mouseButton == m_previousMouseButtonPressedArgs.Button)
                {
                    MouseButtonClicked?.Invoke(this, mouseEventArgs);
                    m_previousMouseButtonClickedArgs = mouseEventArgs;
                    m_previousMouseButtonClickedTime = gameTime.TotalGameTime;
                }
                MouseButtonReleased?.Invoke(this, mouseEventArgs);
                m_doubleClickPerformed = false;
            }
        }

        private void ProcessMouseDragEvents(MouseButtons mouseButton)
        {
            //IL_000f: Unknown result type (might be due to invalid IL or missing references)
            //IL_0014: Unknown result type (might be due to invalid IL or missing references)
            //IL_001d: Unknown result type (might be due to invalid IL or missing references)
            //IL_006c: Unknown result type (might be due to invalid IL or missing references)
            //IL_0081: Unknown result type (might be due to invalid IL or missing references)
            //IL_008c: Unknown result type (might be due to invalid IL or missing references)
            //IL_00be: Unknown result type (might be due to invalid IL or missing references)
            //IL_00c5: Unknown result type (might be due to invalid IL or missing references)
            //IL_00e9: Unknown result type (might be due to invalid IL or missing references)
            if (m_previousMouseButtonPressedArgs != null && m_previousMouseButtonPressedArgs.Button == mouseButton && m_extendedState.IsButtonPressed(mouseButton) && (m_extendedState.DeltaX != 0 || m_extendedState.DeltaY != 0))
            {
                if (m_draggingInProgress)
                {
                    MouseDragMoved?.Invoke(this, CreateMouseEventArgs(m_extendedState, mouseButton));
                }
                else if (CalculateManhattanDistance(m_extendedState.Location, m_previousMouseButtonPressedArgs.Location) > DragDistanceThreshold)
                {
                    m_draggingInProgress = true;
                    MouseDragStarted?.Invoke(this, CreateMouseEventArgs(m_extendedState, mouseButton, m_previousMouseButtonPressedArgs.Location));
                    MouseDragMoved?.Invoke(this, CreateMouseEventArgs(m_extendedState, mouseButton));
                }
            }
        }

        private MouseEventArgs CreateMouseEventArgs(MouseStateEx state)
        {
            return CreateMouseEventArgs(state, c_noMouseButtonValue);
        }

        private MouseEventArgs CreateMouseEventArgs(MouseStateEx state, MouseButtons mouseButton)
        {
            //IL_0003: Unknown result type (might be due to invalid IL or missing references)
            //IL_0006: Unknown result type (might be due to invalid IL or missing references)
            return CreateMouseEventArgs(state, mouseButton, state.Location);
        }

        private MouseEventArgs CreateMouseEventArgs(MouseStateEx state, MouseButtons mouseButton, Point location)
        {
            //IL_0001: Unknown result type (might be due to invalid IL or missing references)
            //IL_0007: Unknown result type (might be due to invalid IL or missing references)
            //IL_000d: Unknown result type (might be due to invalid IL or missing references)
            return new MouseEventArgs(location.X, location.Y, mouseButton, state.ScrollWheelValue, state.HorizontalScrollWheelValue, state);
        }

        private int CalculateManhattanDistance(Point currentLocation, Point previousLocation)
        {
            //IL_0001: Unknown result type (might be due to invalid IL or missing references)
            //IL_0007: Unknown result type (might be due to invalid IL or missing references)
            //IL_0013: Unknown result type (might be due to invalid IL or missing references)
            //IL_0019: Unknown result type (might be due to invalid IL or missing references)
            return Math.Abs(currentLocation.X - previousLocation.X) + Math.Abs(currentLocation.Y - previousLocation.Y);
        }
    }
}
