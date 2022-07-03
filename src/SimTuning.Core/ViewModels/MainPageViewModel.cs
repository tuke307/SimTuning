// Copyright (c) 2021 tuke productions. All rights reserved.
namespace SimTuning.Core.ViewModels
{
    using Microsoft.Extensions.Logging;
    using MvvmCross.Commands;
    using MvvmCross.Navigation;
    using MvvmCross.ViewModels;
    using System.Threading.Tasks;

    /// <summary>
    /// MainPage-ViewModel.
    /// </summary>
    /// <seealso cref="MvvmCross.ViewModels.MvxViewModel" />
    public class MainPageViewModel : MvxViewModel
    {
        /// <summary> Initializes a new instance of the <see cref="MainPageViewModel"/>
        /// class. </summary> <param name="logger"><inheritdoc cref="ILogger"
        /// path="/summary/node()" /></param> <param name="navigationService"><inheritdoc
        /// cref="IMvxNavigationService" path="/summary/node()" /></param
        public MainPageViewModel(
            ILogger<MainPageViewModel> logger,
            IMvxNavigationService navigationService)
        {
            this._logger = logger;
            this._navigationService = navigationService;
        }

        #region Methods

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        /// <returns>Initialisierung.</returns>
        public override Task Initialize()
        {
            return base.Initialize();
        }

        /// <summary>
        /// Prepares this instance. called after construction.
        /// </summary>
        public override void Prepare()
        {
            base.Prepare();
        }

        #endregion Methods

        #region Values

        protected readonly IMvxNavigationService _navigationService;
        private readonly ILogger<MainPageViewModel> _logger;

        /// <summary>
        /// Gets or sets the show home view model command.
        /// </summary>
        /// <value>The show home view model command.</value>
        public IMvxAsyncCommand ShowHomeViewModelCommand { get; protected set; }

        /// <summary>
        /// Gets or sets the show menu view model command.
        /// </summary>
        /// <value>The show menu view model command.</value>
        public IMvxAsyncCommand ShowMenuViewModelCommand { get; protected set; }

        #endregion Values
    }
}