using Data;
using MaterialDesignColors;
using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using SimTuning.Core.Models;
using SimTuning.Forms.WPFCore.Business;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimTuning.Forms.WPFCore.ViewModels.Einstellungen
{
    public class EinstellungenAussehenViewModel : SimTuning.Core.ViewModels.Einstellungen.AussehenViewModel
    {
        private readonly ApplicationChanges color = new ApplicationChanges();
        //private MainWindowViewModel mainWindowViewModel;

        public EinstellungenAussehenViewModel/*MainWindowViewModel mainWindowViewModel*/(IMvxLogProvider logProvider, IMvxNavigationService navigationService) : base(logProvider, navigationService)
        {
            //this.mainWindowViewModel = mainWindowViewModel;

            Swatches = new SwatchesProvider().Swatches;
            ApplyPrimaryCommand = new MvxCommand<object>(ApplyPrimary, CanExecute);
            ApplyAccentCommand = new MvxCommand<object>(ApplyAccent, CanExecute);

            using (var db = new DatabaseContext())
                ToogleDarkmode = db.Settings.ToList().Last().DarkMode;
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

        #region Commands

        protected void ApplyPrimary(object parameter)
        {
            color.SetPrimary(parameter);
        }

        protected void ApplyAccent(object parameter)
        {
            color.SetAccent(parameter);
        }

        protected void ApplyBaseTheme()
        {
            color.SetBaseTheme(ToogleDarkmode);
        }

        private bool CanExecute(object parameter)
        {
            if (!User.LicenseValid)
            {
                //mainWindowViewModel.NotificationSnackbar.Enqueue("Kaufe die Pro Version um die Farben zu ändern");
            }
            return User.LicenseValid;
        }

        #endregion Commands

        #region Values

        private bool _toogleDarkmode;

        public bool ToogleDarkmode
        {
            get => _toogleDarkmode;
            set
            {
                SetProperty(ref _toogleDarkmode, value);
                ApplyBaseTheme();
            }
        }

        #endregion Values
    }
}