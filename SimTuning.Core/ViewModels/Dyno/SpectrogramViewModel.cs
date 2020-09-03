﻿// project=SimTuning.Core, file=SpectrogramViewModel.cs, creation=2020:7:31 Copyright (c)
// 2020 tuke productions. All rights reserved.
using Data;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using OxyPlot;
using SimTuning.Core.ModuleLogic;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Resources;
using System.Threading.Tasks;

namespace SimTuning.Core.ViewModels.Dyno
{
    public class SpectrogramViewModel : MvxNavigationViewModel
    {
        protected readonly AudioLogic audioLogic;

        protected readonly DynoLogic dynoLogic;
        protected readonly ResourceManager rm;

        public IMvxAsyncCommand FilterPlotCommand { get; set; }

        public IMvxAsyncCommand RefreshPlotCommand { get; set; }

        public IMvxAsyncCommand RefreshSpectrogramCommand { get; set; }

        public IMvxAsyncCommand SpecificGraphCommand { get; set; }

        public SpectrogramViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService, MvvmCross.Plugin.Messenger.IMvxMessenger messenger)
            : base(logProvider, navigationService)
        {
            audioLogic = new AudioLogic();
            dynoLogic = new DynoLogic();

            Frequenzbeginn = 3000;
            Frequenzende = 12000;

            Qualitys = new List<string>() { "schlecht", "mittel", "gut", "sehr gut" };
            Quality = Qualitys[1]; //mittel

            Colormaps = Enum.GetValues(typeof(Spectrogram.Colormap)).Cast<Spectrogram.Colormap>().ToList();
            Colormap = Colormaps[0]; //viridis

            Intensitys = new List<double>() { 0.1, 0.2, 0.3, 0.4, 0.5, 0.6, 0.7, 0.8, 0.9, 1.0, 1.5, 2.0, 3.0, 4.0, 5.0 };
            Intensity = Intensitys[4]; // 0.5

            Normal_Refresh = true;
            Badge_Refresh = false;

            this.rm = new ResourceManager(typeof(SimTuning.Core.resources));
        }

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        /// <returns>Initilisierung.</returns>
        public override Task Initialize()
        {
            this.ReloadData();

            return base.Initialize();
        }

        /// <summary>
        /// Prepares this instance. called after construction.
        /// </summary>
        public override void Prepare()
        {
        }

        public void ReloadData(Models.MvxReloaderMessage mvxReloaderMessage = null)
        {
            using (var db = new DatabaseContext())
            {
                try
                {
                    Dyno = db.Dyno.Where(d => d.Active == true).Include(v => v.Audio).First();
                }
                catch { }
            }
        }

        #region Commands

        protected virtual async Task FilterPlot()
        {
            Graphs = null;
            Graph = null;

            dynoLogic.PlotRotionalSpeed(audioLogic.SpectrogramAudio, true);

            Graphs = dynoLogic.PlotAudio.Series.Select(x => x.Title).ToList();

            await RaisePropertyChanged("PlotAudio");
        }

        protected virtual async Task RefreshPlot()
        {
            Graphs = null;
            Graph = null;

            try
            {
                dynoLogic.PlotRotionalSpeed(audioLogic.SpectrogramAudio);
            }
            catch
            { }

            await RaisePropertyChanged("PlotAudio");
        }

        protected Stream ReloadImageAudioSpectrogram()
        {
            Normal_Refresh = true;
            Badge_Refresh = false;

            SKBitmap spec = audioLogic.GetSpectrogram(SimTuning.Core.Constants.AudioFilePath, Quality, Intensity, Colormap, Frequenzbeginn / 60, Frequenzende / 60);
            Stream stream = SimTuning.Core.Business.Converts.SKBitmapToStream(spec);

            return stream;
        }

        protected virtual async Task SpecificGraph()
        {
            if (Graphs == null || Graph == null)
                return;

            dynoLogic.AreaRegression(Graphs.IndexOf(Graph));
            Dyno.Audio = dynoLogic.Dyno.Audio;

            using (var db = new DatabaseContext())
            {
                //in Datenbank einfügen
                db.Dyno.Update(Dyno);
                db.SaveChanges();
            }

            await RaisePropertyChanged("PlotAudio");
        }

        #endregion Commands

        #region Values

        private bool _badge_Refresh;

        //0 schwarz, 1 weiß, 2 blau, 3 grün, 4 lila
        private Spectrogram.Colormap _colormap;

        private List<Spectrogram.Colormap> _colormaps;
        private DynoModel _dyno;

        private int _frequenzbeginn;

        private int _frequenzende;

        private string _graph;

        private List<string> _graphs;

        private double _intensity;

        private List<double> _intensitys;

        private bool _normal_Refresh;

        private string _quality;

        //0 schlecht, 1 mittel, 2 gut, 3 sehr gut
        private List<string> _qualitys;

        public bool Badge_Refresh
        {
            get => _badge_Refresh;
            set { SetProperty(ref _badge_Refresh, value); }
        }

        public Spectrogram.Colormap Colormap
        {
            get => _colormap;
            set
            {
                SetProperty(ref _colormap, value);

                //Warnung setzen
                Normal_Refresh = false;
                Badge_Refresh = true;
            }
        }

        public List<Spectrogram.Colormap> Colormaps
        {
            get => _colormaps;
            set { SetProperty(ref _colormaps, value); }
        }

        public DynoModel Dyno
        {
            get => _dyno;
            set { SetProperty(ref _dyno, value); }
        }

        public int Frequenzbeginn
        {
            get => _frequenzbeginn;
            set { SetProperty(ref _frequenzbeginn, value); }
        }

        public int Frequenzende
        {
            get => _frequenzende;
            set { SetProperty(ref _frequenzende, value); }
        }

        public string Graph
        {
            get => _graph;
            set { SetProperty(ref _graph, value); SpecificGraphCommand.Execute(); }
        }

        public List<string> Graphs
        {
            get => _graphs;
            set { SetProperty(ref _graphs, value); }
        }

        public double Intensity
        {
            get => _intensity;
            set { SetProperty(ref _intensity, value); }
        }

        public List<double> Intensitys
        {
            get => _intensitys;
            set { SetProperty(ref _intensitys, value); }
        }

        public bool Normal_Refresh
        {
            get => _normal_Refresh;
            set { SetProperty(ref _normal_Refresh, value); }
        }

        public PlotModel PlotAudio
        {
            get { return dynoLogic.PlotAudio; }
        }

        public string Quality
        {
            get => _quality;
            set { SetProperty(ref _quality, value); }
        }

        public List<string> Qualitys
        {
            get => _qualitys;
            set { SetProperty(ref _qualitys, value); }
        }

        #endregion Values
    }
}