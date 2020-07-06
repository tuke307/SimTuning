using MvvmCross.Commands;

namespace SimTuning.WPF.ViewModels.Motor
{
    public class MotorVerdichtungViewModel : SimTuning.Core.ViewModels.Motor.VerdichtungViewModel
    {
        public MotorVerdichtungViewModel()
        {
            InsertDataCommand = new MvxCommand(InsertData);
        }
    }
}