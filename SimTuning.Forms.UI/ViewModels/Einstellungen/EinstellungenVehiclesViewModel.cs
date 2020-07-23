﻿using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using System;
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

        private bool CanExecute()
        {
            if (!User.LicenseValid)
                Task.Run(() => MaterialDialog.Instance.SnackbarAsync(message: "Kaufe die Pro Version um Presets zu ändern",
                                          msDuration: MaterialSnackbar.DurationLong));

            return User.LicenseValid;
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