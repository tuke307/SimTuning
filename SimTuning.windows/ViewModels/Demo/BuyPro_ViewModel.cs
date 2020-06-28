using SimTuning.windows.Business;
using System.Windows.Input;

namespace SimTuning.ViewModels.Demo
{
    public class BuyPro_ViewModel : SimTuning.ViewModels.Demo.BuyProViewModel
    {
        public BuyPro_ViewModel()
        {
            OpenWebsiteCommand = new ActionCommand(OpenWebsite);
        }

        //public ICommand OpenWebsiteCommand { get; set; }

        protected override void OpenWebsite(object parameter)
        {
            Functions.GoToSite("https://www.tuke-productions.de");
        }
    }
}