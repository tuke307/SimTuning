// project=SimTuning.Core, file=SpectrogramViewModel.cs, creation=2020:7:31 Copyright (c)
// 2020 tuke productions. All rights reserved.
namespace SimTuning.Core.ViewModels.Dyno
{
    using Data;
    using Data.Models;
    using MediaManager;
    using MediaManager.Library;
    using MediaManager.Playback;
    using MediaManager.Player;
    using MvvmCross.Commands;
    using MvvmCross.Logging;
    using MvvmCross.Navigation;
    using MvvmCross.Plugin.Messenger;
    using MvvmCross.ViewModels;
    using OxyPlot;
    using Plugin.FilePicker.Abstractions;
    using SimTuning.Core.ModuleLogic;
    using SkiaSharp;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Resources;
    using System.Runtime.InteropServices;
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

            this.StopCommand = new MvxAsyncCommand(this.MediaManager.Stop);
            this.PauseCommand = new MvxAsyncCommand(this.MediaManager.Pause);
            this.PlayCommand = new MvxAsyncCommand(this.MediaManager.PlayPause);

            this.MediaManager.PositionChanged += this.Current_PositionChanged;
            mediaManager.StateChanged += this.MediaManager_StateChanged;
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
        /// Cuts the beginn.
        /// </summary>
        /// <returns>
        /// <placeholder>A <see cref="Task" /> representing the asynchronous
        /// operation.</placeholder>
        /// </returns>
        protected virtual async Task CutBeginn()
        {
            this.TrimAudio(this.AudioPosition.Value, 0);

            await this.OpenFileAsync();
        }

        /// <summary>
        /// Cuts the end.
        /// </summary>
        /// <returns>
        /// <placeholder>A <see cref="Task" /> representing the asynchronous
        /// operation.</placeholder>
        /// </returns>
        protected virtual async Task CutEnd()
        {
            this.TrimAudio(0, AudioPosition.Value);

            await this.OpenFileAsync();
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
        /// Opens the file.
        /// </summary>
        /// <returns>
        /// <placeholder>A <see cref="Task" /> representing the asynchronous
        /// operation.</placeholder>
        /// </returns>
        protected virtual async Task OpenFileAsync()
        {
            try
            {
                await this.MediaManager.Play(SimTuning.Core.GeneralSettings.AudioFilePath).ConfigureAwait(true);
                this.StopCommand.Execute();

                await this.RaisePropertyChanged(() => this.AudioMaximum).ConfigureAwait(true);
            }
            catch (Exception exc)
            {
                this.Log.ErrorException("Fehler bei OpenFileAsync: ", exc);
            }
        }

        /// <summary>
        /// Opens the file dialog.
        /// </summary>
        /// <returns>
        /// <placeholder>A <see cref="Task" /> representing the asynchronous
        /// operation.</placeholder>
        /// </returns>
        protected virtual async Task OpenFileDialog(string fileName, Stream stream)
        {
            // wenn Datei ausgewählt
            if (SimTuning.Core.Business.AudioUtils.AudioCopy(fileName, stream))
            {
                await this.OpenFileAsync().ConfigureAwait(true);
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

                SKBitmap spec = AudioLogic.GetSpectrogram(
                    audioFile: SimTuning.Core.GeneralSettings.AudioFilePath,
                    fftSize: _fftSize,
                    intensity: this.Intensity,
                    colormap: this.Colormap,
                    minFreq: this.Frequenzbeginn / 60,
                    maxFreq: this.Frequenzende / 60,
                    targetWidthPx: (int)this.AudioMaximum / 10);

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

        /// <summary>
        /// Schneidet die Audio Datei zurecht speichert den geschnittenen Schnipsel in
        /// einem Stream und überschreibt diese dann
        /// </summary>
        /// <param name="cutStart">The cut start.</param>
        /// <param name="cutEnd">The cut end.</param>
        protected virtual void TrimAudio(double cutStart, double cutEnd)
        {
            try
            {
                this.MediaManager.Stop();
                this.MediaManager.Dispose();

                Stream cuttedFileStream = new MemoryStream();

                if (cutStart > 0)
                {
                    SimTuning.Core.Business.AudioUtils.TrimWavFile(TimeSpan.FromSeconds(cutStart), TimeSpan.FromSeconds(0), outStream: ref cuttedFileStream, inPath: SimTuning.Core.GeneralSettings.AudioFilePath);
                }
                else if (cutEnd > 0)
                {
                    SimTuning.Core.Business.AudioUtils.TrimWavFile(TimeSpan.FromSeconds(0), TimeSpan.FromSeconds(cutEnd), outStream: ref cuttedFileStream, inPath: SimTuning.Core.GeneralSettings.AudioFilePath);
                }

                // löscht alte Datei
                File.Delete(SimTuning.Core.GeneralSettings.AudioFilePath);

                // neue gecuttete temp-Datei für alte Datei ersetzen
                using (var fileStream = File.Create(SimTuning.Core.GeneralSettings.AudioFilePath))
                {
                    cuttedFileStream.Seek(0, SeekOrigin.Begin);
                    cuttedFileStream.CopyTo(fileStream);
                }
            }
            catch (Exception exc)
            {
                this.Log.ErrorException("Fehler bei TrimAudio: ", exc);
            }
        }

        /// <summary>
        /// Handles the PositionChanged event of the Current control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">
        /// The <see cref="PositionChangedEventArgs" /> instance containing the event
        /// data.
        /// </param>
        private void Current_PositionChanged(object sender, PositionChangedEventArgs e)
        {
            this.Log.Debug($"Current position is {e.Position};");
            this.RaisePropertyChanged(() => this.AudioPosition);
        }

        /// <summary>
        /// Handles the StateChanged event of the MediaManager control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">
        /// The <see cref="StateChangedEventArgs" /> instance containing the event data.
        /// </param>
        private void MediaManager_StateChanged(object sender, StateChangedEventArgs e)
        {
            Log.Debug($"Status changed: {System.Enum.GetName(typeof(MediaPlayerState), e.State)};");
        }

        #endregion Methods

        #region Values

        #region Commands

        /// <summary>
        /// Gets or sets the cut beginn command.
        /// </summary>
        /// <value>The cut beginn command.</value>
        public IMvxAsyncCommand CutBeginnCommand { get; set; }

        /// <summary>
        /// Gets or sets the cut end command.
        /// </summary>
        /// <value>The cut end command.</value>
        public IMvxAsyncCommand CutEndCommand { get; set; }

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
        /// Gets or sets the pause command.
        /// </summary>
        /// <value>The pause command.</value>
        public IMvxAsyncCommand PauseCommand { get; set; }

        /// <summary>
        /// Gets or sets the play command.
        /// </summary>
        /// <value>The play command.</value>
        public IMvxAsyncCommand PlayCommand { get; set; }

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
        public IMvxAsyncCommand StopCommand { get; set; }

        #endregion Commands

        #region private

        protected readonly IMediaManager MediaManager /*= CrossMediaManager.Current*/;
        protected readonly ResourceManager rm;
        private static readonly List<string> _qualitys = new List<string>() { "schlecht", "mittel", "gut", "sehr gut" };
        private static readonly string samplewaveLink = "http://simtuning.tuke-productions.de/wp-content/uploads/sample.wav" /*"https://simtuning.tuke-productions.de/download/575/"*/ ;
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
        /// Gets the audio maximum. wenn 0 dann gibt es Fehler in xamarin anwendung.
        /// </summary>
        /// <value>The audio maximum.</value>
        public double? AudioMaximum
        {
            get => this.MediaManager.Duration.TotalMilliseconds == 0 ? 100 : this.MediaManager.Duration.TotalMilliseconds;
        }

        /// <summary>
        /// Gets or sets the audio position.
        /// </summary>
        /// <value>The audio position.</value>
        public double? AudioPosition
        {
            get => this.MediaManager?.Position.Milliseconds ?? 0;
            set
            {
                this.MediaManager.SeekTo(TimeSpan.FromMilliseconds(value.Value));
            }
        }

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
        /// Gets the floated position.
        /// </summary>
        /// <value>The floated position.</value>
        public float FloatedPosition => (float)AudioPosition / (float)AudioMaximum;

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