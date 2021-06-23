﻿// project=SimTuning.Forms.UI, file=DemoMainViewModel.cs, creation=2020:6:30 Copyright (c)
// 2021 tuke productions. All rights reserved.
namespace SimTuning.Forms.UI.ViewModels.Demo
{
    using Microsoft.Extensions.Logging;
    using MvvmCross.Navigation;

    /// <summary>
    /// DemoMainViewModel.
    /// </summary>
    /// <seealso cref="SimTuning.Core.ViewModels.Demo.DemoMainViewModel" />
    public class DemoMainViewModel : SimTuning.Core.ViewModels.Demo.DemoMainViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DemoMainViewModel" /> class.
        /// </summary>
        /// <param name="logFactory">The log provider.</param>
        /// <param name="navigationService">The navigation service.</param>
        public DemoMainViewModel(ILoggerFactory logFactory, IMvxNavigationService navigationService)
            : base(logFactory, navigationService)
        {
        }
    }
}