using Xamarin.Forms;

namespace SimTuning.mobile.ViewModels
{
    public class Motor_UmrechnungViewModel : SimTuning.ViewModels.Motor.UmrechnungViewModel
    {
        public Motor_UmrechnungViewModel()
        {
            InsertDataCommand = new Command(InsertData);
        }
    }
}