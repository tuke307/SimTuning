using Data.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimTuning.Core.ModuleLogic;
using SkiaSharp;
using System.IO;

namespace SimTuning.Core.Test
{
    [TestClass]
    public class AuslassLogicTest
    {
        [TestMethod]
        public void AuspuffTest()
        {
            SKBitmap auspuff;
            string _fileName;
            string _filePath;
            VehiclesModel _vehicle;

            // Vehicle Creation
            _vehicle = new VehiclesModel();
            _vehicle.Motor = new MotorModel();
            _vehicle.Motor.Auslass = new AuslassModel();
            _vehicle.Motor.Auslass.Auspuff = new AuspuffModel();

            _fileName = "Auspuff.png";
            _filePath = Path.Combine(SimTuning.Core.GeneralSettings.FileDirectory, SimTuning.Test.Constants.UnitTestPath, _fileName);

            VehiclesModel _vehicle2 = _vehicle;
            auspuff = AuslassLogic.Auspuff(ref _vehicle2);
            _vehicle = _vehicle2;

            using (var image = SKImage.FromBitmap(auspuff))
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
        public void GasVelocityTest()
        {
            double value;
            double abgasT;

            abgasT = 550;

            value = AuslassLogic.GetGasGeschwindigkeit(abgasT);
        }

        [TestMethod]
        public void ManifoldDiameterTest()
        {
            double value;
            double auslassA;
            int percentage;

            auslassA = 2;
            percentage = 10;

            value = AuslassLogic.GetKruemmerDurchmesser(auslassA, percentage);
        }

        [TestMethod]
        public void ManifoldLengthTest()
        {
            double value;
            double kruemerdurchmesser;
            double drehmomentfaktor;
            double auslassLaenge;

            kruemerdurchmesser = 20;
            drehmomentfaktor = 20;
            auslassLaenge = 20;

            value = AuslassLogic.GetKruemmerLaenge(kruemerdurchmesser, drehmomentfaktor, auslassLaenge);
        }

        [TestMethod]
        public void ResonanceLengthTest()
        {
            double value;
            double auslassSteuerwinkel;
            double abgasTemperatur;
            double resonanzDrehzahl;

            auslassSteuerwinkel = 180;
            abgasTemperatur = 550;
            resonanzDrehzahl = 8000;

            value = AuslassLogic.GetResonanzLaenge(auslassSteuerwinkel, abgasTemperatur, resonanzDrehzahl);
        }

        [TestMethod]
        public void VehiclePortDurationTest()
        {
            double value;
            double auslassSteuerzeit;
            double drehzahl;

            auslassSteuerzeit = 180;
            drehzahl = 8000;

            value = AuslassLogic.GetVehiclePortDuration(auslassSteuerzeit, drehzahl);
        }
    }
}