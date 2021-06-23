// project=SimTuning.WPF.UI, file=MotorSteuerdiagrammViewModel.cs, creation=2020:9:2
// Copyright (c) 2020 tuke productions. All rights reserved.
namespace SimTuning.WPF.UI.ViewModels.Motor
{
    using Microsoft.Extensions.Logging;
    using MvvmCross.Navigation;
    using System.IO;
    using System.Windows.Media.Imaging;

    /// <summary>
    /// WPF-spezifisches Motor-Steuerdiagramm-ViewModel.
    /// </summary>
    /// <seealso cref="SimTuning.Core.ViewModels.Motor.SteuerdiagrammViewModel" />
    public class MotorSteuerdiagrammViewModel : SimTuning.Core.ViewModels.Motor.SteuerdiagrammViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MotorSteuerdiagrammViewModel" />
        /// class.
        /// </summary>
        /// <param name="logFactory">The log provider.</param>
        /// <param name="navigationService">The navigation service.</param>
        public MotorSteuerdiagrammViewModel(ILoggerFactory logFactory, IMvxNavigationService navigationService)
            : base(logFactory, navigationService)
        {
        }

        #region Methods

        /// <summary>
        /// Refreshes the steuerzeit.
        /// </summary>
        protected new void RefreshSteuerzeit()
        {
            Stream stream = base.RefreshSteuerzeit();
            if (stream != null)
            {
                PngBitmapDecoder decoder = new PngBitmapDecoder(stream, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.Default);
                this.Steuerzeiten_Rad = decoder.Frames[0];
            }
        }

        #endregion Methods

        #region Values

        private BitmapSource _steuerzeiten_Rad;

        /// <summary>
        /// Gets or sets the steuerzeit auslass.
        /// </summary>
        /// <value>The steuerzeit auslass.</value>
        public override double? SteuerzeitAuslass
        {
            get => base.SteuerzeitAuslass;
            set
            {
                base.SteuerzeitAuslass = value;
                this.RefreshSteuerzeit();
            }
        }

        /// <summary>
        /// Gets or sets the steuerzeit einlass.
        /// </summary>
        /// <value>The steuerzeit einlass.</value>
        public override double? SteuerzeitEinlass
        {
            get => base.SteuerzeitEinlass;
            set
            {
                base.SteuerzeitEinlass = value;
                this.RefreshSteuerzeit();
            }
        }

        public BitmapSource Steuerzeiten_Rad
        {
            get => _steuerzeiten_Rad;
            private set => SetProperty(ref _steuerzeiten_Rad, value);
        }

        /// <summary>
        /// Gets or sets the steuerzeit ueberstroemer.
        /// </summary>
        /// <value>The steuerzeit ueberstroemer.</value>
        public override double? SteuerzeitUeberstroemer
        {
            get => base.SteuerzeitUeberstroemer;
            set
            {
                base.SteuerzeitUeberstroemer = value;
                this.RefreshSteuerzeit();
            }
        }

        #endregion Values
    }
}