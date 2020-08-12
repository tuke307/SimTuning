// project=SimTuning.Forms.UI, file=EinstellungenKontoViewModel.cs, creation=2020:6:30
// Copyright (c) 2020 tuke productions. All rights reserved.
namespace SimTuning.Forms.UI.ViewModels.Einstellungen
{
    using MvvmCross.Commands;
    using MvvmCross.Logging;
    using MvvmCross.Navigation;
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
        public EinstellungenKontoViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService)
            : base(logProvider, navigationService)
        {
            // override commands
            this.ConnectUserCommand = new MvxAsyncCommand(this.ConnectUser);
            this.RegisterSiteCommand = new MvxCommand(this.RegisterSite);
        }

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
        /// <param name="_user">The user.</param>
        public override void Prepare(UserModel _user)
        {
            base.Prepare(_user);
        }

        /// <summary>
        /// Connects the user.
        /// </summary>
        protected new async Task ConnectUser()
        {
            var result = await API.Login.UserLoginAsync(email: this.Email, password: Core.Business.Converts.StringToSecureString(this.Password)).ConfigureAwait(true);
            this.User = result.Item1;

            Functions.ShowSnackbarDialog(result.Item2);
        }

        /// <summary>
        /// Registers the site.
        /// </summary>
        protected override void RegisterSite()
        {
            Launcher.OpenAsync(new Uri("https://tuke-productions.de/mein-konto/"));
        }
    }
}