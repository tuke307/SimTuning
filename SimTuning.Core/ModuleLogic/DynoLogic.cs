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

        private static double fftBeginn;

        private static double fftEnde;

        private static Func<double, double> function;

        /// <summary>
        /// FFT-Array-elements pro 1 Hertz.
        /// </summary>
        private static double hzPerFFT;

        private static int regressionsAnzahl = 5;

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

        public static int Graphauswahl { get; set; }

        /// <summary>
        /// Gets or sets the plot audio.
        /// </summary>
        /// <value>The plot audio.</value>
        public static PlotModel PlotAudio { get; private set; }

        /// <summary>
        /// Gets or sets the plot ausrollen.
        /// </summary>
        /// <value>The plot ausrollen.</value>
        public static PlotModel PlotAusrollen { get; private set; }

        /// <summary>
        /// Gets or sets the plot beschleunigung.
        /// </summary>
        /// <value>The plot beschleunigung.</value>
        public static PlotModel PlotBeschleunigung { get; private set; }

        /// <summary>
        /// Gets or sets the plot strength.
        /// </summary>
        /// <value>The plot strength.</value>
        public static PlotModel PlotStrength { get; private set; }

        /// <summary>
        /// TODO: nutzer soll dies einstellen können.
        /// </summary>
        public static double PunktAbstand { get; set; }

        #endregion variables

        /// <summary>
        /// Berechnet.
        /// TODO: vervollständigen.
        /// </summary>
        public static void BerechneAusrollGraph(List<AusrollenModel> ausrollenModels = null)
        {
            DefiniereAusrollPlot();
        }

        /// <summary>
        /// Berechnet.
        /// TODO: vervollständigen.
        /// </summary>
        public static void BerechneBeschleunigungsGraph(List<BeschleunigungModel> beschleunigungModels = null)
        {
            DefiniereBeschleunigungsPlot();

            // MathNet.Numerics.Interpolation.CubicSpline.InterpolateAkima();
        }

        /// <summary>
        /// Gibt das Diagramm aus den Spectogram Daten zurück. vorher muss
        /// Audio-Spectrogram der Audio bereits einmal berechnet sein mit
        /// AudioLogic.GetSpectrogram().
        /// </summary>
        /// <param name="areas">
        /// if set to <c>true</c> [filter] Punkte werden zu Bereichen zugeordnet.
        /// </param>
        /// <param name="fitted">
        /// if set to <c>true</c> [fitted] Punkte eines Graphen werden mithilfe der spline
        /// Interpolation interpoliert.
        /// </param>
        public static void BerechneDrehzahlGraph(bool areas = false, bool fitted = false)
        {
            DefiniereAudioPlot();

            if (fitted)
            {
                // Regressions-Punkte bilden
                function = Fit.PolynomialFunc(plotData[Graphauswahl].Select(x => x.X).ToArray(), plotData[Graphauswahl].Select(x => x.Y).ToArray(), regressionsAnzahl);
                // MathNet.Numerics.Interpolation.CubicSpline.InterpolateAkima();

                Dyno = new DynoModel();
                Dyno.Drehzahl = new List<DrehzahlModel>();

                // Audio Werte (X, Y) hinzufügen
                for (int count = 0; count < plotData[0].Count; count++)
                {
                    Dyno.Drehzahl.Add(new DrehzahlModel()
                    {
                        Zeit = plotData[0][count].X,
                        Drehzahl = plotData[0][count].Y,
                    });
                }

                LadeDrehzahlGraph(fitted: true);
            }
            else
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
                PunktAbstand = Math.Round(Math.Sqrt(Convert.ToDouble(AudioLogic.SpectrogramAudio.DisplaySettings.freqHigh - AudioLogic.SpectrogramAudio.DisplaySettings.freqLow)) / 2); // Hälfte der wurzel des Frequenzbereichs

                #endregion SpectrogramData

                HotPoints();

                if (areas)
                {
                    PointsToAreas();
                }

                PointsToRotionalSpeed();

                LadeDrehzahlGraph(fitted: false);
            }
        }

        /// <summary>
        /// Berechnet die Leistung(PS) aus Beschleunigung, Drehzahl und Ausrollen.
        /// </summary>
        /// <param name="dyno">The dyno.</param>
        /// <param name="plot">The plot.</param>
        /// <param name="ps">The ps.</param>
        /// <param name="nm">The nm.</param>
        public static void BerechneLeistungsGraph(DynoModel dyno, out List<DynoPsModel> ps/*, out List<DynoNmModel> nm*/)
        {
            DefiniereLeistungsPlot();

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
        /// Initialisierung des Graphen.
        /// </summary>
        private static void DefiniereAudioPlot()
        {
            if (PlotAudio != null)
            {
                return;
            }

            PlotAudio = new PlotModel();

            // Achsen
            PlotAudio.Axes.Add(new OxyPlot.Axes.LinearAxis { Title = "Zeit in s", Position = OxyPlot.Axes.AxisPosition.Bottom, MajorGridlineStyle = LineStyle.Solid, MinorGridlineStyle = LineStyle.Dot });
            PlotAudio.Axes.Add(new OxyPlot.Axes.LinearAxis { Title = "Drehzahl in 1/min", Position = OxyPlot.Axes.AxisPosition.Left, MajorGridlineStyle = LineStyle.Solid, MinorGridlineStyle = LineStyle.Dot });

            PlotAudio.IsLegendVisible = true;
            PlotAudio.LegendPosition = LegendPosition.RightTop;
        }

        /// <summary>
        /// Definieres the ausroll plot.
        /// </summary>
        private static void DefiniereAusrollPlot()
        {
            if (PlotAusrollen != null)
            {
                return;
            }

            PlotAusrollen = new PlotModel();

            // Achsen
            PlotAusrollen.Axes.Add(new OxyPlot.Axes.LinearAxis { Title = "Zeit in s", Position = OxyPlot.Axes.AxisPosition.Bottom, MajorGridlineStyle = LineStyle.Solid, MinorGridlineStyle = LineStyle.Dot });
            PlotAusrollen.Axes.Add(new OxyPlot.Axes.LinearAxis { Title = "Geschwindigkeit in km/h", Position = OxyPlot.Axes.AxisPosition.Left, MajorGridlineStyle = LineStyle.Solid, MinorGridlineStyle = LineStyle.Dot });

            PlotAusrollen.IsLegendVisible = true;
            PlotAusrollen.LegendPosition = LegendPosition.RightTop;
        }

        /// <summary>
        /// Definieres the beschleunigungs plot.
        /// </summary>
        private static void DefiniereBeschleunigungsPlot()
        {
            if (PlotBeschleunigung != null)
            {
                return;
            }

            PlotBeschleunigung = new PlotModel();

            // Achsen
            PlotBeschleunigung.Axes.Add(new OxyPlot.Axes.LinearAxis { Title = "Zeit in s", Position = OxyPlot.Axes.AxisPosition.Bottom, MajorGridlineStyle = LineStyle.Solid, MinorGridlineStyle = LineStyle.Dot });
            PlotBeschleunigung.Axes.Add(new OxyPlot.Axes.LinearAxis { Title = "Geschwindigkeit in km/h", Position = OxyPlot.Axes.AxisPosition.Left, MajorGridlineStyle = LineStyle.Solid, MinorGridlineStyle = LineStyle.Dot });

            PlotBeschleunigung.IsLegendVisible = true;
            PlotBeschleunigung.LegendPosition = LegendPosition.RightTop;
        }

        /// <summary>
        /// Initialisierung des Graphen.
        /// </summary>
        private static void DefiniereLeistungsPlot()
        {
            PlotStrength = new PlotModel();

            // Achsen
            PlotStrength.Axes.Add(new OxyPlot.Axes.LinearAxis { Title = "Drehzahl in 1/min", Position = OxyPlot.Axes.AxisPosition.Bottom });
            PlotStrength.Axes.Add(new OxyPlot.Axes.LinearAxis { Title = "Leistung in PS", Position = OxyPlot.Axes.AxisPosition.Left });

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
        /// Definiert Graph für alle Punkte und den Areas.
        /// </summary>
        private static void LadeDrehzahlGraph(bool fitted = false)
        {
            PlotAudio.Series.Clear();

            if (fitted)
            {
                // Graph
                OxyPlot.Series.FunctionSeries functionSeries = new OxyPlot.Series.FunctionSeries(
                    function,
                    plotData[Graphauswahl].Select(x => x.X).Min(),
                    plotData[Graphauswahl].Select(x => x.X).Max(),
                    100);

                PlotAudio.Series.Add(functionSeries);
            }
            else
            {
                // graph
                List<OxyPlot.Series.LineSeries> audioLine = new List<OxyPlot.Series.LineSeries>();

                // spalte einfügen
                for (int anzahl = 0; anzahl < plotData.Count; anzahl++)
                {
                    audioLine.Add(new OxyPlot.Series.LineSeries { });
                    audioLine[anzahl].Points.AddRange(plotData[anzahl]);

                    // Style
                    audioLine[anzahl].Title = "Graph " + (anzahl + 1);
                    //if (filtered_graph) { sound_graph[anzahl].LineStyle = LineStyle.Automatic; filtered_graph = false; }
                    //else { sound_graph[anzahl].LineStyle = LineStyle.None; }
                    audioLine[anzahl].LineStyle = LineStyle.None;
                    audioLine[anzahl].MarkerType = MarkerType.Diamond;

                    //Graph einfügen
                    PlotAudio.Series.Add(audioLine[anzahl]);
                }
            }
        }

        /// <summary>
        /// Definieren von Bereichen aus Gesamtpunkten.
        /// </summary>
        private static void PointsToAreas()
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
                            if (differenz_vertikal <= PunktAbstand && differenz_horizontal <= 50)
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