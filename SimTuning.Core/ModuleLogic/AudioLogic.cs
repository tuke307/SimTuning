// project=SimTuning.Core, file=AudioLogic.cs, creation=2020:7:31 Copyright (c) 2020 tuke
// productions. All rights reserved.
namespace SimTuning.Core.ModuleLogic
{
    using NAudio.Wave;
    using OxyPlot;
    using SkiaSharp;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// AudioLogic.
    /// </summary>
    public static class AudioLogic
    {
        #region variables

        private static string _audioFile;
        private static Spectrogram.Colormap _colormap;
        private static int _fftSize;
        private static double _intensity;
        private static int _maxFreq;
        private static int _minFreq;
        private static int _stepSize;
        private static int _targetWidthPx;
        private static SKBitmap bmp;

        /// <summary>
        /// Gets or sets tODO: nutzer soll dies einstellen können.
        /// Math.Round(Math.Sqrt(Convert.ToDouble(AudioLogic.SpectrogramAudio.DisplaySettings.freqHigh
        /// - AudioLogic.SpectrogramAudio.DisplaySettings.freqLow)) / 2);
        /// </summary>
        private static double punktAbstand = 8;

        //private static int sampleRate;

        /// <summary>
        /// Gets array Handling.
        /// 1. Liste = Graphen, 2. Liste = Punkte.
        /// </summary>
        public static List<List<DataPoint>> DrehzahlPoints { get; private set; }

        /// <summary>
        /// Gets the spectrogram audio.
        /// </summary>
        /// <value>The spectrogram audio.</value>
        public static Spectrogram.Spectrogram SpectrogramAudio { get; private set; }

        /// <summary>
        /// Gets the FFT beginn
        /// </summary>
        //private static double FftBeginn
        //{
        //    get => Math.Round(Convert.ToDouble(AudioLogic.SpectrogramAudio.FreqMin) / HzPerFFT);
        //}

        ///// <summary>
        ///// Gets the FFT ende
        ///// </summary>
        //private static double FftEnde
        //{
        //    get => Math.Round(Convert.ToDouble(AudioLogic.SpectrogramAudio.FreqMax) / HzPerFFT);
        //}

        /// <summary>
        /// Gets fFT-Array-elements pro 1 Hertz.
        /// </summary>
        private static double HzPerFFT
        {
            get => AudioLogic.SpectrogramAudio.HzPerPx;
        }

        /// <summary>
        /// Gets wieviel reihen ein Herz wiederspiegeln. HORIZONTAL; anzahl spalten pro
        /// sekunde.
        /// </summary>
        private static double SegmentsPerSecond
        {
            get => AudioLogic.SpectrogramAudio.SecPerPx;
        }

        /// <summary>
        /// Gets spectrogram Data, Liste = Spalten, [] = Zeilen, float = Stärke der
        /// Frequenz.
        /// </summary>
        private static List<double[]> SpecData
        {
            get => AudioLogic.SpectrogramAudio?.GetFFTs();
        }

        #endregion variables

        /// <summary>
        /// Bildet aus den Spectrogram Daten X-Y Punkte.
        /// </summary>
        /// <param name="areas">if set to <c>true</c> [areas].</param>
        public static void GetDrehzahlGraph(bool areas = false, double intensity = 0.75)
        {
            _intensity = intensity;

            HotPoints();

            if (areas)
            {
                PointsToAreas();
            }

            PointsToRotionalSpeed();
        }

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
            string audioFile,
            int fftSize = 16384,
            int minFreq = 25,
            int maxFreq = 250,
            double intensity = 0.75,
            Spectrogram.Colormap colormap = Spectrogram.Colormap.viridis,
            int targetWidthPx = 1000)
        {
            _audioFile = audioFile;

            _intensity = intensity;

            _minFreq = minFreq;

            _maxFreq = maxFreq;

            _colormap = colormap;

            _fftSize = fftSize;

            _targetWidthPx = targetWidthPx;

            Spectogram();

            return bmp;
        }

        /// <summary>
        /// Definiert die Punkte die Punkte die für eine Analyse verwertbar sind.
        /// </summary>
        private static void HotPoints()
        {
            DrehzahlPoints = new List<List<DataPoint>>();
            List<DataPoint> temp_data = new List<DataPoint>();

            for (int col = 0; col < SpecData.Count; col++)
            {
                // nur für ausgewählten bereich
                List<DataPoint> temp_points = new List<DataPoint>();

                for (int row = /*(int)FftBeginn*/0; row < /*FftEnde*/ SpecData[col].Length; row++)
                {
                    // Holen der intensivsten Punkte
                    double value = SpecData[col][row];
                    value *= _intensity;
                    value = Math.Min(value, 255);

                    if (value == 255)
                    {
                        // row speichern
                        temp_points.Add(new DataPoint(col, row));
                    }
                }

                // gespeicherte Punkte(wenn gefunden) der Liste hinzufügen
                if (temp_points.Count > 0)
                {
                    temp_data.AddRange(temp_points);
                }
            }

            if (temp_data.Count > 0)
            {
                DrehzahlPoints.Add(temp_data);
            }
        }

        /// <summary>
        /// Definieren von Bereichen aus Gesamtpunkten.
        /// </summary>
        private static void PointsToAreas()
        {
            List<DataPoint> unfiltered_data = new List<DataPoint>();
            unfiltered_data.AddRange(DrehzahlPoints[0]);
            DrehzahlPoints.Clear();

            // Dummy hinzufügen
            DrehzahlPoints.Add(new List<DataPoint>());
            DrehzahlPoints[0].Add(new DataPoint(0, 0));

            var maxcol = unfiltered_data.Max(i => i.X);

            // X
            for (int col = 0; col <= maxcol; col++)
            {
                // Punkte einer Spalte
                List<DataPoint> col_points = new List<DataPoint>();
                col_points.AddRange(unfiltered_data.Where(x => x.X == col));

                if (col_points.Count == 0)
                {
                    continue;
                }

                // Y
                for (int row = 0; row < col_points.Count; row++)
                {
                    // vergleichen mit vorhandenen areas
                    for (int area = 0; area < DrehzahlPoints.Count; area++)
                    {
                        // Horizontal
                        double differenz_horizontal = Math.Abs(col_points[row].X - DrehzahlPoints[area][DrehzahlPoints[area].Count - 1].X);

                        // Vertikal
                        double differenz_vertikal = Math.Abs(col_points[row].Y - DrehzahlPoints[area][DrehzahlPoints[area].Count - 1].Y);

                        // in vorhandene area hinzufügen
                        if (differenz_vertikal <= punktAbstand && differenz_horizontal <= punktAbstand)
                        {
                            DrehzahlPoints[area].Add(col_points[row]);

                            // area für punkt wurde gefunden
                            break;
                        }
                        else
                        {
                            // neue area beginnen, wenn es keine passende vorhandene area
                            // gibt
                            if (area == DrehzahlPoints.Count - 1)
                            {
                                DrehzahlPoints.Add(new List<DataPoint>() { col_points[row] });

                                // area für punkt wurde gefunden
                                break;
                            }
                        }
                    }
                }
            }

            //dummy löschen
            DrehzahlPoints.Remove(DrehzahlPoints[0]);
        }

        /// <summary>
        /// Umrechnen von Hertz in Drehzahl.
        /// </summary>
        private static void PointsToRotionalSpeed()
        {
            List<List<DataPoint>> temp_plot_data = new List<List<DataPoint>>();

            // anzahl graphen
            for (int area = 0; area < DrehzahlPoints.Count; area++)
            {
                List<DataPoint> temp_list = new List<DataPoint>();

                // Punkt in graphen
                for (int col = 0; col < DrehzahlPoints[area].Count; col++)
                {
                    temp_list.Add(new DataPoint(Math.Round(DrehzahlPoints[area][col].X / 100 / SegmentsPerSecond, 2), Math.Round(DrehzahlPoints[area][col].Y * HzPerFFT * 60)));
                }

                temp_plot_data.Add(temp_list);
            }

            DrehzahlPoints.Clear();
            DrehzahlPoints.AddRange(temp_plot_data);
        }

        /// <summary>
        /// Erstellt das Frequenz-Spectrogram.
        /// </summary>
        private static void Spectogram()
        {
            (int _sampleRate, double[] _audio) = Spectrogram.WavFile.ReadMono(_audioFile);

            int _stepSize = _audio.Length / _targetWidthPx;

            // load audio and process FFT
            SpectrogramAudio = new Spectrogram.Spectrogram(
                                                    sampleRate: _sampleRate,
                                                    fftSize: _fftSize,
                                                    stepSize: _stepSize,
                                                    minFreq: _minFreq,
                                                    maxFreq: _maxFreq);

            SpectrogramAudio.Add(_audio, true);
            SpectrogramAudio.SetColormap(_colormap);
            bmp = SpectrogramAudio.GetBitmap(intensity: _intensity);
        }
    }
}