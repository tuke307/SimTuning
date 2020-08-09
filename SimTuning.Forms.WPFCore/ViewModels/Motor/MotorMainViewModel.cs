using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SimTuning.Forms.WPFCore.ViewModels.Motor
{
    public class MotorMainViewModel : SimTuning.Core.ViewModels.Motor.MainViewModel
    {
        private readonly IMvxNavigationService _navigationService;

        public MotorMainViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService) : base(logProvider, navigationService)
        {
            _navigationService = navigationService;
        }

        public override void Prepare(SimTuning.Core.Models.UserModel _user)
        {
            base.Prepare(_user);
        }

        public override Task Initialize()
        {
            return base.Initialize();
        }

        public override void ViewAppearing()
        {
            _navigationService.Navigate<MotorUmrechnungViewModel>();
            _navigationService.Navigate<MotorSteuerdiagrammViewModel>();
            _navigationService.Navigate<MotorVerdichtungViewModel>();
            _navigationService.Navigate<MotorHubraumViewModel>();

            MotorTabIndex = 0;
        }
    }
}