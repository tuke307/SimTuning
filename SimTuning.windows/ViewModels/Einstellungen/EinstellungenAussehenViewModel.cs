using MaterialDesignColors;
using SimTuning.windows.Business;
using SimTuning.windows.ViewModels;
using System;
using System.Collections.Generic;

namespace SimTuning.ViewModels
{
    public class EinstellungenAussehenViewModel : SimTuning.ViewModels.Einstellungen.AussehenViewModel
    {
        private ApplicationChanges color = new ApplicationChanges();
        private MainWindowViewModel mainWindowViewModel;

        public EinstellungenAussehenViewModel(MainWindowViewModel mainWindowViewModel)
        {
            this.mainWindowViewModel = mainWindowViewModel;

            Swatches = new SwatchesProvider().Swatches;
            ApplyPrimaryCommand = new ActionCommand(ApplyPrimary, CanExecute);
            ApplyAccentCommand = new ActionCommand(ApplyAccent, CanExecute);
        }

        public IEnumerable<Swatch> Swatches { get; }

        //public ICommand ApplyPrimaryCommand { get; set; }
        //public ICommand ApplyAccentCommand { get; set; }

        protected void ApplyPrimary(object parameter)
        {
            color.SetPrimary(parameter);
        }

        protected void ApplyAccent(object parameter)
        {
            color.SetAccent(parameter);
        }

        protected new void ApplyBaseTheme()
        {
            color.SetBaseTheme(ToogleDarkmode);
        }

        private bool CanExecute(object obj)
        {
            if (!mainWindowViewModel.LicenseValid)
                mainWindowViewModel.NotificationSnackbar.Enqueue("Kaufe die Pro Version um die Farben zu ändern");

            return mainWindowViewModel.LicenseValid;
        }
    }
}