using SimTuning.windows.Business;
using SimTuning.windows.ViewModels;
using SimTuning.windows.Views.Einstellungen;
using System.Windows.Input;
using MvvmCross.ViewModels;

namespace SimTuning.ViewModels.Einstellungen
{
    public class EinstellungenViewModel : MvxViewModel
    {
        private readonly MainWindowViewModel mainWindowViewModel;

        public EinstellungenViewModel(MainWindowViewModel mainWindowViewModel)
        {
            this.mainWindowViewModel = mainWindowViewModel;

            NewContentCommand = new ActionCommand(NewContent);
            NewContent("Aussehen");
        }

        private object _einstellungsContent;

        public object EinstellungsContent
        {
            get => _einstellungsContent;
            set => SetProperty(ref _einstellungsContent, value);
        }

        public ICommand NewContentCommand { get; set; }

        public void NewContent(object parameter)
        {
            switch (parameter)
            {
                case "Aussehen":
                    EinstellungsContent = new EinstellungenAussehenView(mainWindowViewModel);
                    break;

                case "Presets":
                    EinstellungsContent = new EinstellungenVehiclesView(mainWindowViewModel);
                    break;

                case "Update":
                    EinstellungsContent = new EinstellungenUpdateView(mainWindowViewModel);
                    break;

                case "Konto":
                    EinstellungsContent = new EinstellungenKontoView(mainWindowViewModel);
                    break;

                default:
                    break;
            }
        }
    }
}