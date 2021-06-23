﻿// project=SimTuning.Forms.UI, file=EinlassKanalViewModel.cs, creation=2020:6:30 Copyright
// (c) 2020 tuke productions. All rights reserved.
namespace SimTuning.Forms.UI.ViewModels.Einlass
{
    using Microsoft.Extensions.Logging;
    using MvvmCross.Navigation;

    /// <summary>
    /// EinlassKanalViewModel.
    /// </summary>
    /// <seealso cref="SimTuning.Core.ViewModels.Einlass.KanalViewModel" />
    public class EinlassKanalViewModel : SimTuning.Core.ViewModels.Einlass.KanalViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EinlassKanalViewModel" /> class.
        /// </summary>
        /// <param name="logFactory">The log provider.</param>
        /// <param name="navigationService">The navigation service.</param>
        public EinlassKanalViewModel(ILoggerFactory logFactory, IMvxNavigationService navigationService)
            : base(logFactory, navigationService)
        {
        }
    }
}