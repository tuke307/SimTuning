// project=SimTuning.WPF.UI, file=EinstellungenAussehenViewModel.cs, creation=2020:9:2
// Copyright (c) 2020 tuke productions. All rights reserved.
namespace SimTuning.WPF.UI.ViewModels.Einstellungen
{
    using Data;
    using MaterialDesignColors;
    using MvvmCross.Commands;
    using MvvmCross.Logging;
    using MvvmCross.Navigation;
    using SimTuning.Core;
    using SimTuning.Core.Models;
    using SimTuning.WPF.UI.Business;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    /// WPF-spezifisches Einstellungen-Aussehen-ViewModel.
    /// </summary>
    /// <seealso cref="SimTuning.Core.ViewModels.Einstellungen.AussehenViewModel" />
    public class EinstellungenAussehenViewModel : SimTuning.Core.ViewModels.Einstellungen.AussehenViewModel
    {
        public IEnumerable<Swatch> Swatches { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="EinstellungenAussehenViewModel"
        /// /> class.
        /// </summary>
        /// <param name="logProvider">The log provider.</param>
        /// <param name="navigationService">The navigation service.</param>
        public EinstellungenAussehenViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService)
            : base(logProvider, navigationService)
        {
            this.Swatches = new SwatchesProvider().Swatches;
            this.ApplyPrimaryCommand = new MvxCommand<Swatch>(ApplyPrimary, CanExecute);
            this.ApplyAccentCommand = new MvxCommand<Swatch>(ApplyAccent, CanExecute);

            this.ToogleDarkmode = ApplicationChanges.IsDarkTheme();
            ShowSnackbar();
        }

        #region Methods

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        /// <returns>Initilisierung.</returns>
        public override Task Initialize()
        {
            return base.Initialize();
        }

        /// <summary>
        /// Prepares this instance.
        /// </summary>
        public override void Prepare()
        {
            base.Prepare();
        }

        /// <summary>
        /// Shows the snackbar.
        /// </summary>
        public void ShowSnackbar()
        {
            if (!UserSettings.LicenseValid)
            {
                Functions.ShowSnackbarDialog(rm.GetString("MES_PRO", CultureInfo.CurrentCulture));
            }
        }

        /// <summary>
        /// Applies the accent.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        protected void ApplyAccent(Swatch parameter)
        {
            ApplicationChanges.SetAccent(parameter);
        }

        /// <summary>
        /// Applies the base theme.
        /// </summary>
        protected void ApplyBaseTheme()
        {
            ApplicationChanges.SetBaseTheme(ToogleDarkmode);
        }

        /// <summary>
        /// Applies the primary.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        protected void ApplyPrimary(Swatch parameter)
        {
            ApplicationChanges.SetPrimary(parameter);
        }

        private bool CanExecute(object parameter)
        {
            return LicenseValid;
        }

        #endregion Methods

        #region Values

        private bool _toogleDarkmode;
        private bool firstTime = true;

        public bool LicenseValid
        {
            get => UserSettings.LicenseValid;
        }

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