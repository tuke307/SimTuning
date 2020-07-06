using MvvmCross.Commands;
using System;

namespace SimTuning.WPF.ViewModels.Einstellungen
{
    public class EinstellungenVehiclesViewModel : SimTuning.Core.ViewModels.Einstellungen.VehiclesViewModel
    {
        private readonly MainWindowViewModel mainWindowViewModel;

        public EinstellungenVehiclesViewModel(MainWindowViewModel mainWindowViewModel)
        {
            this.mainWindowViewModel = mainWindowViewModel;

            NewVehicleCommand = new MvxCommand<string>(new Action<object>(NewVehicle), CanExecute);
            DeleteVehicleCommand = new MvxCommand<string>(new Action<object>(DeleteVehicle), CanExecute);
            SaveVehicleCommand = new MvxCommand<string>(new Action<object>(SaveVehicle), CanExecute);
            ShowSaveButtonCommand = new MvxCommand(ShowSave);
        }

        private bool CanExecute(object obj)
        {
            if (!mainWindowViewModel.LicenseValid)
                mainWindowViewModel.NotificationSnackbar.Enqueue("Kaufe die Pro Version um Presets zu ändern");

            return mainWindowViewModel.LicenseValid;
        }

        protected void NewVehicle(object obj)
        {
            try
            {
                NewVehicle();
            }
            catch
            {
                mainWindowViewModel.NotificationSnackbar.Enqueue("Fehler beim erstellen");
            }
        }

        protected void DeleteVehicle(object obj)
        {
            try
            {
                DeleteVehicle();
            }
            catch
            {
                mainWindowViewModel.NotificationSnackbar.Enqueue("Fehler beim löschen");
            }
        }

        protected void SaveVehicle(object obj)
        {
            try
            {
                SaveVehicle();
            }
            catch
            {
                mainWindowViewModel.NotificationSnackbar.Enqueue("Fehler beim speichern");
            }
        }
    }
}