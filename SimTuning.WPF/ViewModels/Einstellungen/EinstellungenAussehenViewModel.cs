using MaterialDesignColors;
using MvvmCross.Commands;
using SimTuning.WPF.Business;
using System.Collections.Generic;

namespace SimTuning.WPF.ViewModels.Einstellungen
{
    public class EinstellungenAussehenViewModel : SimTuning.Core.ViewModels.Einstellungen.AussehenViewModel
    {
        private ApplicationChanges color = new ApplicationChanges();
        private MainWindowViewModel mainWindowViewModel;

        public EinstellungenAussehenViewModel(MainWindowViewModel mainWindowViewModel)
        {
            this.mainWindowViewModel = mainWindowViewModel;

            Swatches = new SwatchesProvider().Swatches;
            ApplyPrimaryCommand = new MvxCommand<string>(ApplyPrimary, CanExecute);
            ApplyAccentCommand = new MvxCommand<string>(ApplyAccent, CanExecute);
        }

        public IEnumerable<Swatch> Swatches { get; }

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