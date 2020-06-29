using SimTuning.windows.Business;

namespace SimTuning.windows.ViewModels.Motor
{
    public class MotorVerdichtungViewModel : SimTuning.ViewModels.Motor.VerdichtungViewModel
    {
        public MotorVerdichtungViewModel()
        {
            InsertDataCommand = new ActionCommand(InsertData);
        }
    }
}