// project=SimTuning.Core, file=DynoLogic.cs, creation=2020:7:31 Copyright (c) 2020 tuke
// productions. All rights reserved.
using Data.Models;
using MathNet.Numerics;
using OxyPlot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace SimTuning.Core.ModuleLogic
{
    /// <summary>
    /// Dyno Logik.
    /// </summary>
    public static class DynoLogic
    {
        #region variables

        private static Func<double, double> ausrollFunction;
        private static List<DataPoint> ausrollPoints = new List<DataPoint>();
        private static Func<double, double> beschleunigungsFunction;
        private static List<DataPoint> beschleunigungsPoints = new List<DataPoint>();
        private static Func<double, double> drehzahlfunction;
        private static int regressionsAnzahl = 5;

        /// <summary>
        /// Gets the drehzahl points.
        /// </summary>
        /// <value>The drehzahl points.</value>
        private static List<DataPoint> DrehzahlPoints => AudioLogic.DrehzahlPoints[Graphauswahl];

        /// <summary>
        /// Gets or sets the graphauswahl.
        /// </summary>
        /// <value>The graphauswahl.</value>
        public static int Graphauswahl { get; set; }

        /// <summary>
        /// Gets the plot audio.
        /// </summary>
        /// <value>The plot audio.</value>
        public static PlotModel PlotAudio { get; private set; }

        /// <summary>
        /// Gets the plot ausrollen.
        /// </summary>
        /// <value>The plot ausrollen.</value>
        public static PlotModel PlotAusrollen { get; private set; }

        /// <summary>
        /// Gets the plot beschleunigung.
        /// </summary>
        /// <value>The plot beschleunigung.</value>
        public static PlotModel PlotBeschleunigung { get; private set; }

        /// <summary>
        /// Gets the plot strength.
        /// </summary>
        /// <value>The plot strength.</value>
        public static PlotModel PlotStrength { get; private set; }

        /// <summary>
        /// Gets array Handling.
        /// 1. Liste = Graphen, 2. Liste = Punkte.
        /// </summary>
        private static List<List<DataPoint>> DrehzahlArrayPoints => AudioLogic.DrehzahlPoints;

        #endregion variables

        /// <summary>
        /// Berechnet.
        /// TODO: vervollständigen.
        /// </summary>
        public static void GetAusrollGraphFitted(List<AusrollenModel> ausrollenModels)
        {
            DefiniereAusrollGraph();

            // Points definieren
            for (int i = 0; i < ausrollenModels.Count; i++)
            {
                ausrollPoints.Add(new DataPoint(
                    ausrollenModels[i].Speed.Value,
                    (double)ausrollenModels[i].CreatedDate.Value.Millisecond
                ));
            }

            // Regressions-Punkte bilden
            ausrollFunction = Fit.PolynomialFunc(
                ausrollPoints.Select(x => x.X).ToArray(),
                ausrollPoints.Select(y => y.Y).ToArray(),
                regressionsAnzahl);

            LadeAusrollGraph();
        }

        /// <summary>
        /// Berechnet.
        /// TODO: vervollständigen.
        /// </summary>
        public static void GetBeschleunigungsGraphFitted(List<BeschleunigungModel> beschleunigungModels)
        {
            DefiniereBeschleunigungsGraph();

            // Points definieren
            for (int i = 0; i < beschleunigungModels.Count; i++)
            {
                beschleunigungsPoints.Add(new DataPoint(
                    beschleunigungModels[i].Speed.Value,
                    (double)beschleunigungModels[i].CreatedDate.Value.Millisecond
                ));
            }

            // Regressions-Punkte bilden
            beschleunigungsFunction = Fit.PolynomialFunc(
                beschleunigungsPoints.Select(x => x.X).ToArray(),
                beschleunigungsPoints.Select(y => y.Y).ToArray(),
                regressionsAnzahl);

            LadeBeschleunigungsGraph();
        }

        /// <summary>
        /// Gibt das Diagramm aus den Spectogram Daten zurück. vorher muss
        /// Audio-Spectrogram der Audio bereits einmal berechnet sein mit
        /// AudioLogic.GetSpectrogram().
        /// </summary>
        /// <param name="areas">
        /// if set to <c>true</c> [filter] Punkte werden zu Bereichen zugeordnet.
        /// </param>
        public static void GetDrehzahlGraph(bool areas = false, double intensity = 0.75, double areaAbstand = 8)
        {
            DefiniereDrehzahlGraph();

            AudioLogic.GetDrehzahlGraph(areas: areas, intensity: intensity, areaAbstand: areaAbstand);

            LadeDrehzahlGraph(fitted: false);
        }

        /// <summary>
        /// Gets the fitted drehzahl graph.
        /// </summary>
        /// <param name="drehzahlModels">The drehzahl models.</param>
        public static void GetDrehzahlGraphFitted(out List<DrehzahlModel> drehzahlModels)
        {
            DefiniereDrehzahlGraph();

            // Regressions-Punkte bilden
            drehzahlfunction = Fit.PolynomialFunc(
                DrehzahlPoints.Select(x => x.X).ToArray(),
                DrehzahlPoints.Select(x => x.Y).ToArray(),
                regressionsAnzahl);

            var _drehzahlModels = new List<DrehzahlModel>();

            // Audio Werte (X, Y) hinzufügen
            for (int count = 0; count < DrehzahlPoints.Count; count++)
            {
                _drehzahlModels.Add(new DrehzahlModel()
                {
                    Zeit = DrehzahlPoints[count].X,
                    Drehzahl = DrehzahlPoints[count].Y,
                });
            }

            LadeDrehzahlGraph(fitted: true);

            drehzahlModels = _drehzahlModels;
        }

        /// <summary>
        /// Berechnet die Leistung(PS) aus Beschleunigung, Drehzahl und Ausrollen.
        /// </summary>
        /// <param name="dyno">The dyno.</param>
        /// <param name="plot">The plot.</param>
        /// <param name="ps">The ps.</param>
        /// <param name="nm">The nm.</param>
        public static void GetLeistungsGraph(double gewicht, out List<DynoPsModel> dynoPsModels)
        {
            DefiniereLeistungsGraph();

            List<DynoPsModel> dynoPs = new List<DynoPsModel>();

            var max = ausrollPoints.Select(x => x.X).Max();
            for (int i = 0; i < max; i++)
            {
                // TODO: Formel
            }

            LadeLeistungsGraph();

            dynoPsModels = dynoPs;
        }

        /// <summary>
        /// Definieres the ausroll plot.
        /// </summary>
        private static void DefiniereAusrollGraph()
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
        private static void DefiniereBeschleunigungsGraph()
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
        private static void DefiniereDrehzahlGraph()
        {
            //if (PlotAudio != null)
            //{
            //    return;
            //}

            PlotAudio = new PlotModel();

            // Achsen
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

            // Achsen
            PlotStrength.Axes.Add(new OxyPlot.Axes.LinearAxis { Title = "Drehzahl in 1/min", Position = OxyPlot.Axes.AxisPosition.Bottom });
            PlotStrength.Axes.Add(new OxyPlot.Axes.LinearAxis { Title = "Leistung in PS", Position = OxyPlot.Axes.AxisPosition.Left });

            PlotStrength.IsLegendVisible = true;
            PlotStrength.LegendPosition = LegendPosition.RightTop;
        }

        /// <summary>
        /// Lades the ausroll graph.
        /// </summary>
        private static void LadeAusrollGraph(bool fitted = false)
        {
            if (fitted)
            {
                OxyPlot.Series.FunctionSeries functionSeries = new OxyPlot.Series.FunctionSeries(
                   ausrollFunction,
                   DrehzahlPoints.Select(x => x.X).Min(),
                   DrehzahlPoints.Select(x => x.X).Max(),
                   100);

            PlotAusrollen.Series.Add(functionSeries);
            }
            else
            {
            }
        }

        /// <summary>
        /// Lades the beschleunigungs graph.
        /// </summary>
        private static void LadeBeschleunigungsGraph(bool fitted = false)
        {
            if (fitted)
            {
                OxyPlot.Series.FunctionSeries functionSeries = new OxyPlot.Series.FunctionSeries(
                   beschleunigungsFunction,
                   DrehzahlPoints.Select(x => x.X).Min(),
                   DrehzahlPoints.Select(x => x.X).Max(),
                   100);

                PlotBeschleunigung.Series.Add(functionSeries);
            }
            else
            {
            }
        }

        /// <summary>
        /// Definiert Graph für alle Punkte und den Areas.
        /// </summary>
        private static void LadeDrehzahlGraph(bool fitted = false)
        {
            //PlotAudio.Series.Clear();

            if (fitted)
            {
                // Graph
                OxyPlot.Series.FunctionSeries functionSeries = new OxyPlot.Series.FunctionSeries(
                    drehzahlfunction,
                    DrehzahlPoints.Select(x => x.X).Min(),
                    DrehzahlPoints.Select(x => x.X).Max(),
                    100);

                PlotAudio.Series.Add(functionSeries);
            }
            else
            {
                // graph
                List<OxyPlot.Series.LineSeries> audioLine = new List<OxyPlot.Series.LineSeries>();

                // spalte einfügen
                for (int anzahl = 0; anzahl < DrehzahlArrayPoints.Count; anzahl++)
                {
                    audioLine.Add(new OxyPlot.Series.LineSeries { });
                    audioLine[anzahl].Points.AddRange(DrehzahlArrayPoints[anzahl]);

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
        /// Lades the leistungs graph.
        /// </summary>
        private static void LadeLeistungsGraph()
        {
            OxyPlot.Series.LineSeries leistungPsSeries = new OxyPlot.Series.LineSeries();

            // Punkte
            //for (int zaehler = 0; zaehler < dyno.Drehzahl.Count; zaehler++)
            //{
            //    // Nm leistung_nm.Points.Add(new DataPoint(dynoNm[zaehler].Drehzahl,
            //    // dynoNm[zaehler].Nm));

            //    // PS
            //    leistungPsSeries.Points.Add(new DataPoint(dynoPs[zaehler].Drehzahl, dynoPs[zaehler].Ps));
            //}

            // Style, Beschriftung leistung_nm.Title = "Leistung-Nm"; leistung_nm.Color =
            // OxyColors.DarkRed;

            leistungPsSeries.Title = "Leistung-PS";
            leistungPsSeries.Color = OxyColors.DarkBlue;

            // Graphen einfügen PlotStrength.Series.Add(leistung_nm);
            PlotStrength.Series.Add(leistungPsSeries);
        }
    }
}