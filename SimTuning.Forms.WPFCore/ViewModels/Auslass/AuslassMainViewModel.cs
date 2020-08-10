// project=SimTuning.Forms.WPFCore, file=AuslassMainViewModel.cs, creation=2020:7:31
// Copyright (c) 2020 tuke productions. All rights reserved.
using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SimTuning.Forms.WPFCore.ViewModels.Auslass
{
    /// <summary>
    /// WPF-spezifisches AuslassMain-ViewModel.
    /// </summary>
    /// <seealso cref="SimTuning.Core.ViewModels.Auslass.MainViewModel" />
    public class AuslassMainViewModel : SimTuning.Core.ViewModels.Auslass.MainViewModel
    {
        private readonly IMvxNavigationService _navigationService;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuslassMainViewModel"/> class.
        /// </summary>
        /// <param name="logProvider">The log provider.</param>
        /// <param name="navigationService">The navigation service.</param>
        public AuslassMainViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService)
            : base(logProvider, navigationService)
        {
            this._navigationService = navigationService;
        }

        #region Methods

        /// <summary>
        /// Prepares the specified user.
        /// </summary>
        /// <param name="_user">The user.</param>
        public override void Prepare(SimTuning.Core.Models.UserModel _user)
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

        public override void ViewAppearing()
        {
            this._navigationService.Navigate<AuslassTheorieViewModel>();
            this._navigationService.Navigate<AuslassAnwendungViewModel>();

            this.AuslassTabIndex = 0;
        }

        #endregion Methods
    }
}