// project=SimTuning.Forms.UI, file=DynoDataViewModel.cs, creation=2020:6:28
// Copyright (c) 2020 tuke productions. All rights reserved.
using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using SimTuning.Forms.UI.Business;
using System;
using System.Threading.Tasks;
using XF.Material.Forms.UI.Dialogs;

namespace SimTuning.Forms.UI.ViewModels.Dyno
{
    public class DynoDataViewModel : SimTuning.Core.ViewModels.Dyno.DataViewModel
    {
        public DynoDataViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService) : base(logProvider, navigationService)
        {
            //Commands
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
            catch (Exception)
            {
                Functions.ShowSnackbarDialog("Fehler beim erstellen");
            }
        }

        protected override void DeleteDyno()
        {
            try
            {
                base.DeleteDyno();
            }
            catch (Exception)
            {
                Functions.ShowSnackbarDialog("Fehler beim löschen");
            }
        }

        protected new void SaveDyno()
        {
            if (!base.SaveDyno())
            {
                Functions.ShowSnackbarDialog("Fehler beim speichern");
            }
        }
    }
}