using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System.Threading.Tasks;

namespace SimTuning.Core.ViewModels
{
    public class Menu : MvxNavigationViewModel<SimTuning.Core.Models.UserModel>
    {
        public SimTuning.Core.Models.UserModel User;

        public Menu(IMvxLogProvider logProvider, IMvxNavigationService navigationService) : base(logProvider, navigationService)
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

        public IMvxAsyncCommand ShowHomeCommand { get; set; }
        public IMvxAsyncCommand ShowEinlassCommand { get; set; }
        public IMvxAsyncCommand ShowAuslassCommand { get; set; }
        public IMvxAsyncCommand ShowMotorCommand { get; set; }
        public IMvxAsyncCommand ShowDynoCommand { get; set; }
        public IMvxAsyncCommand ShowTuningCommand { get; set; }
        public IMvxAsyncCommand ShowEinstellungenCommand { get; set; }
    }
}