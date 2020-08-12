// project=SimTuning.Forms.UI, file=MotorUmrechnungenViewModel.cs, creation=2020:6:30
// Copyright (c) 2020 tuke productions. All rights reserved.
namespace SimTuning.Forms.UI.ViewModels.Motor
{
    using MvvmCross.Logging;
    using MvvmCross.Navigation;

    /// <summary>
    /// MotorUmrechnungenViewModel.
    /// </summary>
    /// <seealso cref="SimTuning.Core.ViewModels.Motor.UmrechnungViewModel" />
    public class MotorUmrechnungenViewModel : SimTuning.Core.ViewModels.Motor.UmrechnungViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MotorUmrechnungenViewModel" />
        /// class.
        /// </summary>
        /// <param name="logProvider">The log provider.</param>
        /// <param name="navigationService">The navigation service.</param>
        public MotorUmrechnungenViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService)
            : base(logProvider, navigationService)
        {
        }
    }
}