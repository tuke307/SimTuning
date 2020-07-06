using MvvmCross.Commands;
using SimTuning.WPF.Views;
using System.Windows.Input;

namespace SimTuning.WPF.ViewModels.Home
{
    public class HomeMainViewModel : SimTuning.Core.ViewModels.Home.HomeViewModel
    {
        private readonly MainWindowViewModel mainWindowViewModel;

        public HomeMainViewModel(MainWindowViewModel mainWindowViewModel)
        {
            this.mainWindowViewModel = mainWindowViewModel;

            OpenInstagramCommand = new MvxCommand(OpenInstagram);
            OpenWebsiteCommand = new MvxCommand(OpenWebsite);
            OpenTwitterCommand = new MvxCommand(OpenTwitter);
            OpenEmailCommand = new MvxCommand(OpenEmail);
            OpenDonateCommand = new MvxCommand(OpenDonate);

            OpenTutorialCommand = new MvxCommand(OpenTutorial);
        }

        public ICommand OpenTutorialCommand { get; set; }

        protected override void OpenInstagram()
        {
            Business.Functions.GoToSite("https://www.instagram.com/tony.pbt/");
        }

        protected override void OpenWebsite()
        {
            Business.Functions.GoToSite("https://www.tuke-productions.de");
        }

        protected override void OpenTwitter()
        {
            Business.Functions.GoToSite("https://twitter.com/tonxy_");
        }

        protected override void OpenEmail()
        {
            Business.Functions.GoToSite("mailto:tonymeissner70@gmail.com");
        }

        protected override void OpenDonate()
        {
            Business.Functions.GoToSite("https://www.paypal.com/cgi-bin/webscr?cmd=_s-xclick&hosted_button_id=PZ5GBAFYMBPWS&source=url");
        }

        private void OpenTutorial()
        {
            mainWindowViewModel.HomeContent = new Tutorial_main();
        }
    }
}