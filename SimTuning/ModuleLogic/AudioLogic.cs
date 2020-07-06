using SkiaSharp;

namespace SimTuning.Core.ModuleLogic
{
    public class AudioLogic
    {
        #region variables

        //Daten können sich geholt werden
        public Spectrogram.Spectrogram SpectrogramAudio { get; private set; }

        //pfad
        private string audioFile;

        //Daten
        private int sampleRate; //meistens 44100

        private double intensity; //Noise filter
        private int frequenzbeginn;
        private int frequenzende;
        private int fftSize; //Qualität der Auflösung = Spalten
        private Spectrogram.Colormap colormap;
        private SKBitmap bmp;

        #endregion variables

        /// <summary>
        /// Gets the Spectrogram as a
        /// </summary>
        /// <param name="_audioFile">The audio file.</param>
        /// <param name="_Quality">The quality.</param>
        /// <param name="_Intensity">The intensity.</param>
        /// <param name="_Hintergrundfarbe">The hintergrundfarbe.</param>
        /// <param name="_Frequenzbeginn">The frequenzbeginn.</param>
        /// <param name="_Frequenzende">The frequenzende.</param>
        /// <returns></returns>
        public SKBitmap GetSpectrogram(string _audioFile,
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
        /// Definieren des Frequenz-Spectrogram mit bestimmten Parametern
        /// </summary>
        private void Spectogram()
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