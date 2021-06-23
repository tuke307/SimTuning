// project=SimTuning.WPF.UI, file=AuslassTheorieViewModel.cs, creation=2020:9:2 Copyright
// (c) 2020 tuke productions. All rights reserved.
namespace SimTuning.WPF.UI.ViewModels.Auslass
{
    using Microsoft.Extensions.Logging;
    using MvvmCross.Navigation;

    /// <summary>
    /// WPF-spezifisches Auslass-Theorie-ViewModel.
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