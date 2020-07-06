using MvvmCross.Commands;

namespace SimTuning.Forms.UI.ViewModels.Einlass
{
    public class EinlassVergaserViewModel : SimTuning.Core.ViewModels.Einlass.VergaserViewModel
    {
        public EinlassVergaserViewModel()
        {
            InsertDataCommand = new MvxCommand(InsertData);
        }
    }
}