using System;

using Microsoft.Xna.Framework;

namespace RuneForge.Core.Input.EventProviders.Interfaces
{
    public interface IKeyboardEventProvider : IUpdateable
    {
        TimeSpan InitialEventDelay { get; }

        TimeSpan RepeatedEventDelay { get; }

        event EventHandler<KeyboardEventArgs> KeyDown;

        event EventHandler<KeyboardEventArgs> KeyPressed;

        event EventHandler<KeyboardEventArgs> KeyReleased;

        event EventHandler<KeyboardEventArgs> TextTyped;

        KeyboardStateEx GetState();
    }
}
