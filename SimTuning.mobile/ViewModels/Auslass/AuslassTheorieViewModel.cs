using Xamarin.Forms;

namespace SimTuning.mobile.ViewModels.Auslass
{
    public class AuslassTheorieViewModel : SimTuning.ViewModels.Auslass.TheorieViewModel
    {
        public AuslassTheorieViewModel()
        {
            InsertDataCommand = new Command(InsertData);
        }
    }
}