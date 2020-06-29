using System;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace SimTuning.mobile.ViewModels.Demo
{
    internal class DemoMainViewModel : SimTuning.ViewModels.Demo.BuyProViewModel
    {
        public DemoMainViewModel()
        {
            OpenWebsiteCommand = new Command(OpenWebsite);
        }

        protected override void OpenWebsite(object parameter)
        {
            Launcher.OpenAsync(new Uri("https://tuke-productions.de"));
        }
    }
}