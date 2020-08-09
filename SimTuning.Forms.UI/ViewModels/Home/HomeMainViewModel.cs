using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using System;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace SimTuning.Forms.UI.ViewModels.Home
{
    public class HomeMainViewModel : SimTuning.Core.ViewModels.Home.HomeViewModel
    {
        private readonly IMvxNavigationService _navigationService;

        public HomeMainViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService) : base(logProvider, navigationService)
        {
            _navigationService = navigationService;

            //override commands
            OpenInstagramCommand = new MvxCommand(OpenInstagram);
            OpenWebsiteCommand = new MvxCommand(OpenWebsite);
            OpenTwitterCommand = new MvxCommand(OpenTwitter);
            OpenEmailCommand = new MvxCommand(OpenEmail);
            OpenDonateCommand = new MvxCommand(OpenDonate);
            OpenTutorialCommand = new MvxCommand(OpenTutorial);
        }

        /// <summary>
        /// Prepares this instance.
        /// called after construction.
        /// </summary>
        public override void Prepare()
        {
            base.Prepare();
        }

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        /// <returns>Initilisierung.</returns>
        public override Task Initialize()
        {
            return base.Initialize();
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

        protected override void OpenTutorial()
        {
            Launcher.OpenAsync(new Uri("https://simtuning.tuke-productions.de/anleitung/"));
        }
    }
}