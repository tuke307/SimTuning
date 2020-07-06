using MvvmCross.Commands;

namespace SimTuning.WPF.ViewModels.Einlass
{
    public class EinlassVergaserViewModel : SimTuning.Core.ViewModels.Einlass.VergaserViewModel
    {
        public EinlassVergaserViewModel()
        {
            InsertDataCommand = new MvxCommand(InsertData);
        }
    }
}