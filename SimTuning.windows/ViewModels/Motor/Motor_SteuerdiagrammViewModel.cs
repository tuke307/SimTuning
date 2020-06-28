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
            if (stream != null) {
                PngBitmapDecoder decoder = new PngBitmapDecoder(stream, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.Default);
                Steuerzeiten_Rad = decoder.Frames[0];
            }
                
        }

        public BitmapSource Steuerzeiten_Rad
        {
            get => Get<BitmapSource>();
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