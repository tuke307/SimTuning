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
        }

        protected override void NewDyno()
        {
            try
            {
                base.NewDyno();
            }
            catch (Exception)
            {
                Task.Run(async () => await MaterialDialog.Instance.SnackbarAsync(message: "Fehler beim erstellen",
                                           msDuration: MaterialSnackbar.DurationLong).ConfigureAwait(false));
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
                Task.Run(async () => await MaterialDialog.Instance.SnackbarAsync(message: "Fehler beim löschen",
                                           msDuration: MaterialSnackbar.DurationLong).ConfigureAwait(false));
            }
        }

        protected new void SaveDyno()
        {
            if (!base.SaveDyno())
            {
                Task.Run(async () => await MaterialDialog.Instance.SnackbarAsync(message: "Fehler beim speichern",
                                           msDuration: MaterialSnackbar.DurationLong).ConfigureAwait(false));
            }
        }
    }
}