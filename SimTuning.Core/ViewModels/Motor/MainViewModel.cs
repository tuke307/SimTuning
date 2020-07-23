using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System.Threading.Tasks;

namespace SimTuning.Core.ViewModels.Motor
{
    public class MainViewModel : MvxNavigationViewModel<SimTuning.Core.Models.UserModel>
    {
        public SimTuning.Core.Models.UserModel User;

        public MainViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService) : base(logProvider, navigationService)
        {
        }

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
    }
}