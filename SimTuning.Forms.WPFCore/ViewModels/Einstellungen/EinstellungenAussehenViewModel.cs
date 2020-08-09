using Data;
using MaterialDesignColors;
using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using SimTuning.Core.Models;
using SimTuning.Forms.WPFCore.Business;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace SimTuning.Forms.WPFCore.ViewModels.Einstellungen
{
    public class EinstellungenAussehenViewModel : SimTuning.Core.ViewModels.Einstellungen.AussehenViewModel
    {
        private readonly ApplicationChanges color = new ApplicationChanges();
        private bool firstTime = true;

        public EinstellungenAussehenViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService) : base(logProvider, navigationService)
        {
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

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        /// <returns>Initilisierung.</returns>
        public override Task Initialize()
        {
            return base.Initialize();
        }

        public override void ViewAppeared()
        {
            base.ViewAppeared();
            firstTime = false;
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
                if (!firstTime)
                    Functions.ShowSnackbarDialog(rm.GetString("MES_PRO", CultureInfo.CurrentCulture));
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