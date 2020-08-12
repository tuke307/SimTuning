// project=SimTuning.Forms.UI, file=EinlassVergaserViewModel.cs, creation=2020:6:30
// Copyright (c) 2020 tuke productions. All rights reserved.
namespace SimTuning.Forms.UI.ViewModels.Einlass
{
    using MvvmCross.Logging;
    using MvvmCross.Navigation;

    /// <summary>
    /// EinlassVergaserViewModel.
    /// </summary>
    /// <seealso cref="SimTuning.Core.ViewModels.Einlass.VergaserViewModel" />
    public class EinlassVergaserViewModel : SimTuning.Core.ViewModels.Einlass.VergaserViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EinlassVergaserViewModel" />
        /// class.
        /// </summary>
        /// <param name="logProvider">The log provider.</param>
        /// <param name="navigationService">The navigation service.</param>
        public EinlassVergaserViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService)
            : base(logProvider, navigationService)
        {
        }
    }
}