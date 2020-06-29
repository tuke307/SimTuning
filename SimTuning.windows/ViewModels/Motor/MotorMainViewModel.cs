using SimTuning.ViewModels;
using SimTuning.windows.Business;
using SimTuning.windows.Views.Motor;
using System.Windows.Input;
using MvvmCross.ViewModels;

namespace SimTuning.windows.ViewModels.Motor
{
    public class MotorMainViewModel : MvxViewModel
    {
        public MotorMainViewModel()
        {
            NewContentCommand = new ActionCommand(NewContent);
            NewContent("Steuerzeit-Umrechnungen");
        }

        private object _motorContent;

        public object MotorContent
        {
            get => _motorContent;
            set => SetProperty(ref _motorContent, value);
        }

        public ICommand NewContentCommand { get; set; }

        public void NewContent(object parameter)
        {
            switch (parameter)
            {
                case "Steuerzeit-Umrechnungen":
                    MotorContent = new MotorUmrechnungenView();
                    break;

                case "Steuerdiagramm":
                    MotorContent = new MotorSteuerdiagrammView();
                    break;

                case "Verdichtung":
                    MotorContent = new MotorVerdichtungView();
                    break;

                case "Hubraum":
                    MotorContent = new MotorHubraumView();
                    break;

                default:
                    break;
            }
        }
    }
}