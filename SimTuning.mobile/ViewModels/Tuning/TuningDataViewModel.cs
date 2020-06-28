using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using XF.Material.Forms.UI.Dialogs;

namespace SimTuning.mobile.ViewModels.Tuning
{
    public class TuningDataViewModel : SimTuning.ViewModels.Tuning.DataViewModel
    {
        public TuningDataViewModel()
        {
            //Commands
            NewTuningCommand = new Command(new Action<object>(NewTuning));
            DeleteTuningCommand = new Command(new Action<object>(DeleteTuning));
            SaveTuningCommand = new Command(new Action<object>(SaveTuning));
            ShowSaveButtonCommand = new Command(ShowSave);
        }

        protected void NewTuning(object obj)
        {
            try
            {
                NewTuning();
            }
            catch
            {
                Task.Run(async () => await MaterialDialog.Instance.SnackbarAsync(message: "Fehler beim erstellen",
                                           msDuration: MaterialSnackbar.DurationLong));
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
                Task.Run(async () => await MaterialDialog.Instance.SnackbarAsync(message: "Fehler beim löschen",
                                           msDuration: MaterialSnackbar.DurationLong));
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
                Task.Run(async () => await MaterialDialog.Instance.SnackbarAsync(message: "Fehler beim speichern",
                                           msDuration: MaterialSnackbar.DurationLong));
            }
        }
    }
}