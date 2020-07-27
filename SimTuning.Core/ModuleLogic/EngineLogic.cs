using SimTuning.Core.Business;
using SimTuning.Core.Models;
using SkiaSharp;
using System;
using System.Collections.Generic;

namespace SimTuning.Core.ModuleLogic
{
    /// <summary>
    /// Motor Logik.
    /// </summary>
    public static class EngineLogic
    {
        /// <summary>
        /// Gibt die Übersetzung zurück.
        /// </summary>
        /// <param name="getriebe">The getriebe.</param>
        /// <param name="primaer">The primaer.</param>
        /// <param name="sekundaer">The sekundaer.</param>
        /// <returns>Übersetzung.</returns>
        public static double GetTransmission(double getriebe = 0, double primaer = 0, double sekundaer = 0)
        {
            double uebersetzung = getriebe * sekundaer * primaer;

            return uebersetzung;
        }

        /// <summary>
        /// bestimmt den halben radius
        /// </summary>
        /// <param name="hub">The hub.</param>
        /// <param name="pleullaenge">The pleullaenge.</param>
        /// <param name="deachsierung">The deachsierung.</param>
        /// <returns></returns>
        public static double GetStrokeRadius(double hub, double pleullaenge, double deachsierung)
        {
            double hubradius = hub *
                        Math.Sqrt(Math.Abs(Math.Pow(4 * deachsierung, 2) + Math.Pow(hub, 2) - (4 * Math.Pow(pleullaenge, 2)))) /
                        Math.Sqrt(Math.Abs((4 * Math.Pow(hub, 2)) - (16 * Math.Pow(pleullaenge, 2))));

            hubradius = Math.Round(hubradius, 2);

            return hubradius;
        }

        /// <summary>
        /// Berechnet Grad vor OT.
        /// </summary>
        /// <param name="pleullaenge">The pleullaenge.</param>
        /// <param name="hubradius">The hubradius.</param>
        /// <param name="deachsierung">The deachsierung.</param>
        /// <param name="kwgrad">The kwgrad.</param>
        /// <returns>Länge vor OT in mm.</returns>
        public static double GetDistanceToOT(double pleullaenge, double hubradius, double deachsierung, double kwgrad)
        {
            // übergebene Gradmaß in Bogenmaß umrechnen
            kwgrad = kwgrad / 180 * Math.PI * 1;

            double mmvorot = Math.Sqrt(Math.Pow(pleullaenge + hubradius, 2) - Math.Pow(deachsierung, 2)) -
                      (hubradius *
                      Math.Cos(Math.Asin(deachsierung / (pleullaenge + hubradius)) + kwgrad) +
                      Math.Sqrt(
                      Math.Pow(pleullaenge, 2) -
                      Math.Pow(Math.Sin(Math.Asin(deachsierung / (pleullaenge + hubradius)) + kwgrad) * hubradius - deachsierung, 2)));

            mmvorot = Math.Round(mmvorot, 2);

            return mmvorot;
        }

        /// <summary>
        /// Gets the unterschied grad.
        /// </summary>
        /// <param name="inmm">Rückgabeeinheit.</param>
        /// <param name="vorher">The vorher.</param>
        /// <param name="nachher">The nachher.</param>
        /// <param name="pleullaenge">The pleullaenge.</param>
        /// <param name="hubradius">The hubradius.</param>
        /// <param name="deachsierung">The deachsierung.</param>
        /// <returns>Differenz in Grad oder mm</returns>
        public static double GetPortTimingDifference(bool inmm, double vorher, double nachher, double pleullaenge = 0, double hubradius = 0, double deachsierung = 0)
        {
            // umrechnen in mm
            if (inmm)
            {
                vorher = GetDistanceToOT(pleullaenge, hubradius, deachsierung, vorher);
                nachher = GetDistanceToOT(pleullaenge, hubradius, deachsierung, nachher);
            }

            double differenz = nachher - vorher;
            differenz = Math.Abs(differenz);

            differenz = Math.Round(differenz, 2);

            return differenz;
        }

        /// <summary>
        /// Steuerwinkels öffnet.
        /// </summary>
        /// <param name="steuerzeit_einlass">The steuerzeit einlass.</param>
        /// <param name="steuerzeit_auslass">The steuerzeit auslass.</param>
        /// <param name="steuerzeit_ueberstroemer">The steuerzeit ueberstroemer.</param>
        /// <returns></returns>
        public static double GetSteuerwinkelOeffnet(double steuerzeit_einlass = 0, double steuerzeit_auslass = 0, double steuerzeit_ueberstroemer = 0)
        {
            double steuerwinkel_oeffnet = 0;

            if (steuerzeit_einlass != 0)
            {
                steuerwinkel_oeffnet = 360 - (steuerzeit_einlass / 2);
            }
            else if (steuerzeit_auslass != 0)
            {
                steuerwinkel_oeffnet = 180 - (steuerzeit_auslass / 2);
            }
            else if (steuerzeit_ueberstroemer != 0)
            {
                steuerwinkel_oeffnet = 180 - (steuerzeit_ueberstroemer / 2);
            }

            steuerwinkel_oeffnet = Math.Round(steuerwinkel_oeffnet, 2);

            return steuerwinkel_oeffnet;
        }

        /// <summary>
        /// Steuerwinkels schließt.
        /// </summary>
        /// <param name="steuerzeit_einlass">The steuerzeit einlass.</param>
        /// <param name="steuerzeit_auslass">The steuerzeit auslass.</param>
        /// <param name="steuerzeit_ueberstroemer">The steuerzeit ueberstroemer.</param>
        /// <returns></returns>
        public static double GetSteuerwinkelSchließt(double steuerzeit_einlass = 0, double steuerzeit_auslass = 0, double steuerzeit_ueberstroemer = 0)
        {
            double steuerwinkel_schließt = 0;

            if (steuerzeit_einlass != 0)
            {
                steuerwinkel_schließt = steuerzeit_einlass / 2;
            }
            else if (steuerzeit_auslass != 0)
            {
                steuerwinkel_schließt = 180 + (steuerzeit_auslass / 2);
            }
            else if (steuerzeit_ueberstroemer != 0)
            {
                steuerwinkel_schließt = 180 + (steuerzeit_ueberstroemer / 2);
            }

            steuerwinkel_schließt = Math.Round(steuerwinkel_schließt, 2);

            return steuerwinkel_schließt;
        }

        /// <summary>
        /// Gets the steuerwinkel.
        /// </summary>
        /// <param name="vorher_steuerzeit">The vorher steuerzeit.</param>
        /// <param name="nachher_steuerzeit">The nachher steuerzeit.</param>
        /// <param name="kolbenoberkantekante_checked">if set to <c>true</c> [kolbenoberkantekante checked].</param>
        /// <param name="kolbenunterkante_checked">if set to <c>true</c> [kolbenunterkante checked].</param>
        /// <returns></returns>
        public static List<double> GetSteuerwinkel(double vorher_steuerzeit, double nachher_steuerzeit, bool kolbenoberkantekante_checked, bool kolbenunterkante_checked)
        {
            List<double> steuerwinkel = new List<double>();

            //auslass, überströmer
            if (kolbenunterkante_checked)
            {
                //öffnet vorher
                steuerwinkel.Add(180 - (vorher_steuerzeit / 2));

                //schließt vorher
                steuerwinkel.Add(180 + (vorher_steuerzeit / 2));

                //öffnet vorher
                steuerwinkel.Add(180 - (nachher_steuerzeit / 2));

                //schließt vorher
                steuerwinkel.Add(180 + (nachher_steuerzeit / 2));
            }
            //einlass
            else if (kolbenoberkantekante_checked)
            {
                //öffnet nachher
                steuerwinkel.Add(360 - (vorher_steuerzeit / 2));

                //schließt nachher
                steuerwinkel.Add(0 + (vorher_steuerzeit / 2));

                //öffnet nachher
                steuerwinkel.Add(360 - (nachher_steuerzeit / 2));

                //schließt nachher
                steuerwinkel.Add(0 + (nachher_steuerzeit / 2));
            }

            return steuerwinkel;
        }

        /// <summary>
        /// Vorauslasses the specified steuerwinkel auslass.
        /// </summary>
        /// <param name="steuerwinkel_auslass">The steuerwinkel auslass.</param>
        /// <param name="steuerwinkel_ueberstroemer">The steuerwinkel ueberstroemer.</param>
        /// <returns></returns>
        public static double GetVorauslass(double steuerwinkel_auslass, double steuerwinkel_ueberstroemer)
        {
            double vorauslass = (steuerwinkel_auslass - steuerwinkel_ueberstroemer) / 2;

            vorauslass = Math.Round(vorauslass, 2);

            return vorauslass;
        }

        /// <summary>
        /// Zeichnet das Steuerdiagramm.
        /// </summary>
        /// <param name="einlass">Einlass-Steuerzeit.</param>
        /// <param name="auslass">Auslass-Steuerzeit.</param>
        /// <param name="ueberstroemer">Überstroemer-Steuerzeit.</param>
        /// <returns>Bild des Steuerdiagramms.</returns>
        public static SKBitmap GetPortTimingCircle(double einlass, double auslass, double ueberstroemer)
        {
            int radMaß = 500; // quadratisch
            int radius = radMaß / 2;
            int rand = 50; // zu jeder seite hin
            int mitte = (500 + 50 + 50) / 2;

            SKBitmap bmp_rad = new SKBitmap(radMaß + (rand * 2), radMaß + (rand * 2));
            SKCanvas graphic_rad = new SKCanvas(bmp_rad);
            graphic_rad.Clear();
            //SKBitmap bmp_complet = new SKBitmap(radMaß + 100, radMaß + 100);
            //SKCanvas graphic_complet = new SKCanvas(bmp_complet);

            SKPaint blackPen = new SKPaint
            {
                Style = SKPaintStyle.Stroke,
                Color = SKColors.Black,
                StrokeWidth = 4,
                IsAntialias = true,
                TextSize = 22
            };
            SKPaint redPen = new SKPaint
            {
                Style = SKPaintStyle.Stroke,
                Color = SKColors.DarkRed,
                StrokeWidth = 4,
                IsAntialias = true
            };
            SKPaint bluePen = new SKPaint
            {
                Style = SKPaintStyle.Stroke,
                Color = SKColors.DarkBlue,
                StrokeWidth = 4,
                IsAntialias = true
            };
            SKPaint greenPen = new SKPaint
            {
                Style = SKPaintStyle.Stroke,
                Color = SKColors.DarkGreen,
                StrokeWidth = 4,
                IsAntialias = true
            };

            SKPoint koordinaten;

            //Steuerwinkel Kreis
            graphic_rad.DrawCircle(new SKPoint(mitte, mitte),
                                   radius,
                                   blackPen);

            //Dash auf schwarzen pinsel erzeugen
            blackPen.PathEffect = SKPathEffect.CreateDash(new float[] { 5, 5 }, 5);
            blackPen.StrokeWidth = 3;

            ////UT und OT
            graphic_rad.DrawLine(new SKPoint(mitte, 0),
                                 new SKPoint(mitte, bmp_rad.Height), blackPen);

            ////Hilfslinien 270° und 90°
            graphic_rad.DrawLine(new SKPoint(0, mitte),
                                 new SKPoint(bmp_rad.Width, mitte), blackPen);

            //Einlass öffnet
            koordinaten = GetPointOnCircle(GetSteuerwinkelOeffnet(steuerzeit_einlass: einlass), radius);
            graphic_rad.DrawLine(new SKPoint(mitte, mitte),
                                 new SKPoint(koordinaten.X + rand, koordinaten.Y + rand),
                                 redPen);

            //Einlass schließt
            koordinaten = GetPointOnCircle(GetSteuerwinkelSchließt(steuerzeit_einlass: einlass), radius);
            graphic_rad.DrawLine(new SKPoint(mitte, mitte),
                                 new SKPoint(koordinaten.X + rand, koordinaten.Y + rand),
                                 redPen);

            //Auslass öffnet
            koordinaten = GetPointOnCircle(GetSteuerwinkelOeffnet(steuerzeit_auslass: auslass), radius);
            graphic_rad.DrawLine(new SKPoint(mitte, mitte),
                                 new SKPoint(koordinaten.X + rand, koordinaten.Y + rand),
                                 bluePen);

            //Auslass schließt
            koordinaten = GetPointOnCircle(GetSteuerwinkelSchließt(steuerzeit_auslass: auslass), radius);
            graphic_rad.DrawLine(new SKPoint(mitte, mitte),
                                 new SKPoint(koordinaten.X + rand, koordinaten.Y + rand),
                                 bluePen);

            //Überströmer öffnet
            koordinaten = GetPointOnCircle(GetSteuerwinkelOeffnet(steuerzeit_ueberstroemer: ueberstroemer), radius);
            graphic_rad.DrawLine(new SKPoint(mitte, mitte),
                                 new SKPoint(koordinaten.X + rand, koordinaten.Y + rand),
                                 greenPen);

            //Überströmer schließt
            koordinaten = GetPointOnCircle(GetSteuerwinkelSchließt(steuerzeit_ueberstroemer: ueberstroemer), radius);
            graphic_rad.DrawLine(new SKPoint(mitte, mitte),
                                 new SKPoint(koordinaten.X + rand, koordinaten.Y + rand),
                                 greenPen);

            //drehen
            Functions.RotateBitmap(bmp_rad, 270).CopyTo(bmp_rad);

            //gedrehete Bitmap erneut in Canvas einfügen
            graphic_rad = new SKCanvas(bmp_rad);

            //Textbezeichnung hinzufügen
            blackPen.PathEffect = null;
            graphic_rad.DrawText("OT", mitte + 10, 30, blackPen);
            graphic_rad.DrawText("UT", mitte - 40, 580, blackPen);

            return bmp_rad;
        }

        /// <summary>
        /// Points the on circle.
        /// </summary>
        /// <param name="angle">The angle.</param>
        /// <param name="radius">The radius.</param>
        /// <returns></returns>
        public static SKPoint GetPointOnCircle(double angle, double radius)
        {
            SKPoint coordinates = new SKPoint();

            double x_richtung = radius * Math.Cos(angle * Math.PI / 180);
            coordinates.X = Convert.ToInt32(radius + x_richtung);

            double y_richtung = radius * Math.Sin(angle * Math.PI / 180);
            if (y_richtung >= 0)
            {
                coordinates.Y = Convert.ToInt32(radius - y_richtung);
            }
            else if (y_richtung <= 0)
            {
                coordinates.Y = Convert.ToInt32(radius + Math.Abs(y_richtung));
            }

            return coordinates;
        }

        /// <summary>
        /// Gets the verdichtung.
        /// </summary>
        /// <param name="hubraum">The hubraum.</param>
        /// <param name="brennraum">The brennraum.</param>
        /// <param name="durchmesser">The durchmesser.</param>
        /// <returns></returns>
        public static double GetCompression(double hubraum, double brennraum, double durchmesser)
        {
            double verdichtung = (hubraum + brennraum) / brennraum;
            verdichtung = Math.Round(verdichtung, 1);

            return verdichtung;
        }

        /// <summary>
        /// Berechnet den abzunehmenden Bereich am Zylinderkopf.
        /// </summary>
        /// <param name="hubraum">Hubraum.</param>
        /// <param name="brennraum">Brennraum.</param>
        /// <param name="durchmesser">Zylinder-Durchmesser.</param>
        /// <param name="zielVerdichtung">Zielverdichtung.</param>
        /// <returns>den abzunehmenden Bereich in mm.</returns>
        public static double GetToDecreasingLength(double hubraum, double brennraum, double durchmesser, double zielVerdichtung)
        {
            double abdrehen_mm = 4 * ((brennraum * zielVerdichtung) - brennraum - hubraum) / (Math.PI * ((Math.Pow(durchmesser, 2) * zielVerdichtung) - Math.Pow(durchmesser, 2)));
            abdrehen_mm = Math.Round(abdrehen_mm, 2);

            return abdrehen_mm;
        }

        /// <summary>
        /// Gets the hubraum.
        /// </summary>
        /// <param name="bohrungsdurchmesser">The bohrungsdurchmesser.</param>
        /// <param name="hub">The hub.</param>
        /// <returns></returns>
        public static double GetDisplacement(double bohrungsdurchmesser, double hub)
        {
            double hubraum = Math.PI * bohrungsdurchmesser / 4 * hub;
            hubraum = Math.Round(hubraum, 1);

            return hubraum;
        }

        /// <summary>
        /// Berechnet den Bohrungs-Durchmesser.
        /// </summary>
        /// <param name="hubraum">The hubraum.</param>
        /// <param name="hub">The hub.</param>
        /// <returns>Bohrung in mm.</returns>
        public static double GetCylinderHoleDiameter(double hubraum, double hub)
        {
            double durchmesser = 0;

            durchmesser = 2 * Math.Sqrt(hubraum / hub) / Math.Sqrt(Math.PI);
            durchmesser = Math.Round(durchmesser, 2);

            return durchmesser;
        }

        /// <summary>
        /// Gets the kolben durchmesser.
        /// </summary>
        /// <param name="bohrungsdurchmesser">The bohrungsdurchmesser.</param>
        /// <param name="einbauspiel">The einbauspiel.</param>
        /// <returns></returns>
        public static double GetPistonDiameter(double bohrungsdurchmesser, double einbauspiel)
        {
            double durchmesser = 0;

            durchmesser = bohrungsdurchmesser - (einbauspiel / 100);

            return durchmesser;
        }

        /// <summary>
        /// Gets the kolbengeschwindigkeit.
        /// </summary>
        /// <param name="hub">The hub.</param>
        /// <param name="drehzahl">The drehzahl.</param>
        /// <returns></returns>
        public static double GetPistonSpeed(double hub, double drehzahl)
        {
            double kolbengeschwindigkeit = 0;

            kolbengeschwindigkeit = hub * drehzahl / 30;
            kolbengeschwindigkeit = Math.Round(kolbengeschwindigkeit, 2);

            return kolbengeschwindigkeit;
        }

        /// <summary>
        /// Gets the grinding diameters.
        /// </summary>
        /// <param name="diameter">The diameter.</param>
        /// <returns></returns>
        public static GrindingDiametersModel GetGrindingDiameters(double diameter)
        {
            GrindingDiametersModel grindingDiametersModel = new GrindingDiametersModel();

            grindingDiametersModel.Diameter1 = diameter + 0.2;
            grindingDiametersModel.Diameter2 = diameter + 0.4;
            grindingDiametersModel.Diameter3 = diameter + 0.6;
            grindingDiametersModel.Diameter4 = diameter + 0.8;

            return grindingDiametersModel;
        }
    }
}