using Data;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using SimTuning.ModuleLogic;
using SimTuning.windows.Business;
using SkiaSharp;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace SimTuning.windows.ViewModels.Motor
{
    public class Motor_SteuerdiagrammViewModel : SimTuning.ViewModels.Motor.SteuerdiagrammViewModel
    {
        public Motor_SteuerdiagrammViewModel()
        {
            InsertReferenceCommand = new ActionCommand(InsertReference);
            InsertVehicleCommand = new ActionCommand(InsertVehicle);
        }

        protected new void Refresh_Steuerzeit()
        {
            Stream stream = base.Refresh_Steuerzeit();
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