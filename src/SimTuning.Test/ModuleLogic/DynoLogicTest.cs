﻿// Copyright (c) 2021 tuke productions. All rights reserved.
namespace SimTuning.Test
{
    using NUnit.Framework;
    using OxyPlot;
    using OxyPlot.Wpf;
    using SimTuning.Core.ModuleLogic;
    using SimTuning.Test;
    using System.IO;

    /// <summary>
    /// DynoLogicTest.
    /// </summary>
    /// <seealso cref="MvvmCross.Tests.MvxTestFixture" />
    [TestFixture]
    public class DynoLogicTest : MvxTest
    {
        /// <summary>
        /// Plots the creation test.
        /// </summary>
        [Test]
        public void PlotCreationTest()
        {
            string _filePath;
            string _fileName;

            var colormap = AudioLogic.GetSpectrogram(
                audioFile: SimTuning.Test.Constants.DynoAudioFile,
                fftSize: 32768);

            // AudioLogic.GetDrehzahlGraph(areas: false, intensity: 0.75, areaAbstand: 5);

            DynoLogic.GetDrehzahlGraph(areas: false, intensity: 0.75, areaAbstand: 5);

            _fileName = nameof(DynoLogic.PlotAudio) + ".png";
            _filePath = Path.Combine(SimTuning.Test.Constants.Directory, _fileName);

            PngExporter.Export(DynoLogic.PlotAudio, _filePath, 1000, 1000, OxyColors.White);

            DynoLogic.GetDrehzahlGraphFitted(out var drehzahlModels);

            _fileName = nameof(DynoLogic.PlotAudio) + "Fitted" + ".png";
            _filePath = Path.Combine(SimTuning.Test.Constants.Directory, _fileName);

            PngExporter.Export(DynoLogic.PlotAudio, _filePath, 1000, 1000, OxyColors.White);

            DynoLogic.GetAusrollGraphFitted(null/*List < Data.Models.AusrollenModel > ausrollenModels*/);

            _fileName = nameof(DynoLogic.PlotGeschwindigkeit) + "Fitted" + ".png";
            _filePath = Path.Combine(SimTuning.Test.Constants.Directory, _fileName);

            PngExporter.Export(DynoLogic.PlotGeschwindigkeit, _filePath, 1000, 1000, OxyColors.White);

            DynoLogic.GetGeschwindigkeitsGraphFitted(null/*List < Data.Models.BeschleunigungModel > beschleunigungModels*/);

            _fileName = nameof(DynoLogic.PlotAusrollen) + "Fitted" + ".png";
            _filePath = Path.Combine(SimTuning.Test.Constants.Directory, _fileName);

            PngExporter.Export(DynoLogic.PlotAusrollen, _filePath, 1000, 1000, OxyColors.White);

            DynoLogic.GetLeistungsGraph(200, out var dynoPsModels);

            _fileName = nameof(DynoLogic.PlotLeistung) + "Fitted" + ".png";
            _filePath = Path.Combine(SimTuning.Test.Constants.Directory, _fileName);

            PngExporter.Export(DynoLogic.PlotLeistung, _filePath, 1000, 1000, OxyColors.White);
        }
    }
}