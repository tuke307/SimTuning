// project=SimTuning.Forms.UI, file=DemoMainViewModel.cs, creation=2020:6:30 Copyright (c)
// 2020 tuke productions. All rights reserved.
namespace SimTuning.Forms.UI.ViewModels.Demo
{
    using MvvmCross.Commands;
    using MvvmCross.Logging;
    using MvvmCross.Navigation;
    using System;
    using Xamarin.Essentials;

    /// <summary>
    /// DemoMainViewModel.
    /// </summary>
    /// <seealso cref="SimTuning.Core.ViewModels.Demo.DemoMainViewModel" />
    public class DemoMainViewModel : SimTuning.Core.ViewModels.Demo.DemoMainViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DemoMainViewModel" /> class.
        /// </summary>
        /// <param name="logProvider">The log provider.</param>
        /// <param name="navigationService">The navigation service.</param>
        public DemoMainViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService)
            : base(logProvider, navigationService)
        {
            //override commands
            this.OpenWebsiteCommand = new MvxCommand(this.OpenWebsite);
        }

        /// <summary>
        /// Opens the website.
        /// </summary>
        protected override void OpenWebsite()
        {
            Launcher.OpenAsync(new Uri("https://tuke-productions.de"));
        }
    }
}