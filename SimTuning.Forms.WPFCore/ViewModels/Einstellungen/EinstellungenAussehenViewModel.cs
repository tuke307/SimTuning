// project=SimTuning.Forms.WPFCore, file=EinstellungenAussehenViewModel.cs, creation=2020:7:31
// Copyright (c) 2020 tuke productions. All rights reserved.
namespace SimTuning.Forms.WPFCore.ViewModels.Einstellungen
{
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Threading.Tasks;
    using Data;
    using MaterialDesignColors;
    using MvvmCross.Commands;
    using MvvmCross.Logging;
    using MvvmCross.Navigation;
    using SimTuning.Core.Models;
    using SimTuning.Forms.WPFCore.Business;

    /// <summary>
    ///  WPF-spezifisches Einstellungen-Aussehen-ViewModel.
    /// </summary>
    /// <seealso cref="SimTuning.Core.ViewModels.Einstellungen.AussehenViewModel" />
    public class EinstellungenAussehenViewModel : SimTuning.Core.ViewModels.Einstellungen.AussehenViewModel
    {
        public IEnumerable<Swatch> Swatches { get; }

        private readonly ApplicationChanges color = new ApplicationChanges();
        private bool firstTime = true;

        /// <summary>
        /// Initializes a new instance of the <see cref="EinstellungenAussehenViewModel"/> class.
        /// </summary>
        /// <param name="logProvider">The log provider.</param>
        /// <param name="navigationService">The navigation service.</param>
        public EinstellungenAussehenViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService)
            : base(logProvider, navigationService)
        {
            this.Swatches = new SwatchesProvider().Swatches;
            this.ApplyPrimaryCommand = new MvxCommand<object>(ApplyPrimary, CanExecute);
            this.ApplyAccentCommand = new MvxCommand<object>(ApplyAccent, CanExecute);

            using (var db = new DatabaseContext())
            {
                this.ToogleDarkmode = db.Settings.ToList().Last().DarkMode;
            }
        }

        #region Methods

        /// <summary>
        /// Prepares the specified user.
        /// </summary>
        /// <param name="_user">The user.</param>
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

        /// <summary>
        /// Views the appeared.
        /// </summary>
        public override void ViewAppeared()
        {
            base.ViewAppeared();
            firstTime = false;
        }

        /// <summary>
        /// Applies the primary.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        protected void ApplyPrimary(object parameter)
        {
            color.SetPrimary(parameter);
        }

        /// <summary>
        /// Applies the accent.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        protected void ApplyAccent(object parameter)
        {
            color.SetAccent(parameter);
        }

        /// <summary>
        /// Applies the base theme.
        /// </summary>
        protected void ApplyBaseTheme()
        {
            color.SetBaseTheme(ToogleDarkmode);
        }

        /// <summary>
        /// Determines whether this instance can execute the specified parameter.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <returns>
        ///   <c>true</c> if this instance can execute the specified parameter; otherwise, <c>false</c>.
        /// </returns>
        private bool CanExecute(object parameter)
        {
            if (!User.LicenseValid)
            {
                if (!firstTime)
                    Functions.ShowSnackbarDialog(rm.GetString("MES_PRO", CultureInfo.CurrentCulture));
            }
            return User.LicenseValid;
        }

        #endregion Methods

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