namespace SimTuning.Core.ViewModels.Dyno
{
    using MvvmCross.Commands;
    using MvvmCross.Logging;
    using MvvmCross.Navigation;
    using MvvmCross.ViewModels;

    /// <summary>
    /// SettingsViewModel.
    /// </summary>
    /// <seealso cref="MvvmCross.ViewModels.MvxNavigationViewModel" />
    public class SettingsViewModel : MvxNavigationViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SettingsViewModel" /> class.
        /// </summary>
        /// <param name="logProvider">The log provider.</param>
        /// <param name="navigationService">The navigation service.</param>
        public SettingsViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService)
                                    : base(logProvider, navigationService)
        {
        }

        #region Values

        private int _endAcceleration;

        public int EndAcceleration
        {
            get => _endAcceleration;
            set => SetProperty(ref _endAcceleration, value);
        }

        public IMvxAsyncCommand ShowAccelerationCommand { get; protected set; }

        #endregion Values
    }
}