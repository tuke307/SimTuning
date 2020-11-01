// project=SimTuning.Core, file=SpectrogramViewModel.cs, creation=2020:7:31 Copyright (c)
// 2020 tuke productions. All rights reserved.
namespace SimTuning.Core.ViewModels.Dyno
{
    using Data;
    using Data.Models;
    using MediaManager;
    using MediaManager.Library;
    using MvvmCross.Commands;
    using MvvmCross.Logging;
    using MvvmCross.Navigation;
    using MvvmCross.Plugin.Messenger;
    using MvvmCross.ViewModels;
    using Newtonsoft.Json;
    using OxyPlot;
    using SimTuning.Core.ModuleLogic;
    using SkiaSharp;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.IO.Compression;
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
        public SpectrogramViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService, IMvxMessenger messenger, IMediaManager mediaManager)
            : base(logProvider, navigationService)
        {
            this.MediaManager = mediaManager;

            this.Frequenzbeginn = 3000;
            this.Frequenzende = 12000;
            this.FilterValue = 2;

            this.Quality = this.Qualitys[1]; // mittel
            this.Colormap = this.Colormaps[0]; // viridis
            this.Intensity = this.Intensitys[4]; // 0.5

            this.Normal_Refresh = true;
            this.Badge_Refresh = false;

            this.rm = new ResourceManager(typeof(SimTuning.Core.resources));

            //this.NavigationService.Navigate<AudioPlayerViewModel>().ConfigureAwait(true);
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
            base.Prepare();
        }

        /// <summary>
        /// Reloads the data.
        /// </summary>
        /// <param name="mvxReloaderMessage">The MVX reloader message.</param>
        public void ReloadData(Models.MvxReloaderMessage mvxReloaderMessage = null)
        {
            try
            {
                using (var db = new DatabaseContext())
                {
                    this.Dyno = db.Dyno.Single(d => d.Active == true); // .Include(v => v.Audio);
                }
            }
            catch (Exception exc)
            {
                this.Log.ErrorException("Fehler beim Laden des Dyno-Datensatz: ", exc);
            }
        }

        /// <summary>
        /// Filters the plot.
        /// </summary>
        protected virtual async Task FilterPlot()
        {
            try
            {
                DynoLogic.GetDrehzahlGraph(areas: true, areaAbstand: this.FilterValue);

                await this.RaisePropertyChanged(() => this.Graphs).ConfigureAwait(true);

                await this.RaisePropertyChanged(() => this.PlotAudio).ConfigureAwait(true);
            }
            catch (Exception exc)
            {
                this.Log.ErrorException("Fehler bei FilterPlot: ", exc);
            }
        }

        /// <summary>
        /// Opens the file dialog.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        protected virtual async Task OpenFileDialog(string fileName)
        {
            //bool status = false;

            // zip extrahieren
            if (File.Exists(SimTuning.Core.GeneralSettings.DataExportFilePath))
            {
                File.Delete(SimTuning.Core.GeneralSettings.DataExportFilePath);
            }
            if (File.Exists(SimTuning.Core.GeneralSettings.AudioFilePath))
            {
                File.Delete(SimTuning.Core.GeneralSettings.AudioFilePath);
            }
            ZipFile.ExtractToDirectory(fileName, SimTuning.Core.GeneralSettings.FileDirectory);

            // wenn Datei ausgewählt
            //using (FileStream sourceStream = File.Open(fileName, FileMode.OpenOrCreate))
            //{
            //    status = SimTuning.Core.Business.AudioUtils.AudioCopy(SimTuning.Core.GeneralSettings.AudioFile, sourceStream);
            //}

            //if (status)
            //{
            await this.PlayFileAsync().ConfigureAwait(true);
            //}

            // TODO: only for testing
            if (File.Exists(SimTuning.Core.GeneralSettings.DataExportFilePath))
            {
                string json = File.ReadAllText(SimTuning.Core.GeneralSettings.DataExportFilePath);
                DynoModel _dyno = JsonConvert.DeserializeObject<DynoModel>(json);
            }
        }

        /// <summary>
        /// Opens the file.
        /// </summary>
        /// <returns>
        /// <placeholder>A <see cref="Task" /> representing the asynchronous
        /// operation.</placeholder>
        /// </returns>
        protected virtual async Task PlayFileAsync()
        {
            try
            {
                var generatedMediaItem = await MediaManager.Extractor.CreateMediaItem(GeneralSettings.AudioFilePath).ConfigureAwait(true);
                CrossMediaManager.Current.Queue.Add(generatedMediaItem);
            }
            catch (Exception exc)
            {
                this.Log.ErrorException("Fehler bei OpenFileAsync: ", exc);
            }
        }

        /// <summary>
        /// Refreshes the plot.
        /// </summary>
        /// <returns>
        /// <placeholder>A <see cref="Task" /> representing the asynchronous
        /// operation.</placeholder>
        /// </returns>
        protected virtual async Task RefreshPlot()
        {
            try
            {
                DynoLogic.GetDrehzahlGraph(intensity: this.Intensity);

                await this.RaisePropertyChanged(() => this.PlotAudio).ConfigureAwait(true);
            }
            catch (Exception exc)
            {
                this.Log.ErrorException("Fehler bei RefreshPlot: ", exc);
            }
        }

        /// <summary>
        /// Reloads the image audio spectrogram.
        /// </summary>
        /// <returns></returns>
        protected Stream ReloadImageAudioSpectrogram()
        {
            try
            {
                // Normal_Refresh = true; Badge_Refresh = false;
                int _fftSize;
                switch (this.Quality)
                {
                    case "schlecht":
                        _fftSize = 8192; //2^13
                        break;

                    case "mittel":
                        _fftSize = 16384; //2^14
                        break;

                    case "gut":
                        _fftSize = 32768; //2^15
                        break;

                    case "sehr gut":
                        _fftSize = 65536;//2^16
                        break;

                    default:
                        _fftSize = 16384;
                        break;
                }

                // TODO: duration from audio
                SKBitmap spec = AudioLogic.GetSpectrogram(
                    audioFile: SimTuning.Core.GeneralSettings.AudioFilePath,
                    fftSize: _fftSize,
                    intensity: this.Intensity,
                    colormap: this.Colormap,
                    minFreq: this.Frequenzbeginn / 60,
                    maxFreq: this.Frequenzende / 60,
                    targetWidthPx: /*(int)this.Duration / 10*/1000);

                Stream stream = SimTuning.Core.Business.Converts.SKBitmapToStream(spec);

                return stream;
            }
            catch (Exception exc)
            {
                this.Log.ErrorException("Fehler bei ReloadImageAudioSpectrogram: ", exc);
                return null;
            }
        }

        /// <summary>
        /// Specifics the graph.
        /// </summary>
        protected virtual void SpecificGraph()
        {
            if (this.Graphs == null || this.Graph == null)
            {
                return;
            }

            try
            {
                DynoLogic.Graphauswahl = this.Graphs.IndexOf(this.Graph);
                DynoLogic.GetDrehzahlGraphFitted(out var drehzahlModels);
                this.Dyno.Drehzahl = drehzahlModels;

                using (var db = new DatabaseContext())
                {
                    // in Datenbank einfügen
                    db.Dyno.Update(this.Dyno);
                    db.SaveChanges();
                }

                this.RaisePropertyChanged(() => PlotAudio);
            }
            catch (Exception exc)
            {
                this.Log.ErrorException("Fehler bei SpecificGraph: ", exc);
            }
        }

        #endregion Methods

        #region Values

        #region Commands

        /// <summary>
        /// Gets or sets the filter plot command.
        /// </summary>
        /// <value>The filter plot command.</value>
        public IMvxAsyncCommand FilterPlotCommand { get; set; }

        /// <summary>
        /// Gets or sets the open file command.
        /// </summary>
        /// <value>The open file command.</value>
        public IMvxAsyncCommand OpenFileCommand { get; set; }

        /// <summary>
        /// Gets or sets the open file command.
        /// </summary>
        /// <value>The open file command.</value>
        public IMvxAsyncCommand RefreshAudioFileCommand { get; set; }

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

        /// <summary>
        /// Gets or sets the stop command.
        /// </summary>
        /// <value>The stop command.</value>
        //public IMvxAsyncCommand StopCommand { get; set; }

        #endregion Commands

        #region private

        protected readonly IMediaManager MediaManager /*= CrossMediaManager.Current*/;
        protected readonly ResourceManager rm;
        private static readonly List<string> _qualitys = new List<string>() { "schlecht", "mittel", "gut", "sehr gut" };
        private bool _badge_Refresh;

        private Spectrogram.Colormap _colormap;

        private List<Spectrogram.Colormap> _colormaps = Enum.GetValues(typeof(Spectrogram.Colormap)).Cast<Spectrogram.Colormap>().ToList();
        private DynoModel _dyno;

        private int _filterValue;
        private int _frequenzbeginn;

        private int _frequenzende;

        private string _graph;

        private double _intensity;

        private List<double> _intensitys = new List<double>() { 0.1, 0.2, 0.3, 0.4, 0.5, 0.6, 0.7, 0.8, 0.9, 1.0, 1.5, 2.0, 3.0, 4.0, 5.0 };

        private bool _normal_Refresh;

        private string _quality;

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
        /// Gets the buffered.
        /// </summary>
        /// <value>The buffered.</value>
        public int Buffered => Convert.ToInt32(MediaManager.Buffered.TotalSeconds);

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
            //set => SetProperty(ref _colormaps, value);
        }

        /// <summary>
        /// Gets the current.
        /// </summary>
        /// <value>The current.</value>
        public IMediaItem Current => MediaManager.Queue.Current;

        /// <summary>
        /// Gets the current subtitle.
        /// </summary>
        /// <value>The current subtitle.</value>
        public string CurrentSubtitle => Current.DisplaySubtitle;

        /// <summary>
        /// Gets the current title.
        /// </summary>
        /// <value>The current title.</value>
        public string CurrentTitle => Current.DisplayTitle;

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
        /// Wert für den Abstand, um einen Punkt herum. Dieser Wert wird bei der Funktion
        /// der Areas verwendet.
        /// </summary>
        public int FilterValue
        {
            get => this._filterValue;
            set => this.SetProperty(ref this._filterValue, value);
        }

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
            get => DynoLogic.PlotAudio?.Series?.Select(x => x.Title).ToList();
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
            get => DynoLogic.PlotAudio;
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
            //set => SetProperty(ref _qualitys, value);
        }

        #endregion Values
    }
}