namespace SimTuning.WPF.UI.Views.Dyno
{
    using MvvmCross;
    using MvvmCross.Platforms.Wpf.Presenters.Attributes;
    using MvvmCross.Platforms.Wpf.Views;
    using MvvmCross.ViewModels;
    using SimTuning.WPF.UI.Region;
    using SimTuning.WPF.UI.ViewModels.Dyno;

    [MvxWpfPresenter("DynoAudioRegion", mvxViewPosition.NewOrExsist)]
    //[MvxContentPresentation]
    public partial class DynoAudioPlayerView : MvxWpfView<DynoAudioPlayerViewModel>
    {
        public DynoAudioPlayerView()
        {
            this.InitializeComponent();

            //if (!(ViewModel is DynoAudioPlayerViewModel))
            //{
            //    if (Mvx.IoCProvider.TryResolve<DynoAudioPlayerViewModel>(out var miniPlayerViewModel))
            //    {
            //        ViewModel = miniPlayerViewModel;
            //        return;
            //    }

            // var _viewModelLoader = Mvx.IoCProvider.Resolve<IMvxViewModelLoader>(); var
            // request = new
            // MvxViewModelInstanceRequest(typeof(DynoAudioPlayerViewModel));
            // request.ViewModelInstance = _viewModelLoader.LoadViewModel(request, null);
            // ViewModel = request.ViewModelInstance as DynoAudioPlayerViewModel;

            //    Mvx.IoCProvider.RegisterSingleton<DynoAudioPlayerViewModel>(ViewModel);
            //}
        }

        private void Slider_DragCompleted(object sender, System.Windows.Controls.Primitives.DragCompletedEventArgs e)
        {
            this.ViewModel.DragCompletedCommand.Execute();
        }

        private void Slider_DragStarted(object sender, System.Windows.Controls.Primitives.DragStartedEventArgs e)
        {
            this.ViewModel.DragStartedCommand.Execute();
        }
    }
}