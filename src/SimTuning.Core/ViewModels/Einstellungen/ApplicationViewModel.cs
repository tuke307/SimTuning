// Copyright (c) 2021 tuke productions. All rights reserved.
namespace SimTuning.Core.ViewModels.Einstellungen
{
    using Microsoft.Extensions.Logging;
    using MvvmCross.Navigation;
    using MvvmCross.ViewModels;
    using System.Threading.Tasks;

    /// <summary>
    /// ApplicationViewModel.
    /// </summary>
    /// <seealso cref="MvvmCross.ViewModels.MvxViewModel" />
    public class ApplicationViewModel : MvxViewModel
    {
        /// <summary> Initializes a new instance of the <see cref="ApplicationViewModel"/>
        /// class. </summary> <param name="logger"><inheritdoc cref="ILogger"
        /// path="/summary/node()" /></param> <param name="navigationService"><inheritdoc
        /// cref="IMvxNavigationService" path="/summary/node()" /></param
        public ApplicationViewModel(
            ILogger<ApplicationViewModel> logger,
            IMvxNavigationService navigationService)
        {
            this._logger = logger;
            this._navigationService = navigationService;
        }

        #region Methods

        private readonly ILogger<ApplicationViewModel> _logger;

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        /// <returns>Initilisierung.</returns>
        public override Task Initialize()
        {
            return base.Initialize();
        }

        public override void Prepare()
        {
        }

        #endregion Methods

        #region Values

        protected readonly IMvxNavigationService _navigationService;

        /// <summary>
        /// Gets or sets the rounding accuracy.
        /// </summary>
        /// <value>The rounding accuracy.</value>
        public int RoundingAccuracy
        {
            get => Data.UnitSettings.RoundingAccuracy;
            set
            {
                Data.UnitSettings.RoundingAccuracy = value;
                this.RaisePropertyChanged(() => this.RoundingAccuracy);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [round on unit change].
        /// </summary>
        /// <value><c>true</c> if [round on unit change]; otherwise, <c>false</c>.</value>
        public bool RoundOnUnitChange
        {
            get => Data.UnitSettings.RoundOnUnitChange;
            set
            {
                Data.UnitSettings.RoundOnUnitChange = value;
                this.RaisePropertyChanged(() => this.RoundOnUnitChange);
            }
        }

        #endregion Values
    }
}