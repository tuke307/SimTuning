using Xamarin.Forms;

namespace SimTuning.mobile.ViewModels
{
    public class MotorUmrechnungViewModel : SimTuning.ViewModels.Motor.UmrechnungViewModel
    {
        public MotorUmrechnungViewModel()
        {
            InsertDataCommand = new Command(InsertData);
        }
    }
}