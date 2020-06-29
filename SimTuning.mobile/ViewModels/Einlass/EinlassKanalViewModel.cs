using Xamarin.Forms;

namespace SimTuning.mobile.ViewModels.Einlass
{
    public class EinlassKanalViewModel : SimTuning.ViewModels.Einlass.KanalViewModel
    {
        public EinlassKanalViewModel()
        {
            InsertDataCommand = new Command(InsertData);
        }
    }
}