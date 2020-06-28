using Data.Models;
using MathNet.Numerics;
using OxyPlot;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SimTuning.ModuleLogic
{
    public class DynoLogic
    {
        #region variables

        public PlotModel PlotStrength { get; set; }
        public PlotModel PlotAudio { get; set; }
        public DynoModel Dyno { get; private set; }

        //Spectrogram Data, Liste = Spalten, [] = Zeilen, float = Stärke der Frequenz
        private List<float[]> spec_data = new List<float[]>();

        //Array Handling
        private List<List<DataPoint>> plot_data = new List<List<DataPoint>>(); //1. Liste = Graphen, 2. Liste = Punkte

        private double strongPoint;
        private double abstand;

        private double hzPerFFT; //wieviel reihen ein Herz wiederspiegeln
        private double segmentsPerSecond;
        private double fft_beginn;
        private double fft_ende;

        //Punkte für die Polynomfunktion
        private List<double> function = new List<double>();

        #endregion variables

        /// <summary>
        /// Gibt das Diagramm aus den Spectogram Daten zurück
        /// </summary>
        /// <param name="SpectrogramAudio">Das Spectogram</param>
        /// <param name="filter">if set to <c>true</c> [filter] Punkte werden zu Bereichen zugeordnet</param>
        /// <returns></returns>
        public void PlotRotionalSpeed(Spectrogram.Spectrogram SpectrogramAudio, bool filter = false)
        {
            spec_data.Clear();
            plot_data.Clear();

            #region SpectrogramData

            //Daten verarbeiten
            spec_data.AddRange(SpectrogramAudio.fftList);
            hzPerFFT = SpectrogramAudio.fftSettings.fftResolution; //FFT-Array-elements pro 1 Hertz-> spec.fftSettings.maxFreq / fftSize
            segmentsPerSecond = SpectrogramAudio.fftSettings.segmentsPerSecond; //HORIZONTAL; anzahl spalten pro sekunde
            fft_beginn = Math.Round(Convert.ToDouble(SpectrogramAudio.displaySettings.freqLow) / hzPerFFT);
            fft_ende = Math.Round(Convert.ToDouble(SpectrogramAudio.displaySettings.freqHigh) / hzPerFFT);
            strongPoint = 350 / SpectrogramAudio.displaySettings.brightness; //selbst ausgewählt
            abstand = Math.Round(Math.Sqrt(Convert.ToDouble(SpectrogramAudio.displaySettings.freqHigh - SpectrogramAudio.displaySettings.freqLow)) / 2); //Hälfte der wurzel des Frequenzbereichs

            #endregion SpectrogramData

            HotPoints();

            if (filter) { PointAreas(); }

            PointsToRotionalSpeed();

            DefineAudioPlot();

            PlotAreas();
        }

        /// <summary>
        /// Bildet eine Regression aus Punkten
        /// </summary>
        /// <param name="choice">Der zu filternde Graph</param>
        public void AreaRegression(int choice)
        {
            Dyno = new DynoModel();
            Dyno.Audio = new List<DynoAudioModel>();

            //Regressions-Punkte bilden
            function = Fit.Polynomial(plot_data[choice].Select(x => x.X).ToArray(), plot_data[choice].Select(x => x.Y).ToArray(), 4).ToList();  // 5 Punkte

            PlotAreaRegression(choice);

            //Audio Werte (X, Y) hinzufügen
            for (int count = 0; count < plot_data[0].Count; count++)
            {
                Dyno.Audio.Add(new DynoAudioModel()
                {
                    X = plot_data[0][count].X,
                    Y = plot_data[0][count].Y
                });
            }
        }

        /// <summary>
        /// Definiert die Punkte die Punkte die für eine Analyse verwertbar sind
        /// </summary>
        private void HotPoints()
        {
            List<DataPoint> temp_data = new List<DataPoint>();

            for (int col = 0; col < spec_data.Count; col++)
            {
                //nur für ausgewählten bereich
                List<DataPoint> temp_points = new List<DataPoint>();

                for (int row = (int)fft_beginn; row <= fft_ende; row++)
                {
                    //Holen der intensivsten Punkte
                    if (spec_data[col][row] >= strongPoint)
                    {
                        //row speichern
                        temp_points.Add(new DataPoint(col, row));
                    }
                }

                //gespeicherte Punkte(wenn gefunden) der Liste hinzufügen
                if (temp_points.Count > 0) { temp_data.AddRange(temp_points); }
            }

            if (temp_data.Count > 0) { plot_data.Add(temp_data); }
        }

        /// <summary>
        /// Definieren von Bereichen aus Gesamtpunkten
        /// </summary>
        private void PointAreas()
        {
            List<DataPoint> unfiltered_data = new List<DataPoint>();
            unfiltered_data.AddRange(plot_data[0]);
            plot_data.Clear();

            //Dummy hinzufügen
            plot_data.Add(new List<DataPoint>());
            plot_data[0].Add(new DataPoint(0, 0));

            var maxcol = unfiltered_data.Max(i => i.X);

            for (int col = 0; col <= maxcol; col++) //X
            {
                List<DataPoint> col_points = new List<DataPoint>();

                col_points.AddRange(unfiltered_data.Where(x => x.X == col));
                double differenz_horizontal = 0.0;
                double differenz_vertikal = 0.0;

                for (int row = 0; row < col_points.Count; row++) //Y
                {
                    if (col_points.Count > 0)
                    {
                        //vergleichen mit hinzugefügten areas
                        for (int area = 0; area < plot_data.Count; area++)
                        {
                            //Horizontal
                            differenz_horizontal = Math.Abs(col_points[row].X - plot_data[area][plot_data[area].Count - 1].X);
                            //vertikal
                            differenz_vertikal = Math.Abs(col_points[row].Y - plot_data[area][plot_data[area].Count - 1].Y);

                            //vorhandenem graphen hinzufügen
                            if (differenz_vertikal <= abstand && differenz_horizontal <= 50) { plot_data[area].Add(col_points[row]); break; }
                            else
                            {
                                //neuen graphen beginnen, wenn nichts gefundnen wurde
                                if (area == plot_data.Count - 1) { plot_data.Add(new List<DataPoint>() { col_points[row] }); break; }
                            }
                        }
                    }
                }
            }

            //dummy löschen
            plot_data.Remove(plot_data[0]);
        }

        /// <summary>
        /// Umrechnen von Hertz in Drehzahl
        /// </summary>
        private void PointsToRotionalSpeed()
        {
            List<List<DataPoint>> temp_plot_data = new List<List<DataPoint>>();

            //anzahl graphen
            for (int area = 0; area < plot_data.Count; area++)
            {
                List<DataPoint> temp_list = new List<DataPoint>();

                //Punkt in graphen
                for (int col = 0; col < plot_data[area].Count; col++)
                {
                    temp_list.Add(new DataPoint(Math.Round(plot_data[area][col].X / segmentsPerSecond, 2), Math.Round(plot_data[area][col].Y * hzPerFFT * 60)));
                }

                temp_plot_data.Add(temp_list);
            }

            plot_data.Clear();
            plot_data.AddRange(temp_plot_data);
        }

        /// <summary>
        /// Initialisierung des Graphen
        /// </summary>
        private void DefineAudioPlot()
        {
            PlotAudio = new PlotModel();

            //Achsen
            PlotAudio.Axes.Add(new OxyPlot.Axes.LinearAxis { Title = "Zeit in s", Position = OxyPlot.Axes.AxisPosition.Bottom, MajorGridlineStyle = LineStyle.Solid, MinorGridlineStyle = LineStyle.Dot });
            PlotAudio.Axes.Add(new OxyPlot.Axes.LinearAxis { Title = "Drehzahl in 1/min", Position = OxyPlot.Axes.AxisPosition.Left, MajorGridlineStyle = LineStyle.Solid, MinorGridlineStyle = LineStyle.Dot });

            PlotAudio.IsLegendVisible = true;
            PlotAudio.LegendPosition = LegendPosition.RightTop;
        }

        /// <summary>
        /// Definiert Graph für alle Punkte und den Ares
        /// </summary>
        private void PlotAreas()
        {
            PlotAudio.Series.Clear();

            //graph
            List<OxyPlot.Series.LineSeries> sound_graph = new List<OxyPlot.Series.LineSeries>();

            //spalte einfügen
            for (int anzahl = 0; anzahl < plot_data.Count; anzahl++)
            {
                sound_graph.Add(new OxyPlot.Series.LineSeries { });
                sound_graph[anzahl].Points.AddRange(plot_data[anzahl]);

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
        /// Definiert den Graph für den Graph der Regression eines Bereichs
        /// </summary>
        private void PlotAreaRegression(int choice)
        {
            PlotAudio.Series.Clear();
            PlotAudio.IsLegendVisible = true;

            Func<double, double> polyF = (x) => function[0] + (function[1] * x) + (function[2] * Math.Pow(x, 2)) + (function[3] * Math.Pow(x, 3) + (function[4] * Math.Pow(x, 4)));

            //graph
            OxyPlot.Series.FunctionSeries functionSeries = new OxyPlot.Series.FunctionSeries(polyF, plot_data[choice].Select(x => x.X).Min(), plot_data[choice].Select(x => x.X).Max(), 100);

            PlotAudio.Series.Add(functionSeries);
        }

        /// <summary>
        /// Initialisierung des Graphen
        /// </summary>
        private void DefineStrengthPlot()
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
        /// Plots the data leistung.
        /// </summary>
        /// <param name="dyno">The dyno.</param>
        /// <param name="plot">The plot.</param>
        /// <param name="ps">The ps.</param>
        /// <param name="nm">The nm.</param>
        public void CalculateStrengthPlot(DynoModel dyno, out List<DynoPSModel> ps, out List<DynoNmModel> nm)
        {
            DefineStrengthPlot();

            List<DynoNmModel> DynoNm = new List<DynoNmModel>();
            List<DynoPSModel> DynoPs = new List<DynoPSModel>();

            //in m
            double radhalbmesser = 0.4064; //16zoll

            //in kg/m^3
            double luftdichte = dyno.Environment.LuftdruckP.Value / 287.05 * (dyno.Environment.TemperaturT.Value + 273.15); //Gaskonstante 287.05 J/kg*K(trockene Luft), °C in Kelvin umrechnen

            for (int col = 0; col < dyno.Audio.Count; col++)
            {
                //in 1/min
                double drehzahl = dyno.Audio[col].Y;
                //in s
                double zeit = dyno.Audio[col].X;

                //in m/s
                double geschwindigkeit = (2 * radhalbmesser * Math.PI * drehzahl / (dyno.Vehicle.Uebersetzung.Value * 1000)) / 3.6;

                //(a = v / t) in m/s
                double beschleunigung = geschwindigkeit / zeit;

                //vertikale Kraft (F=m*a) in N
                double kraft_gewicht = dyno.Vehicle.Gewicht.Value * 9.81;

                //horizontale Kraft
                double kraft_treibend = dyno.Vehicle.Gewicht.Value * beschleunigung;
                double kraft_bremsend = (dyno.Vehicle.Cw.Value * dyno.Vehicle.FrontA.Value * luftdichte * Math.Pow(geschwindigkeit, 2)) / 2; //Luftwiderstand

                //GESAMT KRAFT
                double kraft = Math.Sqrt(Math.Pow(kraft_treibend - kraft_bremsend, 2) + Math.Pow(kraft_gewicht, 2));

                //(s=v*t) in m
                double weg = geschwindigkeit * zeit;

                //(W=F*s) in Nm
                double Nm = kraft * weg;
                DynoNm.Add(new DynoNmModel() { X = drehzahl, Y = Nm });

                //P=W/t (1Ps=1Nm/735.498750000002) in PS
                double PS = Nm / zeit / 735.498750000002;
                DynoPs.Add(new DynoPSModel() { X = drehzahl, Y = PS });
            }

            OxyPlot.Series.LineSeries leistung_nm = new OxyPlot.Series.LineSeries();
            OxyPlot.Series.LineSeries leistung_ps = new OxyPlot.Series.LineSeries();

            //Punkte
            for (int zaehler = 0; zaehler < dyno.Audio.Count; zaehler++)
            {
                //Nm
                leistung_nm.Points.Add(new DataPoint(DynoNm[zaehler].X, DynoNm[zaehler].Y));

                //PS
                leistung_ps.Points.Add(new DataPoint(DynoPs[zaehler].X, DynoPs[zaehler].Y));
            }

            //Style, Beschriftung
            leistung_nm.Title = "Leistung-Nm";
            leistung_nm.Color = OxyColors.DarkRed;

            leistung_ps.Title = "Leistung-PS";
            leistung_ps.Color = OxyColors.DarkBlue;

            //Graphen einfügen
            PlotStrength.Series.Add(leistung_nm);
            PlotStrength.Series.Add(leistung_ps);

            nm = DynoNm;
            ps = DynoPs;
        }
    }
}