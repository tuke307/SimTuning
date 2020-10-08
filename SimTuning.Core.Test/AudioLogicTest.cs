using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimTuning.Core.ModuleLogic;
using SkiaSharp;
using System.IO;

namespace SimTuning.Core.Test
{
    [TestClass]
    public class AudioLogicTest
    {
        [TestMethod]
        public void SpectrogramCreationTest()
        {
            int _fftSize;
            string _fileName;
            string _filePath;
            SKBitmap colormap;

            _fftSize = 4096; //2^12
            _fileName = "colormap" + _fftSize + ".png";
            _filePath = Path.Combine(SimTuning.Core.GeneralSettings.FileDirectory, SimTuning.Test.Constants.UnitTestPath, _fileName);

            colormap = AudioLogic.GetSpectrogram(
                audioFile: SimTuning.Core.GeneralSettings.AudioFilePath,
                fftSize: _fftSize);

            using (var image = SKImage.FromBitmap(colormap))
            using (var data = image.Encode())
            {
                // save the data to a stream
                using (var stream = File.OpenWrite(_filePath))
                {
                    data.SaveTo(stream);
                }
            }

            _fftSize = 8192; //2^13
            _fileName = "colormap" + _fftSize + ".png";
            _filePath = Path.Combine(SimTuning.Core.GeneralSettings.FileDirectory, SimTuning.Test.Constants.UnitTestPath, _fileName);

            colormap = AudioLogic.GetSpectrogram(
                audioFile: SimTuning.Core.GeneralSettings.AudioFilePath,
                fftSize: _fftSize);

            using (var image = SKImage.FromBitmap(colormap))
            using (var data = image.Encode())
            {
                // save the data to a stream
                using (var stream = File.OpenWrite(_filePath))
                {
                    data.SaveTo(stream);
                }
            }

            _fftSize = 16384; //2^14
            _fileName = "colormap" + _fftSize + ".png";
            _filePath = Path.Combine(SimTuning.Core.GeneralSettings.FileDirectory, SimTuning.Test.Constants.UnitTestPath, _fileName);

            colormap = AudioLogic.GetSpectrogram(
               audioFile: SimTuning.Core.GeneralSettings.AudioFilePath,
               fftSize: _fftSize);

            using (var image = SKImage.FromBitmap(colormap))
            using (var data = image.Encode())
            {
                // save the data to a stream
                using (var stream = File.OpenWrite(_filePath))
                {
                    data.SaveTo(stream);
                }
            }

            _fftSize = 32768;//2^15
            _fileName = "colormap" + _fftSize + ".png";
            _filePath = Path.Combine(SimTuning.Core.GeneralSettings.FileDirectory, SimTuning.Test.Constants.UnitTestPath, _fileName);

            colormap = AudioLogic.GetSpectrogram(
               audioFile: SimTuning.Core.GeneralSettings.AudioFilePath,
               fftSize: _fftSize);

            using (var image = SKImage.FromBitmap(colormap))
            using (var data = image.Encode())
            {
                // save the data to a stream
                using (var stream = File.OpenWrite(_filePath))
                {
                    data.SaveTo(stream);
                }
            }
        }
    }
}