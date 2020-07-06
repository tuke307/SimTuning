using MvvmCross.Commands;
using System;

namespace SimTuning.WPF.ViewModels.Dyno
{
    public class DynoDataViewModel : SimTuning.Core.ViewModels.Dyno.DataViewModel
    {
        private MainWindowViewModel mainWindowViewModel;

        public DynoDataViewModel(MainWindowViewModel mainWindowViewModel)
        {
            this.mainWindowViewModel = mainWindowViewModel;

            NewDynoCommand = new MvxCommand<string>(new Action<object>(NewDyno));
            DeleteDynoCommand = new MvxCommand<string>(new Action<object>(DeleteDyno));
            SaveDynoCommand = new MvxCommand<string>(new Action<object>(SaveDyno));
            ShowSaveButtonCommand = new MvxCommand(ShowSave);
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