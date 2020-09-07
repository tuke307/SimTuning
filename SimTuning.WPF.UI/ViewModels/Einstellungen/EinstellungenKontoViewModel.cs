// project=SimTuning.WPF.UI, file=EinstellungenKontoViewModel.cs, creation=2020:9:2
// Copyright (c) 2020 tuke productions. All rights reserved.
namespace SimTuning.WPF.UI.ViewModels.Einstellungen
{
    using API;
    using MvvmCross.Commands;
    using MvvmCross.Logging;
    using MvvmCross.Navigation;
    using SimTuning.Core.Models;
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
        public EinstellungenKontoViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService)
            : base(logProvider, navigationService)
        {
            // override Commands
            this.ConnectUserCommand = new MvxAsyncCommand(ConnectUser);
            this.RegisterSiteCommand = new MvxCommand(RegisterSite);

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
            var result = await API.Login.UserLoginAsync(email: Email, password: Password);
            this.User = result.Item1;
            Functions.ShowSnackbarDialog(result.Item2);
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

        /// <summary>
        /// Registers the site.
        /// </summary>
        protected override void RegisterSite()
        {
            Functions.GoToSite("https://tuke-productions.de/mein-konto/");
        }

        #endregion Methods
    }
}