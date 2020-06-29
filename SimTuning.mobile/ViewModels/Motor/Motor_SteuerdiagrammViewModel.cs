using System.IO;
using Xamarin.Forms;

namespace SimTuning.mobile.ViewModels
{
    public class Motor_SteuerdiagrammViewModel : SimTuning.ViewModels.Motor.SteuerdiagrammViewModel
    {
        public Motor_SteuerdiagrammViewModel()
        {
            InsertReferenceCommand = new Command(InsertReference);
            InsertVehicleCommand = new Command(InsertVehicle);
        }

        protected new void Refresh_Steuerzeit()
        {
            Stream stream = base.Refresh_Steuerzeit();
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
            set { SetProperty(ref _einlass_Steuerzeit, value); Refresh_Steuerzeit(); }
        }

        private double? _auslass_Steuerzeit;

        public override double? Auslass_Steuerzeit
        {
            get => _auslass_Steuerzeit;
            set { SetProperty(ref _auslass_Steuerzeit, value); Refresh_Steuerzeit(); }
        }

        private double? _ueberstroemer_Steuerzeit;

        public override double? Ueberstroemer_Steuerzeit
        {
            get => _ueberstroemer_Steuerzeit;
            set { SetProperty(ref _ueberstroemer_Steuerzeit, value); Refresh_Steuerzeit(); }
        }
    }
}