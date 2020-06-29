using SimTuning.windows.Business;
using System.Windows.Input;

namespace SimTuning.ViewModels.Demo
{
    public class DemoMainViewModel : SimTuning.ViewModels.Demo.BuyProViewModel
    {
        public DemoMainViewModel()
        {
            OpenWebsiteCommand = new ActionCommand(OpenWebsite);
        }

        protected override void OpenWebsite(object parameter)
        {
            Functions.GoToSite("https://www.tuke-productions.de");
        }
    }
}