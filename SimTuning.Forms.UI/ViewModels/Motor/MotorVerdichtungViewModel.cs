﻿// project=SimTuning.Forms.UI, file=MotorVerdichtungViewModel.cs, creation=2020:6:30
// Copyright (c) 2020 tuke productions. All rights reserved.
namespace SimTuning.Forms.UI.ViewModels.Motor
{
    using Microsoft.Extensions.Logging;
    using MvvmCross.Navigation;

    /// <summary>
    /// MotorVerdichtungViewModel.
    /// </summary>
    /// <seealso cref="SimTuning.Core.ViewModels.Motor.VerdichtungViewModel" />
    public class MotorVerdichtungViewModel : SimTuning.Core.ViewModels.Motor.VerdichtungViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MotorVerdichtungViewModel" />
        /// class.
        /// </summary>
        /// <param name="logFactory">The log provider.</param>
        /// <param name="navigationService">The navigation service.</param>
        public MotorVerdichtungViewModel(ILoggerFactory logFactory, IMvxNavigationService navigationService)
            : base(logFactory, navigationService)
        {
        }
    }
}