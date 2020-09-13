// project=SimTuning.Forms.UI, file=TuningMainViewModel.cs, creation=2020:6:30 Copyright
// (c) 2020 tuke productions. All rights reserved.
namespace SimTuning.Forms.UI.ViewModels.Tuning
{
    using MvvmCross.Logging;
    using MvvmCross.Navigation;
    using SimTuning.Core.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// TuningMainViewModel.
    /// </summary>
    /// <seealso cref="SimTuning.Core.ViewModels.Tuning.MainViewModel" />
    public class TuningMainViewModel : SimTuning.Core.ViewModels.Tuning.MainViewModel
    {
        private readonly IMvxNavigationService _navigationService;
        private bool _firstTime = true;

        /// <summary>
        /// Initializes a new instance of the <see cref="TuningMainViewModel" /> class.
        /// </summary>
        /// <param name="logProvider">The log provider.</param>
        /// <param name="navigationService">The navigation service.</param>
        public TuningMainViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService)
            : base(logProvider, navigationService)
        {
            this._navigationService = navigationService;
        }

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
        /// <param name="">The user.</param>
        public override void Prepare()
        {
            base.Prepare();
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
                this._navigationService.Navigate<TuningDataViewModel>(),
                this._navigationService.Navigate<TuningInputViewModel>(),
                this._navigationService.Navigate<TuningDiagnosisViewModel>()
            };
            return Task.WhenAll(tasks);
        }
    }
}