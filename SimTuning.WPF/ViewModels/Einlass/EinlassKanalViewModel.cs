using MvvmCross.Commands;

namespace SimTuning.WPF.ViewModels.Einlass
{
    public class EinlassKanalViewModel : SimTuning.Core.ViewModels.Einlass.KanalViewModel
    {
        public EinlassKanalViewModel()
        {
            InsertDataCommand = new MvxCommand(InsertData);
        }
    }
}