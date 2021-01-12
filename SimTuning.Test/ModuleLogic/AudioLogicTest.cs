namespace SimTuning.Core.Test
{
    using NUnit.Framework;
    using SimTuning.Core.ModuleLogic;
    using SimTuning.Test;
    using SkiaSharp;
    using System.IO;

    /// <summary>
    /// AudioLogicTest.
    /// </summary>
    /// <seealso cref="MvvmCross.Tests.MvxTestFixture" />
    [TestFixture]
    public class AudioLogicTest : MvxTest
    {
        /// <summary>
        /// Spectrograms the creation test.
        /// </summary>
        /// <param name="fftSize">Size of the FFT.</param>
        [TestCase(8192)] //2^13
        [TestCase(16384)] //2^14
        [TestCase(32768)] //2^15
        [TestCase(65536)] //2^16
        public void SpectrogramCreationTest(int fftSize)
        {
            int _fftSize = fftSize;
            string _fileName;
            string _filePath;
            SKBitmap colormap;

            _fileName = "colormap" + _fftSize + ".png";
            _filePath = Path.Combine(SimTuning.Test.Constants.Directory, _fileName);

            colormap = AudioLogic.GetSpectrogram(
                audioFile: SimTuning.Test.Constants.DynoAudioFile,
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