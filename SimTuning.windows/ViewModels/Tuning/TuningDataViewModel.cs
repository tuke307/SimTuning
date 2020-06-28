using SimTuning.windows.Business;
using System;

namespace SimTuning.windows.ViewModels.Tuning
{
    public class TuningDataViewModel : SimTuning.ViewModels.Tuning.DataViewModel
    {
        private MainWindowViewModel mainWindowViewModel;

        public TuningDataViewModel(MainWindowViewModel mainWindowViewModel)
        {
            this.mainWindowViewModel = mainWindowViewModel;

            NewTuningCommand = new ActionCommand(new Action<object>(NewTuning));
            DeleteTuningCommand = new ActionCommand(new Action<object>(DeleteTuning));
            SaveTuningCommand = new ActionCommand(new Action<object>(SaveTuning));
            ShowSaveButtonCommand = new ActionCommand(ShowSave);
        }

        protected void NewTuning(object obj)
        {
            try
            {
                NewTuning();
            }
            catch
            {
                mainWindowViewModel.NotificationSnackbar.Enqueue("Fehler beim erstellen");
            }
        }

        protected void DeleteTuning(object obj)
        {
            try
            {
                DeleteTuning();
            }
            catch
            {
                mainWindowViewModel.NotificationSnackbar.Enqueue("Fehler beim löschen");
            }
        }

        protected void SaveTuning(object obj)
        {
            try
            {
                SaveTuning();
            }
            catch
            {
                mainWindowViewModel.NotificationSnackbar.Enqueue("Fehler beim speichern");
            }
        }
    }
}