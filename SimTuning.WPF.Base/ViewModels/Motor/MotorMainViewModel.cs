using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SimTuning.WPF.Base.ViewModels.Motor
{
    public class MotorMainViewModel : SimTuning.Core.ViewModels.Motor.MainViewModel
    {
        private readonly IMvxNavigationService _navigationService;
        private bool _firstTime = true;

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

        private Task ShowInitialViewModels()
        {
            var tasks = new List<Task>
            {
                _navigationService.Navigate<MotorUmrechnungViewModel>(),
                _navigationService.Navigate<MotorSteuerdiagrammViewModel>(),
                _navigationService.Navigate<MotorVerdichtungViewModel>(),
                _navigationService.Navigate<MotorHubraumViewModel>()
            };
            return Task.WhenAll(tasks);
        }

        public override void ViewAppearing()
        {
            if (_firstTime)
            {
                ShowInitialViewModels();
                _firstTime = false;

                MotorTabIndex = 0;
            }
        }
    }
}