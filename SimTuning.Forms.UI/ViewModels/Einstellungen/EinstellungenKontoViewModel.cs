using MvvmCross.Commands;
using System;
using Xamarin.Essentials;
using XF.Material.Forms.UI.Dialogs;

namespace SimTuning.Forms.UI.ViewModels.Einstellungen
{
    public class EinstellungenKontoViewModel : SimTuning.Core.ViewModels.Einstellungen.KontoViewModel
    {
        private readonly MainPageViewModel mainWindowViewModel;

        public EinstellungenKontoViewModel(MainPageViewModel mainWindowViewModel)
        {
            this.mainWindowViewModel = mainWindowViewModel;

            ConnectUserCommand = new MvxCommand(ConnectUser);
            RegisterSiteCommand = new MvxCommand(RegisterSite);
        }

        protected override void ConnectUser()
        {
            //var tuple = await API.API.UserLoginAsync(email: Email, password: SimTuning.Core.Business.Converts.StringToSecureString(Password));
            //mainWindowViewModel.UserValid = tuple.Item1;
            //mainWindowViewModel.LicenseValid = tuple.Item2;

            //for (int i = 0; i < tuple.Item3.Count; i++)
            //{
            //    await MaterialDialog.Instance.SnackbarAsync(message: tuple.Item3[i],
            //                                msDuration: MaterialSnackbar.DurationLong);
            //}
        }

        protected override void RegisterSite()
        {
            Launcher.OpenAsync(new Uri("https://tuke-productions.de/mein-konto/"));
        }

        private string _password;

        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }
    }
}