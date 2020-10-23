﻿// project=SimTuning.WPF.UI, file=EinstellungenKontoViewModel.cs, creation=2020:9:2
// Copyright (c) 2020 tuke productions. All rights reserved.
namespace SimTuning.WPF.UI.ViewModels.Einstellungen
{
    using API;
    using MvvmCross.Commands;
    using MvvmCross.Logging;
    using MvvmCross.Navigation;
    using MvvmCross.Plugin.Messenger;
    using SimTuning.WPF.UI.Business;
    using System.Security;
    using System.Threading.Tasks;
    using System.Windows.Controls;

    /// <summary>
    /// WPF-spezifisches Einstellungen-Konto-ViewModel.
    /// </summary>
    /// <seealso cref="SimTuning.Core.ViewModels.Einstellungen.KontoViewModel" />
    public class EinstellungenKontoViewModel : SimTuning.Core.ViewModels.Einstellungen.KontoViewModel
    {
        private SecureString Password;

        /// <summary>
        /// Initializes a new instance of the <see cref="EinstellungenKontoViewModel" />
        /// class.
        /// </summary>
        /// <param name="logProvider">The log provider.</param>
        /// <param name="navigationService">The navigation service.</param>
        public EinstellungenKontoViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService, IMvxMessenger messenger)
            : base(logProvider, navigationService, messenger)
        {
            // override Commands
            this.ConnectUserCommand = new MvxAsyncCommand(ConnectUser);

            this.PasswordChangedCommand = new MvxCommand<object>(PasswordChanged);
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
            base.Prepare();
        }

        /// <summary>
        /// Connects the user.
        /// </summary>
        protected new async Task ConnectUser()
        {
            var result = await Login.UserLoginAsync(email: Email, password: Password).ConfigureAwait(true);
            SimTuning.Core.UserSettings.User = result.Item1;
            SimTuning.Core.UserSettings.Order = result.Item2;

            Functions.ShowSnackbarDialog(result.Item3);

            base.ConnectUser();
        }

        /// <summary>
        /// Passwords the changed.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        protected override void PasswordChanged(object parameter)
        {
            if (parameter is PasswordBox passwordBox)
            {
                this.Password = passwordBox.SecurePassword;
            }
        }

        #endregion Methods
    }
}