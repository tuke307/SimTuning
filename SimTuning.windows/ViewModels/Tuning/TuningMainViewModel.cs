using MvvmCross.ViewModels;
using SimTuning.windows.Business;
using SimTuning.windows.Views.Tuning;
using System.Windows.Input;

namespace SimTuning.windows.ViewModels.Tuning
{
    public class TuningMainViewModel : MvxViewModel
    {
        private MainWindowViewModel mainWindowViewModel;

        public TuningMainViewModel(MainWindowViewModel mainWindowViewModel)
        {
            this.mainWindowViewModel = mainWindowViewModel;

            NewContentCommand = new ActionCommand(NewContent);
            NewContent("Datensatz");
        }

        private object _tuningContent;

        public object TuningContent
        {
            get => _tuningContent;
            set => SetProperty(ref _tuningContent, value);
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