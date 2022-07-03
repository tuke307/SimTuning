// Copyright (c) 2021 tuke productions. All rights reserved.
namespace SimTuning.Core.ViewModels.Einstellungen
{
    using Microsoft.Extensions.Logging;
    using MvvmCross.Commands;
    using MvvmCross.Navigation;
    using MvvmCross.ViewModels;
    using System.Threading.Tasks;

    /// <summary>
    /// MenuViewModel.
    /// </summary>
    /// <seealso cref="MvvmCross.ViewModels.MvxViewModel" />
    public class MenuViewModel : MvxViewModel
    {
        /// <summary> Initializes a new instance of the <see cref="MenuViewModel"/> class.
        /// </summary> <param name="logger"><inheritdoc cref="ILogger"
        /// path="/summary/node()" /></param> <param name="navigationService"><inheritdoc
        /// cref="IMvxNavigationService" path="/summary/node()" /></param
        public MenuViewModel(
            ILogger<MenuViewModel> logger,
            IMvxNavigationService navigationService)
        {
            this._logger = logger;
            this._navigationService = navigationService;
        }

        #region Methods

        // <summary>
        /// Initializes this instance. </summary> <returns>Initilisierung.</returns>
        public override Task Initialize()
        {
            return base.Initialize();
        }

        /// <summary>
        /// Prepares the specified user.
        /// </summary>

        public override void Prepare()
        {
            base.Prepare();
        }

        #endregion Methods

        #region Values

        protected readonly IMvxNavigationService _navigationService;
        private readonly ILogger<MenuViewModel> _logger;

        public MvxAsyncCommand OpenApplicationCommand { get; set; }

        public MvxAsyncCommand OpenAussehenCommand { get; set; }

        public MvxAsyncCommand OpenKontoCommand { get; set; }

        public MvxAsyncCommand OpenVehiclesCommand { get; set; }

        #endregion Values
    }
}