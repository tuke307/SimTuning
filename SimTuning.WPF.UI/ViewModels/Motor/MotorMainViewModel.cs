// project=SimTuning.WPF.UI, file=MotorMainViewModel.cs, creation=2020:9:2 Copyright (c)
// 2020 tuke productions. All rights reserved.
namespace SimTuning.WPF.UI.ViewModels.Motor
{
    using Microsoft.Extensions.Logging;
    using MvvmCross.Navigation;
    using System.Threading.Tasks;

    /// <summary>
    /// WPF-spezifisches Motor-Main-ViewModel.
    /// </summary>
    /// <seealso cref="SimTuning.Core.ViewModels.Motor.MainViewModel" />
    public class MotorMainViewModel : SimTuning.Core.ViewModels.Motor.MainViewModel
    {
        private readonly IMvxNavigationService _navigationService;

        /// <summary>
        /// Initializes a new instance of the <see cref="MotorMainViewModel" /> class.
        /// </summary>
        /// <param name="logFactory">The log provider.</param>
        /// <param name="navigationService">The navigation service.</param>
        public MotorMainViewModel(ILoggerFactory logFactory, IMvxNavigationService navigationService)
            : base(logFactory, navigationService)
        {
            this._navigationService = navigationService;
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
        /// Prepares the specified user.
        /// </summary>

        public override void Prepare()
        {
            base.Prepare();
        }

        /// <summary>
        /// Views the appearing.
        /// </summary>
        public override void ViewAppearing()
        {
            this._navigationService.Navigate<MotorUmrechnungViewModel>();
            this._navigationService.Navigate<MotorSteuerdiagrammViewModel>();
            this._navigationService.Navigate<MotorVerdichtungViewModel>();
            this._navigationService.Navigate<MotorHubraumViewModel>();

            this.MotorTabIndex = 0;
        }

        #endregion Methods
    }
}