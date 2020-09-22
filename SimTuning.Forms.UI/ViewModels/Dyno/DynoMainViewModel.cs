// project=SimTuning.Forms.UI, file=DynoMainViewModel.cs, creation=2020:6:30 Copyright (c)
// 2020 tuke productions. All rights reserved.
namespace SimTuning.Forms.UI.ViewModels.Dyno
{
    using MvvmCross.Logging;
    using MvvmCross.Navigation;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// DynoMainViewModel.
    /// </summary>
    /// <seealso cref="SimTuning.Core.ViewModels.Dyno.MainViewModel" />
    public class DynoMainViewModel : SimTuning.Core.ViewModels.Dyno.MainViewModel
    {
        private readonly IMvxNavigationService _navigationService;

        /// <summary>
        /// Initializes a new instance of the <see cref="DynoMainViewModel" /> class.
        /// </summary>
        /// <param name="logProvider">The log provider.</param>
        /// <param name="navigationService">The navigation service.</param>
        public DynoMainViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService)
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
        public override void Prepare()
        {
            base.Prepare();
        }

        /// <summary>
        /// Views the appearing.
        /// </summary>
        public override void ViewAppearing()
        {
            this.ShowInitialViewModels();
        }

        /// <summary>
        /// Shows the initial view models.
        /// </summary>
        /// <returns></returns>
        private Task ShowInitialViewModels()
        {
            var tasks = new List<Task>
            {
                //this._navigationService.Navigate<DynoDataViewModel>(),
                //this._navigationService.Navigate<DynoAudioViewModel>(),
                this._navigationService.Navigate<DynoSpectrogramViewModel>(),
                this._navigationService.Navigate<DynoDiagnosisViewModel>(),
            };
            return Task.WhenAll(tasks);
        }
    }
}