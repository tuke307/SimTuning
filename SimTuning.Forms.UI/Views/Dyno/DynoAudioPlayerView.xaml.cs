namespace SimTuning.Forms.UI.Views.Dyno
{
    using MvvmCross;
    using MvvmCross.Forms.Views;
    using MvvmCross.ViewModels;
    using SimTuning.Forms.UI.ViewModels.Dyno;

    /// <summary>
    /// DynoAudioPlayerView.
    /// </summary>
    /// <seealso cref="MvvmCross.Forms.Views.MvxContentPage{SimTuning.Core.ViewModels.Dyno.AudioPlayerViewModel}" />
    public partial class DynoAudioPlayerView : MvxContentView<DynoAudioPlayerViewModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DynoAudioPlayerView" /> class.
        /// </summary>
        public DynoAudioPlayerView()
        {
            this.InitializeComponent();

            if (!(ViewModel is DynoAudioPlayerViewModel))
            {
                if (Mvx.IoCProvider.TryResolve<DynoAudioPlayerViewModel>(out var miniPlayerViewModel))
                {
                    ViewModel = miniPlayerViewModel;
                    return;
                }

                var _viewModelLoader = Mvx.IoCProvider.Resolve<IMvxViewModelLoader>(); var
                request = new
                MvxViewModelInstanceRequest(typeof(DynoAudioPlayerViewModel));
                request.ViewModelInstance = _viewModelLoader.LoadViewModel(request, null);
                ViewModel = request.ViewModelInstance as DynoAudioPlayerViewModel;

                Mvx.IoCProvider.RegisterSingleton<DynoAudioPlayerViewModel>(ViewModel);
            }
        }
    }
}