using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace RuneForge.Core.Input
{
    public struct MouseStateEx
    {
        public readonly MouseState CurrentState;

        public readonly MouseState PreviousState;

        public int X
        {
            get
            {
                //IL_0001: Unknown result type (might be due to invalid IL or missing references)
                //IL_0006: Unknown result type (might be due to invalid IL or missing references)
                MouseState currentState = CurrentState;
                return currentState.X;
            }
        }

        public int Y
        {
            get
            {
                //IL_0001: Unknown result type (might be due to invalid IL or missing references)
                //IL_0006: Unknown result type (might be due to invalid IL or missing references)
                MouseState currentState = CurrentState;
                return currentState.Y;
            }
        }

        public int DeltaX
        {
            get
            {
                //IL_0001: Unknown result type (might be due to invalid IL or missing references)
                //IL_0006: Unknown result type (might be due to invalid IL or missing references)
                //IL_000f: Unknown result type (might be due to invalid IL or missing references)
                //IL_0014: Unknown result type (might be due to invalid IL or missing references)
                MouseState val = CurrentState;
                int x = val.X;
                val = PreviousState;
                return x - val.X;
            }
        }

        public int DeltaY
        {
            get
            {
                //IL_0001: Unknown result type (might be due to invalid IL or missing references)
                //IL_0006: Unknown result type (might be due to invalid IL or missing references)
                //IL_000f: Unknown result type (might be due to invalid IL or missing references)
                //IL_0014: Unknown result type (might be due to invalid IL or missing references)
                MouseState val = CurrentState;
                int y = val.Y;
                val = PreviousState;
                return y - val.Y;
            }
        }

        public Point Location => new Point(X, Y);

        public Point DeltaLocation => new Point(DeltaX, DeltaY);

        public ButtonState LeftButton
        {
            get
            {
                //IL_0001: Unknown result type (might be due to invalid IL or missing references)
                //IL_0006: Unknown result type (might be due to invalid IL or missing references)
                //IL_0009: Unknown result type (might be due to invalid IL or missing references)
                MouseState currentState = CurrentState;
                return currentState.LeftButton;
            }
        }

        public ButtonState MiddleButton
        {
            get
            {
                //IL_0001: Unknown result type (might be due to invalid IL or missing references)
                //IL_0006: Unknown result type (might be due to invalid IL or missing references)
                //IL_0009: Unknown result type (might be due to invalid IL or missing references)
                MouseState currentState = CurrentState;
                return currentState.MiddleButton;
            }
        }

        public ButtonState RightButton
        {
            get
            {
                //IL_0001: Unknown result type (might be due to invalid IL or missing references)
                //IL_0006: Unknown result type (might be due to invalid IL or missing references)
                //IL_0009: Unknown result type (might be due to invalid IL or missing references)
                MouseState currentState = CurrentState;
                return currentState.RightButton;
            }
        }

        public ButtonState XButton1
        {
            get
            {
                //IL_0001: Unknown result type (might be due to invalid IL or missing references)
                //IL_0006: Unknown result type (might be due to invalid IL or missing references)
                //IL_0009: Unknown result type (might be due to invalid IL or missing references)
                MouseState currentState = CurrentState;
                return currentState.XButton1;
            }
        }

        public ButtonState XButton2
        {
            get
            {
                //IL_0001: Unknown result type (might be due to invalid IL or missing references)
                //IL_0006: Unknown result type (might be due to invalid IL or missing references)
                //IL_0009: Unknown result type (might be due to invalid IL or missing references)
                MouseState currentState = CurrentState;
                return currentState.XButton2;
            }
        }

        public int ScrollWheelValue
        {
            get
            {
                //IL_0001: Unknown result type (might be due to invalid IL or missing references)
                //IL_0006: Unknown result type (might be due to invalid IL or missing references)
                MouseState currentState = CurrentState;
                return currentState.ScrollWheelValue;
            }
        }

        public int DeltaScrollWheelValue
        {
            get
            {
                //IL_0001: Unknown result type (might be due to invalid IL or missing references)
                //IL_0006: Unknown result type (might be due to invalid IL or missing references)
                //IL_000f: Unknown result type (might be due to invalid IL or missing references)
                //IL_0014: Unknown result type (might be due to invalid IL or missing references)
                MouseState val = CurrentState;
                int scrollWheelValue = val.ScrollWheelValue;
                val = PreviousState;
                return scrollWheelValue - val.ScrollWheelValue;
            }
        }

        public int HorizontalScrollWheelValue
        {
            get
            {
                //IL_0001: Unknown result type (might be due to invalid IL or missing references)
                //IL_0006: Unknown result type (might be due to invalid IL or missing references)
                MouseState currentState = CurrentState;
                return currentState.HorizontalScrollWheelValue;
            }
        }

        public int DeltaHorizontalScrollWheelValue
        {
            get
            {
                //IL_0001: Unknown result type (might be due to invalid IL or missing references)
                //IL_0006: Unknown result type (might be due to invalid IL or missing references)
                //IL_000f: Unknown result type (might be due to invalid IL or missing references)
                //IL_0014: Unknown result type (might be due to invalid IL or missing references)
                MouseState val = CurrentState;
                int horizontalScrollWheelValue = val.HorizontalScrollWheelValue;
                val = PreviousState;
                return horizontalScrollWheelValue - val.HorizontalScrollWheelValue;
            }
        }

        public MouseStateEx(MouseState currentState, MouseState previousState)
        {
            //IL_0002: Unknown result type (might be due to invalid IL or missing references)
            //IL_0003: Unknown result type (might be due to invalid IL or missing references)
            //IL_0009: Unknown result type (might be due to invalid IL or missing references)
            //IL_000a: Unknown result type (might be due to invalid IL or missing references)
            CurrentState = currentState;
            PreviousState = previousState;
        }

        public bool IsButtonPressed(MouseButtons mouseButton)
        {
            //IL_0008: Unknown result type (might be due to invalid IL or missing references)
            //IL_0009: Unknown result type (might be due to invalid IL or missing references)
            //IL_000f: Invalid comparison between Unknown and I4
            return (int)GetButtonState(in CurrentState, mouseButton) == 1;
        }

        public bool IsButtonReleased(MouseButtons mouseButton)
        {
            //IL_0008: Unknown result type (might be due to invalid IL or missing references)
            //IL_0009: Unknown result type (might be due to invalid IL or missing references)
            //IL_000f: Invalid comparison between Unknown and I4
            return GetButtonState(in CurrentState, mouseButton) == 0;
        }

        public bool WasButtonJustPressed(MouseButtons mouseButton)
        {
            //IL_0008: Unknown result type (might be due to invalid IL or missing references)
            //IL_0009: Unknown result type (might be due to invalid IL or missing references)
            //IL_000f: Invalid comparison between Unknown and I4
            //IL_0018: Unknown result type (might be due to invalid IL or missing references)
            //IL_0019: Unknown result type (might be due to invalid IL or missing references)
            //IL_001f: Invalid comparison between Unknown and I4
            return (int)GetButtonState(in CurrentState, mouseButton) == 1 && GetButtonState(in PreviousState, mouseButton) == 0;
        }

        public bool WasButtonJustReleased(MouseButtons mouseButton)
        {
            //IL_0008: Unknown result type (might be due to invalid IL or missing references)
            //IL_0009: Unknown result type (might be due to invalid IL or missing references)
            //IL_0017: Unknown result type (might be due to invalid IL or missing references)
            //IL_0018: Unknown result type (might be due to invalid IL or missing references)
            //IL_001e: Invalid comparison between Unknown and I4
            return GetButtonState(in CurrentState, mouseButton) == 0 && (int)GetButtonState(in PreviousState, mouseButton) == 1;
        }

        private ButtonState GetButtonState(in MouseState mouseState, MouseButtons mouseButton)
        {
            //IL_0002: Unknown result type (might be due to invalid IL or missing references)
            //IL_001e: Unknown result type (might be due to invalid IL or missing references)
            //IL_0020: Unknown result type (might be due to invalid IL or missing references)
            //IL_0036: Expected I4, but got Unknown
            //IL_0038: Unknown result type (might be due to invalid IL or missing references)
            //IL_003a: Invalid comparison between Unknown and I4
            //IL_003e: Unknown result type (might be due to invalid IL or missing references)
            //IL_0041: Invalid comparison between Unknown and I4
            //IL_0046: Unknown result type (might be due to invalid IL or missing references)
            //IL_004b: Unknown result type (might be due to invalid IL or missing references)
            //IL_004e: Unknown result type (might be due to invalid IL or missing references)
            //IL_0053: Unknown result type (might be due to invalid IL or missing references)
            //IL_0057: Unknown result type (might be due to invalid IL or missing references)
            //IL_005c: Unknown result type (might be due to invalid IL or missing references)
            //IL_005f: Unknown result type (might be due to invalid IL or missing references)
            //IL_0064: Unknown result type (might be due to invalid IL or missing references)
            //IL_0068: Unknown result type (might be due to invalid IL or missing references)
            //IL_006d: Unknown result type (might be due to invalid IL or missing references)
            //IL_0070: Unknown result type (might be due to invalid IL or missing references)
            //IL_0075: Unknown result type (might be due to invalid IL or missing references)
            //IL_0079: Unknown result type (might be due to invalid IL or missing references)
            //IL_007e: Unknown result type (might be due to invalid IL or missing references)
            //IL_0081: Unknown result type (might be due to invalid IL or missing references)
            //IL_0086: Unknown result type (might be due to invalid IL or missing references)
            //IL_008a: Unknown result type (might be due to invalid IL or missing references)
            //IL_008f: Unknown result type (might be due to invalid IL or missing references)
            //IL_0092: Unknown result type (might be due to invalid IL or missing references)
            //IL_0097: Unknown result type (might be due to invalid IL or missing references)
            //IL_009b: Unknown result type (might be due to invalid IL or missing references)
            //IL_00a2: Unknown result type (might be due to invalid IL or missing references)
            //IL_00a3: Unknown result type (might be due to invalid IL or missing references)
            //IL_00a6: Unknown result type (might be due to invalid IL or missing references)
            if (!IsSingleBitValue(mouseButton))
            {
                throw new InvalidOperationException("Unable to get state of multiple buttons at once.");
            }
            if (1 == 0)
            {
            }
            MouseState val;
            ButtonState result;
            switch ((int)mouseButton - 1)
            {
                default:
                    if ((int)mouseButton != 8)
                    {
                        if ((int)mouseButton != 16)
                        {
                            goto case 2;
                        }
                        val = mouseState;
                        result = val.XButton2;
                        break;
                    }
                    val = mouseState;
                    result = val.XButton1;
                    break;
                case 0:
                    val = mouseState;
                    result = val.LeftButton;
                    break;
                case 1:
                    val = mouseState;
                    result = val.MiddleButton;
                    break;
                case 3:
                    val = mouseState;
                    result = val.RightButton;
                    break;
                case 2:
                    result = 0;
                    break;
            }
            if (1 == 0)
            {
            }
            return result;
        }

        private bool IsSingleBitValue(MouseButtons mouseButtons)
        {
            //IL_0001: Unknown result type (might be due to invalid IL or missing references)
            //IL_0007: Unknown result type (might be due to invalid IL or missing references)
            //IL_0008: Unknown result type (might be due to invalid IL or missing references)
            //IL_000a: Unknown result type (might be due to invalid IL or missing references)
            //IL_0010: Unknown result type (might be due to invalid IL or missing references)
            //IL_0011: Unknown result type (might be due to invalid IL or missing references)
            //IL_0013: Expected I4, but got Unknown
            int num = ((int)mouseButtons & 0x55555555) + (((int)mouseButtons >> 1) & 0x55555555);
            num = (num & 0x33333333) + ((num >> 2) & 0x33333333);
            num = (num & 0xF0F0F0F) + ((num >> 4) & 0xF0F0F0F);
            num = (num & 0xFF00FF) + ((num >> 8) & 0xFF00FF);
            num = (num & 0xFFFF) + ((num >> 16) & 0xFFFF);
            return num == 1;
        }
    }
}
