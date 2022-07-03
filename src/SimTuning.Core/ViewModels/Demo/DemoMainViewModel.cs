// Copyright (c) 2021 tuke productions. All rights reserved.
using Microsoft.Extensions.Logging;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using SimTuning.Core.Services;
using System.Threading.Tasks;

namespace SimTuning.Core.ViewModels.Demo
{
    /// <summary>
    /// DemoMainViewModel.
    /// </summary>
    /// <seealso cref="MvvmCross.ViewModels.MvxViewModel" />
    public class DemoMainViewModel : MvxViewModel
    {
        /// <summary> Initializes a new instance of the <see cref="DemoMainViewModel" />
        /// class. </summary> <param name="logger"> <inheritdoc cref="ILogger"
        /// path="/summary/node()" /> </param> <param name="navigationService">
        /// <inheritdoc cref="IMvxNavigationService" path="/summary/node()" /> </param
        public DemoMainViewModel(
            ILogger<DemoMainViewModel> logger,
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
            this.OpenWebsiteCommand = new MvxCommand(() => _browserService.OpenBrowser(SimTuning.Core.WebsiteConstants.RegisterWebsite));

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
        private readonly ILogger<DemoMainViewModel> _logger;

        public IMvxCommand OpenWebsiteCommand { get; set; }

        #endregion Values
    }
}