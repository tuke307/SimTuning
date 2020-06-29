using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using XF.Material.Forms.UI.Dialogs;

namespace SimTuning.mobile.ViewModels.Einstellungen
{
    public class EinstellungenVehiclesViewModel : SimTuning.ViewModels.Einstellungen.VehiclesViewModel
    {
        private readonly MainWindowViewModel mainWindowViewModel;

        public EinstellungenVehiclesViewModel(MainWindowViewModel mainWindowViewModel)
        {
            this.mainWindowViewModel = mainWindowViewModel;

            NewVehicleCommand = new Command(new Action<object>(NewVehicle), CanExecute);
            DeleteVehicleCommand = new Command(new Action<object>(DeleteVehicle), CanExecute);
            SaveVehicleCommand = new Command(new Action<object>(SaveVehicle), CanExecute);
            ShowSaveButtonCommand = new Command(ShowSave);
        }

        private bool CanExecute(object obj)
        {
            Task.Run(async () => await MaterialDialog.Instance.SnackbarAsync(message: "Kaufe die Pro Version um Presets zu ändern",
                                           msDuration: MaterialSnackbar.DurationLong));

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
                Task.Run(async () => await MaterialDialog.Instance.SnackbarAsync(message: "Fehler beim erstellen",
                                           msDuration: MaterialSnackbar.DurationLong));
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
                Task.Run(async () => await MaterialDialog.Instance.SnackbarAsync(message: "Fehler beim löschen",
                                           msDuration: MaterialSnackbar.DurationLong));
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
                Task.Run(async () => await MaterialDialog.Instance.SnackbarAsync(message: "Fehler beim speichern",
                                           msDuration: MaterialSnackbar.DurationLong));
            }
        }
    }
}