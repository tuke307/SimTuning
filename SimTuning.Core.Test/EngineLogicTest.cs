using Data.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimTuning.Core.Models;
using SimTuning.Core.ModuleLogic;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SimTuning.Core.Test
{
    [TestClass]
    public class EngineLogicTest

    {
        [TestMethod]
        public void CompressionTest()
        {
            double value;
            double hubraum;
            double brennraum;
            double durchmesser;

            hubraum = 50000;
            brennraum = 17000;
            durchmesser = 38;

            value = EngineLogic.GetCompression(hubraum,
             brennraum,
             durchmesser);
        }

        [TestMethod]
        public void CylinderHoleDiameterTest()
        {
            double value;
            double hubraum;
            double hub;

            hubraum = 50000;
            hub = 17000;

            value = EngineLogic.GetCylinderHoleDiameter(hubraum, hub);
        }

        [TestMethod]
        public void DisplacementTest()
        {
            double value;
            double bohrungsdurchmesser;
            double hub;

            hub = 17000;
            bohrungsdurchmesser = 38;

            value = EngineLogic.GetDisplacement(bohrungsdurchmesser, hub);
        }

        [TestMethod]
        public void DistanceToOTTest()
        {
            double value;
            double pleullaenge;
            double hubradius;
            double deachsierung;
            double kwgrad;

            pleullaenge = 44;
            hubradius = 22;
            deachsierung = 2;
            kwgrad = 120;

            value = EngineLogic.GetDistanceToOT(pleullaenge, hubradius, deachsierung, kwgrad);
        }

        [TestMethod]
        public void GrindingDiametersTest()
        {
            GrindingDiametersModel value;
            double durchmesser;

            durchmesser = 38;

            value = EngineLogic.GetGrindingDiameters(durchmesser);
        }

        [TestMethod]
        public void HubRadiusTest()
        {
            double value;
            double hub;
            double pleullaenge;
            double deachsierung;

            hub = 44;
            pleullaenge = 44;
            deachsierung = 2;

            value = EngineLogic.GetHubRadius(hub, pleullaenge, deachsierung);
        }

        [TestMethod]
        public void KolbenDurchmesserTest()
        {
            double value;
            double bohrungsdurchmesser;
            double einbauspiel;

            bohrungsdurchmesser = 38;
            einbauspiel = 0.03;

            value = EngineLogic.GetKolbenDurchmesser(bohrungsdurchmesser, einbauspiel);
        }

        [TestMethod]
        public void KolbenGeschwindigkeitTest()
        {
            double value;
            double hub;
            double drehzahl;

            hub = 44;
            drehzahl = 8000;

            value = EngineLogic.GetKolbenGeschwindigkeit(hub, drehzahl);
        }

        [TestMethod]
        public void SteuerdiagrammTest()
        {
            SKBitmap value;
            double einlass;
            double auslass;
            double ueberstroemer;
            string _fileName;
            string _filePath;

            _fileName = "Steuerdiagramm.png";
            _filePath = Path.Combine(SimTuning.Core.GeneralSettings.FileDirectory, SimTuning.Test.Constants.UnitTestPath, _fileName);
            einlass = 120;
            auslass = 120;
            ueberstroemer = 120;

            value = EngineLogic.GetSteuerdiagramm(einlass, auslass, ueberstroemer);

            using (var image = SKImage.FromBitmap(value))
            using (var data = image.Encode())
            {
                // save the data to a stream
                using (var stream = File.OpenWrite(_filePath))
                {
                    data.SaveTo(stream);
                }
            }
        }

        [TestMethod]
        public void SteuerwinkelTest()
        {
            double value;
            double vorherSteuerzeit;
            double nachherSteuerzeit;
            bool kolbenoberkante;
            bool kolbenunterkante;

            vorherSteuerzeit = 120;
            nachherSteuerzeit = 130;
            kolbenoberkante = true;
            kolbenunterkante = false;

            EngineLogic.GetSteuerwinkel(vorherSteuerzeit, nachherSteuerzeit, kolbenoberkante, kolbenunterkante);
            value = EngineLogic.GetSteuerwinkelOeffnet();
            value = EngineLogic.GetSteuerwinkelSchließt();
        }

        [TestMethod]
        public void ToDecreasingLengthTest()
        {
            double value;
            double hubraum;
            double brennraum;
            double durchmesser;
            double zielVerdichtung;

            hubraum = 50000;
            brennraum = 17000;
            durchmesser = 38;
            zielVerdichtung = 12;

            value = EngineLogic.GetToDecreasingLength(hubraum, brennraum, durchmesser, zielVerdichtung);
        }

        [TestMethod]
        public void VorauslassTest()
        {
            double value;
            double auslassSW;
            double ueberstroemerSW;

            auslassSW = 190;
            ueberstroemerSW = 125;

            value = EngineLogic.GetVorauslass(auslassSW, ueberstroemerSW);
        }
    }
}