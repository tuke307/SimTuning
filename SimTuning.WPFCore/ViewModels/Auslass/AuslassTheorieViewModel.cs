using MvvmCross.Commands;

namespace SimTuning.WPFCore.ViewModels.Auslass
{
    public class AuslassTheorieViewModel : SimTuning.Core.ViewModels.Auslass.TheorieViewModel
    {
        public AuslassTheorieViewModel()
        {
            InsertDataCommand = new MvxCommand(InsertData);
        }
    }
}