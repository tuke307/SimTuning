using MvvmCross.Commands;
using MvvmCross.ViewModels;
using System.Windows.Input;

namespace SimTuning.WPFCore.ViewModels.Motor
{
    public class MotorMainViewModel : SimTuning.Core.ViewModels.Motor.MainViewModel
    {
        public MotorMainViewModel()
        {
            //NewContentCommand = new MvxCommand<string>(NewContent);
            //NewContent("Steuerzeit-Umrechnungen");
        }

        private object _motorContent;

        public object MotorContent
        {
            get => _motorContent;
            set => SetProperty(ref _motorContent, value);
        }

        public ICommand NewContentCommand { get; set; }

        //public void NewContent(object parameter)
        //{
        //    switch (parameter)
        //    {
        //        case "Steuerzeit-Umrechnungen":
        //            MotorContent = new MotorUmrechnungenView();
        //            break;

        //        case "Steuerdiagramm":
        //            MotorContent = new MotorSteuerdiagrammView();
        //            break;

        //        case "Verdichtung":
        //            MotorContent = new MotorVerdichtungView();
        //            break;

        //        case "Hubraum":
        //            MotorContent = new MotorHubraumView();
        //            break;

        //        default:
        //            break;
        //    }
        //}
    }
}