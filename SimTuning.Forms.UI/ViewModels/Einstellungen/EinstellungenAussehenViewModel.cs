﻿using MvvmCross.Logging;
using MvvmCross.Navigation;
using SimTuning.Forms.UI.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        /// <param name="logProvider">The log provider.</param>
        /// <param name="navigationService">The navigation service.</param>
        public EinstellungenAussehenViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService)
                                          : base(logProvider, navigationService)
        {
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

        private Array _baseTheme = Enum.GetValues(typeof(Themes.BaseTheme));

        private Themes.BaseTheme _baseThemeValue;

        public List<Themes.BaseTheme> BaseThemeList
        {
            get => _baseTheme.OfType<Themes.BaseTheme>().ToList();
        }

        /// <summary>
        /// Gets or sets the base theme value.
        /// </summary>
        /// <value>The base theme value.</value>
        public Themes.BaseTheme BaseThemeValue
        {
            get => _baseThemeValue;
            set
            {
                SetProperty(ref _baseThemeValue, value);
                ApplicationChanges.SetBaseTheme(value);
            }
        }

        #endregion Values
    }
}