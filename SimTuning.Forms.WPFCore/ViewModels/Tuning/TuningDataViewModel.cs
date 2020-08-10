// project=SimTuning.Forms.WPFCore, file=TuningDataViewModel.cs, creation=2020:7:31
// Copyright (c) 2020 tuke productions. All rights reserved.
using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using SimTuning.Forms.WPFCore.Business;
using System;

namespace SimTuning.Forms.WPFCore.ViewModels.Tuning
{
    public class TuningDataViewModel : SimTuning.Core.ViewModels.Tuning.DataViewModel
    {
        public TuningDataViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService) : base(logProvider, navigationService)
        {
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
                Functions.ShowSnackbarDialog("Fehler beim erstellen");
            }
        }

        protected void DeleteTuning(object obj)
        {
            if (!base.DeleteTuning())
            {
                Functions.ShowSnackbarDialog("Fehler beim löschen");
            }
        }

        protected void SaveTuning(object obj)
        {
            if (!base.SaveTuning())
            {
                Functions.ShowSnackbarDialog("Fehler beim speichern");
            }
        }
    }
}