using MvvmCross.Commands;

namespace SimTuning.WPF.ViewModels.Motor
{
    public class MotorUmrechnungViewModel : SimTuning.Core.ViewModels.Motor.UmrechnungViewModel
    {
        public MotorUmrechnungViewModel()
        {
            InsertDataCommand = new MvxCommand(InsertData);
        }
    }
}