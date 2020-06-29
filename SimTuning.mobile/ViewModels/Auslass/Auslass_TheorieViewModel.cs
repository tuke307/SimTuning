using Xamarin.Forms;

namespace SimTuning.mobile.ViewModels.Auslass
{
    public class Auslass_TheorieViewModel : SimTuning.ViewModels.Auslass.TheorieViewModel
    {
        public Auslass_TheorieViewModel()
        {
            InsertDataCommand = new Command(InsertData);
        }
    }
}