﻿// project=SimTuning.WPF.UI, file=MotorUmrechnungViewModel.cs, creation=2020:9:2 Copyright
// (c) 2020 tuke productions. All rights reserved.
namespace SimTuning.WPF.UI.ViewModels.Motor
{
    using MvvmCross.Logging;
    using MvvmCross.Navigation;

    /// <summary>
    /// WPF-spezifisches Motor-Umrechnung-ViewModel.
    /// </summary>
    /// <seealso cref="SimTuning.Core.ViewModels.Motor.UmrechnungViewModel" />
    public class MotorUmrechnungViewModel : SimTuning.Core.ViewModels.Motor.UmrechnungViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MotorUmrechnungViewModel" />
        /// class.
        /// </summary>
        /// <param name="logProvider">The log provider.</param>
        /// <param name="navigationService">The navigation service.</param>
        public MotorUmrechnungViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService)
            : base(logProvider, navigationService)
        {
        }
    }
}