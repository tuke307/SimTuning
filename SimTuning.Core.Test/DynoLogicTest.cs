using Microsoft.VisualStudio.TestTools.UnitTesting;
using OxyPlot;
using OxyPlot.Wpf;
using SimTuning.Core.ModuleLogic;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SimTuning.Core.Test
{
    [TestClass]
    public class DynoLogicTest
    {
        [TestMethod]
        public void PlotCreationTest()
        {
            string _filePath;
            string _fileName;

            _fileName = nameof(DynoLogic.PlotAudio) + ".png";
            _filePath = Path.Combine(SimTuning.Core.GeneralSettings.FileDirectory, SimTuning.Test.Constants.UnitTestPath, _fileName);

            PngExporter.Export(DynoLogic.PlotAudio, _filePath, 1000, 1000, OxyColors.White);

            _fileName = nameof(DynoLogic.PlotBeschleunigung) + ".png";
            _filePath = Path.Combine(SimTuning.Core.GeneralSettings.FileDirectory, SimTuning.Test.Constants.UnitTestPath, _fileName);

            PngExporter.Export(DynoLogic.PlotBeschleunigung, _filePath, 1000, 1000, OxyColors.White);

            _fileName = nameof(DynoLogic.PlotAusrollen) + ".png";
            _filePath = Path.Combine(SimTuning.Core.GeneralSettings.FileDirectory, SimTuning.Test.Constants.UnitTestPath, _fileName);

            PngExporter.Export(DynoLogic.PlotAusrollen, _filePath, 1000, 1000, OxyColors.White);

            _fileName = nameof(DynoLogic.PlotStrength) + ".png";
            _filePath = Path.Combine(SimTuning.Core.GeneralSettings.FileDirectory, SimTuning.Test.Constants.UnitTestPath, _fileName);

            PngExporter.Export(DynoLogic.PlotStrength, _filePath, 1000, 1000, OxyColors.White);
        }
    }
}