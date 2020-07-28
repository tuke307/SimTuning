using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using SimTuning.Core.Models;
using System;
using System.Globalization;
using System.Threading.Tasks;
using XF.Material.Forms.UI.Dialogs;

namespace SimTuning.Forms.UI.ViewModels.Einstellungen
{
    public class EinstellungenVehiclesViewModel : SimTuning.Core.ViewModels.Einstellungen.VehiclesViewModel
    {
        public EinstellungenVehiclesViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService) : base(logProvider, navigationService)
        {
            NewVehicleCommand = new MvxCommand(NewVehicle, CanExecute);
            DeleteVehicleCommand = new MvxCommand(DeleteVehicle, CanExecute);
            SaveVehicleCommand = new MvxCommand(SaveVehicle, CanExecute);
        }

        public override void Prepare(UserModel _user)
        {
            base.Prepare(_user);
        }

        public override Task Initialize()
        {
            return base.Initialize();
        }

        private bool CanExecute()
        {
            if (!this.User.LicenseValid)
            {
                Task.Run(() => MaterialDialog.Instance.SnackbarAsync(message: rm.GetString("MES_PRO", CultureInfo.CurrentCulture),
                                          msDuration: MaterialSnackbar.DurationLong));
            }

            return this.User.LicenseValid;
        }

        protected override void NewVehicle()
        {
            try
            {
                base.NewVehicle();
            }
            catch (Exception)
            {
                Task.Run(() => MaterialDialog.Instance.SnackbarAsync(message: "Fehler beim erstellen",
                                          msDuration: MaterialSnackbar.DurationLong));
            }
        }

        protected override void DeleteVehicle()
        {
            try
            {
                base.DeleteVehicle();
            }
            catch (Exception)
            {
                Task.Run(() => MaterialDialog.Instance.SnackbarAsync(message: "Fehler beim löschen",
                                          msDuration: MaterialSnackbar.DurationLong));
            }
        }

        protected override void SaveVehicle()
        {
            try
            {
                base.SaveVehicle();
            }
            catch (Exception)
            {
                Task.Run(() => MaterialDialog.Instance.SnackbarAsync(message: "Fehler beim speichern",
                                          msDuration: MaterialSnackbar.DurationLong));
            }
        }
    }
}