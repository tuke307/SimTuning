using SimTuning.windows.Business;

namespace SimTuning.ViewModels.Auslass
{
    public class AuslassTheorieViewModel : SimTuning.ViewModels.Auslass.TheorieViewModel
    {
        public AuslassTheorieViewModel()
        {
            InsertDataCommand = new ActionCommand(InsertData);
        }
    }
}