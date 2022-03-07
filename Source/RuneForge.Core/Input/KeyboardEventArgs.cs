using System;

using Microsoft.Xna.Framework.Input;

namespace RuneForge.Core.Input
{
    public class KeyboardEventArgs : EventArgs
    {
        public Key Key { get; }

        public ModifierKeys ModifierKeys { get; }

        public char Character { get; }

        public KeyboardStateEx State { get; }

        public bool Handled { get; private set; }

        public KeyboardEventArgs(Key key, ModifierKeys modifierKeys, char character, KeyboardStateEx state)
        {
            //IL_0009: Unknown result type (might be due to invalid IL or missing references)
            //IL_000a: Unknown result type (might be due to invalid IL or missing references)
            Key = key;
            ModifierKeys = modifierKeys;
            Character = character;
            State = state;
            Handled = false;
        }

        public void Handle()
        {
            Handled = true;
        }
    }
}
