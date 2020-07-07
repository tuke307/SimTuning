using MvvmCross.Commands;
using System.IO;
using Xamarin.Forms;

namespace SimTuning.Forms.UI.ViewModels.Motor
{
    public class MotorSteuerdiagrammViewModel : SimTuning.Core.ViewModels.Motor.SteuerdiagrammViewModel
    {
        public MotorSteuerdiagrammViewModel()
        {
        }

        protected new void RefreshSteuerzeit()
        {
            Stream stream = base.RefreshSteuerzeit();
            if (stream != null)
                PortTimingCircle = ImageSource.FromStream(() => stream);
        }

        private ImageSource _portTimingCircle;

        public ImageSource PortTimingCircle
        {
            get => _portTimingCircle;
            private set => SetProperty(ref _portTimingCircle, value);
        }

        private double? _einlass_Steuerzeit;

        public override double? Einlass_Steuerzeit
        {
            get => _einlass_Steuerzeit;
            set { SetProperty(ref _einlass_Steuerzeit, value); RefreshSteuerzeit(); }
        }

        private double? _auslass_Steuerzeit;

        public override double? Auslass_Steuerzeit
        {
            get => _auslass_Steuerzeit;
            set { SetProperty(ref _auslass_Steuerzeit, value); RefreshSteuerzeit(); }
        }

        private double? _ueberstroemer_Steuerzeit;

        public override double? Ueberstroemer_Steuerzeit
        {
            get => _ueberstroemer_Steuerzeit;
            set { SetProperty(ref _ueberstroemer_Steuerzeit, value); RefreshSteuerzeit(); }
        }
    }
}