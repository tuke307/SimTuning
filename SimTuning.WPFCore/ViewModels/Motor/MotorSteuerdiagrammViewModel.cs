using MvvmCross.Commands;
using System.IO;
using System.Windows.Media.Imaging;

namespace SimTuning.WPFCore.ViewModels.Motor
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
            {
                PngBitmapDecoder decoder = new PngBitmapDecoder(stream, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.Default);
                Steuerzeiten_Rad = decoder.Frames[0];
            }
        }

        private BitmapSource _steuerzeiten_Rad;

        public BitmapSource Steuerzeiten_Rad
        {
            get => _steuerzeiten_Rad;
            private set => SetProperty(ref _steuerzeiten_Rad, value);
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