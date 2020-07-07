using MvvmCross.Commands;
using System;

namespace SimTuning.WPFCore.ViewModels.Tuning
{
    public class TuningDataViewModel : SimTuning.Core.ViewModels.Tuning.DataViewModel
    {
        private MainWindowViewModel mainWindowViewModel;

        public TuningDataViewModel(MainWindowViewModel mainWindowViewModel)
        {
            this.mainWindowViewModel = mainWindowViewModel;

            NewTuningCommand = new MvxCommand<string>(new Action<object>(NewTuning));
            DeleteTuningCommand = new MvxCommand<string>(new Action<object>(DeleteTuning));
            SaveTuningCommand = new MvxCommand<string>(new Action<object>(SaveTuning));
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
            if (!base.DeleteTuning())
            {
                mainWindowViewModel.NotificationSnackbar.Enqueue("Fehler beim löschen");
            }
        }

        protected void SaveTuning(object obj)
        {
            if (!base.SaveTuning())
            {
                mainWindowViewModel.NotificationSnackbar.Enqueue("Fehler beim speichern");
            }
        }
    }
}