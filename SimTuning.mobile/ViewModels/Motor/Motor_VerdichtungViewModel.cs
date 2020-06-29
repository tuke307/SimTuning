using Xamarin.Forms;

namespace SimTuning.mobile.ViewModels.Motor
{
    public class Motor_VerdichtungViewModel : SimTuning.ViewModels.Motor.VerdichtungViewModel
    {
        public Motor_VerdichtungViewModel()
        {
            InsertDataCommand = new Command(InsertData);
        }
    }
}