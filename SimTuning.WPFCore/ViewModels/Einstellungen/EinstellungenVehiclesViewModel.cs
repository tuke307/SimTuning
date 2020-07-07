using MvvmCross.Commands;

namespace SimTuning.WPFCore.ViewModels.Einstellungen
{
    public class EinstellungenVehiclesViewModel : SimTuning.Core.ViewModels.Einstellungen.VehiclesViewModel
    {
        private readonly MainWindowViewModel mainWindowViewModel;

        public EinstellungenVehiclesViewModel(MainWindowViewModel mainWindowViewModel)
        {
            this.mainWindowViewModel = mainWindowViewModel;

            NewVehicleCommand = new MvxCommand(NewVehicle, CanExecute);
            DeleteVehicleCommand = new MvxCommand(DeleteVehicle, CanExecute);
            SaveVehicleCommand = new MvxCommand(SaveVehicle, CanExecute);
        }

        private bool CanExecute()
        {
            if (!User.LicenseValid)
                mainWindowViewModel.NotificationSnackbar.Enqueue("Kaufe die Pro Version um Presets zu ändern");

            return User.LicenseValid;
        }

        protected override void NewVehicle()
        {
            try
            {
                base.NewVehicle();
            }
            catch
            {
                mainWindowViewModel.NotificationSnackbar.Enqueue("Fehler beim erstellen");
            }
        }

        protected override void DeleteVehicle()
        {
            try
            {
                base.DeleteVehicle();
            }
            catch
            {
                mainWindowViewModel.NotificationSnackbar.Enqueue("Fehler beim löschen");
            }
        }

        protected override void SaveVehicle()
        {
            try
            {
                base.SaveVehicle();
            }
            catch
            {
                mainWindowViewModel.NotificationSnackbar.Enqueue("Fehler beim speichern");
            }
        }
    }
}