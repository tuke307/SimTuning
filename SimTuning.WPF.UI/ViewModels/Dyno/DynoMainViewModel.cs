// project=SimTuning.WPF.UI, file=DynoMainViewModel.cs, creation=2020:9:2 Copyright (c)
// 2020 tuke productions. All rights reserved.
namespace SimTuning.WPF.UI.ViewModels.Dyno
{
    using MvvmCross;
    using MvvmCross.Commands;
    using MvvmCross.Logging;
    using MvvmCross.Navigation;
    using MvvmCross.ViewModels;
    using MvvmCross.Views;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// Dyno-Main-ViewModel.
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
            this.ShowInitialViewModelsCommand = new MvxAsyncCommand(this.ShowInitialViewModels);
        }

        #region Methods

        /// <summary>
        /// Gets the show initial view models command.
        /// </summary>
        /// <value>The show initial view models command.</value>
        public IMvxAsyncCommand ShowInitialViewModelsCommand { get; private set; }

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
            base.ViewAppearing();

            this.ShowInitialViewModelsCommand.Execute();
            this.DynoTabIndex = 0;
        }

        private Task ShowInitialViewModels()
        {
            var tasks = new List<Task>();
            tasks.Add(this._navigationService.Navigate<DynoDataViewModel>());
            //tasks.Add(this._navigationService.Navigate<DynoAudioViewModel>());
            tasks.Add(this._navigationService.Navigate<DynoSpectrogramViewModel>());
            tasks.Add(this._navigationService.Navigate<DynoBeschleunigungViewModel>());
            tasks.Add(this._navigationService.Navigate<DynoAusrollenViewModel>());
            tasks.Add(this._navigationService.Navigate<DynoDiagnosisViewModel>());
            return Task.WhenAll(tasks);
        }

        #endregion Methods
    }
}