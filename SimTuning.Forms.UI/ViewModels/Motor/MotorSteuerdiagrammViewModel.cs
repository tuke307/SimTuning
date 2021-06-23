// project=SimTuning.Forms.UI, file=MotorSteuerdiagrammViewModel.cs, creation=2020:6:30
// Copyright (c) 2021 tuke productions. All rights reserved.
namespace SimTuning.Forms.UI.ViewModels.Motor
{
    using Microsoft.Extensions.Logging;
    using MvvmCross.Navigation;
    using System.IO;
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
        /// <param name="logFactory">The log provider.</param>
        /// <param name="navigationService">The navigation service.</param>
        public MotorSteuerdiagrammViewModel(ILoggerFactory logFactory, IMvxNavigationService navigationService)
            : base(logFactory, navigationService)
        {
        }

        /// <summary>
        /// Refreshes the steuerzeit.
        /// </summary>
        protected new void RefreshSteuerzeit()
        {
            Stream stream = base.RefreshSteuerzeit();
            if (stream != null)
            {
                this.PortTimingCircle = ImageSource.FromStream(() => stream);
            }
        }

        #region Values

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