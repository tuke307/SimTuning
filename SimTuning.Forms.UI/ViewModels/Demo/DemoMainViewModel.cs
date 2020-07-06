using MvvmCross.Commands;
using MvvmCross.Navigation;
using System;
using Xamarin.Essentials;

namespace SimTuning.Forms.UI.ViewModels.Demo
{
    public class DemoMainViewModel : SimTuning.Core.ViewModels.Demo.BuyProViewModel
    {
        private readonly IMvxNavigationService _navigationService;

        public DemoMainViewModel(IMvxNavigationService navigationService)
        {
            _navigationService = navigationService;
            OpenWebsiteCommand = new MvxCommand(OpenWebsite);
        }

        protected override void OpenWebsite()
        {
            Launcher.OpenAsync(new Uri("https://tuke-productions.de"));
        }
    }
}