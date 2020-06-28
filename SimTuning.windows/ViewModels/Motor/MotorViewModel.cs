using SimTuning.ViewModels;
using SimTuning.windows.Business;
using SimTuning.windows.Views.Motor;
using System.Windows.Input;

namespace SimTuning.windows.ViewModels.Motor
{
    public class MotorViewModel : BaseViewModel
    {
        public MotorViewModel()
        {
            NewContentCommand = new ActionCommand(NewContent);
            NewContent("Steuerzeit-Umrechnungen");
        }

        public object MotorContent
        {
            get => Get<object>();
            set => Set(value);
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