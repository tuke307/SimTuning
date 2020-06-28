using SkiaSharp;
using System;

namespace SimTuning.ModuleLogic
{
    public class AuslassLogic
    {
        /// <summary>
        /// Berechnet Abgasgeschwindigkeit
        /// </summary>
        /// <param name="abgastemperatur">The abgastemperatur.</param>
        /// <returns></returns>
        public double Get_Abgasgeschwindigkeit(double abgastemperatur)
        {
            double abgasgeschwindigkeit = 331 + 0.6 * abgastemperatur;
            abgasgeschwindigkeit = Math.Round(abgasgeschwindigkeit, 2);

            return abgasgeschwindigkeit;
        }

        /// <summary>
        /// Berechnet Resonanzlänge
        /// </summary>
        /// <param name="vehicleSteuerwinkel">The vehicle steuerwinkel.</param>
        /// <param name="abgasTemperatur">The abgas temperatur.</param>
        /// <param name="resonanzDrehzahl">The resonanz drehzahl.</param>
        /// <returns></returns>
        public double Get_Resonanzlaenge(double vehicleSteuerwinkel, double abgasTemperatur, double resonanzDrehzahl)
        {
            // von mm in cm /10
            double resonanzlaenge = vehicleSteuerwinkel * abgasTemperatur / (360 * 2 * resonanzDrehzahl / 60) * 1000;
            resonanzlaenge = Math.Round(resonanzlaenge, 2);

            return resonanzlaenge;
        }

        /// <summary>
        /// Berechnet wie lange der vehicle Kanal offen ist
        /// </summary>
        /// <param name="vehicleSteuerzeit">The vehicle steuerzeit.</param>
        /// <param name="drehzahl">The drehzahl.</param>
        /// <returns></returns>
        public double Get_vehicleKanalOffen(double vehicleSteuerzeit, double drehzahl)
        {
            double zeit = 60 * vehicleSteuerzeit / drehzahl * 360;

            return zeit;
        }

        /// <summary>
        /// Berechnet die Krümmerlänge
        /// </summary>
        /// <param name="kruemmerdurchmesser">The kruemmerdurchmesser.</param>
        /// <param name="drehmomentfaktor">The drehmomentfaktor.</param>
        /// <param name="vehicleLaenge">The vehicle laenge.</param>
        /// <returns></returns>
        public double Get_Kruemmerlaenge(double kruemmerdurchmesser, double drehmomentfaktor, double vehicleLaenge)
        {
            // von mm in cm /10
            double kruemmerlaenge = drehmomentfaktor * kruemmerdurchmesser - vehicleLaenge /*/ 10*/;
            kruemmerlaenge = Math.Round(kruemmerlaenge, 2);

            return kruemmerlaenge;
        }

        /// <summary>
        /// Berechnet den Krümmer Durchmesser
        /// </summary>
        /// <param name="vehicleflaeche">vehiclefläche in cm²</param>
        /// <param name="percentage">Flächenvergrößerung in %, normalerweise im Bereich von 10% bis 20% </param>
        /// <returns>Krümmerdurchmesser in cm</returns>
        public double Get_KruemmerDurchmesser(double vehicleflaeche, int percentage = 10)
        {
            //Vergrößerung um mehr als 100%
            double factor = 1 + (percentage / 100);

            //radius mal 2
            double durchmesser = Math.Sqrt(vehicleflaeche * factor) / Math.Sqrt(Math.PI) * 2;
            durchmesser = Math.Round(durchmesser, 2);

            return durchmesser;
        }

        /// <summary>
        /// Berechnet einen Auspuff
        /// </summary>
        /// <param name="vehicle">Die Eingabedaten für die Auspuffberechnung</param>
        /// <param name="_vehicle">Die Berechneten Ausgabedaten für den Auspuff</param>
        /// <returns>Bild des Auspuffs</returns>
        public SKBitmap Auspuff(ref Data.Models.VehiclesModel vehicle)
        {
            #region Berechnung

            /*
            * Berechnung
            */
            vehicle.Motor.Auslass.Auspuff.ResonanzL = Get_Resonanzlaenge(vehicle.Motor.Auslass.SteuerzeitSZ.Value, vehicle.Motor.Auslass.Auspuff.AbgasT.Value, vehicle.Motor.ResonanzU.Value);

            //KRÜMMER
            vehicle.Motor.Auslass.Auspuff.KruemmerD = vehicle.Motor.Auslass.DurchmesserD.Value;/*Get_KruemmerDurchmesser(vehicleflaeche);*/
            vehicle.Motor.Auslass.Auspuff.KruemmerL = Get_Kruemmerlaenge(vehicle.Motor.Auslass.DurchmesserD.Value, vehicle.Motor.Auslass.Auspuff.KruemmerF.Value, vehicle.Motor.Auslass.LaengeL.Value);

            //MITTELTEIL
            vehicle.Motor.Auslass.Auspuff.MittelteilD = Math.Round(Math.Sqrt(vehicle.Motor.Auslass.FlaecheA.Value * 4 / Math.PI) * vehicle.Motor.Auslass.Auspuff.MittelteilF.Value, 2);

            //konus
            vehicle.Motor.Auslass.Auspuff.DiffusorL1 = 0;
            vehicle.Motor.Auslass.Auspuff.DiffusorD1 = 0;
            vehicle.Motor.Auslass.Auspuff.DiffusorL2 = 0;
            vehicle.Motor.Auslass.Auspuff.DiffusorD2 = 0;
            vehicle.Motor.Auslass.Auspuff.DiffusorL3 = 0;
            vehicle.Motor.Auslass.Auspuff.DiffusorD3 = 0;
            switch (vehicle.Motor.Auslass.Auspuff.DiffusorStage)
            {
                case 1:
                    vehicle.Motor.Auslass.Auspuff.DiffusorD1 = Math.Round(2 * Math.Tan(vehicle.Motor.Auslass.Auspuff.KruemmerW.Value * Math.PI / 360) * vehicle.Motor.Auslass.Auspuff.KruemmerL.Value + vehicle.Motor.Auslass.Auspuff.KruemmerD.Value, 2);
                    vehicle.Motor.Auslass.Auspuff.DiffusorL1 = Math.Round((vehicle.Motor.Auslass.Auspuff.MittelteilD.Value - vehicle.Motor.Auslass.Auspuff.DiffusorD1.Value) / (2 * Math.Tan(vehicle.Motor.Auslass.Auspuff.DiffusorW1.Value * (2 * Math.PI / 360))), 2);
                    break;

                case 2:
                    vehicle.Motor.Auslass.Auspuff.DiffusorL1 = 0;
                    vehicle.Motor.Auslass.Auspuff.DiffusorD1 = 0;
                    vehicle.Motor.Auslass.Auspuff.DiffusorL2 = 0;
                    vehicle.Motor.Auslass.Auspuff.DiffusorD2 = 0;
                    break;

                case 3:
                    vehicle.Motor.Auslass.Auspuff.DiffusorL1 = 0;
                    vehicle.Motor.Auslass.Auspuff.DiffusorD1 = 0;
                    vehicle.Motor.Auslass.Auspuff.DiffusorL2 = 0;
                    vehicle.Motor.Auslass.Auspuff.DiffusorD2 = 0;
                    vehicle.Motor.Auslass.Auspuff.DiffusorL3 = 0;
                    vehicle.Motor.Auslass.Auspuff.DiffusorD3 = 0;
                    break;

                default:
                    break;
            }

            //GEGENKONUS
            vehicle.Motor.Auslass.Auspuff.GegenkonusL = Math.Round((vehicle.Motor.Auslass.Auspuff.MittelteilD.Value - vehicle.Motor.Auslass.Auspuff.EndrohrD.Value) / (2 * Math.Tan(vehicle.Motor.Auslass.Auspuff.GegenKonusW.Value * Math.PI / 180)), 2);
            vehicle.Motor.Auslass.Auspuff.GegenkonusD = vehicle.Motor.Auslass.Auspuff.MittelteilD.Value;

            //MITTELTEIL
            vehicle.Motor.Auslass.Auspuff.MittelteilL = Math.Round(vehicle.Motor.Auslass.Auspuff.ResonanzL.Value - (vehicle.Motor.Auslass.Auspuff.KruemmerL.Value + vehicle.Motor.Auslass.Auspuff.DiffusorL1.Value + vehicle.Motor.Auslass.Auspuff.GegenkonusL.Value / 2), 2);

            #endregion Berechnung

            #region Groeßen

            /*
           * BILD
         * 1 = Krümmer
         * 2 = konus 1
         * 3 = konus 2
         * 4 = konus 3
         * 5 = Mittelstück
         * 6 = Gegenkonus
         * 7 = Endrohr
         */

            //allgemeine Bildgröße
            int pic_width = 2000;
            int pic_height = 1000;

            //HORIZONTAL, länge der Teile
            int width1 = (int)vehicle.Motor.Auslass.Auspuff.KruemmerL;
            int width2 = (int)vehicle.Motor.Auslass.Auspuff.DiffusorL1;
            int width3 = (int)vehicle.Motor.Auslass.Auspuff.DiffusorL2;
            int width4 = (int)vehicle.Motor.Auslass.Auspuff.DiffusorL3;
            int width5 = (int)vehicle.Motor.Auslass.Auspuff.MittelteilL;
            int width6 = (int)vehicle.Motor.Auslass.Auspuff.GegenkonusL;
            int width7 = (int)vehicle.Motor.Auslass.Auspuff.EndrohrL;

            //VERTIKAL, (Anfangs)Höhe der Teile
            int height1 = (int)vehicle.Motor.Auslass.Auspuff.KruemmerD * 2;
            int height2 = (int)vehicle.Motor.Auslass.Auspuff.DiffusorD1 * 2;
            int height3 = (int)vehicle.Motor.Auslass.Auspuff.DiffusorD2 * 2;
            int height4 = (int)vehicle.Motor.Auslass.Auspuff.DiffusorD3 * 2;
            int height5 = (int)vehicle.Motor.Auslass.Auspuff.MittelteilD * 2;
            int height6 = (int)vehicle.Motor.Auslass.Auspuff.GegenkonusD * 2;
            int height7 = (int)vehicle.Motor.Auslass.Auspuff.EndrohrD * 2;

            #endregion Groeßen

            #region Bild

            SKBitmap bmp_auspuff = new SKBitmap(pic_width, pic_height);
            SKCanvas graphic_auspuff = new SKCanvas(bmp_auspuff);

            SKPaint blackPen = new SKPaint
            {
                Style = SKPaintStyle.Stroke,
                Color = SKColors.Black,
                StrokeWidth = 3,
                IsAntialias = true,
                TextSize = 25
            };

            SKPaint blackDoubleArrowPen = new SKPaint
            {
                Style = SKPaintStyle.Stroke,
                Color = SKColors.Black,
                StrokeWidth = 4,
                IsAntialias = true
                //AdjustableArrowCap bigArrow = new AdjustableArrowCap(3, 5); //spitzer Pfeil
                //blackDoubleArrowPen.CustomEndCap = bigArrow;
                //blackDoubleArrowPen.CustomStartCap = bigArrow;
            };

            SKPaint redPen = new SKPaint
            {
                Style = SKPaintStyle.Stroke,
                Color = SKColors.DarkRed,
                StrokeWidth = 4,
                IsAntialias = true
            };

            #endregion Bild

            #region Positionshilfen

            int Xbeginn = 100; //Abstand links
            int Xstart1 = Xbeginn;
            int Xstart2 = Xstart1 + width1;
            int Xstart3 = Xstart2 + width2;
            int Xstart4 = Xstart3 + width3;
            int Xstart5 = Xstart4 + width4;
            int Xstart6 = Xstart5 + width5;
            int Xstart7 = Xstart6 + width6;

            int Ymiddle = (pic_height / 2) + (pic_height / 4);
            int Ydiff1 = height1 / 2;
            int Ydiff2 = height2 / 2;
            int Ydiff3 = height3 / 2;
            int Ydiff4 = height4 / 2;
            int Ydiff5 = height5 / 2;
            int Ydiff6 = height6 / 2;
            int Ydiff7 = height7 / 2;

            #endregion Positionshilfen

            #region AuspuffGeruest

            //1
            graphic_auspuff.DrawLine(new SKPoint(Xstart1, Ymiddle - Ydiff1),
                                     new SKPoint(Xstart2, Ymiddle - Ydiff2),
                                     redPen);//oben
            graphic_auspuff.DrawLine(new SKPoint(Xstart1, Ymiddle + Ydiff1),
                                     new SKPoint(Xstart2, Ymiddle + Ydiff2),
                                     redPen);//unten
            graphic_auspuff.DrawLine(new SKPoint(Xstart1, Ymiddle - Ydiff1),
                                     new SKPoint(Xstart1, Ymiddle + Ydiff1),
                                     redPen);//Links

            //ONE-Stage Diffusor
            if (vehicle.Motor.Auslass.Auspuff.DiffusorL1 != 0)
            {
                //TWO-Stage Diffusor
                if (vehicle.Motor.Auslass.Auspuff.DiffusorL2 != 0)
                {
                    //THREE-Stage Diffusor
                    if (vehicle.Motor.Auslass.Auspuff.DiffusorL3 != 0)
                    {
                        //THREE-Stages malen
                        graphic_auspuff.DrawLine(Xstart4, Ymiddle - Ydiff4,
                                                 Xstart5, Ymiddle - Ydiff5,
                                                 redPen);//oben
                        graphic_auspuff.DrawLine(Xstart4, Ymiddle + Ydiff4,
                                                 Xstart5, Ymiddle + Ydiff5,
                                                 redPen);//unten
                        graphic_auspuff.DrawLine(Xstart4, Ymiddle - Ydiff4,
                                                 Xstart4, Ymiddle + Ydiff4,
                                                 redPen);//links
                    }
                    else //TWO-Stages malen
                    {
                        graphic_auspuff.DrawLine(new SKPoint(Xstart3, Ymiddle - Ydiff3),
                                                 new SKPoint(Xstart4, Ymiddle - Ydiff4),
                                                 redPen);//oben
                        graphic_auspuff.DrawLine(new SKPoint(Xstart3, Ymiddle + Ydiff3),
                                                 new SKPoint(Xstart4, Ymiddle + Ydiff4),
                                                 redPen);//unten
                        graphic_auspuff.DrawLine(new SKPoint(Xstart3, Ymiddle - Ydiff3),
                                                 new SKPoint(Xstart3, Ymiddle + Ydiff3),
                                                 redPen);//links
                    }
                }
                else //ONE-Stage malen
                {
                    //2
                    graphic_auspuff.DrawLine(new SKPoint(Xstart2, Ymiddle - Ydiff2),
                                             new SKPoint(Xstart5, Ymiddle - Ydiff5),
                                             redPen);//oben
                    graphic_auspuff.DrawLine(new SKPoint(Xstart2, Ymiddle + Ydiff2),
                                             new SKPoint(Xstart5, Ymiddle + Ydiff5),
                                             redPen);//unten
                    graphic_auspuff.DrawLine(new SKPoint(Xstart2, Ymiddle - Ydiff2),
                                             new SKPoint(Xstart2, Ymiddle + Ydiff2),
                                             redPen); //links
                }
            }

            //5
            graphic_auspuff.DrawRect(Xstart5, Ymiddle - Ydiff5,
                                     width5, height5,
                                     redPen);
            //6
            graphic_auspuff.DrawLine(new SKPoint(Xstart6, Ymiddle - Ydiff6),
                                     new SKPoint(Xstart7, Ymiddle - Ydiff7),
                                     redPen);//oben
            graphic_auspuff.DrawLine(new SKPoint(Xstart6, Ymiddle + Ydiff6),
                                     new SKPoint(Xstart7, Ymiddle + Ydiff7),
                                     redPen);//unten
            //7
            graphic_auspuff.DrawRect(Xstart7, Ymiddle - Ydiff7,
                                     width7, height7,
                                     redPen);

            #endregion AuspuffGeruest

            #region MaßLinien

            /*
             * LÄNGEN
             */
            //1
            graphic_auspuff.DrawLine(Xstart1, Ymiddle + Ydiff1,
                                     Xstart1, Ymiddle + 200,
                                     blackPen);//Begrenzung-Links
            graphic_auspuff.DrawLine(Xstart1, Ymiddle + 175,
                                     Xstart2, Ymiddle + 175,
                                     blackDoubleArrowPen);//Maß-Linie

            //ONE-Stage Diffusor
            if (vehicle.Motor.Auslass.Auspuff.DiffusorL1 != 0)
            {
                //TWO-Stage Diffusor
                if (vehicle.Motor.Auslass.Auspuff.DiffusorL2 != 0)
                {
                    //THREE-Stage Diffusor
                    if (vehicle.Motor.Auslass.Auspuff.DiffusorL3 != 0)
                    {
                        //2
                        graphic_auspuff.DrawLine(Xstart2, Ymiddle + Ydiff2,
                                                 Xstart2, Ymiddle + 200,
                                                 blackPen);//Begrenzung-links
                        graphic_auspuff.DrawLine(Xstart2, Ymiddle + 175,
                                                 Xstart3, Ymiddle + 175,
                                                 blackDoubleArrowPen);//Maß-Linie

                        //3
                        graphic_auspuff.DrawLine(Xstart4, Ymiddle + Ydiff3,
                                                 Xstart4, Ymiddle + 200,
                                                 blackPen);//Begrenzung-Mitte
                        graphic_auspuff.DrawLine(Xstart4, Ymiddle + 175,
                                                 Xstart5, Ymiddle + 175,
                                                 blackDoubleArrowPen);//Maß-Linie
                        graphic_auspuff.DrawLine(Xstart4, Ymiddle + 175,
                                                 Xstart5, Ymiddle + 175,
                                                 blackDoubleArrowPen);//Maß-Linie

                        //4
                        graphic_auspuff.DrawLine(Xstart6, Ymiddle + Ydiff4,
                                                 Xstart6, Ymiddle + 200,
                                                 blackPen);//Begrenzung-Mitte
                        graphic_auspuff.DrawLine(Xstart4, Ymiddle + 175,
                                                 Xstart5, Ymiddle + 175,
                                                 blackDoubleArrowPen);//Maß-Linie
                    }
                    else
                    {
                        //2
                        graphic_auspuff.DrawLine(Xstart2, Ymiddle + Ydiff2,
                                                 Xstart2, Ymiddle + 200,
                                                 blackPen);//Begrenzung-links
                        graphic_auspuff.DrawLine(Xstart2, Ymiddle + 175,
                                                 Xstart3, Ymiddle + 175,
                                                 blackDoubleArrowPen);//Maß-Linie

                        //3
                        graphic_auspuff.DrawLine(Xstart4, Ymiddle + Ydiff3,
                                                 Xstart4, Ymiddle + 200,
                                                 blackPen);//Begrenzung-Mitte
                        graphic_auspuff.DrawLine(Xstart4, Ymiddle + 175,
                                                 Xstart5, Ymiddle + 175,
                                                 blackDoubleArrowPen);//Maß-Linie
                        graphic_auspuff.DrawLine(Xstart4, Ymiddle + 175,
                                                 Xstart5, Ymiddle + 175,
                                                 blackDoubleArrowPen);//Maß-Linie
                    }
                }
                else
                {
                    //2
                    graphic_auspuff.DrawLine(Xstart2, Ymiddle + Ydiff2,
                                             Xstart2, Ymiddle + 200,
                                             blackPen);//Begrenzung-links
                    graphic_auspuff.DrawLine(Xstart2, Ymiddle + 175,
                                             Xstart3, Ymiddle + 175,
                                             blackDoubleArrowPen);//Maß-Linie
                }
            }

            //5
            graphic_auspuff.DrawLine(Xstart5, Ymiddle + Ydiff5,
                                     Xstart5, Ymiddle + 200,
                                     blackPen);//Begrenzung-Links
            graphic_auspuff.DrawLine(Xstart5, Ymiddle + 175,
                                     Xstart6, Ymiddle + 175,
                                     blackDoubleArrowPen);//Maß-Linie
            //6
            graphic_auspuff.DrawLine(Xstart6, Ymiddle + Ydiff6,
                                     Xstart6, Ymiddle + 200,
                                     blackPen);//Begrenzung-Links
            graphic_auspuff.DrawLine(Xstart6, Ymiddle + 175,
                                     Xstart7, Ymiddle + 175,
                                     blackDoubleArrowPen);//Maß-Linie
            //7
            graphic_auspuff.DrawLine(Xstart7, Ymiddle + Ydiff7,
                                     Xstart7, Ymiddle + 200,
                                     blackPen);//Begrenzung-Links
            graphic_auspuff.DrawLine(Xstart7 + width7, Ymiddle + Ydiff7,
                                     Xstart7 + width7, Ymiddle + 200,
                                     blackPen);//Begrenzung-Rechts
            graphic_auspuff.DrawLine(Xstart7, Ymiddle + 175,
                                     Xstart7 + width7, Ymiddle + 175,
                                     blackDoubleArrowPen);//Maß-Linie
            //konus
            graphic_auspuff.DrawLine(Xstart2, Ymiddle + 175,
                                     Xstart5, Ymiddle + 175,
                                     blackDoubleArrowPen);//Maß-Linie

            /*
             * Durchmesser
             */
            //Header
            graphic_auspuff.DrawLine(Xstart1, Ymiddle - Ydiff1,
                                     Xstart1 - 50, Ymiddle - Ydiff1,
                                     blackPen);//Begrenzung-oben
            graphic_auspuff.DrawLine(Xstart1, Ymiddle + Ydiff1,
                                     Xstart1 - 50, Ymiddle + Ydiff1,
                                     blackPen);//Begrenzung-unten
            graphic_auspuff.DrawLine(Xstart1 - 35, Ymiddle - Ydiff1,
                                     Xstart1 - 35, Ymiddle + Ydiff1,
                                     blackDoubleArrowPen);//Maß-Linie
            //Stringer
            graphic_auspuff.DrawLine(Xstart7 + width7, Ymiddle - Ydiff7,
                                     Xstart7 + width7 + 50, Ymiddle - Ydiff7,
                                     blackPen);//Begrenzung-oben
            graphic_auspuff.DrawLine(Xstart7 + width7, Ymiddle + Ydiff7,
                                     Xstart7 + width7 + 50, Ymiddle + Ydiff7,
                                     blackPen);//Begrenzung-unten
            graphic_auspuff.DrawLine(Xstart7 + width7 + 35, Ymiddle - Ydiff7,
                                     Xstart7 + width7 + 35, Ymiddle + Ydiff7,
                                     blackDoubleArrowPen);//Maß-Linie

            #endregion MaßLinien

            #region WerteBezeichnung

            /*
             * übergebene Werte werden genommen da genauer
             */

            //Längen
            graphic_auspuff.DrawText(/*kruemmerL.ToString()*/"LK", Xstart1 + (width1 / 2) - 15, Ymiddle + 200, blackPen);

            //ONE-Stage Diffusor
            if (vehicle.Motor.Auslass.Auspuff.DiffusorL1 != 0)
            {
                //TWO-Stage Diffusor
                if (vehicle.Motor.Auslass.Auspuff.DiffusorL2 != 0)
                {
                    //THREE-Stage Diffusor
                    if (vehicle.Motor.Auslass.Auspuff.DiffusorL3 != 0)
                    {
                        graphic_auspuff.DrawText(/*konus1L.ToString()*/"LD1", Xstart2 + (width2 / 2) - 15, Ymiddle + 150, blackPen);
                        graphic_auspuff.DrawText(/*konus2L.ToString()*/"LD2", Xstart3 + (width3 / 2) - 15, Ymiddle + 150, blackPen);
                        graphic_auspuff.DrawText(/*konus3L.ToString()*/"LD3", Xstart4 + (width4 / 2) - 15, Ymiddle + 150, blackPen);
                    }
                    else
                    {
                        graphic_auspuff.DrawText(/*konus1L.ToString()*/"LD1", Xstart2 + (width2 / 2) - 15, Ymiddle + 150, blackPen);
                        graphic_auspuff.DrawText(/*konus2L.ToString()*/"LD2", Xstart3 + (width3 / 2) - 15, Ymiddle + 150, blackPen);
                    }
                }
                else
                {
                    graphic_auspuff.DrawText(/*konus1L.ToString()*/"LD", Xstart2 + (width2 / 2) - 15, Ymiddle + 200, blackPen);
                }
            }

            graphic_auspuff.DrawText(/*mittelteilL.ToString()*/"LM", Xstart5 + (width5 / 2) - 15, Ymiddle + 200, blackPen);
            graphic_auspuff.DrawText(/*gegenkonusL.ToString()*/"LG", Xstart6 + (width6 / 2) - 15, Ymiddle + 200, blackPen);
            graphic_auspuff.DrawText(/*endrohrL.ToString()*/"LE", Xstart7 + (width7 / 2) - 15, Ymiddle + 200, blackPen);

            //Durchmesser
            graphic_auspuff.DrawText("D1",
                                     Xstart1 - 90, Ymiddle - 15,
                                     blackPen);
            graphic_auspuff.DrawText("D2",
                                     Xstart7 + width7 + 65, Ymiddle - 15,
                                     blackPen);
            //graphic_auspuff.DrawString("D3", drawFont, drawBrushblack, Xstart7 + width7 + 65, Ymiddle - 15);
            //graphic_auspuff.DrawString("D4", drawFont, drawBrushblack, Xstart7 + width7 + 65, Ymiddle - 15);

            #endregion WerteBezeichnung

            #region TeilBezeichnung

            blackPen.TextSize = 25;
            graphic_auspuff.DrawText("Krümmer",
                                     Xstart1 + 20, Ymiddle,
                                     blackPen);
            graphic_auspuff.DrawText("Konus",
                                     Xstart2 + 20, Ymiddle,
                                     blackPen);
            graphic_auspuff.DrawText("Mittelteil",
                                     Xstart5 + 20, Ymiddle,
                                     blackPen);
            graphic_auspuff.DrawText("Gegenkonus",
                                     Xstart6 + 20, Ymiddle,
                                     blackPen);
            graphic_auspuff.DrawText("Endrohr",
                                     Xstart7 + 20, Ymiddle,
                                     blackPen);

            #endregion TeilBezeichnung

            return bmp_auspuff;
        }
    }
}