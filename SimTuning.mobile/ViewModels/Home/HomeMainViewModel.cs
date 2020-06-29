using System;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace SimTuning.mobile.ViewModels.Home
{
    public class HomeMainViewModel : SimTuning.ViewModels.Home.HomeViewModel
    {
        public HomeMainViewModel()
        {
            OpenInstagramCommand = new Command(OpenInstagram);
            OpenWebsiteCommand = new Command(OpenWebsite);
            OpenTwitterCommand = new Command(OpenTwitter);
            OpenEmailCommand = new Command(OpenEmail);
            OpenDonateCommand = new Command(OpenDonate);
        }

        protected override void OpenInstagram(object parameter)
        {
            Launcher.OpenAsync(new Uri("https://www.instagram.com/tony.pbt/"));
        }

        protected override void OpenWebsite(object parameter)
        {
            Launcher.OpenAsync(new Uri("https://www.tuke-productions.de"));
        }

        protected override void OpenTwitter(object parameter)
        {
            Launcher.OpenAsync(new Uri("https://twitter.com/tonxy_"));
        }

        protected override void OpenEmail(object parameter)
        {
            Launcher.OpenAsync(new Uri("mailto:tonymeissner70@gmail.com"));
        }

        protected override void OpenDonate(object parameter)
        {
            Launcher.OpenAsync(new Uri("https://www.paypal.com/cgi-bin/webscr?cmd=_s-xclick&hosted_button_id=PZ5GBAFYMBPWS&source=url"));
        }
    }
}