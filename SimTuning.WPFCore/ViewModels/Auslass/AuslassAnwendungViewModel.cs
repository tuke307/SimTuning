﻿using MvvmCross.Commands;
using System.IO;
using System.Windows.Media.Imaging;

namespace SimTuning.WPFCore.ViewModels.Auslass
{
    public class AuslassAnwendungViewModel : SimTuning.Core.ViewModels.Auslass.AnwendungViewModel
    {
        public AuslassAnwendungViewModel()
        {
            //override command
            CalculateCommand = new MvxCommand(Calculate);
        }

        protected new void Calculate()
        {
            Stream stream = base.Calculate();
            PngBitmapDecoder decoder = new PngBitmapDecoder(stream, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.Default);
            Auspuff = decoder.Frames[0];
        }

        private BitmapSource _auspuff;

        public BitmapSource Auspuff
        {
            get => _auspuff;
            set => SetProperty(ref _auspuff, value);
        }
    }
}