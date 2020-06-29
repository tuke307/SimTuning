using SimTuning.windows.Business;

namespace SimTuning.ViewModels.Auslass
{
    public class Auslass_TheorieViewModel : SimTuning.ViewModels.Auslass.TheorieViewModel
    {
        public Auslass_TheorieViewModel()
        {
            InsertDataCommand = new ActionCommand(InsertData);
        }
    }
}