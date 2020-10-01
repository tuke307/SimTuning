// project=SimTuning.Core, file=AudioLogic.cs, creation=2020:7:31 Copyright (c) 2020 tuke
// productions. All rights reserved.
namespace SimTuning.Core.ModuleLogic
{
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

        private static string audioFile;

        private static SKBitmap bmp;

        private static Spectrogram.Colormap colormap;

        private static int fftSize;

        private static int frequenzbeginn;

        private static int frequenzende;

        private static double intensity;

        /// <summary>
        /// Gets or sets tODO: nutzer soll dies einstellen können.
        /// </summary>
        private static double punktAbstand = 20;

        private static int sampleRate;

        /// <summary>
        /// TODO: nutzer soll dies einstellen können.
        /// </summary>
        private static double strongPoint = 350;

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
        private static double FftBeginn
        {
            get => Math.Round(Convert.ToDouble(AudioLogic.SpectrogramAudio.DisplaySettings.freqLow) / HzPerFFT);
        }

        /// <summary>
        /// Gets the FFT ende
        /// </summary>
        private static double FftEnde
        {
            get => Math.Round(Convert.ToDouble(AudioLogic.SpectrogramAudio.DisplaySettings.freqHigh) / HzPerFFT);
        }

        /// <summary>
        /// Gets fFT-Array-elements pro 1 Hertz.
        /// </summary>
        private static double HzPerFFT
        {
            get => AudioLogic.SpectrogramAudio.FftSettings.fftResolution;
        }

        /// <summary>
        /// Gets wieviel reihen ein Herz wiederspiegeln. HORIZONTAL; anzahl spalten pro
        /// sekunde.
        /// </summary>
        private static double SegmentsPerSecond
        {
            get => AudioLogic.SpectrogramAudio.FftSettings.segmentsPerSecond;
        }

        /// <summary>
        /// Gets spectrogram Data, Liste = Spalten, [] = Zeilen, float = Stärke der
        /// Frequenz.
        /// </summary>
        private static List<float[]> SpecData
        {
            get => AudioLogic.SpectrogramAudio?.FftList;
        }

        #endregion variables

        /// <summary>
        /// Bildet aus den Spectrogram Daten X-Y Punkte.
        /// </summary>
        /// <param name="areas">if set to <c>true</c> [areas].</param>
        public static void GetDrehzahlGraph(bool areas = false)
        {
            //SpecData.Clear();
            DrehzahlPoints = new List<List<DataPoint>>();

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
        /// Definiert die Punkte die Punkte die für eine Analyse verwertbar sind.
        /// </summary>
        private static void HotPoints()
        {
            List<DataPoint> temp_data = new List<DataPoint>();

            for (int col = 0; col < SpecData.Count; col++)
            {
                // nur für ausgewählten bereich
                List<DataPoint> temp_points = new List<DataPoint>();

                for (int row = (int)FftBeginn; row <= FftEnde; row++)
                {
                    // Holen der intensivsten Punkte
                    if (SpecData[col][row] >= strongPoint)
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

            for (int col = 0; col <= maxcol; col++) // X
            {
                List<DataPoint> col_points = new List<DataPoint>();

                col_points.AddRange(unfiltered_data.Where(x => x.X == col));
                double differenz_horizontal = 0.0;
                double differenz_vertikal = 0.0;

                for (int row = 0; row < col_points.Count; row++) // Y
                {
                    if (col_points.Count > 0)
                    {
                        // vergleichen mit hinzugefügten areas
                        for (int area = 0; area < DrehzahlPoints.Count; area++)
                        {
                            // Horizontal
                            differenz_horizontal = Math.Abs(col_points[row].X - DrehzahlPoints[area][DrehzahlPoints[area].Count - 1].X);
                            // vertikal
                            differenz_vertikal = Math.Abs(col_points[row].Y - DrehzahlPoints[area][DrehzahlPoints[area].Count - 1].Y);

                            // vorhandenem graphen hinzufügen
                            if (differenz_vertikal <= punktAbstand && differenz_horizontal <= 50)
                            {
                                DrehzahlPoints[area].Add(col_points[row]);
                                break;
                            }
                            else
                            {
                                // neuen graphen beginnen, wenn nichts gefundnen wurde
                                if (area == DrehzahlPoints.Count - 1)
                                {
                                    DrehzahlPoints.Add(new List<DataPoint>() { col_points[row] });
                                    break;
                                }
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
                    temp_list.Add(new DataPoint(Math.Round(DrehzahlPoints[area][col].X / SegmentsPerSecond, 2), Math.Round(DrehzahlPoints[area][col].Y * HzPerFFT * 60)));
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