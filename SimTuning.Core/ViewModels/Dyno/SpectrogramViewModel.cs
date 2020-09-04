// project=SimTuning.Core, file=SpectrogramViewModel.cs, creation=2020:7:31 Copyright (c)
// 2020 tuke productions. All rights reserved.

namespace SimTuning.Core.ViewModels.Dyno
{
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

    /// <summary>
    /// SpectrogramViewModel.
    /// </summary>
    /// <seealso cref="MvvmCross.ViewModels.MvxNavigationViewModel" />
    public class SpectrogramViewModel : MvxNavigationViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SpectrogramViewModel" /> class.
        /// </summary>
        /// <param name="logProvider">The log provider.</param>
        /// <param name="navigationService">The navigation service.</param>
        /// <param name="messenger">The messenger.</param>
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

        #region Methods

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

        /// <summary>
        /// Reloads the data.
        /// </summary>
        /// <param name="mvxReloaderMessage">The MVX reloader message.</param>
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

        /// <summary>
        /// Filters the plot.
        /// </summary>
        protected virtual async Task FilterPlot()
        {
            //Graphs = null;
            Graph = null;

            dynoLogic.PlotRotionalSpeed(audioLogic.SpectrogramAudio, true);

            //Graphs = dynoLogic.PlotAudio.Series.Select(x => x.Title).ToList();
            await this.RaisePropertyChanged(() => Graphs);

            await RaisePropertyChanged(() => PlotAudio);
        }

        /// <summary>
        /// Refreshes the plot.
        /// </summary>
        protected virtual async Task RefreshPlot()
        {
            //Graphs = null;
            Graph = null;

            try
            {
                dynoLogic.PlotRotionalSpeed(audioLogic.SpectrogramAudio);
            }
            catch
            { }

            await RaisePropertyChanged("PlotAudio");
        }

        /// <summary>
        /// Reloads the image audio spectrogram.
        /// </summary>
        /// <returns></returns>
        protected Stream ReloadImageAudioSpectrogram()
        {
            Normal_Refresh = true;
            Badge_Refresh = false;

            SKBitmap spec = audioLogic.GetSpectrogram(SimTuning.Core.Constants.AudioFilePath, Quality, Intensity, Colormap, Frequenzbeginn / 60, Frequenzende / 60);
            Stream stream = SimTuning.Core.Business.Converts.SKBitmapToStream(spec);

            return stream;
        }

        /// <summary>
        /// Specifics the graph.
        /// </summary>
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

            await RaisePropertyChanged(() => PlotAudio);
        }

        #endregion Methods

        #region Values

        #region private

        protected readonly AudioLogic audioLogic;
        protected readonly DynoLogic dynoLogic;
        protected readonly ResourceManager rm;
        private bool _badge_Refresh;

        private Spectrogram.Colormap _colormap;

        private List<Spectrogram.Colormap> _colormaps;
        private DynoModel _dyno;

        private int _frequenzbeginn;

        private int _frequenzende;

        private string _graph;

        private double _intensity;

        private List<double> _intensitys;

        private bool _normal_Refresh;

        private string _quality;

        private List<string> _qualitys;

        #endregion private

        /// <summary>
        /// Gets or sets a value indicating whether [badge refresh].
        /// </summary>
        /// <value><c>true</c> if [badge refresh]; otherwise, <c>false</c>.</value>
        public bool Badge_Refresh
        {
            get => _badge_Refresh;
            set => SetProperty(ref _badge_Refresh, value);
        }

        /// <summary>
        /// Gets or sets the colormap.
        /// </summary>
        /// <value>The colormap.</value>
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

        /// <summary>
        /// Gets or sets the colormaps.
        /// </summary>
        /// <value>The colormaps.</value>
        public List<Spectrogram.Colormap> Colormaps
        {
            get => _colormaps;
            set => SetProperty(ref _colormaps, value);
        }

        /// <summary>
        /// Gets or sets the dyno.
        /// </summary>
        /// <value>The dyno.</value>
        public DynoModel Dyno
        {
            get => _dyno;
            set => SetProperty(ref _dyno, value);
        }

        /// <summary>
        /// Gets or sets the filter plot command.
        /// </summary>
        /// <value>The filter plot command.</value>
        public IMvxAsyncCommand FilterPlotCommand { get; set; }

        /// <summary>
        /// Gets or sets the frequenzbeginn.
        /// </summary>
        /// <value>The frequenzbeginn.</value>
        public int Frequenzbeginn
        {
            get => _frequenzbeginn;
            set => SetProperty(ref _frequenzbeginn, value);
        }

        /// <summary>
        /// Gets or sets the frequenzende.
        /// </summary>
        /// <value>The frequenzende.</value>
        public int Frequenzende
        {
            get => _frequenzende;
            set => SetProperty(ref _frequenzende, value);
        }

        /// <summary>
        /// Gets or sets the graph.
        /// </summary>
        /// <value>The graph.</value>
        public string Graph
        {
            get => _graph;
            set
            {
                if (value == null)
                {
                    return;
                }

                SetProperty(ref _graph, value);
                SpecificGraphCommand.Execute();
            }
        }

        /// <summary>
        /// Gets the graphs.
        /// </summary>
        /// <value>The graphs.</value>
        public List<string> Graphs
        {
            get => this.dynoLogic?.PlotAudio?.Series?.Select(x => x.Title).ToList();
        }

        /// <summary>
        /// Gets or sets the intensity.
        /// </summary>
        /// <value>The intensity.</value>
        public double Intensity
        {
            get => _intensity;
            set => SetProperty(ref _intensity, value);
        }

        /// <summary>
        /// Gets or sets the intensitys.
        /// </summary>
        /// <value>The intensitys.</value>
        public List<double> Intensitys
        {
            get => _intensitys;
            set => SetProperty(ref _intensitys, value);
        }

        /// <summary>
        /// Gets or sets a value indicating whether [normal refresh].
        /// </summary>
        /// <value><c>true</c> if [normal refresh]; otherwise, <c>false</c>.</value>
        public bool Normal_Refresh
        {
            get => _normal_Refresh;
            set => SetProperty(ref _normal_Refresh, value);
        }

        /// <summary>
        /// Gets the plot audio.
        /// </summary>
        /// <value>The plot audio.</value>
        public PlotModel PlotAudio
        {
            get { return dynoLogic.PlotAudio; }
        }

        /// <summary>
        /// Gets or sets the quality.
        /// </summary>
        /// <value>The quality.</value>
        public string Quality
        {
            get => _quality;
            set => SetProperty(ref _quality, value);
        }

        /// <summary>
        /// Gets or sets the qualitys. 0 schlecht, 1 mittel, 2 gut, 3 sehr gut
        /// </summary>
        /// <value>The qualitys.</value>
        public List<string> Qualitys
        {
            get => _qualitys;
            set => SetProperty(ref _qualitys, value);
        }

        /// <summary>
        /// Gets or sets the refresh plot command.
        /// </summary>
        /// <value>The refresh plot command.</value>
        public IMvxAsyncCommand RefreshPlotCommand { get; set; }

        /// <summary>
        /// Gets or sets the refresh spectrogram command.
        /// </summary>
        /// <value>The refresh spectrogram command.</value>
        public IMvxAsyncCommand RefreshSpectrogramCommand { get; set; }

        /// <summary>
        /// Gets or sets the specific graph command.
        /// </summary>
        /// <value>The specific graph command.</value>
        public IMvxAsyncCommand SpecificGraphCommand { get; set; }

        #endregion Values
    }
}