using Xamarin.Forms;

namespace SimTuning.mobile.ViewModels.Einlass
{
    public class EinlassVergaserViewModel : SimTuning.ViewModels.Einlass.VergaserViewModel
    {
        public EinlassVergaserViewModel()
        {
            InsertDataCommand = new Command(InsertData);
        }
    }
}