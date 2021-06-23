// project=SimTuning.Core, file=HomeViewModel.cs, creation=2020:7:31 Copyright (c) 2020
// tuke productions. All rights reserved.
namespace SimTuning.Core.ViewModels.Home
{
    using Microsoft.Extensions.Logging;
    using MvvmCross.Commands;
    using MvvmCross.Navigation;
    using MvvmCross.ViewModels;
    using System.Threading.Tasks;

    /// <summary>
    /// HomeViewModel.
    /// </summary>
    /// <seealso cref="MvvmCross.ViewModels.MvxNavigationViewModel" />
    public class HomeViewModel : MvxNavigationViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HomeViewModel" /> class.
        /// </summary>
        /// <param name="logFactory">The log provider.</param>
        /// <param name="navigationService">The navigation service.</param>
        public HomeViewModel(ILoggerFactory logFactory, IMvxNavigationService navigationService)
            : base(logFactory, navigationService)
        {
            this.OpenInstagramCommand = new MvxCommand(() => SimTuning.Core.Business.Functions.OpenSite(SimTuning.Core.WebsiteConstants.MyInstagram));
            this.OpenWebsiteCommand = new MvxCommand(() => SimTuning.Core.Business.Functions.OpenSite(SimTuning.Core.WebsiteConstants.MyWebsite));
            this.OpenTwitterCommand = new MvxCommand(() => SimTuning.Core.Business.Functions.OpenSite(SimTuning.Core.WebsiteConstants.MyTwitter));
            this.OpenEmailCommand = new MvxCommand(() => SimTuning.Core.Business.Functions.OpenSite(SimTuning.Core.WebsiteConstants.MailLink));
            this.OpenDonateCommand = new MvxCommand(() => SimTuning.Core.Business.Functions.OpenSite(SimTuning.Core.WebsiteConstants.Paypaldonation));
            this.OpenTutorialCommand = new MvxCommand(() => SimTuning.Core.Business.Functions.OpenSite(SimTuning.Core.WebsiteConstants.TutorialWebsite));
        }

        #region Commands

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

        #endregion Commands

        #region Values

        public IMvxCommand OpenDonateCommand { get; set; }

        public IMvxCommand OpenEmailCommand { get; set; }

        public IMvxCommand OpenInstagramCommand { get; set; }

        public MvxCommand OpenTutorialCommand { get; set; }

        public IMvxCommand OpenTwitterCommand { get; set; }

        public IMvxCommand OpenWebsiteCommand { get; set; }

        #endregion Values
    }
}