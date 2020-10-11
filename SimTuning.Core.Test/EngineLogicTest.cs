using NUnit.Framework;
using SimTuning.Core.Models;
using SimTuning.Core.ModuleLogic;
using SkiaSharp;
using System.IO;

namespace SimTuning.Core.Test
{
    [TestFixture]
    public class EngineLogicTest

    {
        [Test]
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

        [Test]
        public void CylinderHoleDiameterTest()
        {
            double value;
            double hubraum;
            double hub;

            hubraum = 50000;
            hub = 17000;

            value = EngineLogic.GetCylinderHoleDiameter(hubraum, hub);
        }

        [Test]
        public void DisplacementTest()
        {
            double value;
            double bohrungsdurchmesser;
            double hub;

            hub = 17000;
            bohrungsdurchmesser = 38;

            value = EngineLogic.GetDisplacement(bohrungsdurchmesser, hub);
        }

        [Test]
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

        [Test]
        public void GrindingDiametersTest()
        {
            GrindingDiametersModel value;
            double durchmesser;

            durchmesser = 38;

            value = EngineLogic.GetGrindingDiameters(durchmesser);
        }

        [Test]
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

        [Test]
        public void KolbenDurchmesserTest()
        {
            double value;
            double bohrungsdurchmesser;
            double einbauspiel;

            bohrungsdurchmesser = 38;
            einbauspiel = 0.03;

            value = EngineLogic.GetKolbenDurchmesser(bohrungsdurchmesser, einbauspiel);
        }

        [Test]
        public void KolbenGeschwindigkeitTest()
        {
            double value;
            double hub;
            double drehzahl;

            hub = 44;
            drehzahl = 8000;

            value = EngineLogic.GetKolbenGeschwindigkeit(hub, drehzahl);
        }

        [Test]
        public void SteuerdiagrammTest()
        {
            SKBitmap value;
            double einlass;
            double auslass;
            double ueberstroemer;
            string _fileName;
            string _filePath;

            _fileName = "Steuerdiagramm.png";
            _filePath = Path.Combine(SimTuning.Test.Constants.Directory, _fileName);
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

        [Test]
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

        [Test]
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

        [Test]
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