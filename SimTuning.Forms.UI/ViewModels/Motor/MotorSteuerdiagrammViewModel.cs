// project=SimTuning.Forms.UI, file=MotorSteuerdiagrammViewModel.cs, creation=2020:6:30
// Copyright (c) 2020 tuke productions. All rights reserved.
namespace SimTuning.Forms.UI.ViewModels.Motor
{
    using MvvmCross.Logging;
    using MvvmCross.Navigation;
    using System.IO;
    using Xamarin.Forms;

    /// <summary>
    /// MotorSteuerdiagrammViewModel.
    /// </summary>
    /// <seealso cref="SimTuning.Core.ViewModels.Motor.SteuerdiagrammViewModel" />
    public class MotorSteuerdiagrammViewModel : SimTuning.Core.ViewModels.Motor.SteuerdiagrammViewModel
    {
        private double? _auslass_Steuerzeit;

        private double? _einlass_Steuerzeit;

        private ImageSource _portTimingCircle;

        private double? _ueberstroemer_Steuerzeit;

        public override double? Auslass_Steuerzeit
        {
            get => _auslass_Steuerzeit;
            set { SetProperty(ref _auslass_Steuerzeit, value); RefreshSteuerzeit(); }
        }

        public override double? Einlass_Steuerzeit
        {
            get => _einlass_Steuerzeit;
            set { SetProperty(ref _einlass_Steuerzeit, value); RefreshSteuerzeit(); }
        }

        public ImageSource PortTimingCircle
        {
            get => _portTimingCircle;
            private set => SetProperty(ref _portTimingCircle, value);
        }

        public override double? Ueberstroemer_Steuerzeit
        {
            get => _ueberstroemer_Steuerzeit;
            set { SetProperty(ref _ueberstroemer_Steuerzeit, value); RefreshSteuerzeit(); }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MotorSteuerdiagrammViewModel" />
        /// class.
        /// </summary>
        /// <param name="logProvider">The log provider.</param>
        /// <param name="navigationService">The navigation service.</param>
        public MotorSteuerdiagrammViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService)
            : base(logProvider, navigationService)
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
    }
}