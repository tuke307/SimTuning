// project=SimTuning.WPF.UI, file=EinstellungenAussehenViewModel.cs, creation=2020:9:2
// Copyright (c) 2020 tuke productions. All rights reserved.
namespace SimTuning.WPF.UI.ViewModels.Einstellungen
{
    using MaterialDesignColors;
    using MaterialDesignThemes.Wpf;
    using MvvmCross.Commands;
    using MvvmCross.Logging;
    using MvvmCross.Navigation;
    using SimTuning.Core;
    using SimTuning.WPF.UI.Business;
    using System;
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

            BaseThemeValue = (MaterialDesignThemes.Wpf.BaseTheme)ColorSettings.Theme;
            ShowSnackbar();
            OpenMenuCommand = new MvxAsyncCommand(() => NavigationService.Navigate<EinstellungenMenuViewModel>());
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
        /// Applies the primary.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        protected void ApplyPrimary(Swatch parameter)
        {
            ApplicationChanges.SetPrimary(parameter);
        }

        /// <summary>
        /// Determines whether this instance can execute the specified parameter.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <returns>
        /// <c>true</c> if this instance can execute the specified parameter; otherwise,
        /// <c>false</c>.
        /// </returns>
        private bool CanExecute(object parameter)
        {
            return LicenseValid;
        }

        #endregion Methods

        #region Values

        private Array _baseTheme = Enum.GetValues(typeof(MaterialDesignThemes.Wpf.BaseTheme));

        private BaseTheme _baseThemeValue;

        /// <summary>
        /// Gets or sets the apply accent command.
        /// </summary>
        /// <value>The apply accent command.</value>
        public IMvxCommand ApplyAccentCommand { get; set; }

        /// <summary>
        /// Gets or sets the apply primary command.
        /// </summary>
        /// <value>The apply primary command.</value>
        public IMvxCommand ApplyPrimaryCommand { get; set; }

        public List<MaterialDesignThemes.Wpf.BaseTheme> BaseThemeList
        {
            get => _baseTheme.OfType<MaterialDesignThemes.Wpf.BaseTheme>().ToList();
        }

        /// <summary>
        /// Gets or sets the base theme value.
        /// </summary>
        /// <value>The base theme value.</value>
        public MaterialDesignThemes.Wpf.BaseTheme BaseThemeValue
        {
            get => _baseThemeValue;
            set
            {
                SetProperty(ref _baseThemeValue, value);
                ApplicationChanges.SetBaseTheme(value);
            }
        }

        /// <summary>
        /// Gets a value indicating whether [license valid].
        /// </summary>
        /// <value><c>true</c> if [license valid]; otherwise, <c>false</c>.</value>
        public bool LicenseValid
        {
            get => UserSettings.LicenseValid;
        }

        /// <summary>
        /// Gets or sets the open menu command.
        /// </summary>
        /// <value>The open menu command.</value>
        public MvxAsyncCommand OpenMenuCommand { get; set; }

        /// <summary>
        /// Gets the swatches.
        /// </summary>
        /// <value>The swatches.</value>
        public IEnumerable<Swatch> Swatches { get; }

        #endregion Values
    }
}