using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using XF.Material.Forms.UI.Dialogs;

namespace SimTuning.mobile.ViewModels.Dyno
{
    public class DynoDataViewModel : SimTuning.ViewModels.Dyno.DataViewModel
    {
        public DynoDataViewModel()
        {
            //Commands
            NewDynoCommand = new Command(new Action<object>(NewDyno));
            DeleteDynoCommand = new Command(new Action<object>(DeleteDyno));
            SaveDynoCommand = new Command(new Action<object>(SaveDyno));
            ShowSaveButtonCommand = new Command(ShowSave);
        }

        protected void NewDyno(object obj)
        {
            try
            {
                NewDyno();
            }
            catch
            {
                Task.Run(async () => await MaterialDialog.Instance.SnackbarAsync(message: "Fehler beim erstellen",
                                           msDuration: MaterialSnackbar.DurationLong));
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
                Task.Run(async () => await MaterialDialog.Instance.SnackbarAsync(message: "Fehler beim löschen",
                                           msDuration: MaterialSnackbar.DurationLong));
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
                Task.Run(async () => await MaterialDialog.Instance.SnackbarAsync(message: "Fehler beim speichern",
                                           msDuration: MaterialSnackbar.DurationLong));
            }
        }
    }
}