using System;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace SimTuning.mobile.ViewModels.Demo
{
    internal class BuyPro_ViewModel : SimTuning.ViewModels.Demo.BuyProViewModel
    {
        public BuyPro_ViewModel()
        {
            OpenWebsiteCommand = new Command(OpenWebsite);
        }

        protected override void OpenWebsite(object parameter)
        {
            Launcher.OpenAsync(new Uri("https://tuke-productions.de"));
        }
    }
}