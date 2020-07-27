using MaterialDesignColors;
using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using SimTuning.Core.Models;
using SimTuning.WPFCore.Business;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SimTuning.WPFCore.ViewModels.Einstellungen
{
    public class EinstellungenAussehenViewModel : SimTuning.Core.ViewModels.Einstellungen.AussehenViewModel
    {
        private readonly ApplicationChanges color = new ApplicationChanges();
        //private MainWindowViewModel mainWindowViewModel;

        public EinstellungenAussehenViewModel/*MainWindowViewModel mainWindowViewModel*/(IMvxLogProvider logProvider, IMvxNavigationService navigationService) : base(logProvider, navigationService)
        {
            //this.mainWindowViewModel = mainWindowViewModel;

            Swatches = new SwatchesProvider().Swatches;
            ApplyPrimaryCommand = new MvxCommand(ApplyPrimary, CanExecute);
            ApplyAccentCommand = new MvxCommand(ApplyAccent, CanExecute);
        }

        public IEnumerable<Swatch> Swatches { get; }

        public override void Prepare(UserModel _user)
        {
            base.Prepare(_user);
        }

        public override Task Initialize()
        {
            return base.Initialize();
        }

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

        private bool CanExecute()
        {
            if (!User.LicenseValid)
            {
                //mainWindowViewModel.NotificationSnackbar.Enqueue("Kaufe die Pro Version um die Farben zu ändern");
            }
            return User.LicenseValid;
        }
    }
}