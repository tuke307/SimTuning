using MvvmCross.Commands;
using System;
using System.Threading.Tasks;
using XF.Material.Forms.UI.Dialogs;

namespace SimTuning.Forms.UI.ViewModels.Dyno
{
    public class DynoDataViewModel : SimTuning.Core.ViewModels.Dyno.DataViewModel
    {
        public DynoDataViewModel()
        {
            //Commands
            NewDynoCommand = new MvxCommand(NewDyno);
            DeleteDynoCommand = new MvxCommand(DeleteDyno);
            SaveDynoCommand = new MvxCommand(SaveDyno);
            ShowSaveButtonCommand = new MvxCommand(ShowSave);
        }

        protected void NewDyno()
        {
            try
            {
                base.NewDyno();
            }
            catch
            {
                Task.Run(async () => await MaterialDialog.Instance.SnackbarAsync(message: "Fehler beim erstellen",
                                           msDuration: MaterialSnackbar.DurationLong));
            }
        }

        protected void DeleteDyno()
        {
            try
            {
                base.DeleteDyno();
            }
            catch (Exception)
            {
                Task.Run(async () => await MaterialDialog.Instance.SnackbarAsync(message: "Fehler beim löschen",
                                           msDuration: MaterialSnackbar.DurationLong));
            }
        }

        protected void SaveDyno()
        {
            if (!base.SaveDyno())

            {
                Task.Run(async () => await MaterialDialog.Instance.SnackbarAsync(message: "Fehler beim speichern",
                                           msDuration: MaterialSnackbar.DurationLong));
            }
        }
    }
}