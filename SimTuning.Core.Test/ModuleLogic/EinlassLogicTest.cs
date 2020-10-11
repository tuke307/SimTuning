using MvvmCross.Tests;
using NUnit.Framework;
using SimTuning.Core.ModuleLogic;

namespace SimTuning.Core.Test
{
    [TestFixture]
    public class EinlassLogicTest : MvxTestFixture
    {
        [Test]
        public void ResonanzLaengeTest()
        {
            double value;
            double einlassflaeche;
            double einlasssteuerwinkel;
            double kurbelgehausevolumen;
            double resonanzdrehzahl;
            double ansaugleitungsdurchmesser;

            einlassflaeche = 20;
            einlasssteuerwinkel = 120;
            kurbelgehausevolumen = 17000;
            resonanzdrehzahl = 8000;
            ansaugleitungsdurchmesser = 20;

            value = EinlassLogic.GetResonanzLaenge(einlassflaeche,
            einlasssteuerwinkel,
            kurbelgehausevolumen,
            resonanzdrehzahl,
            ansaugleitungsdurchmesser);
        }

        [Test]
        public void VergaserDurchmesserTest()
        {
            double value;
            double hubvolumen;
            double resonanzdrehzahl;
            double widerstandsFaktor;

            hubvolumen = 50000;
            resonanzdrehzahl = 8000;
            widerstandsFaktor = 0.9;

            value = EinlassLogic.GetVergaserDurchmesser(hubvolumen, resonanzdrehzahl, widerstandsFaktor);
        }

        [Test]
        public void VergaserHauptduesenDurchmesserTest()
        {
            double value;
            double vergasergroeße;
            double widerstandsFaktor;

            vergasergroeße = 18;
            widerstandsFaktor = 0.95;

            value = EinlassLogic.GetVergaserHauptduesenDurchmesser(vergasergroeße, widerstandsFaktor);
        }
    }
}