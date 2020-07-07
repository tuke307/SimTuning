using MvvmCross.Commands;
using MvvmCross.Navigation;
using System;
using Xamarin.Essentials;

namespace SimTuning.Forms.UI.ViewModels.Home
{
    public class HomeMainViewModel : SimTuning.Core.ViewModels.Home.HomeViewModel
    {
        private readonly IMvxNavigationService _navigationService;

        public HomeMainViewModel(IMvxNavigationService navigationService)
        {
            _navigationService = navigationService;

            //override commands
            OpenInstagramCommand = new MvxCommand(OpenInstagram);
            OpenWebsiteCommand = new MvxCommand(OpenWebsite);
            OpenTwitterCommand = new MvxCommand(OpenTwitter);
            OpenEmailCommand = new MvxCommand(OpenEmail);
            OpenDonateCommand = new MvxCommand(OpenDonate);
        }

        protected override void OpenInstagram()
        {
            Launcher.OpenAsync(new Uri("https://www.instagram.com/tony.pbt/"));
        }

        protected override void OpenWebsite()
        {
            Launcher.OpenAsync(new Uri("https://www.tuke-productions.de"));
        }

        protected override void OpenTwitter()
        {
            Launcher.OpenAsync(new Uri("https://twitter.com/tonxy_"));
        }

        protected override void OpenEmail()
        {
            Launcher.OpenAsync(new Uri("mailto:tonymeissner70@gmail.com"));
        }

        protected override void OpenDonate()
        {
            Launcher.OpenAsync(new Uri("https://www.paypal.com/cgi-bin/webscr?cmd=_s-xclick&hosted_button_id=PZ5GBAFYMBPWS&source=url"));
        }
    }
}