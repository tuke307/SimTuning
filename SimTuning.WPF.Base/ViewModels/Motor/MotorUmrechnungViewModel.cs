using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;

namespace SimTuning.WPF.Base.ViewModels.Motor
{
    public class MotorUmrechnungViewModel : SimTuning.Core.ViewModels.Motor.UmrechnungViewModel
    {
        public MotorUmrechnungViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService) : base(logProvider, navigationService)
        {
        }
    }
}