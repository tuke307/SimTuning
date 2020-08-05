using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System.Threading.Tasks;

namespace SimTuning.Core.ViewModels
{
    public class Menu : MvxNavigationViewModel<SimTuning.Core.Models.UserModel>
    {
        public SimTuning.Core.Models.UserModel User { get; protected set; }

        public Menu(IMvxLogProvider logProvider, IMvxNavigationService navigationService) : base(logProvider, navigationService)
        {
            OpenMenuVis = true;

            ButtonOpenMenu = new MvxCommand(() => OpenMenuVis = false);
            ButtonCloseMenu = new MvxCommand(() => OpenMenuVis = true);
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

        private bool _openMenuVis;

        public bool OpenMenuVis
        {
            get => _openMenuVis;
            set { SetProperty(ref _openMenuVis, value); }
        }

        public MvxCommand ButtonOpenMenu { get; set; }
        public MvxCommand ButtonCloseMenu { get; set; }
        public IMvxAsyncCommand ShowHomeCommand { get; set; }
        public IMvxAsyncCommand ShowEinlassCommand { get; set; }
        public IMvxAsyncCommand ShowAuslassCommand { get; set; }
        public IMvxAsyncCommand ShowMotorCommand { get; set; }
        public IMvxAsyncCommand ShowDynoCommand { get; set; }
        public IMvxAsyncCommand ShowTuningCommand { get; set; }
        public IMvxAsyncCommand ShowEinstellungenCommand { get; set; }
    }
}