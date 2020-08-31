// project=SimTuning.Forms.WPFCore, file=MenuViewModel.cs, creation=2020:7:30
// Copyright (c) 2020 tuke productions. All rights reserved.
namespace SimTuning.Forms.WPFCore.ViewModels
{
    using System.Threading.Tasks;
    using MvvmCross.Commands;
    using MvvmCross.Logging;
    using MvvmCross.Navigation;
    using SimTuning.Core.Models;
    using SimTuning.Forms.WPFCore.ViewModels.Auslass;
    using SimTuning.Forms.WPFCore.ViewModels.Demo;
    using SimTuning.Forms.WPFCore.ViewModels.Dyno;
    using SimTuning.Forms.WPFCore.ViewModels.Einlass;
    using SimTuning.Forms.WPFCore.ViewModels.Einstellungen;
    using SimTuning.Forms.WPFCore.ViewModels.Home;
    using SimTuning.Forms.WPFCore.ViewModels.Motor;
    using SimTuning.Forms.WPFCore.ViewModels.Tuning;

    /// <summary>
    ///  WPF-spezifisches Menu-ViewModel.
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
            _navigationService = navigationService;

            ShowHomeCommand = new MvxAsyncCommand(() => _navigationService.Navigate<HomeMainViewModel>());

            ShowEinlassCommand = new MvxAsyncCommand(() => _navigationService.Navigate<EinlassMainViewModel, UserModel>(User));

            ShowAuslassCommand = new MvxAsyncCommand(() => _navigationService.Navigate<AuslassMainViewModel, UserModel>(User));

            ShowMotorCommand = new MvxAsyncCommand(() => _navigationService.Navigate<MotorMainViewModel, UserModel>(User));

            ShowDynoCommand = new MvxAsyncCommand(ShowDyno);

            ShowTuningCommand = new MvxAsyncCommand(ShowTuning);

            ShowEinstellungenCommand = new MvxAsyncCommand(() => _navigationService.Navigate<EinstellungenMainViewModel, UserModel>(User));

            this.LoginUserCommand = new MvxAsyncCommand(this.LoginUser);
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
        /// Logins the user.
        /// </summary>
        protected new async Task LoginUser()
        {
            var result = await API.Login.UserLoginAsync().ConfigureAwait(true);
            this.User = result.Item1;

            WPFCore.Business.Functions.ShowSnackbarDialog(result.Item2);
        }

        /// <summary>
        /// Views the appeared.
        /// </summary>
        public override void ViewAppeared()
        {
            base.ViewAppeared();

            this.LoginUserCommand.Execute();
        }

        /// <summary>
        /// Shows the dyno.
        /// </summary>
        private async Task ShowDyno()
        {
            if (User.LicenseValid)
            {
                await _navigationService.Navigate<DynoMainViewModel, UserModel>(User);
            }
            else
            {
                await _navigationService.Navigate<DemoMainViewModel>();
            }
        }

        /// <summary>
        /// Shows the tuning.
        /// </summary>
        private async Task ShowTuning()
        {
            if (User.LicenseValid)
            {
                await _navigationService.Navigate<TuningMainViewModel, UserModel>(User);
            }
            else
            {
                await _navigationService.Navigate<DemoMainViewModel>();
            }
        }

        #endregion Methods
    }
}