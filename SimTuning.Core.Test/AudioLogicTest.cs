using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimTuning.Core.ModuleLogic;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimTuning.Core.Test
{
    [TestClass]
    public class AudioLogicTest
    {
        //private int _fftSize;
        //private int _intensity;
        //private int _colormap;
        //private int _minFreq;
        //private int _maxFreq;
        //private int _targetWidthPx;

        [TestMethod]
        public void SpectrogramTest()
        {
            AudioLogic.GetSpectrogram(
                audioFile: SimTuning.Core.GeneralSettings.AudioFilePath,
                //fftSize: this._fftSize,
                //intensity: this._intensity,
                //colormap: this._colormap,
                //minFreq: this.Frequenzbeginn / 60,
                //maxFreq: this.Frequenzende / 60,
                //targetWidthPx: (int)this.AudioMaximum / 10);
        }
    }
}