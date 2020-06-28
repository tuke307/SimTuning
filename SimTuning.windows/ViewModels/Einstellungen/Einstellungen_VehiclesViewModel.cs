﻿using SimTuning.windows.Business;
using SimTuning.windows.ViewModels;
using System;

namespace SimTuning.ViewModels.Einstellungen
{
    public class Einstellungen_VehiclesViewModel : SimTuning.ViewModels.Einstellungen.VehiclesViewModel
    {
        private readonly MainWindowViewModel mainWindowViewModel;

        public Einstellungen_VehiclesViewModel(MainWindowViewModel mainWindowViewModel)
        {
            this.mainWindowViewModel = mainWindowViewModel;

            NewVehicleCommand = new ActionCommand(new Action<object>(NewVehicle), CanExecute);
            DeleteVehicleCommand = new ActionCommand(new Action<object>(DeleteVehicle), CanExecute);
            SaveVehicleCommand = new ActionCommand(new Action<object>(SaveVehicle), CanExecute);
            ShowSaveButtonCommand = new ActionCommand(ShowSave);
        }

        private bool CanExecute(object obj)
        {
            if (!mainWindowViewModel.LICENSE_VALID)
                mainWindowViewModel.NotificationSnackbar.Enqueue("Kaufe die Pro Version um Presets zu ändern");

            return mainWindowViewModel.LICENSE_VALID;
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