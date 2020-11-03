namespace SimTuning.Core.Test
{
    using MvvmCross.Tests;
    using NUnit.Framework;
    using OxyPlot;
    using OxyPlot.Wpf;
    using SimTuning.Core.ModuleLogic;
    using System.IO;

    /// <summary>
    /// DynoLogicTest.
    /// </summary>
    /// <seealso cref="MvvmCross.Tests.MvxTestFixture" />
    [TestFixture]
    public class DynoLogicTest : MvxTestFixture
    {
        /// <summary>
        /// Plots the creation test.
        /// </summary>
        [Test]
        public void PlotCreationTest()
        {
            string _filePath;
            string _fileName;

            _fileName = nameof(DynoLogic.PlotAudio) + ".png";
            _filePath = Path.Combine(SimTuning.Test.Constants.Directory, _fileName);

            PngExporter.Export(DynoLogic.PlotAudio, _filePath, 1000, 1000, OxyColors.White);

            _fileName = nameof(DynoLogic.PlotBeschleunigung) + ".png";
            _filePath = Path.Combine(SimTuning.Test.Constants.Directory, _fileName);

            PngExporter.Export(DynoLogic.PlotBeschleunigung, _filePath, 1000, 1000, OxyColors.White);

            _fileName = nameof(DynoLogic.PlotAusrollen) + ".png";
            _filePath = Path.Combine(SimTuning.Test.Constants.Directory, _fileName);

            PngExporter.Export(DynoLogic.PlotAusrollen, _filePath, 1000, 1000, OxyColors.White);

            _fileName = nameof(DynoLogic.PlotStrength) + ".png";
            _filePath = Path.Combine(SimTuning.Test.Constants.Directory, _fileName);

            PngExporter.Export(DynoLogic.PlotStrength, _filePath, 1000, 1000, OxyColors.White);
        }
    }
}