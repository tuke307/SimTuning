using MvvmCross.ViewModels;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SimTuning.ViewModels.Home
{
    public class HomeViewModel : MvxViewModel
    {
        public HomeViewModel()
        {
        }

        public ICommand OpenInstagramCommand { get; set; }
        public ICommand OpenWebsiteCommand { get; set; }
        public ICommand OpenTwitterCommand { get; set; }
        public ICommand OpenEmailCommand { get; set; }
        public ICommand OpenDonateCommand { get; set; }

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

        protected virtual void OpenInstagram(object parameter)
        {
        }

        protected virtual void OpenWebsite(object parameter)
        {
        }

        protected virtual void OpenTwitter(object parameter)
        {
        }

        protected virtual void OpenEmail(object parameter)
        {
        }

        protected virtual void OpenDonate(object parameter)
        {
        }

        #endregion Commands
    }
}