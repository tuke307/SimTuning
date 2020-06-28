using System.Windows.Input;

namespace SimTuning.ViewModels.Demo
{
    public class BuyProViewModel : BaseViewModel
    {
        public BuyProViewModel()
        {
            //OpenWebsiteCommand = new ActionCommand(OpenWebsite);
        }

        public ICommand OpenWebsiteCommand { get; set; }

        protected virtual void OpenWebsite(object parameter)
        {
            //Business.Functions.GoToSite("https://www.tuke-productions.de");
        }
    }
}