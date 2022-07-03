// Copyright (c) 2021 tuke productions. All rights reserved.
namespace SimTuning.Forms.UI.ViewModels.Motor
{
    using Microsoft.Extensions.Logging;
    using MvvmCross.Navigation;
    using SimTuning.Core.Services;
    using System.IO;
    using System.Threading.Tasks;
    using Xamarin.Forms;

    /// <summary>
    /// MotorSteuerdiagrammViewModel.
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
                this.PortTimingCircle = ImageSource.FromStream(() => stream);
            }
        }

        #endregion Methods

        #region Values

        private readonly ILogger<MotorSteuerdiagrammViewModel> _logger;
        private ImageSource _portTimingCircle;

        public ImageSource PortTimingCircle
        {
            get => _portTimingCircle;
            private set => SetProperty(ref _portTimingCircle, value);
        }

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