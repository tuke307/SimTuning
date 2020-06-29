using Xamarin.Forms;

namespace SimTuning.mobile.ViewModels.Einlass
{
    public class Einlass_KanalViewModel : SimTuning.ViewModels.Einlass.KanalViewModel
    {
        public Einlass_KanalViewModel()
        {
            InsertDataCommand = new Command(InsertData);
        }
    }
}