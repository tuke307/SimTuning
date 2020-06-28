using SimTuning.windows.Business;
using SimTuning.windows.Views;
using System.Windows.Input;

namespace SimTuning.windows.ViewModels.Home
{
    public class HomeViewModel : SimTuning.ViewModels.Home.HomeViewModel
    {
        private readonly MainWindowViewModel mainWindowViewModel;

        public HomeViewModel(MainWindowViewModel mainWindowViewModel)
        {
            this.mainWindowViewModel = mainWindowViewModel;

            OpenInstagramCommand = new ActionCommand(OpenInstagram);
            OpenWebsiteCommand = new ActionCommand(OpenWebsite);
            OpenTwitterCommand = new ActionCommand(OpenTwitter);
            OpenEmailCommand = new ActionCommand(OpenEmail);
            OpenDonateCommand = new ActionCommand(OpenDonate);

            OpenTutorialCommand = new ActionCommand(OpenTutorial);
        }

        //public ICommand OpenInstagramCommand { get; set; }
        //public ICommand OpenWebsiteCommand { get; set; }
        //public ICommand OpenTwitterCommand { get; set; }
        //public ICommand OpenEmailCommand { get; set; }
        //public ICommand OpenDonateCommand { get; set; }
        public ICommand OpenTutorialCommand { get; set; }

        protected override void OpenInstagram(object parameter)
        {
            Business.Functions.GoToSite("https://www.instagram.com/tony.pbt/");
        }

        protected override void OpenWebsite(object parameter)
        {
            Business.Functions.GoToSite("https://www.tuke-productions.de");
        }

        protected override void OpenTwitter(object parameter)
        {
            Business.Functions.GoToSite("https://twitter.com/tonxy_");
        }

        protected override void OpenEmail(object parameter)
        {
            Business.Functions.GoToSite("mailto:tonymeissner70@gmail.com");
        }

        protected override void OpenDonate(object parameter)
        {
            Business.Functions.GoToSite("https://www.paypal.com/cgi-bin/webscr?cmd=_s-xclick&hosted_button_id=PZ5GBAFYMBPWS&source=url");
        }

        private void OpenTutorial(object obj)
        {
            mainWindowViewModel.HomeContent = new Tutorial_main();
        }
    }
}