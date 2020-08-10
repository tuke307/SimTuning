// project=SimTuning.Forms.UI, file=TuningDataViewModel.cs, creation=2020:6:28
// Copyright (c) 2020 tuke productions. All rights reserved.
using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using SimTuning.Forms.UI.Business;
using System;
using System.Threading.Tasks;
using XF.Material.Forms.UI.Dialogs;

namespace SimTuning.Forms.UI.ViewModels.Tuning
{
    public class TuningDataViewModel : SimTuning.Core.ViewModels.Tuning.DataViewModel
    {
        public TuningDataViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService) : base(logProvider, navigationService)
        {
            //Commands
            NewTuningCommand = new MvxCommand(NewTuning);
            DeleteTuningCommand = new MvxCommand(DeleteTuning);
            SaveTuningCommand = new MvxCommand(SaveTuning);
        }

        protected override void NewTuning()
        {
            try
            {
                base.NewTuning();
            }
            catch (Exception)
            {
                Functions.ShowSnackbarDialog("Fehler beim erstellen");
            }
        }

        protected new void DeleteTuning()
        {
            if (!base.DeleteTuning())
            {
                Functions.ShowSnackbarDialog("Fehler beim löschen");
            }
        }

        protected new void SaveTuning()
        {
            if (!base.SaveTuning())
            {
                Functions.ShowSnackbarDialog("Fehler beim speichern");
            }
        }
    }
}