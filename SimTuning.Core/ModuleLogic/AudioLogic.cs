// project=SimTuning.Core, file=AudioLogic.cs, creation=2020:7:31 Copyright (c) 2020 tuke
// productions. All rights reserved.
namespace SimTuning.Core.ModuleLogic
{
    using SkiaSharp;

    /// <summary>
    /// AudioLogic.
    /// </summary>
    public static class AudioLogic
    {
        #region variables

        private static string audioFile;

        private static SKBitmap bmp;

        private static Spectrogram.Colormap colormap;

        private static int fftSize;

        private static int frequenzbeginn;

        private static int frequenzende;

        private static double intensity;

        private static int sampleRate;

        /// <summary>
        /// Gets the spectrogram audio.
        /// </summary>
        /// <value>The spectrogram audio.</value>
        public static Spectrogram.Spectrogram SpectrogramAudio { get; private set; }

        #endregion variables

        /// <summary>
        /// Definieren des Frequenz-Spectrogram mit bestimmten Parametern.
        /// </summary>
        /// <param name="_audioFile">The audio file.</param>
        /// <param name="_Quality">The quality.</param>
        /// <param name="_Intensity">The intensity.</param>
        /// <param name="_Hintergrundfarbe">The hintergrundfarbe.</param>
        /// <param name="_Frequenzbeginn">The frequenzbeginn.</param>
        /// <param name="_Frequenzende">The frequenzende.</param>
        /// <returns>Spectrogram.</returns>
        public static SKBitmap GetSpectrogram(
            string _audioFile,
            string _Quality = "gut",
            double _Intensity = 0.75,
            Spectrogram.Colormap _Hintergrundfarbe = Spectrogram.Colormap.viridis,
            int _Frequenzbeginn = 25,
            int _Frequenzende = 250)
        {
            //Werte setzen
            audioFile = _audioFile;

            sampleRate = /*_sampleRate*/44100;

            intensity = _Intensity;

            frequenzbeginn = _Frequenzbeginn;

            frequenzende = _Frequenzende;

            colormap = _Hintergrundfarbe;

            switch (_Quality)
            {
                case "schlecht":
                    fftSize = 4096; //2^12
                    break;

                case "mittel":
                    fftSize = 8192; //2^13
                    break;

                case "gut":
                    fftSize = 16384; //2^14
                    break;

                case "sehr gut":
                    fftSize = 32768;//2^15
                    break;

                default:
                    fftSize = 8192;
                    break;
            }

            Spectogram();

            return bmp;
        }

        /// <summary>
        /// Erstellt das Frequenz-Spectrogram.
        /// </summary>
        private static void Spectogram()
        {
            // load audio and process FFT
            SpectrogramAudio = new Spectrogram.Spectrogram(
                                                    sampleRate: sampleRate,
                                                    fftSize: fftSize,
                                                    step: 200);

            float[] values = Spectrogram.Tools.ReadWav(audioFile);

            SpectrogramAudio.AddExtend(values);

            // convert FFT to an image
            bmp = SpectrogramAudio.GetBitmap(
                                 intensity: intensity,
                                 freqHigh: frequenzende,
                                 freqLow: frequenzbeginn,
                                 colormap: colormap);
        }
    }
}