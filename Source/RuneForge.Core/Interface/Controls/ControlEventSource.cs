using System;

using RuneForge.Core.Input;

namespace RuneForge.Core.Interface.Controls
{
    public class ControlEventSource
    {
        public event EventHandler<KeyboardEventArgs> KeyDown;

        public event EventHandler<KeyboardEventArgs> KeyPressed;

        public event EventHandler<KeyboardEventArgs> KeyReleased;

        public event EventHandler<KeyboardEventArgs> TextTyped;

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

        public void HandleKeyDown(object sender, KeyboardEventArgs e)
        {
            KeyDown?.Invoke(sender, e);
        }

        public void HandleKeyPressed(object sender, KeyboardEventArgs e)
        {
            KeyPressed?.Invoke(sender, e);
        }

        public void HandleKeyReleased(object sender, KeyboardEventArgs e)
        {
            KeyReleased?.Invoke(sender, e);
        }

        public void HandleTextTyped(object sender, KeyboardEventArgs e)
        {
            TextTyped?.Invoke(sender, e);
        }

        public void HandleMouseMoved(object sender, MouseEventArgs e)
        {
            MouseMoved?.Invoke(sender, e);
        }

        public void HandleMouseButtonDown(object sender, MouseEventArgs e)
        {
            MouseButtonDown?.Invoke(sender, e);
        }

        public void HandleMouseButtonPressed(object sender, MouseEventArgs e)
        {
            MouseButtonPressed?.Invoke(sender, e);
        }

        public void HandleMouseButtonReleased(object sender, MouseEventArgs e)
        {
            MouseButtonReleased?.Invoke(sender, e);
        }

        public void HandleMouseButtonClicked(object sender, MouseEventArgs e)
        {
            MouseButtonClicked?.Invoke(sender, e);
        }

        public void HandleMouseButtonDoubleClicked(object sender, MouseEventArgs e)
        {
            MouseButtonDoubleClicked?.Invoke(sender, e);
        }

        public void HandleMouseScrollWheelMoved(object sender, MouseEventArgs e)
        {
            MouseScrollWheelMoved?.Invoke(sender, e);
        }

        public void HandleMouseHorizontalScrollWheelMoved(object sender, MouseEventArgs e)
        {
            MouseHorizontalScrollWheelMoved?.Invoke(sender, e);
        }

        public void HandleMouseDragStarted(object sender, MouseEventArgs e)
        {
            MouseDragStarted?.Invoke(sender, e);
        }

        public void HandleMouseDragFinished(object sender, MouseEventArgs e)
        {
            MouseDragFinished?.Invoke(sender, e);
        }

        public void HandleMouseDragMoved(object sender, MouseEventArgs e)
        {
            MouseDragMoved?.Invoke(sender, e);
        }
    }
}
