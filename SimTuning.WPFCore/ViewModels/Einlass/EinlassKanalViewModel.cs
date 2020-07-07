using MvvmCross.Commands;

namespace SimTuning.WPFCore.ViewModels.Einlass
{
    public class EinlassKanalViewModel : SimTuning.Core.ViewModels.Einlass.KanalViewModel
    {
        public EinlassKanalViewModel()
        {
            InsertDataCommand = new MvxCommand(InsertData);
        }
    }
}