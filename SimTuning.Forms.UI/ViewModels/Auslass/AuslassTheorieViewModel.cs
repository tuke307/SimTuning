// project=SimTuning.Forms.UI, file=AuslassTheorieViewModel.cs, creation=2020:6:30
// Copyright (c) 2021 tuke productions. All rights reserved.
namespace SimTuning.Forms.UI.ViewModels.Auslass
{
    using Microsoft.Extensions.Logging;
    using MvvmCross.Navigation;

    /// <summary>
    /// AuslassTheorieViewModel.
    /// </summary>
    /// <seealso cref="SimTuning.Core.ViewModels.Auslass.TheorieViewModel" />
    public class AuslassTheorieViewModel : SimTuning.Core.ViewModels.Auslass.TheorieViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AuslassTheorieViewModel" />
        /// class.
        /// </summary>
        /// <param name="logFactory">The log provider.</param>
        /// <param name="navigationService">The navigation service.</param>
        public AuslassTheorieViewModel(ILoggerFactory logFactory, IMvxNavigationService navigationService)
            : base(logFactory, navigationService)
        {
        }
    }
}