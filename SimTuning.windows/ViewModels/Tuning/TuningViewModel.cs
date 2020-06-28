using SimTuning.ViewModels;
using SimTuning.windows.Business;
using SimTuning.windows.Views.Tuning;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace SimTuning.windows.ViewModels.Tuning
{
    public class TuningViewModel : BaseViewModel
    {
        private MainWindowViewModel mainWindowViewModel;

        public TuningViewModel(MainWindowViewModel mainWindowViewModel)
        {
            this.mainWindowViewModel = mainWindowViewModel;

            NewContentCommand = new ActionCommand(NewContent);
            NewContent("Datensatz");
        }

        public object TuningContent
        {
            get => Get<object>();
            set => Set(value);
        }

        public ICommand NewContentCommand { get; set; }

        public void NewContent(object parameter)
        {
            switch (parameter)
            {
                case "Datensatz":
                    TuningContent = new TuningDataView(mainWindowViewModel);
                    break;

                case "Eingabewerte":
                    TuningContent = new TuningInputView(mainWindowViewModel);
                    break;

                case "Diagnose":
                    TuningContent = new TuningDiagnosisView(mainWindowViewModel);
                    break;

                default:
                    break;
            }
        }
    }
}