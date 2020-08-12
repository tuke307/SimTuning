// project=SimTuning.Forms.UI, file=HomeMainViewModel.cs, creation=2020:6:30 Copyright (c)
// 2020 tuke productions. All rights reserved.
namespace SimTuning.Forms.UI.ViewModels.Home
{
    using MvvmCross.Commands;
    using MvvmCross.Logging;
    using MvvmCross.Navigation;
    using System;
    using System.Threading.Tasks;
    using Xamarin.Essentials;

    /// <summary>
    /// HomeMainViewModel.
    /// </summary>
    /// <seealso cref="SimTuning.Core.ViewModels.Home.HomeViewModel" />
    public class HomeMainViewModel : SimTuning.Core.ViewModels.Home.HomeViewModel
    {
        private readonly IMvxNavigationService _navigationService;

        /// <summary>
        /// Initializes a new instance of the <see cref="HomeMainViewModel" /> class.
        /// </summary>
        /// <param name="logProvider">The log provider.</param>
        /// <param name="navigationService">The navigation service.</param>
        public HomeMainViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService)
            : base(logProvider, navigationService)
        {
            this._navigationService = navigationService;

            // override commands
            this.OpenInstagramCommand = new MvxCommand(this.OpenInstagram);
            this.OpenWebsiteCommand = new MvxCommand(this.OpenWebsite);
            this.OpenTwitterCommand = new MvxCommand(this.OpenTwitter);
            this.OpenEmailCommand = new MvxCommand(this.OpenEmail);
            this.OpenDonateCommand = new MvxCommand(this.OpenDonate);
            this.OpenTutorialCommand = new MvxCommand(this.OpenTutorial);
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
        /// Prepares this instance. called after construction.
        /// </summary>
        public override void Prepare()
        {
            base.Prepare();
        }

        /// <summary>
        /// Opens the donate.
        /// </summary>
        protected override void OpenDonate()
        {
            Launcher.OpenAsync(new Uri("https://www.paypal.com/cgi-bin/webscr?cmd=_s-xclick&hosted_button_id=PZ5GBAFYMBPWS&source=url"));
        }

        /// <summary>
        /// Opens the email.
        /// </summary>
        protected override void OpenEmail()
        {
            Launcher.OpenAsync(new Uri("mailto:tonymeissner70@gmail.com"));
        }

        /// <summary>
        /// Opens the instagram.
        /// </summary>
        protected override void OpenInstagram()
        {
            Launcher.OpenAsync(new Uri("https://www.instagram.com/tony.pbt/"));
        }

        /// <summary>
        /// Opens the tutorial.
        /// </summary>
        protected override void OpenTutorial()
        {
            Launcher.OpenAsync(new Uri("https://simtuning.tuke-productions.de/anleitung/"));
        }

        /// <summary>
        /// Opens the twitter.
        /// </summary>
        protected override void OpenTwitter()
        {
            Launcher.OpenAsync(new Uri("https://twitter.com/tonxy_"));
        }

        /// <summary>
        /// Opens the website.
        /// </summary>
        protected override void OpenWebsite()
        {
            Launcher.OpenAsync(new Uri("https://www.tuke-productions.de"));
        }
    }
}