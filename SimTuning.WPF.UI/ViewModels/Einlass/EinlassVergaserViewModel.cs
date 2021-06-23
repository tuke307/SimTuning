// project=SimTuning.WPF.UI, file=EinlassVergaserViewModel.cs, creation=2020:9:2 Copyright
// (c) 2020 tuke productions. All rights reserved.
namespace SimTuning.WPF.UI.ViewModels.Einlass
{
    using Microsoft.Extensions.Logging;
    using MvvmCross.Navigation;

    /// <summary>
    /// WPF-spezifisches Einlass-Vergaser-ViewModel.
    /// </summary>
    /// <seealso cref="SimTuning.Core.ViewModels.Einlass.VergaserViewModel" />
    public class EinlassVergaserViewModel : SimTuning.Core.ViewModels.Einlass.VergaserViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EinlassVergaserViewModel" />
        /// class.
        /// </summary>
        /// <param name="logFactory">The log provider.</param>
        /// <param name="navigationService">The navigation service.</param>
        public EinlassVergaserViewModel(ILoggerFactory logFactory, IMvxNavigationService navigationService)
             : base(logFactory, navigationService)
        {
        }
    }
}