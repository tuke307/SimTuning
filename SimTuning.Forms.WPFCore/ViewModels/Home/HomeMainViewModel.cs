using MaterialDesignThemes.Wpf;
using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using SimTuning.Forms.WPFCore.Business;
using SimTuning.Forms.WPFCore.Views.Dialog;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SimTuning.Forms.WPFCore.ViewModels.Home
{
    public class HomeMainViewModel : SimTuning.Core.ViewModels.Home.HomeViewModel
    {
        public HomeMainViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService) : base(logProvider, navigationService)
        {
            //override commands
            OpenInstagramCommand = new MvxCommand(OpenInstagram);
            OpenWebsiteCommand = new MvxCommand(OpenWebsite);
            OpenTwitterCommand = new MvxCommand(OpenTwitter);
            OpenEmailCommand = new MvxCommand(OpenEmail);
            OpenDonateCommand = new MvxCommand(OpenDonate);
            OpenTutorialCommand = new MvxCommand(OpenTutorial);
        }

        public override void Prepare()
        {
            base.Prepare();
        }

        public override Task Initialize()
        {
            return base.Initialize();
        }

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

        protected override void OpenTutorial()
        {
            Business.Functions.GoToSite("https://simtuning.tuke-productions.de/anleitung/");
        }
    }
}