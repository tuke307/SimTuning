// project=SimTuning.Forms.WPFCore, file=HomeMainViewModel.cs, creation=2020:7:31
// Copyright (c) 2020 tuke productions. All rights reserved.
namespace SimTuning.Forms.WPFCore.ViewModels.Home
{
    using System.Threading.Tasks;
    using MvvmCross.Commands;
    using MvvmCross.Logging;
    using MvvmCross.Navigation;

    /// <summary>
    ///  WPF-spezifisches Home-Main-ViewModel.
    /// </summary>
    /// <seealso cref="SimTuning.Core.ViewModels.Home.HomeViewModel" />
    public class HomeMainViewModel : SimTuning.Core.ViewModels.Home.HomeViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HomeMainViewModel"/> class.
        /// </summary>
        /// <param name="logProvider">The log provider.</param>
        /// <param name="navigationService">The navigation service.</param>
        public HomeMainViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService)
            : base(logProvider, navigationService)
        {
            // override commands
            this.OpenInstagramCommand = new MvxCommand(OpenInstagram);
            this.OpenWebsiteCommand = new MvxCommand(OpenWebsite);
            this.OpenTwitterCommand = new MvxCommand(OpenTwitter);
            this.OpenEmailCommand = new MvxCommand(OpenEmail);
            this.OpenDonateCommand = new MvxCommand(OpenDonate);
            this.OpenTutorialCommand = new MvxCommand(OpenTutorial);
        }

        #region Methods

        /// <summary>
        /// Prepares this instance.
        /// called after construction.
        /// </summary>
        public override void Prepare()
        {
            base.Prepare();
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
        /// Opens the instagram.
        /// </summary>
        protected override void OpenInstagram()
        {
            Business.Functions.GoToSite("https://www.instagram.com/tony.pbt/");
        }

        /// <summary>
        /// Opens the website.
        /// </summary>
        protected override void OpenWebsite()
        {
            Business.Functions.GoToSite("https://www.tuke-productions.de");
        }

        /// <summary>
        /// Opens the twitter.
        /// </summary>
        protected override void OpenTwitter()
        {
            Business.Functions.GoToSite("https://twitter.com/tonxy_");
        }

        /// <summary>
        /// Opens the email.
        /// </summary>
        protected override void OpenEmail()
        {
            Business.Functions.GoToSite("mailto:tonymeissner70@gmail.com");
        }

        /// <summary>
        /// Opens the donate.
        /// </summary>
        protected override void OpenDonate()
        {
            Business.Functions.GoToSite("https://www.paypal.com/cgi-bin/webscr?cmd=_s-xclick&hosted_button_id=PZ5GBAFYMBPWS&source=url");
        }

        /// <summary>
        /// Opens the tutorial.
        /// </summary>
        protected override void OpenTutorial()
        {
            Business.Functions.GoToSite("https://simtuning.tuke-productions.de/anleitung/");
        }

        #endregion Methods
    }
}