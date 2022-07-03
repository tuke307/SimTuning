// Copyright (c) 2021 tuke productions. All rights reserved.
namespace SimTuning.Core.ViewModels.Motor
{
    using Microsoft.Extensions.Logging;
    using MvvmCross.Navigation;
    using MvvmCross.ViewModels;
    using System.Threading.Tasks;

    /// <summary>
    /// Motor-Main-ViewModel.
    /// </summary>
    /// <seealso cref="MvvmCross.ViewModels.MvxViewModel" />
    public class MainViewModel : MvxViewModel
    {
        /// <summary> Initializes a new instance of the <see cref="MainViewModel"/> class.
        /// </summary> <param name="logger"><inheritdoc cref="ILogger"
        /// path="/summary/node()" /></param> <param name="navigationService"><inheritdoc
        /// cref="IMvxNavigationService" path="/summary/node()" /></param
        public MainViewModel(
            ILogger<MainViewModel> logger,
            IMvxNavigationService navigationService)
        {
            this._logger = logger;
            this._navigationService = navigationService;
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
        /// Prepares the specified user.
        /// </summary>

        public override void Prepare()
        {
        }

        #endregion Methods

        #region Values

        protected readonly IMvxNavigationService _navigationService;
        private readonly ILogger<MainViewModel> _logger;
        private int _motorTabIndex;

        public int MotorTabIndex
        {
            get => _motorTabIndex;
            set { SetProperty(ref _motorTabIndex, value); }
        }

        #endregion Values
    }
}