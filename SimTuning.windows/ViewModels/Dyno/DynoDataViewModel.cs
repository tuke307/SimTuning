using SimTuning.windows.Business;
using System;

namespace SimTuning.windows.ViewModels.Dyno
{
    public class DynoDataViewModel : SimTuning.ViewModels.Dyno.DataViewModel
    {
        private MainWindowViewModel mainWindowViewModel;

        public DynoDataViewModel(MainWindowViewModel mainWindowViewModel)
        {
            this.mainWindowViewModel = mainWindowViewModel;

            NewDynoCommand = new ActionCommand(new Action<object>(NewDyno));
            DeleteDynoCommand = new ActionCommand(new Action<object>(DeleteDyno));
            SaveDynoCommand = new ActionCommand(new Action<object>(SaveDyno));
            ShowSaveButtonCommand = new ActionCommand(ShowSave);
        }

        protected void NewDyno(object obj)
        {
            try
            {
                NewDyno();
            }
            catch
            {
                mainWindowViewModel.NotificationSnackbar.Enqueue("Fehler beim erstellen");
            }
        }

        protected void DeleteDyno(object obj)
        {
            try
            {
                DeleteDyno();
            }
            catch
            {
                mainWindowViewModel.NotificationSnackbar.Enqueue("Fehler beim löschen");
            }
        }

        protected void SaveDyno(object obj)
        {
            try
            {
                SaveDyno();
            }
            catch
            {
                mainWindowViewModel.NotificationSnackbar.Enqueue("Fehler beim speichern");
            }
        }
    }
}