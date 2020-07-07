using MvvmCross.Commands;
using MvvmCross.ViewModels;
using System.Windows.Input;

namespace SimTuning.WPFCore.ViewModels.Einstellungen
{
    public class EinstellungenMainViewModel : SimTuning.Core.ViewModels.Einstellungen.MainViewModel
    {
        private readonly MainWindowViewModel mainWindowViewModel;

        public EinstellungenMainViewModel(MainWindowViewModel mainWindowViewModel)
        {
            this.mainWindowViewModel = mainWindowViewModel;

            //NewContentCommand = new MvxCommand<string>(NewContent);
            //NewContent("Aussehen");
        }

        private object _einstellungsContent;

        public object EinstellungsContent
        {
            get => _einstellungsContent;
            set => SetProperty(ref _einstellungsContent, value);
        }

        public ICommand NewContentCommand { get; set; }

        //public void NewContent(object parameter)
        //{
        //    switch (parameter)
        //    {
        //        case "Aussehen":
        //            EinstellungsContent = new EinstellungenAussehenView(mainWindowViewModel);
        //            break;

        //        case "Presets":
        //            EinstellungsContent = new EinstellungenVehiclesView(mainWindowViewModel);
        //            break;

        //        case "Update":
        //            EinstellungsContent = new EinstellungenUpdateView(mainWindowViewModel);
        //            break;

        //        case "Konto":
        //            EinstellungsContent = new EinstellungenKontoView(mainWindowViewModel);
        //            break;

        //        default:
        //            break;
        //    }
        //}
    }
}