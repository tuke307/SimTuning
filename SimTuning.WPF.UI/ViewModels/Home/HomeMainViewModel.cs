// project=SimTuning.WPF.UI, file=HomeMainViewModel.cs, creation=2020:9:2 Copyright (c)
// 2020 tuke productions. All rights reserved.
namespace SimTuning.WPF.UI.ViewModels.Home
{
    using Microsoft.Extensions.Logging;
    using MvvmCross.Navigation;
    using System.Threading.Tasks;

    /// <summary>
    /// WPF-spezifisches Home-Main-ViewModel.
    /// </summary>
    /// <seealso cref="SimTuning.Core.ViewModels.Home.HomeViewModel" />
    public class HomeMainViewModel : SimTuning.Core.ViewModels.Home.HomeViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HomeMainViewModel" /> class.
        /// </summary>
        /// <param name="logFactory">The log provider.</param>
        /// <param name="navigationService">The navigation service.</param>
        public HomeMainViewModel(ILoggerFactory logFactory, IMvxNavigationService navigationService)
            : base(logFactory, navigationService)
        {
        }

        #region Methods

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        /// <returns>Initilisierung.</returns>
        public override Task Initialize()
        {
            return base.Initialize();
        }

        /// <summary>
        /// Prepares this instance. called after construction.
        /// </summary>
        public override void Prepare()
        {
            base.Prepare();
        }

        #endregion Methods
    }
}