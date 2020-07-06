using MvvmCross.Commands;
using MvvmCross.ViewModels;
using SimTuning.WPF.Views.Tuning;
using System.Windows.Input;

namespace SimTuning.WPF.ViewModels.Tuning
{
    public class TuningMainViewModel : MvxViewModel
    {
        private MainWindowViewModel mainWindowViewModel;

        public TuningMainViewModel(MainWindowViewModel mainWindowViewModel)
        {
            this.mainWindowViewModel = mainWindowViewModel;

            NewContentCommand = new MvxCommand<string>(NewContent);
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