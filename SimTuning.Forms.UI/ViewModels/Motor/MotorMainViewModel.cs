// project=SimTuning.Forms.UI, file=MotorMainViewModel.cs, creation=2020:6:30 Copyright
// (c) 2020 tuke productions. All rights reserved.
namespace SimTuning.Forms.UI.ViewModels.Motor
{
    using MvvmCross.Logging;
    using MvvmCross.Navigation;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// MotorMainViewModel.
    /// </summary>
    /// <seealso cref="SimTuning.Core.ViewModels.Motor.MainViewModel" />
    public class MotorMainViewModel : SimTuning.Core.ViewModels.Motor.MainViewModel
    {
        private readonly IMvxNavigationService _navigationService;
        private bool _firstTime = true;

        /// <summary>
        /// Initializes a new instance of the <see cref="MotorMainViewModel" /> class.
        /// </summary>
        /// <param name="logProvider">The log provider.</param>
        /// <param name="navigationService">The navigation service.</param>
        public MotorMainViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService)
            : base(logProvider, navigationService)
        {
            this._navigationService = navigationService;
        }

        /// <summary>
        /// Views the appearing.
        /// </summary>
        public override void ViewAppearing()
        {
            if (this._firstTime)
            {
                this.ShowInitialViewModels();
                this._firstTime = false;
            }
        }

        /// <summary>
        /// Shows the initial view models.
        /// </summary>
        /// <returns></returns>
        private Task ShowInitialViewModels()
        {
            var tasks = new List<Task>
            {
                this._navigationService.Navigate<MotorUmrechnungenViewModel>(),
                this._navigationService.Navigate<MotorSteuerdiagrammViewModel>(),
                this._navigationService.Navigate<MotorVerdichtungViewModel>(),
                this._navigationService.Navigate<MotorHubraumViewModel>()
            };
            return Task.WhenAll(tasks);
        }
    }
}