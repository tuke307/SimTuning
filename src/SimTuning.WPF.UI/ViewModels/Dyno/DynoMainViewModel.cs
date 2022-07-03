// Copyright (c) 2021 tuke productions. All rights reserved.
namespace SimTuning.WPF.UI.ViewModels.Dyno
{
    using Microsoft.Extensions.Logging;
    using MvvmCross.Commands;
    using MvvmCross.Navigation;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// Dyno-Main-ViewModel.
    /// </summary>
    /// <seealso cref="SimTuning.Core.ViewModels.Dyno.MainViewModel" />
    public class DynoMainViewModel : SimTuning.Core.ViewModels.Dyno.MainViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DynoMainViewModel" /> class.
        /// </summary>
        /// <param name="logger">The log provider.</param>
        /// <param name="navigationService">The navigation service.</param>
        public DynoMainViewModel(
            ILogger<DynoMainViewModel> logger,
            IMvxNavigationService navigationService)
            : base(logger, navigationService)
        {
            this._logger = logger;
        }

        #region Values

        private readonly ILogger<DynoMainViewModel> _logger;
        private bool _AudioPlayerVisibility;

        public bool AudioPlayerVisibility
        {
            get => _AudioPlayerVisibility;
            set => this.SetProperty(ref _AudioPlayerVisibility, value);
        }

        public override int DynoTabIndex
        {
            get => base.DynoTabIndex;
            set
            {
                base.DynoTabIndex = value;

                if (value == 1 || value == 4)
                {
                    AudioPlayerVisibility = true;
                }
                else
                {
                    AudioPlayerVisibility = false;
                }
            }
        }

        /// <summary>
        /// Gets the show initial view models command.
        /// </summary>
        /// <value>The show initial view models command.</value>
        public IMvxAsyncCommand ShowInitialViewModelsCommand { get; private set; }

        #endregion Values

        #region Methods

        /// <inheritdoc />
        public override Task Initialize()
        {
            this.ShowInitialViewModelsCommand = new MvxAsyncCommand(this.ShowInitialViewModels);
            AudioPlayerVisibility = false;

            return base.Initialize();
        }

        /// <inheritdoc />
        public override void Prepare()
        {
            base.Prepare();
        }

        /// <inheritdoc />
        public override void ViewAppearing()
        {
            base.ViewAppearing();

            this.ShowInitialViewModelsCommand.Execute();
            this.DynoTabIndex = 0;
        }

        /// <summary>
        /// Shows the initial view models.
        /// </summary>
        /// <returns></returns>
        private Task ShowInitialViewModels()
        {
            var tasks = new List<Task>();
            tasks.Add(this._navigationService.Navigate<DynoDataViewModel>());
            // tasks.Add(this._navigationService.Navigate<DynoAudioViewModel>());
            tasks.Add(this._navigationService.Navigate<DynoSpectrogramViewModel>());
            tasks.Add(this._navigationService.Navigate<DynoAudioPlayerViewModel>());
            tasks.Add(this._navigationService.Navigate<DynoGeschwindigkeitViewModel>());
            tasks.Add(this._navigationService.Navigate<DynoAusrollenViewModel>());
            tasks.Add(this._navigationService.Navigate<DynoDiagnosisViewModel>());
            return Task.WhenAll(tasks);
        }

        #endregion Methods
    }
}