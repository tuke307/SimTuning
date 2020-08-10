// project=SimTuning.Core, file=MainViewModel.cs, creation=2020:7:31
// Copyright (c) 2020 tuke productions. All rights reserved.
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System.Threading.Tasks;

namespace SimTuning.Core.ViewModels.Dyno
{
    /// <summary>
    /// Dyno-Main-ViewModel.
    /// </summary>
    /// <seealso cref="MvvmCross.ViewModels.MvxNavigationViewModel{SimTuning.Core.Models.UserModel}" />
    public class MainViewModel : MvxNavigationViewModel<SimTuning.Core.Models.UserModel>
    {
        public SimTuning.Core.Models.UserModel User { get; protected set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MainViewModel"/> class.
        /// </summary>
        /// <param name="logProvider">The log provider.</param>
        /// <param name="navigationService">The navigation service.</param>
        public MainViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService)
            : base(logProvider, navigationService)
        {
        }

        #region Methods

        /// <summary>
        /// Prepares the specified user.
        /// </summary>
        /// <param name="_user">The user.</param>
        public override void Prepare(SimTuning.Core.Models.UserModel _user)
        {
            this.User = _user;
        }

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        /// <returns>Initilisierung.</returns>
        public override Task Initialize()
        {
            return base.Initialize();
        }

        #endregion Methods

        #region Values

        private int _dynoTabIndex;

        public int DynoTabIndex
        {
            get => _dynoTabIndex;
            set { SetProperty(ref _dynoTabIndex, value); }
        }

        #endregion Values
    }
}