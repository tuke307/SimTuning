using System.Windows.Input;

namespace SimTuning.ViewModels.Home
{
    public class HomeViewModel : BaseViewModel
    {
        //protected readonly MainWindow mainWindow;

        public HomeViewModel(/*MainWindow mainWindowViewModel*/)
        {
            //this.mainWindow = mainWindowViewModel;

            //OpenInstagramCommand = new ActionCommand(OpenInstagram);
            //OpenWebsiteCommand = new ActionCommand(OpenWebsite);
            //OpenTwitterCommand = new ActionCommand(OpenTwitter);
            //OpenEmailCommand = new ActionCommand(OpenEmail);
            //OpenDonateCommand = new ActionCommand(OpenDonate);

            //OpenTutorialCommand = new ActionCommand(OpenTutorial);
        }

        public ICommand OpenInstagramCommand { get; set; }
        public ICommand OpenWebsiteCommand { get; set; }
        public ICommand OpenTwitterCommand { get; set; }
        public ICommand OpenEmailCommand { get; set; }
        public ICommand OpenDonateCommand { get; set; }
        //public ICommand OpenTutorialCommand { get; set; }

        protected virtual void OpenInstagram(object parameter)
        {
            //Business.Functions.GoToSite("https://www.instagram.com/tony.pbt/");
        }

        protected virtual void OpenWebsite(object parameter)
        {
            //Business.Functions.GoToSite("https://www.tuke-productions.de");
        }

        protected virtual void OpenTwitter(object parameter)
        {
            //Business.Functions.GoToSite("https://twitter.com/tonxy_");
        }

        protected virtual void OpenEmail(object parameter)
        {
            //Business.Functions.GoToSite("mailto:tonymeissner70@gmail.com");
        }

        protected virtual void OpenDonate(object parameter)
        {
            //Business.Functions.GoToSite("https://www.paypal.com/cgi-bin/webscr?cmd=_s-xclick&hosted_button_id=PZ5GBAFYMBPWS&source=url");
        }

        //protected virtual void OpenTutorial(object obj)
        //{
        //    //mainWindowViewModel.HomeContent = new Tutorial_main();
        //}
    }
}