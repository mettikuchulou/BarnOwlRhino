using BarnOwlRhino.Models;
using Rhino;
using SharpDX.XInput;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BarnOwlRhino.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public readonly XInput XInput;
        public bool IsConnected => XInput.IsConnected;

        public double LeftThumbX => XInput.LeftThumbX;
        public double LeftThumbY => XInput.LeftThumbY;
        public double LeftTrigger => XInput.LeftTrigger;
        public double RightThumbX => XInput.RightThumbX;
        public double RightThumbY => XInput.RightThumbY;
        public double RightTrigger => XInput.RightTrigger;

        public event PropertyChangedEventHandler PropertyChanged;

        public BaseViewModel() 
        {
            XInput = new XInput();
            XInput.PropertyChanged += XInput_PropertyChanged;
            XInput.ButtonPressed += XInput_ButtonPressed;
        }

        private void XInput_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            OnPropertyChanged(e.PropertyName);
        }

        private void XInput_ButtonPressed(object sender, ButtonEventArgs e)
        {
            switch (e.ButtonFlags)
            {
                case GamepadButtonFlags.A:
                    RhinoApp.WriteLine("Button A was pressed.");
                    RhinoApp.ExecuteCommand(RhinoDoc.ActiveDoc, "BarnOwlRhinoCommand");
                    break;
                case GamepadButtonFlags.B:
                    RhinoApp.WriteLine("Button B was pressed.");
                    // Perform action for Button B
                    break;
                    // Add cases for other buttons as needed
            }
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
