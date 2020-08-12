// project=SimTuning.Forms.UI, file=MenuViewModel.cs, creation=2020:7:2
// Copyright (c) 2020 tuke productions. All rights reserved.
namespace SimTuning.Forms.UI.ViewModels
{
    using System.Threading.Tasks;
    using MvvmCross.Commands;
    using MvvmCross.Logging;
    using MvvmCross.Navigation;
    using SimTuning.Core.Models;
    using SimTuning.Forms.UI.Business;
    using SimTuning.Forms.UI.ViewModels.Auslass;
    using SimTuning.Forms.UI.ViewModels.Demo;
    using SimTuning.Forms.UI.ViewModels.Dyno;
    using SimTuning.Forms.UI.ViewModels.Einlass;
    using SimTuning.Forms.UI.ViewModels.Einstellungen;
    using SimTuning.Forms.UI.ViewModels.Home;
    using SimTuning.Forms.UI.ViewModels.Motor;
    using SimTuning.Forms.UI.ViewModels.Tuning;

    /// <summary>
    /// MenuViewModel.
    /// </summary>
    /// <seealso cref="SimTuning.Core.ViewModels.Menu" />
    public class MenuViewModel : SimTuning.Core.ViewModels.Menu
    {
        private readonly IMvxNavigationService _navigationService;

        /// <summary>
        /// Initializes a new instance of the <see cref="MenuViewModel"/> class.
        /// </summary>
        /// <param name="logProvider">The log provider.</param>
        /// <param name="navigationService">The navigation service.</param>
        public MenuViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService) : base(logProvider, navigationService)
        {
            this._navigationService = navigationService;

            this.ShowHomeCommand = new MvxAsyncCommand(() => this._navigationService.Navigate<HomeMainViewModel>());
            this.ShowEinlassCommand = new MvxAsyncCommand(() => this._navigationService.Navigate<EinlassMainViewModel, UserModel>(this.User));
            this.ShowAuslassCommand = new MvxAsyncCommand(() => this._navigationService.Navigate<AuslassMainViewModel, UserModel>(this.User));
            this.ShowMotorCommand = new MvxAsyncCommand(() => this._navigationService.Navigate<MotorMainViewModel, UserModel>(this.User));
            this.ShowDynoCommand = new MvxAsyncCommand(this.ShowDyno);
            this.ShowTuningCommand = new MvxAsyncCommand(this.ShowTuning);
            this.ShowEinstellungenCommand = new MvxAsyncCommand(() => this._navigationService.Navigate<EinstellungenMainViewModel, UserModel>(this.User));
            this.LoginUserCommand = new MvxAsyncCommand(this.LoginUser);
        }

        #region Methods

        /// <summary>
        /// Views the appeared.
        /// </summary>
        public override void ViewAppeared()
        {
            base.ViewAppeared();

            this.LoginUserCommand.Execute();
        }

        /// <summary>
        /// Logins the user.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        protected new async Task LoginUser()
        {
            var result = await API.Login.UserLoginAsync().ConfigureAwait(true);
            this.User = result.Item1;

            Functions.ShowSnackbarDialog(result.Item2);
        }

        /// <summary>
        /// Shows the dyno.
        /// </summary>
        private async Task ShowDyno()
        {
            if (this.User.LicenseValid)
            {
                await this._navigationService.Navigate<DynoMainViewModel, UserModel>(this.User).ConfigureAwait(true);
            }
            else
            {
                await this._navigationService.Navigate<DemoMainViewModel>().ConfigureAwait(true);
            }
        }

        /// <summary>
        /// Shows the tuning.
        /// </summary>
        private async Task ShowTuning()
        {
            if (this.User.LicenseValid)
            {
                await this._navigationService.Navigate<TuningMainViewModel, UserModel>(this.User).ConfigureAwait(true);
            }
            else
            {
                await this._navigationService.Navigate<DemoMainViewModel>().ConfigureAwait(true);
            }
        }

        #endregion Methods
    }
}