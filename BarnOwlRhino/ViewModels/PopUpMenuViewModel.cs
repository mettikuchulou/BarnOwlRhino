using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace BarnOwlRhino.ViewModels
{
    public class PopUpMenuViewModel : BaseViewModel
    {
        private double leftDirection;
        public double LeftDirection
        {
            get { return leftDirection; }
            set
            {
                leftDirection = value;
                OnPropertyChanged();
            }
        }

        private double leftMagnitude;
        public double LeftMagnitude
        {
            get { return leftMagnitude; }
            set
            {
                leftMagnitude = value;
                OnPropertyChanged();
            }
        }

        private string cursurVisibility;
        public string CursurVisibility
        {
            get { return cursurVisibility; }
            set
            {
                cursurVisibility = value;
                OnPropertyChanged();
            }
        }

        private DispatcherTimer timer;

        public PopUpMenuViewModel() 
        {
            cursurVisibility = "Hidden";

            timer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(10) };
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            XInput.UpdateState();
            LeftDirection = XInput.LeftDirection;
            LeftMagnitude = XInput.LeftMagnitude;

            if (LeftMagnitude > 0.3) { CursurVisibility = "Visible"; }
            else { CursurVisibility = "Hidden"; }
        }
    }
}
