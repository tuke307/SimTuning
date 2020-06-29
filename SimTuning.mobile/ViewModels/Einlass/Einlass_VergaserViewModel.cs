using Xamarin.Forms;

namespace SimTuning.mobile.ViewModels.Einlass
{
    public class Einlass_VergaserViewModel : SimTuning.ViewModels.Einlass.VergaserViewModel
    {
        public Einlass_VergaserViewModel()
        {
            InsertDataCommand = new Command(InsertData);
        }
    }
}