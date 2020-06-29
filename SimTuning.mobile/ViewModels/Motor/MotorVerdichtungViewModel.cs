using Xamarin.Forms;

namespace SimTuning.mobile.ViewModels.Motor
{
    public class MotorVerdichtungViewModel : SimTuning.ViewModels.Motor.VerdichtungViewModel
    {
        public MotorVerdichtungViewModel()
        {
            InsertDataCommand = new Command(InsertData);
        }
    }
}