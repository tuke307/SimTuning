// project=SimTuning.WPF.UI, file=DynoMainViewModel.cs, creation=2020:9:2 Copyright (c)
// 2020 tuke productions. All rights reserved.
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
        private readonly IMvxNavigationService _navigationService;

        /// <summary>
        /// Initializes a new instance of the <see cref="DynoMainViewModel" /> class.
        /// </summary>
        /// <param name="logFactory">The log provider.</param>
        /// <param name="navigationService">The navigation service.</param>
        public DynoMainViewModel(ILoggerFactory logFactory, IMvxNavigationService navigationService)
            : base(logFactory, navigationService)
        {
            this._navigationService = navigationService;
            this.ShowInitialViewModelsCommand = new MvxAsyncCommand(this.ShowInitialViewModels);
            AudioPlayerVisibility = false;
        }

        #region Methods

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
            tasks.Add(this._navigationService.Navigate<DynoAudioPlayerViewModel>());
            tasks.Add(this._navigationService.Navigate<DynoGeschwindigkeitViewModel>());
            tasks.Add(this._navigationService.Navigate<DynoAusrollenViewModel>());
            tasks.Add(this._navigationService.Navigate<DynoDiagnosisViewModel>());
            return Task.WhenAll(tasks);
        }

        #endregion Methods
    }
}