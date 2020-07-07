using MvvmCross.Commands;
using SimTuning.WPFCore.Business;

namespace SimTuning.WPFCore.ViewModels.Demo
{
    public class DemoMainViewModel : SimTuning.Core.ViewModels.Demo.DemoMainViewModel
    {
        public DemoMainViewModel()
        {
            //override commands
            OpenWebsiteCommand = new MvxCommand(OpenWebsite);
        }

        protected override void OpenWebsite()
        {
            Functions.GoToSite("https://www.tuke-productions.de");
        }
    }
}