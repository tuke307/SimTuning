using Data;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using MvvmCross.Commands;
using MvvmCross.ViewModels;
using OxyPlot;
using SimTuning.Core.ModuleLogic;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SimTuning.Core.ViewModels.Dyno
{
    public class SpectrogramViewModel : MvxViewModel
    {
        protected readonly AudioLogic audioLogic;

        protected readonly DynoLogic dynoLogic;

        public SpectrogramViewModel()
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

            using (var db = new DatabaseContext())
            {
                try
                {
                    Dyno = db.Dyno.Where(d => d.Active == true).Include(v => v.Audio).First();
                }
                catch { }
            }
        }

        public IMvxAsyncCommand RefreshSpectrogramCommand { get; set; }
        public IMvxAsyncCommand RefreshPlotCommand { get; set; }
        public IMvxAsyncCommand FilterPlotCommand { get; set; }
        public IMvxAsyncCommand SpecificGraphCommand { get; set; }

        public override void Prepare()
        {
            // This is the first method to be called after construction
        }

        public override Task Initialize()
        {
            // Async initialization

            return base.Initialize();
        }

        #region Commands

        protected virtual void RefreshPlot()
        {
            Graphs = null;
            Graph = null;

            try
            {
                dynoLogic.PlotRotionalSpeed(audioLogic.SpectrogramAudio);
            }
            catch
            { }
        }

        protected virtual void FilterPlot()
        {
            Graphs = null;
            Graph = null;

            dynoLogic.PlotRotionalSpeed(audioLogic.SpectrogramAudio, true);

            Graphs = dynoLogic.PlotAudio.Series.Select(x => x.Title).ToList();
        }

        protected Stream ReloadImageAudioSpectrogram()
        {
            Normal_Refresh = true;
            Badge_Refresh = false;

            SKBitmap spec = audioLogic.GetSpectrogram(SimTuning.Core.Constants.AudioFilePath, Quality, Intensity, Colormap, Frequenzbeginn / 60, Frequenzende / 60);
            Stream stream = SimTuning.Core.Business.Converts.SKBitmapToStream(spec);

            return stream;
        }

        protected virtual void SpecificGraph()
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
        }

        #endregion Commands

        #region Values

        private DynoModel _dyno;

        public DynoModel Dyno
        {
            get => _dyno;
            set { SetProperty(ref _dyno, value); }
        }

        public PlotModel PlotAudio
        {
            get { return dynoLogic.PlotAudio; }
        }

        private List<double> _intensitys;

        public List<double> Intensitys
        {
            get => _intensitys;
            set { SetProperty(ref _intensitys, value); }
        }

        private double _intensity;

        public double Intensity
        {
            get => _intensity;
            set { SetProperty(ref _intensity, value); }
        }

        //0 schlecht, 1 mittel, 2 gut, 3 sehr gut
        private List<string> _qualitys;

        public List<string> Qualitys
        {
            get => _qualitys;
            set { SetProperty(ref _qualitys, value); }
        }

        private string _quality;

        public string Quality
        {
            get => _quality;
            set { SetProperty(ref _quality, value); }
        }

        private List<string> _graphs;

        public List<string> Graphs
        {
            get => _graphs;
            set { SetProperty(ref _graphs, value); }
        }

        private string _graph;

        public string Graph
        {
            get => _graph;
            set { SetProperty(ref _graph, value); }
        }

        //0 schwarz, 1 weiß, 2 blau, 3 grün, 4 lila
        private Spectrogram.Colormap _colormap;

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

        private List<Spectrogram.Colormap> _colormaps;

        public List<Spectrogram.Colormap> Colormaps
        {
            get => _colormaps;
            set { SetProperty(ref _colormaps, value); }
        }

        private bool _badge_Refresh;

        public bool Badge_Refresh
        {
            get => _badge_Refresh;
            set { SetProperty(ref _badge_Refresh, value); }
        }

        private bool _normal_Refresh;

        public bool Normal_Refresh
        {
            get => _normal_Refresh;
            set { SetProperty(ref _normal_Refresh, value); }
        }

        private int _frequenzbeginn;

        public int Frequenzbeginn
        {
            get => _frequenzbeginn;
            set { SetProperty(ref _frequenzbeginn, value); }
        }

        private int _frequenzende;

        public int Frequenzende
        {
            get => _frequenzende;
            set { SetProperty(ref _frequenzende, value); }
        }

        #endregion Values
    }
}