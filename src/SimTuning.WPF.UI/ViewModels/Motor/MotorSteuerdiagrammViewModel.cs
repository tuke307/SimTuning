// Copyright (c) 2021 tuke productions. All rights reserved.
namespace SimTuning.WPF.UI.ViewModels.Motor
{
    using Microsoft.Extensions.Logging;
    using MvvmCross.Navigation;
    using SimTuning.Core.Services;
    using System.IO;
    using System.Threading.Tasks;
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
        /// <param name="logger">The log provider.</param>
        /// <param name="navigationService">The navigation service.</param>
        public MotorSteuerdiagrammViewModel(
            ILogger<MotorSteuerdiagrammViewModel> logger,
            IMvxNavigationService navigationService,
            IVehicleService vehicleService)
            : base(logger, navigationService, vehicleService)
        {
            this._logger = logger;
        }

        #region Methods

        /// <inheritdoc />
        public override Task Initialize()
        {
            return base.Initialize();
        }

        /// <inheritdoc />
        public override void Prepare()
        {
            base.Prepare();
        }

        /// <inheritdoc cref="Core.ViewModels.Motor.SteuerdiagrammViewModel.RefreshSteuerzeit" />
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

        private readonly ILogger<MotorSteuerdiagrammViewModel> _logger;
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