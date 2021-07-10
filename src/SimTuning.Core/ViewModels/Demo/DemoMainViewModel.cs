// project=SimTuning.Core, file=DemoMainViewModel.cs, creation=2020:7:31 Copyright (c)
// 2021 tuke productions. All rights reserved.
using Microsoft.Extensions.Logging;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System.Threading.Tasks;

namespace SimTuning.Core.ViewModels.Demo
{
    public class DemoMainViewModel : MvxNavigationViewModel
    {
        public IMvxCommand OpenWebsiteCommand { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DemoMainViewModel" /> class.
        /// </summary>
        /// <param name="logFactory">The log provider.</param>
        /// <param name="navigationService">The navigation service.</param>
        public DemoMainViewModel(ILoggerFactory logFactory, IMvxNavigationService navigationService)
            : base(logFactory, navigationService)
        {
            this.OpenWebsiteCommand = new MvxCommand(() => SimTuning.Core.Business.Functions.OpenSite(SimTuning.Core.WebsiteConstants.RegisterWebsite));
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
    }
}