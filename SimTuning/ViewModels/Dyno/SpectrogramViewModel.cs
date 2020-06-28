using Data;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using OxyPlot;
using SimTuning.ModuleLogic;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SimTuning.ViewModels.Dyno
{
    public class SpectrogramViewModel : BaseViewModel
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

        public ICommand RefreshSpectrogram { get; set; }
        public ICommand AuswahlFenster { get; set; }
        public ICommand RefreshPlot { get; set; }
        public ICommand FilterPlotCommand { get; set; }
        public ICommand SpecificGraphCommand { get; set; }

        protected DynoModel Dyno
        {
            get => Get<DynoModel>();
            set => Set(value);
        }

        protected virtual void Refresh_Plot()
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

            SKBitmap spec = audioLogic.GetSpectrogram(SimTuning.Constants.AudioFilePath, Quality, Intensity, Colormap, Frequenzbeginn / 60, Frequenzende / 60);
            Stream stream = SimTuning.Business.Converts.SKBitmapToStream(spec);

            return stream;
        }

        public PlotModel PlotAudio
        {
            get { return dynoLogic.PlotAudio; }
            set => Set(value);
        }

        //public PlotController Plot_Controller
        //{
        //    get => Get<PlotController>();
        //    set => Set(value);
        //}

        public List<double> Intensitys
        {
            get => Get<List<double>>();
            set => Set(value);
        }

        public double Intensity
        {
            get => Get<double>();
            set => Set(value);
        }

        //0 schlecht, 1 mittel, 2 gut, 3 sehr gut
        public List<string> Qualitys
        {
            get => Get<List<string>>();
            set => Set(value);
        }

        public string Quality
        {
            get => Get<string>();
            set => Set(value);
        }

        public List<string> Graphs
        {
            get => Get<List<string>>();
            set => Set(value);
        }

        public string Graph
        {
            get => Get<string>();
            set => Set(value);
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

        //0 schwarz, 1 weiß, 2 blau, 3 grün, 4 lila
        public Spectrogram.Colormap Colormap
        {
            get => Get<Spectrogram.Colormap>();
            set
            {
                Set(value);

                //Warnung setzen
                Normal_Refresh = false;
                Badge_Refresh = true;
            }
        }

        public List<Spectrogram.Colormap> Colormaps
        {
            get => Get<List<Spectrogram.Colormap>>();
            set => Set(value);
        }

        public bool Badge_Refresh
        {
            get => Get<bool>();
            set => Set(value);
        }

        public bool Normal_Refresh
        {
            get => Get<bool>();
            set => Set(value);
        }

        public int Frequenzbeginn
        {
            get => Get<int>();
            set => Set(value);
        }

        public int Frequenzende
        {
            get => Get<int>();
            set => Set(value);
        }
    }
}