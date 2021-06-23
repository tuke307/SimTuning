// project=SimTuning.WPF.UI, file=AuslassAnwendungViewModel.cs, creation=2020:9:2
// Copyright (c) 2020 tuke productions. All rights reserved.
namespace SimTuning.WPF.UI.ViewModels.Auslass
{
    using MaterialDesignThemes.Wpf;
    using Microsoft.Extensions.Logging;
    using MvvmCross.Commands;
    using MvvmCross.Navigation;
    using SimTuning.WPF.UI.Dialog;
    using System.IO;
    using System.Threading.Tasks;
    using System.Windows.Media.Imaging;

    /// <summary>
    /// WPF-spezifisches Auslass-Anwendung-ViewModel.
    /// </summary>
    /// <seealso cref="SimTuning.Core.ViewModels.Auslass.AnwendungViewModel" />
    public class AuslassAnwendungViewModel : SimTuning.Core.ViewModels.Auslass.AnwendungViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AuslassAnwendungViewModel" />
        /// class.
        /// </summary>
        /// <param name="logFactory">The log provider.</param>
        /// <param name="navigationService">The navigation service.</param>
        public AuslassAnwendungViewModel(ILoggerFactory logFactory, IMvxNavigationService navigationService)
            : base(logFactory, navigationService)
        {
        }

        #region Methods

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        /// <returns>Initilisierung.</returns>
        public override Task Initialize()
        {
            //override command
            this.CalculateCommand = new MvxAsyncCommand(CalculateAsync);

            return base.Initialize();
        }

        /// <summary>
        /// Calculates the asynchronous.
        /// </summary>
        protected new async Task CalculateAsync()
        {
            await DialogHost.Show(new DialogLoadingView(), "DialogLoading", delegate (object sender, DialogOpenedEventArgs args)
            {
                Stream stream = base.Calculate();
                PngBitmapDecoder decoder = new PngBitmapDecoder(stream, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.Default);
                Auspuff = decoder.Frames[0];

                args.Session.Close();
            }).ConfigureAwait(true);
        }

        #endregion Methods

        #region Values

        private BitmapSource _auspuff;

        public BitmapSource Auspuff
        {
            get => _auspuff;
            set => SetProperty(ref _auspuff, value);
        }

        #endregion Values
    }
}