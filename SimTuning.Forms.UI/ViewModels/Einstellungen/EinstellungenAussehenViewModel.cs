// project=SimTuning.Forms.UI, file=EinstellungenAussehenViewModel.cs, creation=2020:12:14
// Copyright (c) 2021 tuke productions. All rights reserved.
using Microsoft.Extensions.Logging;
using MvvmCross.Navigation;
using SimTuning.Forms.UI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimTuning.Forms.UI.ViewModels.Einstellungen
{
    /// <summary>
    /// EinstellungenAussehenViewModel.
    /// </summary>
    /// <seealso cref="SimTuning.Core.ViewModels.Einstellungen.AussehenViewModel" />
    public class EinstellungenAussehenViewModel : SimTuning.Core.ViewModels.Einstellungen.AussehenViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EinstellungenAussehenViewModel"
        /// /> class.
        /// </summary>
        /// <param name="logFactory">The log provider.</param>
        /// <param name="navigationService">The navigation service.</param>
        public EinstellungenAussehenViewModel(ILoggerFactory logFactory, IMvxNavigationService navigationService, IThemeService themeService)
             : base(logFactory, navigationService)
        {
            this._themeService = themeService;
            BaseThemeValue = (Themes.BaseTheme)ColorSettings.Theme;
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

        #endregion Methods

        #region Values

        private readonly IThemeService _themeService;
        private Array _baseTheme = Enum.GetValues(typeof(Themes.BaseTheme));

        private Themes.BaseTheme? _baseThemeValue;

        public List<Themes.BaseTheme> BaseThemeList
        {
            get => _baseTheme.OfType<Themes.BaseTheme>().ToList();
        }

        /// <summary>
        /// Gets or sets the base theme value.
        /// </summary>
        /// <value>The base theme value.</value>
        public Themes.BaseTheme? BaseThemeValue
        {
            get => _baseThemeValue;
            set
            {
                if (_baseThemeValue != null)
                {
                    if (ColorSettings.Theme != (int)value.Value)
                    {
                        ColorSettings.Theme = (int)value.Value;
                        _themeService.UpdateTheme(value.Value);
                    }
                }

                this.SetProperty(ref _baseThemeValue, value);
            }
        }

        #endregion Values
    }
}