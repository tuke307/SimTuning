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
            if(stream != null)
                PortTimingCircle = ImageSource.FromStream(() => stream);
        }

        public ImageSource PortTimingCircle
        {
            get => Get<ImageSource>();
            private set => Set(value);
        }

        public override double? Einlass_Steuerzeit
        {
            get => Get<double?>();
            set { Set(value); Refresh_Steuerzeit(); }
        }

        public override double? Auslass_Steuerzeit
        {
            get => Get<double?>();
            set { Set(value); Refresh_Steuerzeit(); }
        }

        public override double? Ueberstroemer_Steuerzeit
        {
            get => Get<double?>();
            set { Set(value); Refresh_Steuerzeit(); }
        }
    }
}