using SharpDX.XInput;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BarnOwlRhino.Models
{
    public class XInput
    {
        public Controller controller;
        private State previousState;
        private State currentState;
        private bool isConnected;

        private double leftThumbX;
        private double leftThumbY;
        private double leftTrigger;
        private double rightThumbX;
        private double rightThumbY;
        private double rightTrigger;

        private double leftDirection;
        private double leftMagnitude;
        private double rightDirection;

        public XInput()
        {
            controller = new Controller(UserIndex.One);
            UpdateState();
        }

        public bool IsConnected
        {
            get => isConnected;
            private set
            {
                isConnected = value;
            }
        }

        public State GamepadState
        {
            get => currentState;
            private set
            {
                currentState = value;
            }
        }

        public void UpdateState()
        {
            IsConnected = controller.IsConnected;

            if (IsConnected)
            {
                previousState = currentState;
                currentState = controller.GetState();

                LeftThumbX = currentState.Gamepad.LeftThumbX;
                LeftThumbY = currentState.Gamepad.LeftThumbY;
                LeftTrigger = currentState.Gamepad.LeftTrigger;
                RightThumbX = currentState.Gamepad.RightThumbX;
                RightThumbY = currentState.Gamepad.RightThumbY;
                RightTrigger = currentState.Gamepad.RightTrigger;

                LeftDirection = Math.Atan2(LeftThumbX, LeftThumbY);
                LeftMagnitude = Math.Pow(LeftThumbX, 2) + Math.Pow(LeftThumbY, 2);
                RightDirection = Math.Atan2(RightThumbX, RightThumbY);

                DetectButtonPresses();
            }
        }

        private void DetectButtonPresses()
        {
            if (previousState.Gamepad.Buttons != currentState.Gamepad.Buttons)
            {
                var pressedButtons = currentState.Gamepad.Buttons & ~previousState.Gamepad.Buttons;
                if (pressedButtons != 0)
                {
                    ButtonPressed?.Invoke(this, new ButtonEventArgs(pressedButtons));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler<ButtonEventArgs> ButtonPressed;

        public double LeftDirection
        {
            get => leftDirection;
            set
            {
                leftDirection = value;
                leftDirection = (leftDirection > 0 ? leftDirection : (2 * Math.PI + leftDirection)) * 360 / (2 * Math.PI);
                leftDirection = Math.Round(leftDirection, 2);
            }
        }
        public double LeftMagnitude
        {
            get => leftMagnitude;
            set
            {
                leftMagnitude = value;
                leftMagnitude = Math.Round(leftMagnitude, 2);
            }
        }

        public double LeftThumbX
        {
            get => leftThumbX;
            set
            {
                leftThumbX = value;
                leftThumbX = 2 * (leftThumbX - short.MinValue) / (short.MaxValue - short.MinValue) - 1;
                leftThumbX = Math.Round(leftThumbX, 2);
            }
        }

        public double LeftThumbY
        {
            get => leftThumbY;
            set
            {
                leftThumbY = value;
                leftThumbY = 2 * (leftThumbY - short.MinValue) / (short.MaxValue - short.MinValue) - 1;
                leftThumbY = Math.Round(leftThumbY, 2);
            }
        }

        public double LeftTrigger
        {
            get => leftTrigger;
            set
            {
                leftTrigger = value;
                leftTrigger = leftTrigger / byte.MaxValue;
                leftTrigger = Math.Round(leftTrigger, 2);
            }
        }

        public double RightDirection
        {
            get => rightDirection;
            set
            {
                rightDirection = value;
                rightDirection = (rightDirection > 0 ? rightDirection : (2 * Math.PI + rightDirection)) * 360 / (2 * Math.PI);
                rightDirection = Math.Round(rightDirection, 2);
            }
        }


        public double RightThumbX
        {
            get => rightThumbX;
            set
            {
                rightThumbX = value;
                rightThumbX = 2 * (rightThumbX - short.MinValue) / (short.MaxValue - short.MinValue) - 1;
                rightThumbX = Math.Round(rightThumbX, 1);
            }
        }

        public double RightThumbY
        {
            get => rightThumbY;
            set
            {
                rightThumbY = value;
                rightThumbY = 2 * (rightThumbY - short.MinValue) / (short.MaxValue - short.MinValue) - 1;
                rightThumbY = Math.Round(rightThumbY, 1);
            }
        }

        public double RightTrigger
        {
            get => rightTrigger;
            set
            {
                rightTrigger = value;
                rightTrigger = rightTrigger / byte.MaxValue;
                rightTrigger = Math.Round(rightTrigger, 2);
            }
        }

    }

    public class ButtonEventArgs : EventArgs
    {
        public GamepadButtonFlags ButtonFlags { get; }

        public ButtonEventArgs(GamepadButtonFlags buttonFlags) 
        {
            ButtonFlags = buttonFlags;
        }
    }
}
