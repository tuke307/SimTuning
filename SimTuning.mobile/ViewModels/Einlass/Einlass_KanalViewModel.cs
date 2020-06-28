using Xamarin.Forms;

namespace SimTuning.mobile.ViewModels.Einlass
{
    public class Einlass_KanalViewModel : SimTuning.ViewModels.Einlass.KanalViewModel
    {
        // private EinlassLogic einlass = new EinlassLogic();

        public Einlass_KanalViewModel()
        {
            InsertDataCommand = new Command(InsertData);
        }
    }
}