using MvvmCross.Commands;
using System;
using System.Threading.Tasks;
using XF.Material.Forms.UI.Dialogs;

namespace SimTuning.Forms.UI.ViewModels.Tuning
{
    public class TuningDataViewModel : SimTuning.Core.ViewModels.Tuning.DataViewModel
    {
        public TuningDataViewModel()
        {
            //Commands
            NewTuningCommand = new MvxCommand(NewTuning);
            DeleteTuningCommand = new MvxCommand(DeleteTuning);
            SaveTuningCommand = new MvxCommand(SaveTuning);
            ShowSaveButtonCommand = new MvxCommand(ShowSave);
        }

        protected override void NewTuning()
        {
            try
            {
                base.NewTuning();
            }
            catch (Exception)
            {
                Task.Run(() => MaterialDialog.Instance.SnackbarAsync(message: "Fehler beim erstellen",
                                          msDuration: MaterialSnackbar.DurationLong));
            }
        }

        protected new void DeleteTuning()
        {
            if (!base.DeleteTuning())
            {
                Task.Run(() => MaterialDialog.Instance.SnackbarAsync(message: "Fehler beim löschen",
                                           msDuration: MaterialSnackbar.DurationLong));
            }
        }

        protected new void SaveTuning()
        {
            if (!base.SaveTuning())
            {
                Task.Run(() => MaterialDialog.Instance.SnackbarAsync(message: "Fehler beim speichern",
                                           msDuration: MaterialSnackbar.DurationLong));
            }
        }
    }
}