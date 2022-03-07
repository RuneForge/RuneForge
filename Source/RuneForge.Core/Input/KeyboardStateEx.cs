using Microsoft.Xna.Framework.Input;

namespace RuneForge.Core.Input
{
    public struct KeyboardStateEx
    {
        public readonly KeyboardState CurrentState;

        public readonly KeyboardState PreviousState;

        public bool CapsLock
        {
            get
            {
                //IL_0001: Unknown result type (might be due to invalid IL or missing references)
                //IL_0006: Unknown result type (might be due to invalid IL or missing references)
                KeyboardState currentState = CurrentState;
                return currentState.CapsLock;
            }
        }

        public bool NumLock
        {
            get
            {
                //IL_0001: Unknown result type (might be due to invalid IL or missing references)
                //IL_0006: Unknown result type (might be due to invalid IL or missing references)
                KeyboardState currentState = CurrentState;
                return currentState.NumLock;
            }
        }

        public KeyboardStateEx(KeyboardState currentState, KeyboardState previousState)
        {
            //IL_0002: Unknown result type (might be due to invalid IL or missing references)
            //IL_0003: Unknown result type (might be due to invalid IL or missing references)
            //IL_0009: Unknown result type (might be due to invalid IL or missing references)
            //IL_000a: Unknown result type (might be due to invalid IL or missing references)
            CurrentState = currentState;
            PreviousState = previousState;
        }

        public bool IsKeyDown(Key key)
        {
            //IL_0001: Unknown result type (might be due to invalid IL or missing references)
            //IL_0006: Unknown result type (might be due to invalid IL or missing references)
            //IL_0009: Unknown result type (might be due to invalid IL or missing references)
            KeyboardState currentState = CurrentState;
            return currentState.IsKeyDown(key);
        }

        public bool IsKeyUp(Key key)
        {
            //IL_0001: Unknown result type (might be due to invalid IL or missing references)
            //IL_0006: Unknown result type (might be due to invalid IL or missing references)
            //IL_0009: Unknown result type (might be due to invalid IL or missing references)
            KeyboardState currentState = CurrentState;
            return currentState.IsKeyUp(key);
        }

        public bool WasKeyJustPressed(Key key)
        {
            //IL_0001: Unknown result type (might be due to invalid IL or missing references)
            //IL_0006: Unknown result type (might be due to invalid IL or missing references)
            //IL_0009: Unknown result type (might be due to invalid IL or missing references)
            //IL_0012: Unknown result type (might be due to invalid IL or missing references)
            //IL_0017: Unknown result type (might be due to invalid IL or missing references)
            //IL_001a: Unknown result type (might be due to invalid IL or missing references)
            KeyboardState val = CurrentState;
            int result;
            if (val.IsKeyDown(key))
            {
                val = PreviousState;
                result = val.IsKeyUp(key) ? 1 : 0;
            }
            else
            {
                result = 0;
            }
            return (byte)result != 0;
        }

        public bool WasKeyJustReleased(Key key)
        {
            //IL_0001: Unknown result type (might be due to invalid IL or missing references)
            //IL_0006: Unknown result type (might be due to invalid IL or missing references)
            //IL_0009: Unknown result type (might be due to invalid IL or missing references)
            //IL_0012: Unknown result type (might be due to invalid IL or missing references)
            //IL_0017: Unknown result type (might be due to invalid IL or missing references)
            //IL_001a: Unknown result type (might be due to invalid IL or missing references)
            KeyboardState val = CurrentState;
            int result;
            if (val.IsKeyUp(key))
            {
                val = PreviousState;
                result = val.IsKeyDown(key) ? 1 : 0;
            }
            else
            {
                result = 0;
            }
            return (byte)result != 0;
        }

        public int GetPressedKeyCount()
        {
            //IL_0001: Unknown result type (might be due to invalid IL or missing references)
            //IL_0006: Unknown result type (might be due to invalid IL or missing references)
            KeyboardState currentState = CurrentState;
            return currentState.GetPressedKeyCount();
        }

        public void GetPressedKeys(Key[] keys)
        {
            //IL_0001: Unknown result type (might be due to invalid IL or missing references)
            //IL_0006: Unknown result type (might be due to invalid IL or missing references)
            KeyboardState currentState = CurrentState;
            currentState.GetPressedKeys(keys);
        }

        public Key[] GetPressedKeys()
        {
            //IL_0001: Unknown result type (might be due to invalid IL or missing references)
            //IL_0006: Unknown result type (might be due to invalid IL or missing references)
            KeyboardState currentState = CurrentState;
            return currentState.GetPressedKeys();
        }

        public ModifierKeys GetModifierKeys()
        {
            ModifierKeys modifierKeys = ModifierKeys.None;
            if (IsKeyDown((Key)164) || IsKeyDown((Key)165))
            {
                modifierKeys |= ModifierKeys.Alt;
            }
            if (IsKeyDown((Key)162) || IsKeyDown((Key)163))
            {
                modifierKeys |= ModifierKeys.Control;
            }
            if (IsKeyDown((Key)160) || IsKeyDown((Key)161))
            {
                modifierKeys |= ModifierKeys.Shift;
            }
            return modifierKeys;
        }
    }
}
