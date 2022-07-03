// Copyright (c) 2021 tuke productions. All rights reserved.
namespace SimTuning.Core.ViewModels.Einstellungen
{
    using Microsoft.Extensions.Logging;
    using MvvmCross.Navigation;
    using MvvmCross.Plugin.Messenger;
    using MvvmCross.ViewModels;
    using System.Threading.Tasks;

    /// <summary>
    /// AussehenViewModel.
    /// </summary>
    /// <seealso cref="MvvmCross.ViewModels.MvxViewModel" />
    public class AussehenViewModel : MvxViewModel
    {
        /// <summary> Initializes a new instance of the <see cref="AussehenViewModel"/>
        /// class. </summary> <param name="logger"><inheritdoc cref="ILogger"
        /// path="/summary/node()" /></param> <param name="navigationService"><inheritdoc
        /// cref="IMvxNavigationService" path="/summary/node()" /></param
        public AussehenViewModel(
            ILogger<AussehenViewModel> logger,
            IMvxNavigationService navigationService,
            IMvxMessenger messenger)
        {
            this._logger = logger;
            this._navigationService = navigationService;
            this._messenger = messenger;
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

        protected readonly IMvxMessenger _messenger;
        protected readonly IMvxNavigationService _navigationService;
        private readonly ILogger<AussehenViewModel> _logger;

        #endregion Values
    }
}