using MvvmCross.Commands;
using System;

namespace SimTuning.WPFCore.ViewModels.Dyno
{
    public class DynoDataViewModel : SimTuning.Core.ViewModels.Dyno.DataViewModel
    {
        private MainWindowViewModel mainWindowViewModel;

        public DynoDataViewModel(MainWindowViewModel mainWindowViewModel)
        {
            this.mainWindowViewModel = mainWindowViewModel;

            NewDynoCommand = new MvxCommand(NewDyno);
            DeleteDynoCommand = new MvxCommand(DeleteDyno);
            SaveDynoCommand = new MvxCommand(SaveDyno);
        }

        protected override void NewDyno()
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

        protected override void DeleteDyno()
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

        protected new void SaveDyno()
        {
            if (!base.SaveDyno())
            {
                mainWindowViewModel.NotificationSnackbar.Enqueue("Fehler beim speichern");
            }
        }
    }
}