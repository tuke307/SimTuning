using MvvmCross.ViewModels;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SimTuning.ViewModels.Demo
{
    public class BuyProViewModel : MvxViewModel
    {
        public BuyProViewModel()
        {
        }

        public ICommand OpenWebsiteCommand { get; set; }

        public override void Prepare()
        {
            // This is the first method to be called after construction
        }

        public override Task Initialize()
        {
            // Async initialization

            return base.Initialize();
        }

        #region Commands

        protected virtual void OpenWebsite(object parameter)
        {
            //Business.Functions.GoToSite("https://www.tuke-productions.de");
        }

        #endregion Commands
    }
}