// project=SimTuning.Forms.UI, file=EinstellungenKontoViewModel.cs, creation=2020:6:30
// Copyright (c) 2020 tuke productions. All rights reserved.
namespace SimTuning.Forms.UI.ViewModels.Einstellungen
{
    using MvvmCross.Commands;
    using MvvmCross.Logging;
    using MvvmCross.Navigation;
    using MvvmCross.Plugin.Messenger;
    using SimTuning.Core.Models;
    using SimTuning.Forms.UI.Business;
    using System;
    using System.Threading.Tasks;
    using Xamarin.Essentials;

    /// <summary>
    /// EinstellungenKontoViewModel.
    /// </summary>
    /// <seealso cref="SimTuning.Core.ViewModels.Einstellungen.KontoViewModel" />
    public class EinstellungenKontoViewModel : SimTuning.Core.ViewModels.Einstellungen.KontoViewModel
    {
        private string _password;

        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EinstellungenKontoViewModel" />
        /// class.
        /// </summary>
        /// <param name="logProvider">The log provider.</param>
        /// <param name="navigationService">The navigation service.</param>
        public EinstellungenKontoViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService, IMvxMessenger messenger)
            : base(logProvider, navigationService, messenger)
        {
            // override commands
            this.ConnectUserCommand = new MvxAsyncCommand(this.ConnectUser);
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
            base.Prepare();
        }

        /// <summary>
        /// Connects the user.
        /// </summary>
        protected new async Task ConnectUser()
        {
            var result = await API.Login.UserLoginAsync(email: this.Email, password: Core.Business.Converts.StringToSecureString(this.Password)).ConfigureAwait(true);
            SimTuning.Core.UserSettings.User = result.Item1;
            SimTuning.Core.UserSettings.Order = result.Item2;

            Functions.ShowSnackbarDialog(result.Item3);

            base.ConnectUser();
        }

        #endregion Methods
    }
}