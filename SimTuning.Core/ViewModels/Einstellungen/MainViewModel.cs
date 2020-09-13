// project=SimTuning.Core, file=MainViewModel.cs, creation=2020:7:31 Copyright (c) 2020
// tuke productions. All rights reserved.
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System.Threading.Tasks;

namespace SimTuning.Core.ViewModels.Einstellungen
{
    /// <summary>
    /// Einstellungen-Main-ViewModel.
    /// </summary>

    public class MainViewModel : MvxNavigationViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MainViewModel" /> class.
        /// </summary>
        /// <param name="logProvider">The log provider.</param>
        /// <param name="navigationService">The navigation service.</param>
        public MainViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService)
            : base(logProvider, navigationService)
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
        /// <param name="">The user.</param>
        public override void Prepare()
        {
        }

        #endregion Methods

        #region Values

        private int _einstellungenTabIndex;

        public int EinstellungenTabIndex
        {
            get => _einstellungenTabIndex;
            set { SetProperty(ref _einstellungenTabIndex, value); }
        }

        #endregion Values
    }
}