using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using System;

namespace SimTuning.WPF.Base.ViewModels.Dyno
{
    public class DynoDataViewModel : SimTuning.Core.ViewModels.Dyno.DataViewModel
    {
        //private MainWindowViewModel mainWindowViewModel;

        public DynoDataViewModel/*MainWindowViewModel mainWindowViewModel*/(IMvxLogProvider logProvider, IMvxNavigationService navigationService) : base(logProvider, navigationService)
        {
            //this.mainWindowViewModel = mainWindowViewModel;

            NewDynoCommand = new MvxCommand(NewDyno);
            DeleteDynoCommand = new MvxCommand(DeleteDyno);
            SaveDynoCommand = new MvxCommand(SaveDyno);
        }

        protected override void NewDyno()
        {
            try
            {
                base.NewDyno();
            }
            catch
            {
                //mainWindowViewModel.NotificationSnackbar.Enqueue("Fehler beim erstellen");
            }
        }

        protected override void DeleteDyno()
        {
            try
            {
                base.DeleteDyno();
            }
            catch
            {
                //mainWindowViewModel.NotificationSnackbar.Enqueue("Fehler beim löschen");
            }
        }

        protected new void SaveDyno()
        {
            if (!base.SaveDyno())
            {
                //mainWindowViewModel.NotificationSnackbar.Enqueue("Fehler beim speichern");
            }
        }
    }
}