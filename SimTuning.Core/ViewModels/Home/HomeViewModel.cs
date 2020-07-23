using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System.Threading.Tasks;

namespace SimTuning.Core.ViewModels.Home
{
    public class HomeViewModel : MvxNavigationViewModel<SimTuning.Core.Models.UserModel>
    {
        public SimTuning.Core.Models.UserModel User;

        public HomeViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService) : base(logProvider, navigationService)
        {
        }

        public IMvxCommand OpenInstagramCommand { get; set; }
        public IMvxCommand OpenWebsiteCommand { get; set; }
        public IMvxCommand OpenTwitterCommand { get; set; }
        public IMvxCommand OpenEmailCommand { get; set; }
        public IMvxCommand OpenDonateCommand { get; set; }

        public override void Prepare(SimTuning.Core.Models.UserModel _user)
        {
            // This is the first method to be called after construction

            User = _user;
        }

        public override Task Initialize()
        {
            // Async initialization

            return base.Initialize();
        }

        #region Commands

        protected virtual void OpenInstagram()
        {
        }

        protected virtual void OpenWebsite()
        {
        }

        protected virtual void OpenTwitter()
        {
        }

        protected virtual void OpenEmail()
        {
        }

        protected virtual void OpenDonate()
        {
        }

        #endregion Commands
    }
}