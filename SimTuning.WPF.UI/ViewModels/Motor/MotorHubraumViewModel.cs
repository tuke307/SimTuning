﻿// project=SimTuning.WPF.UI, file=MotorHubraumViewModel.cs, creation=2020:7:30 Copyright
// (c) 2020 tuke productions. All rights reserved.
namespace SimTuning.WPF.UI.ViewModels.Motor
{
    using MvvmCross.Logging;
    using MvvmCross.Navigation;

    /// <summary>
    /// WPF-spezifisches Motor-Hubraum-ViewModel.
    /// </summary>
    /// <seealso cref="SimTuning.Core.ViewModels.Motor.HubraumViewModel" />
    public class MotorHubraumViewModel : SimTuning.Core.ViewModels.Motor.HubraumViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MotorHubraumViewModel" /> class.
        /// </summary>
        /// <param name="logProvider">The log provider.</param>
        /// <param name="navigationService">The navigation service.</param>
        public MotorHubraumViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService)
            : base(logProvider, navigationService)
        {
        }
    }
}