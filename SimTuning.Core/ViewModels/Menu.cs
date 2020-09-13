// project=SimTuning.Core, file=Menu.cs, creation=2020:7:31 Copyright (c) 2020 tuke
// productions. All rights reserved.
namespace SimTuning.Core.ViewModels
{
    using MvvmCross.Commands;
    using MvvmCross.Logging;
    using MvvmCross.Navigation;
    using MvvmCross.ViewModels;
    using SimTuning.Core.Models;
    using System.Threading.Tasks;

    /// <summary>
    /// Menu-ViewModel.
    /// </summary>
    /// <seealso cref="MvvmCross.ViewModels.MvxNavigationViewModel" />
    public class Menu : MvxNavigationViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Menu" /> class.
        /// </summary>
        /// <param name="logProvider">The log provider.</param>
        /// <param name="navigationService">The navigation service.</param>
        public Menu(IMvxLogProvider logProvider, IMvxNavigationService navigationService)
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
        /// Prepares this instance. called after construction.
        /// </summary>
        public override void Prepare()
        {
        }

        /// <summary>
        /// Logins the user.
        /// </summary>
        protected virtual void LoginUser()
        {
        }

        #endregion Methods

        #region Values

        #region Commands

        public MvxCommand ButtonCloseMenu { get; set; }

        public MvxCommand ButtonOpenMenu { get; set; }

        public IMvxAsyncCommand LoginUserCommand { get; protected set; }

        public IMvxAsyncCommand ShowAuslassCommand { get; set; }

        public IMvxAsyncCommand ShowDynoCommand { get; set; }

        public IMvxAsyncCommand ShowEinlassCommand { get; set; }

        public IMvxAsyncCommand ShowEinstellungenCommand { get; set; }

        public IMvxAsyncCommand ShowHomeCommand { get; set; }

        public IMvxAsyncCommand ShowMotorCommand { get; set; }

        public IMvxAsyncCommand ShowTuningCommand { get; set; }

        #endregion Commands

        #endregion Values
    }
}