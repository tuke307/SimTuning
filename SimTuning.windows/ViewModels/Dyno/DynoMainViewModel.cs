using SimTuning.ViewModels;
using SimTuning.windows.Business;
using SimTuning.windows.Views.Dyno;
using System.Windows.Input;
using MvvmCross.ViewModels;

namespace SimTuning.windows.ViewModels.Dyno
{
    public class DynoMainViewModel : MvxViewModel
    {
        private readonly MainWindowViewModel mainWindowViewModel;

        public DynoMainViewModel(MainWindowViewModel mainWindowViewModel)
        {
            this.mainWindowViewModel = mainWindowViewModel; //LoadingScreen

            NewContentCommand = new ActionCommand(NewContent);
            NewContent("Datensatz");
        }

        private object _dynoContent;

        public object DynoContent
        {
            get => _dynoContent;
            set => SetProperty(ref _dynoContent, value);
        }

        public ICommand NewContentCommand { get; set; }

        public void NewContent(object parameter)
        {
            switch (parameter)
            {
                case "Datensatz":
                    DynoContent = new DynoDataView(mainWindowViewModel);
                    break;

                case "Audio-Verarbeitung":
                    DynoContent = new DynoAudioView(mainWindowViewModel);
                    break;

                case "Audio-Spectrogram":
                    DynoContent = new DynoSpectrogramView(mainWindowViewModel);
                    break;

                case "Leistungsfeststellung":
                    DynoContent = new DynoDiagnosisView(mainWindowViewModel);
                    break;

                default:
                    break;
            }
        }
    }
}