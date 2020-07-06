using MvvmCross.Commands;
using MvvmCross.ViewModels;
using SimTuning.WPF.Views.Motor;
using System.Windows.Input;

namespace SimTuning.WPF.ViewModels.Motor
{
    public class MotorMainViewModel : MvxViewModel
    {
        public MotorMainViewModel()
        {
            NewContentCommand = new MvxCommand<string>(NewContent);
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