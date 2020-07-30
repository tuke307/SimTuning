using MvvmCross.Logging;
using MvvmCross.Navigation;

namespace SimTuning.WPF.Base.ViewModels.Motor
{
    public class MotorHubraumViewModel : SimTuning.Core.ViewModels.Motor.HubraumViewModel
    {
        public MotorHubraumViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService) : base(logProvider, navigationService)
        {
        }
    }
}