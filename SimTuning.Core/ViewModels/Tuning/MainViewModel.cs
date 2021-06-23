// project=SimTuning.Core, file=MainViewModel.cs, creation=2020:7:31 Copyright (c) 2020
// tuke productions. All rights reserved.
using Microsoft.Extensions.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System.Threading.Tasks;

namespace SimTuning.Core.ViewModels.Tuning
{
    /// <summary>
    /// Tuning-Main-ViewModel.
    /// </summary>

    public class MainViewModel : MvxNavigationViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MainViewModel" /> class.
        /// </summary>
        /// <param name="logFactory">The log provider.</param>
        /// <param name="navigationService">The navigation service.</param>
        public MainViewModel(ILoggerFactory logFactory, IMvxNavigationService navigationService)
            : base(logFactory, navigationService)
        {
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

        private int _tuningTabIndex;

        public int TuningTabIndex
        {
            get => _tuningTabIndex;
            set { SetProperty(ref _tuningTabIndex, value); }
        }

        #endregion Values
    }
}