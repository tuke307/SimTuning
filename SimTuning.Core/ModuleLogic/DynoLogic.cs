// project=SimTuning.Core, file=DynoLogic.cs, creation=2020:7:31 Copyright (c) 2020 tuke
// productions. All rights reserved.
using Data.Models;
using MathNet.Numerics;
using OxyPlot;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SimTuning.Core.ModuleLogic
{
    /// <summary>
    /// Dyno Logik.
    /// </summary>
    public class DynoLogic
    {
        #region variables

        /// <summary>
        /// Array Handling.
        /// 1. Liste = Graphen, 2. Liste = Punkte.
        /// </summary>
        private static readonly List<List<DataPoint>> plotData = new List<List<DataPoint>>();

        /// <summary>
        /// Spectrogram Data, Liste = Spalten, [] = Zeilen, float = Stärke der Frequenz.
        /// </summary>
        private static readonly List<float[]> specData = new List<float[]>();

        /// <summary>
        /// TODO: nutzer soll dies einstellen können.
        /// </summary>
        private static double abstand;

        private static double fftBeginn;

        private static double fftEnde;

        /// <summary>
        /// Punkte für die Polynomfunktion.
        /// </summary>
        private static List<double> function = new List<double>();

        /// <summary>
        /// FFT-Array-elements pro 1 Hertz.
        /// </summary>
        private static double hzPerFFT;

        /// <summary>
        /// wieviel reihen ein Herz wiederspiegeln. HORIZONTAL; anzahl spalten pro
        /// sekunde.
        /// </summary>
        private static double segmentsPerSecond;

        /// <summary>
        /// TODO: nutzer soll dies einstellen können.
        /// </summary>
        private static double strongPoint;

        /// <summary>
        /// Gets the dyno.
        /// </summary>
        /// <value>The dyno.</value>
        public static DynoModel Dyno { get; private set; }

        /// <summary>
        /// Gets or sets the plot audio.
        /// </summary>
        /// <value>The plot audio.</value>
        public static PlotModel PlotAudio { get; set; }

        /// <summary>
        /// Gets or sets the plot ausrollen.
        /// </summary>
        /// <value>The plot ausrollen.</value>
        public static PlotModel PlotAusrollen { get; set; }

        /// <summary>
        /// Gets or sets the plot beschleunigung.
        /// </summary>
        /// <value>The plot beschleunigung.</value>
        public static PlotModel PlotBeschleunigung { get; set; }

        /// <summary>
        /// Gets or sets the plot strength.
        /// </summary>
        /// <value>The plot strength.</value>
        public static PlotModel PlotStrength { get; set; }

        #endregion variables

        /// <summary>
        /// Bildet eine Regression aus Punkten.
        /// </summary>
        /// <param name="choice">Der zu filternde Graph.</param>
        public static void AreaRegression(int choice)
        {
            Dyno = new DynoModel();
            Dyno.Drehzahl = new List<DrehzahlModel>();

            // Regressions-Punkte bilden
            function = Fit.Polynomial(plotData[choice].Select(x => x.X).ToArray(), plotData[choice].Select(x => x.Y).ToArray(), 4).ToList();  // 5 Punkte

            PlotAreaRegression(choice);

            // Audio Werte (X, Y) hinzufügen
            for (int count = 0; count < plotData[0].Count; count++)
            {
                Dyno.Drehzahl.Add(new DrehzahlModel()
                {
                    Zeit = plotData[0][count].X,
                    Drehzahl = plotData[0][count].Y,
                });
            }
        }

        /// <summary>
        /// Berechnet.
        /// TODO: vervollständigen.
        /// </summary>
        public static void BerechneAusrollGraph(List<AusrollenModel> ausrollenModels = null)
        {
        }

        /// <summary>
        /// Berechnet.
        /// TODO: vervollständigen.
        /// </summary>
        public static void BerechneBeschleunigungsGraph(List<BeschleunigungModel> beschleunigungModels = null)
        {
        }

        /// <summary>
        /// Gibt das Diagramm aus den Spectogram Daten zurück. vorher muss
        /// Audio-Spectrogram der Audio bereits einmal berechnet sein mit
        /// AudioLogic.GetSpectrogram().
        /// </summary>
        /// <param name="filter">
        /// if set to <c>true</c> [filter] Punkte werden zu Bereichen zugeordnet.
        /// </param>
        public static void BerechneDrehzahlGraph(bool filter = false)
        {
            specData.Clear();
            plotData.Clear();

            #region SpectrogramData

            // Daten verarbeiten
            specData.AddRange(AudioLogic.SpectrogramAudio?.FftList);
            hzPerFFT = AudioLogic.SpectrogramAudio.FftSettings.fftResolution;
            segmentsPerSecond = AudioLogic.SpectrogramAudio.FftSettings.segmentsPerSecond;
            fftBeginn = Math.Round(Convert.ToDouble(AudioLogic.SpectrogramAudio.DisplaySettings.freqLow) / hzPerFFT);
            fftEnde = Math.Round(Convert.ToDouble(AudioLogic.SpectrogramAudio.DisplaySettings.freqHigh) / hzPerFFT);

            // TODO: muss besser werden
            strongPoint = 350 / AudioLogic.SpectrogramAudio.DisplaySettings.brightness;
            abstand = Math.Round(Math.Sqrt(Convert.ToDouble(AudioLogic.SpectrogramAudio.DisplaySettings.freqHigh - AudioLogic.SpectrogramAudio.DisplaySettings.freqLow)) / 2); // Hälfte der wurzel des Frequenzbereichs

            #endregion SpectrogramData

            HotPoints();

            if (filter)
            {
                PointAreas();
            }

            PointsToRotionalSpeed();

            DefineAudioPlot();

            PlotAreas();
        }

        /// <summary>
        /// Berechnet.
        /// </summary>
        /// <param name="dyno">The dyno.</param>
        /// <param name="plot">The plot.</param>
        /// <param name="ps">The ps.</param>
        /// <param name="nm">The nm.</param>
        public static void BerechneLeistungsGraph(DynoModel dyno, out List<DynoPsModel> ps/*, out List<DynoNmModel> nm*/)
        {
            DefiniereLeistungsGraph();

            //List<DynoNmModel> dynoNm = new List<DynoNmModel>();
            List<DynoPsModel> dynoPs = new List<DynoPsModel>();

            // in m
            //double radhalbmesser = 0.4064; // 16zoll

            // in kg/m^3
            //double luftdichte = dyno.Environment.LuftdruckP.Value / 287.05 * (dyno.Environment.TemperaturT.Value + 273.15); // Gaskonstante 287.05 J/kg*K(trockene Luft), °C in Kelvin umrechnen

            for (int col = 0; col < dyno.Drehzahl.Count; col++)
            {
                // in 1/min
                double drehzahl = dyno.Drehzahl[col].Drehzahl;
                // in s
                double zeit = dyno.Drehzahl[col].Zeit;

                // in m/s
                //double geschwindigkeit = (2 * radhalbmesser * Math.PI * drehzahl / (dyno.Vehicle.Uebersetzung.Value * 1000)) / 3.6;

                //// (a = v / t) in m/s
                //double beschleunigung = geschwindigkeit / zeit;

                //// vertikale Kraft (F=m*a) in N
                //double kraft_gewicht = dyno.Vehicle.Gewicht.Value * 9.81;

                //// horizontale Kraft
                //double kraft_treibend = dyno.Vehicle.Gewicht.Value * beschleunigung;
                //double kraft_bremsend = (dyno.Vehicle.Cw.Value * dyno.Vehicle.FrontA.Value * luftdichte * Math.Pow(geschwindigkeit, 2)) / 2; //Luftwiderstand

                //// GESAMT KRAFT
                //double kraft = Math.Sqrt(Math.Pow(kraft_treibend - kraft_bremsend, 2) + Math.Pow(kraft_gewicht, 2));

                //// (s=v*t) in m
                //double weg = geschwindigkeit * zeit;

                //// (W=F*s) in Nm
                //double Nm = kraft * weg;
                //dynoNm.Add(new DynoNmModel() { Drehzahl = drehzahl, Nm = Nm });

                // P=W/t (1Ps=1Nm/735.498750000002) in PS double PS = Nm / zeit /
                // 735.498750000002;
                //dynoPs.Add(new DynoPsModel() { Drehzahl = drehzahl, Ps = PS });
            }

            //OxyPlot.Series.LineSeries leistung_nm = new OxyPlot.Series.LineSeries();
            OxyPlot.Series.LineSeries leistung_ps = new OxyPlot.Series.LineSeries();

            // Punkte
            for (int zaehler = 0; zaehler < dyno.Drehzahl.Count; zaehler++)
            {
                // Nm leistung_nm.Points.Add(new DataPoint(dynoNm[zaehler].Drehzahl,
                // dynoNm[zaehler].Nm));

                // PS
                leistung_ps.Points.Add(new DataPoint(dynoPs[zaehler].Drehzahl, dynoPs[zaehler].Ps));
            }

            // Style, Beschriftung leistung_nm.Title = "Leistung-Nm"; leistung_nm.Color =
            // OxyColors.DarkRed;

            leistung_ps.Title = "Leistung-PS";
            leistung_ps.Color = OxyColors.DarkBlue;

            // Graphen einfügen PlotStrength.Series.Add(leistung_nm);
            PlotStrength.Series.Add(leistung_ps);

            // nm = dynoNm;
            ps = dynoPs;
        }

        /// <summary>
        /// SplineInterpolation.
        /// </summary>
        public static void SplineInterpolation()
        {
        }

        /// <summary>
        /// Initialisierung des Graphen.
        /// </summary>
        private static void DefineAudioPlot()
        {
            PlotAudio = new PlotModel();

            //Achsen
            PlotAudio.Axes.Add(new OxyPlot.Axes.LinearAxis { Title = "Zeit in s", Position = OxyPlot.Axes.AxisPosition.Bottom, MajorGridlineStyle = LineStyle.Solid, MinorGridlineStyle = LineStyle.Dot });
            PlotAudio.Axes.Add(new OxyPlot.Axes.LinearAxis { Title = "Drehzahl in 1/min", Position = OxyPlot.Axes.AxisPosition.Left, MajorGridlineStyle = LineStyle.Solid, MinorGridlineStyle = LineStyle.Dot });

            PlotAudio.IsLegendVisible = true;
            PlotAudio.LegendPosition = LegendPosition.RightTop;
        }

        /// <summary>
        /// Initialisierung des Graphen.
        /// </summary>
        private static void DefiniereLeistungsGraph()
        {
            PlotStrength = new PlotModel();

            //Achsen
            PlotStrength.Axes.Add(new OxyPlot.Axes.LinearAxis { Title = "Drehzahl in 1/min", Position = OxyPlot.Axes.AxisPosition.Bottom });
            PlotStrength.Axes.Add(new OxyPlot.Axes.LinearAxis { Title = "Leistung in PS", Position = OxyPlot.Axes.AxisPosition.Left });

            //Achsen
            //PlotAudio.Axes.Add(new OxyPlot.Axes.LinearAxis { Title = "Zeit in s", Position = OxyPlot.Axes.AxisPosition.Bottom, MajorGridlineStyle = LineStyle.Solid, MinorGridlineStyle = LineStyle.Dot });
            //PlotAudio.Axes.Add(new OxyPlot.Axes.LinearAxis { Title = "Drehzahl in 1/min", Position = OxyPlot.Axes.AxisPosition.Left, MajorGridlineStyle = LineStyle.Solid, MinorGridlineStyle = LineStyle.Dot });

            PlotStrength.IsLegendVisible = true;
            PlotStrength.LegendPosition = LegendPosition.RightTop;
        }

        /// <summary>
        /// Definiert die Punkte die Punkte die für eine Analyse verwertbar sind.
        /// </summary>
        private static void HotPoints()
        {
            List<DataPoint> temp_data = new List<DataPoint>();

            for (int col = 0; col < specData.Count; col++)
            {
                // nur für ausgewählten bereich
                List<DataPoint> temp_points = new List<DataPoint>();

                for (int row = (int)fftBeginn; row <= fftEnde; row++)
                {
                    // Holen der intensivsten Punkte
                    if (specData[col][row] >= strongPoint)
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
                plotData.Add(temp_data);
            }
        }

        /// <summary>
        /// Definiert den Graph für den Graph der Regression eines Bereichs.
        /// TODO: keine neuerstellung des plots.
        /// </summary>
        private static void PlotAreaRegression(int choice)
        {
            PlotAudio = new PlotModel();

            // Achsen
            PlotAudio.Axes.Add(new OxyPlot.Axes.LinearAxis { Title = "Zeit in s", Position = OxyPlot.Axes.AxisPosition.Bottom, MajorGridlineStyle = LineStyle.Solid, MinorGridlineStyle = LineStyle.Dot });
            PlotAudio.Axes.Add(new OxyPlot.Axes.LinearAxis { Title = "Drehzahl in 1/min", Position = OxyPlot.Axes.AxisPosition.Left, MajorGridlineStyle = LineStyle.Solid, MinorGridlineStyle = LineStyle.Dot });
            // PlotAudio.Series.Clear();
            PlotAudio.IsLegendVisible = false;
            // PlotAudio.InvalidatePlot(true);

            Func<double, double> polyF = (x) => (function[0] * Math.Pow(x, 0)) + (function[1] * Math.Pow(x, 1)) + (function[2] * Math.Pow(x, 2)) + ((function[3] * Math.Pow(x, 3)) + (function[4] * Math.Pow(x, 4)));

            // Graph
            OxyPlot.Series.FunctionSeries functionSeries = new OxyPlot.Series.FunctionSeries(polyF, plotData[choice].Select(x => x.X).Min(), plotData[choice].Select(x => x.X).Max(), 100);

            PlotAudio.Series.Add(functionSeries);
        }

        /// <summary>
        /// Definiert Graph für alle Punkte und den Areas.
        /// </summary>
        private static void PlotAreas()
        {
            PlotAudio.Series.Clear();

            // graph
            List<OxyPlot.Series.LineSeries> sound_graph = new List<OxyPlot.Series.LineSeries>();

            // spalte einfügen
            for (int anzahl = 0; anzahl < plotData.Count; anzahl++)
            {
                sound_graph.Add(new OxyPlot.Series.LineSeries { });
                sound_graph[anzahl].Points.AddRange(plotData[anzahl]);

                // Style
                sound_graph[anzahl].Title = "Graph " + (anzahl + 1);
                //if (filtered_graph) { sound_graph[anzahl].LineStyle = LineStyle.Automatic; filtered_graph = false; }
                //else { sound_graph[anzahl].LineStyle = LineStyle.None; }
                sound_graph[anzahl].LineStyle = LineStyle.None;
                sound_graph[anzahl].MarkerType = MarkerType.Diamond;

                //Graph einfügen
                PlotAudio.Series.Add(sound_graph[anzahl]);
            }
        }

        /// <summary>
        /// Definieren von Bereichen aus Gesamtpunkten.
        /// </summary>
        private static void PointAreas()
        {
            List<DataPoint> unfiltered_data = new List<DataPoint>();
            unfiltered_data.AddRange(plotData[0]);
            plotData.Clear();

            // Dummy hinzufügen
            plotData.Add(new List<DataPoint>());
            plotData[0].Add(new DataPoint(0, 0));

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
                        for (int area = 0; area < plotData.Count; area++)
                        {
                            // Horizontal
                            differenz_horizontal = Math.Abs(col_points[row].X - plotData[area][plotData[area].Count - 1].X);
                            // vertikal
                            differenz_vertikal = Math.Abs(col_points[row].Y - plotData[area][plotData[area].Count - 1].Y);

                            // vorhandenem graphen hinzufügen
                            if (differenz_vertikal <= abstand && differenz_horizontal <= 50)
                            {
                                plotData[area].Add(col_points[row]);
                                break;
                            }
                            else
                            {
                                // neuen graphen beginnen, wenn nichts gefundnen wurde
                                if (area == plotData.Count - 1)
                                {
                                    plotData.Add(new List<DataPoint>() { col_points[row] });
                                    break;
                                }
                            }
                        }
                    }
                }
            }

            //dummy löschen
            plotData.Remove(plotData[0]);
        }

        /// <summary>
        /// Umrechnen von Hertz in Drehzahl.
        /// </summary>
        private static void PointsToRotionalSpeed()
        {
            List<List<DataPoint>> temp_plot_data = new List<List<DataPoint>>();

            // anzahl graphen
            for (int area = 0; area < plotData.Count; area++)
            {
                List<DataPoint> temp_list = new List<DataPoint>();

                // Punkt in graphen
                for (int col = 0; col < plotData[area].Count; col++)
                {
                    temp_list.Add(new DataPoint(Math.Round(plotData[area][col].X / segmentsPerSecond, 2), Math.Round(plotData[area][col].Y * hzPerFFT * 60)));
                }

                temp_plot_data.Add(temp_list);
            }

            plotData.Clear();
            plotData.AddRange(temp_plot_data);
        }
    }
}