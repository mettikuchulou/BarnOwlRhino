using System;
using System.Windows;
using System.Windows.Threading;
using BarnOwlRhino.ViewModels;
using Rhino;
using Rhino.UI;

namespace BarnOwlRhino.Views
{
    /// <summary>
    /// Interaction logic for RadialMenu.xaml
    /// </summary>
    public partial class PopupMenu : Window
    {
        public PopupMenu()
        {
            Loaded += RadialMenu_Loaded;
            Closing += RadialMenu_Closing;

            PopUpMenuViewModel viewModel = new PopUpMenuViewModel();
            this.DataContext = viewModel;

            InitializeComponent();
        }

        private void RadialMenu_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            RhinoApp.WriteLine("Radial Menu is closing...");
        }

        private void RadialMenu_Loaded(object sender, RoutedEventArgs e)
        {

        }

    }
}
