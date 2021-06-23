﻿// project=SimTuning.Forms.UI, file=MotorHubraumViewModel.cs, creation=2020:6:30 Copyright
// (c) 2021 tuke productions. All rights reserved.
namespace SimTuning.Forms.UI.ViewModels.Motor
{
    using Microsoft.Extensions.Logging;
    using MvvmCross.Navigation;

    /// <summary>
    /// MotorHubraumViewModel.
    /// </summary>
    /// <seealso cref="SimTuning.Core.ViewModels.Motor.HubraumViewModel" />
    public class MotorHubraumViewModel : SimTuning.Core.ViewModels.Motor.HubraumViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MotorHubraumViewModel" /> class.
        /// </summary>
        /// <param name="logFactory">The log provider.</param>
        /// <param name="navigationService">The navigation service.</param>
        public MotorHubraumViewModel(ILoggerFactory logFactory, IMvxNavigationService navigationService)
            : base(logFactory, navigationService)
        {
        }
    }
}