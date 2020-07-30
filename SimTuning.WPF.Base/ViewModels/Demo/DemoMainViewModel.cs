using System.Threading.Tasks;
using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using SimTuning.WPF.Base.Business;

namespace SimTuning.WPF.Base.ViewModels.Demo
{
    public class DemoMainViewModel : SimTuning.Core.ViewModels.Demo.DemoMainViewModel
    {
        public DemoMainViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService) : base(logProvider, navigationService)
        {
            //override commands
            OpenWebsiteCommand = new MvxCommand(OpenWebsite);
        }

        public override void Prepare()
        {
            base.Prepare();
        }

        public override Task Initialize()
        {
            return base.Initialize();
        }

        protected override void OpenWebsite()
        {
            Functions.GoToSite("https://www.tuke-productions.de");
        }
    }
}