using System;

using Microsoft.Xna.Framework;

namespace RuneForge.Core.Input.EventProviders.Interfaces
{
    public interface IMouseEventProvider : IUpdateable
    {
        TimeSpan DoubleClickTimeSpan { get; }

        int DragDistanceThreshold { get; }

        event EventHandler<MouseEventArgs> MouseMoved;

        event EventHandler<MouseEventArgs> MouseButtonDown;

        event EventHandler<MouseEventArgs> MouseButtonPressed;

        event EventHandler<MouseEventArgs> MouseButtonReleased;

        event EventHandler<MouseEventArgs> MouseButtonClicked;

        event EventHandler<MouseEventArgs> MouseButtonDoubleClicked;

        event EventHandler<MouseEventArgs> MouseScrollWheelMoved;

        event EventHandler<MouseEventArgs> MouseHorizontalScrollWheelMoved;

        event EventHandler<MouseEventArgs> MouseDragStarted;

        event EventHandler<MouseEventArgs> MouseDragFinished;

        event EventHandler<MouseEventArgs> MouseDragMoved;

        MouseStateEx GetState();
    }
}
