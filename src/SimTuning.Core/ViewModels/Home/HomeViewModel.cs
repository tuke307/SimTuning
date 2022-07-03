// Copyright (c) 2021 tuke productions. All rights reserved.
namespace SimTuning.Core.ViewModels.Home
{
    using Microsoft.Extensions.Logging;
    using MvvmCross.Commands;
    using MvvmCross.Navigation;
    using MvvmCross.ViewModels;
    using SimTuning.Core.Services;
    using System.Threading.Tasks;

    /// <summary>
    /// HomeViewModel.
    /// </summary>
    /// <seealso cref="MvvmCross.ViewModels.MvxViewModel" />
    public class HomeViewModel : MvxViewModel
    {
        /// <summary> Initializes a new instance of the <see cref="HomeViewModel"/> class.
        /// </summary> <param name="logger"><inheritdoc cref="ILogger"
        /// path="/summary/node()" /></param> <param name="navigationService"><inheritdoc
        /// cref="IMvxNavigationService" path="/summary/node()" /></param
        public HomeViewModel(
            ILogger<HomeViewModel> logger,
            IMvxNavigationService navigationService,
            IBrowserService browserService)
        {
            this._logger = logger;
            this._browserService = browserService;
        }

        #region Commands

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        /// <returns>Initilisierung.</returns>
        public override Task Initialize()
        {
            this.OpenInstagramCommand = new MvxCommand(() => _browserService.OpenBrowser(SimTuning.Core.WebsiteConstants.MyInstagram));
            this.OpenWebsiteCommand = new MvxCommand(() => _browserService.OpenBrowser(SimTuning.Core.WebsiteConstants.MyWebsite));
            this.OpenTwitterCommand = new MvxCommand(() => _browserService.OpenBrowser(SimTuning.Core.WebsiteConstants.MyTwitter));
            this.OpenEmailCommand = new MvxCommand(() => _browserService.OpenBrowser(SimTuning.Core.WebsiteConstants.MailLink));
            this.OpenDonateCommand = new MvxCommand(() => _browserService.OpenBrowser(SimTuning.Core.WebsiteConstants.Paypaldonation));
            this.OpenTutorialCommand = new MvxCommand(() => _browserService.OpenBrowser(SimTuning.Core.WebsiteConstants.TutorialWebsite));

            return base.Initialize();
        }

        /// <summary>
        /// Prepares this instance. called after construction.
        /// </summary>
        public override void Prepare()
        {
            base.Prepare();
        }

        #endregion Commands

        #region Values

        private readonly IBrowserService _browserService;
        private readonly ILogger<HomeViewModel> _logger;

        public IMvxCommand OpenDonateCommand { get; set; }

        public IMvxCommand OpenEmailCommand { get; set; }

        public IMvxCommand OpenInstagramCommand { get; set; }

        public MvxCommand OpenTutorialCommand { get; set; }

        public IMvxCommand OpenTwitterCommand { get; set; }

        public IMvxCommand OpenWebsiteCommand { get; set; }

        #endregion Values
    }
}