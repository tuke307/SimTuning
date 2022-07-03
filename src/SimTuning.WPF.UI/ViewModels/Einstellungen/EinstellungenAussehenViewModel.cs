// Copyright (c) 2021 tuke productions. All rights reserved.
namespace SimTuning.WPF.UI.ViewModels.Einstellungen
{
    using MaterialDesignColors;
    using MaterialDesignThemes.Wpf;
    using Microsoft.Extensions.Logging;
    using MvvmCross.Commands;
    using MvvmCross.Navigation;
    using MvvmCross.Plugin.Messenger;
    using SimTuning.Core;
    using SimTuning.WPF.UI.Business;
    using SimTuning.WPF.UI.Messages;
    using SimTuning.WPF.UI.Services;
    using System;
    using System.Collections.Generic;
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
        /// <param name="logger">The logger.</param>
        /// <param name="navigationService">The navigation service.</param>
        /// <param name="themeService">The theme service.</param>
        public EinstellungenAussehenViewModel(
            ILogger<EinstellungenAussehenViewModel> logger,
            IMvxNavigationService navigationService,
            IThemeService themeService,
            IMvxMessenger messenger)
            : base(logger, navigationService, messenger)
        {
            this._themeService = themeService;
            this._logger = logger;

            this.Swatches = new SwatchesProvider().Swatches;
        }

        #region Methods

        /// <inheritdoc />
        public override Task Initialize()
        {
            this.ApplyPrimaryCommand = new MvxCommand<Swatch>(ApplyPrimary, CanExecute);
            this.ApplyAccentCommand = new MvxCommand<Swatch>(ApplyAccent, CanExecute);

            BaseThemeValue = (MaterialDesignThemes.Wpf.BaseTheme)ColorSettings.Theme;
            ShowSnackbar();
            OpenMenuCommand = new MvxAsyncCommand(() => _navigationService.Navigate<EinstellungenMenuViewModel>());

            return base.Initialize();
        }

        /// <inheritdoc />
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
                _messenger.Publish(
                    new ShowSnackbarMessage(
                        this,
                        SimTuning.Core.Helpers.Functions.GetLocalisedRes(typeof(SimTuning.Core.resources), "MES_PRO")));
            }
        }

        /// <summary>
        /// Applies the accent.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        protected void ApplyAccent(Swatch parameter)
        {
            MaterialDesignColors.SecondaryColor secondaryColor = (MaterialDesignColors.SecondaryColor)Enum.Parse(typeof(MaterialDesignColors.SecondaryColor), parameter.Name);

            ColorSettings.Secondary = (int)secondaryColor;
            _themeService.UpdateSecondary(secondaryColor);

            // ApplicationChanges.SetAccent(parameter);
        }

        /// <summary>
        /// Applies the primary.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        protected void ApplyPrimary(Swatch parameter)
        {
            MaterialDesignColors.PrimaryColor primaryColor = (MaterialDesignColors.PrimaryColor)Enum.Parse(typeof(MaterialDesignColors.PrimaryColor), parameter.ToString(), true);

            ColorSettings.Primary = (int)primaryColor;
            _themeService.UpdatePrimary(primaryColor);

            // ApplicationChanges.SetPrimary(parameter);
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

        private readonly ILogger<EinstellungenAussehenViewModel> _logger;
        private readonly IThemeService _themeService;
        private Array _baseTheme = Enum.GetValues(typeof(MaterialDesignThemes.Wpf.BaseTheme));

        private BaseTheme? _baseThemeValue;

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
        public MaterialDesignThemes.Wpf.BaseTheme? BaseThemeValue
        {
            get => _baseThemeValue;
            set
            {
                // nur wenn eine Änderung gewünscht ist
                if (_baseThemeValue != null)
                {
                    _themeService.UpdateTheme(value.Value);
                }

                SetProperty(ref _baseThemeValue, value);
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