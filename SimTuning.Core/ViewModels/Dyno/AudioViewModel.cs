using Data;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using Plugin.FilePicker;
using Plugin.FilePicker.Abstractions;
using Plugin.SimpleAudioPlayer;
using SimTuning.Core.ModuleLogic;
using SkiaSharp;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Threading.Tasks;

namespace SimTuning.Core.ViewModels.Dyno
{
    public class AudioViewModel : MvxNavigationViewModel
    {
        protected AudioLogic audioLogic;
        protected ISimpleAudioPlayer player;
        protected ResourceManager rm;

        public AudioViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService) : base(logProvider, navigationService)
        {
            audioLogic = new AudioLogic();
            BadgeFileOpen = false;

            StopCommand = new MvxCommand(Stop);
            PauseCommand = new MvxCommand(Pause);
            PlayCommand = new MvxCommand(Play);
        }

        public IMvxAsyncCommand OpenFileCommand { get; set; }
        public IMvxAsyncCommand CutBeginnCommand { get; set; }
        public IMvxAsyncCommand CutEndCommand { get; set; }
        public IMvxCommand StopCommand { get; set; }
        public IMvxCommand PauseCommand { get; set; }
        public IMvxCommand PlayCommand { get; set; }

        public override void Prepare()
        {
            // This is the first method to be called after construction
        }

        public override Task Initialize()
        {
            using (var db = new DatabaseContext())
            {
                try
                {
                    Dyno = db.Dyno.Where(d => d.Active == true)/*.Include(v => v.Audio)*/.First();
                }
                catch { }
            }

            //messages
            this.rm = new ResourceManager(typeof(SimTuning.Core.resources));

            return base.Initialize();
        }

        #region Commands

        /// <summary>
        /// Opens the file dialog.
        /// </summary>
        protected virtual async Task OpenFileDialog()
        {
            FileData fileData = await CrossFilePicker.Current.PickFile(new string[] { ".wav", ".mp3" }).ConfigureAwait(true);

            if (fileData == null)
                return; // user canceled file picking

            //wenn Datei ausgewählt
            if (SimTuning.Core.Business.AudioUtils.AudioCopy(fileData.FileName, fileData.GetStream()))
                OpenFile();
        }

        protected virtual void Play()
        {
            if (player != null)
            {
                player.Play();

                //Position aktualisieren
                Task t = Task.Run(() =>
                {
                    while (player.IsPlaying)
                    {
                        //AudioPosition = player.CurrentPosition;
                        RaisePropertyChanged("AudioPosition");
                    }
                });
            }
        }

        protected virtual void Pause()
        {
            if (player != null)
                player.Pause();
        }

        protected virtual void Stop()
        {
            if (player != null)
            {
                player.Stop();
                AudioPosition = 0;
                RaisePropertyChanged("AudioPosition");
            }
        }

        protected virtual void OpenFile()
        {
            //initialisieren
            var stream = File.OpenRead(SimTuning.Core.Constants.AudioFilePath);
            player = CrossSimpleAudioPlayer.CreateSimpleAudioPlayer();
            player.Load(stream);
            stream.Dispose();

            Task t = Task.Run(() =>
            {
                Task.Delay(1000).Wait();
                RaisePropertyChanged("AudioMaximum");
            });
        }

        protected virtual async Task CutBeginn()
        {
            TrimAudio(AudioPosition.Value, 0);

            OpenFile();

            await RaisePropertyChanged("AudioPosition");
        }

        protected virtual async Task CutEnd()
        {
            TrimAudio(0, AudioPosition.Value);

            OpenFile();

            await RaisePropertyChanged("AudioPosition");
        }

        protected Stream ReloadImageAudioSpectrogram()
        {
            SKBitmap spec = audioLogic.GetSpectrogram(SimTuning.Core.Constants.AudioFilePath);
            Stream stream = SimTuning.Core.Business.Converts.SKBitmapToStream(spec);

            return stream;
        }

        /// <summary>
        /// Schneidet die Audio Datei zurecht
        /// speichert den geschnittenen Schnipsel in einem Stream und überschreibt diese dann
        /// </summary>
        /// <param name="cutStart">The cut start.</param>
        /// <param name="cutEnd">The cut end.</param>
        protected virtual void TrimAudio(double cutStart, double cutEnd)
        {
            player.Stop();
            player.Dispose();
            player = null;

            //string tempFile = Path.Combine(SimTuning.Core.Constants.FileDirectory, "temp.wav");
            Stream cuttedFileStream = new MemoryStream();
            //FileStream fsSource = new FileStream(SimTuning.Core.Constants.AudioFilePath, FileMode.Open, FileAccess.Read);

            if (cutStart > 0)
                SimTuning.Core.Business.AudioUtils.TrimWavFile(TimeSpan.FromSeconds(cutStart), TimeSpan.FromSeconds(0), outStream: ref cuttedFileStream, inPath: SimTuning.Core.Constants.AudioFilePath);
            else if (cutEnd > 0)
                SimTuning.Core.Business.AudioUtils.TrimWavFile(TimeSpan.FromSeconds(0), TimeSpan.FromSeconds(cutEnd), outStream: ref cuttedFileStream, inPath: SimTuning.Core.Constants.AudioFilePath);

            //löscht alte Datei
            File.Delete(SimTuning.Core.Constants.AudioFilePath);

            //neue gecuttete temp-Datei für alte Datei ersetzen
            //File.Create(tempFile, SimTuning.Core.Constants.AudioFilePath);

            using (var fileStream = File.Create(SimTuning.Core.Constants.AudioFilePath))
            {
                cuttedFileStream.Seek(0, SeekOrigin.Begin);
                cuttedFileStream.CopyTo(fileStream);
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

        private bool _badgeFileOpen;

        public bool BadgeFileOpen
        {
            get => _badgeFileOpen;
            set { SetProperty(ref _badgeFileOpen, value); }
        }

        public double? AudioMaximum
        {
            get
            {
                if (player != null)
                    return player.Duration;
                else
                    return null;
            }
        }

        private double? _audioPosition;

        public double? AudioPosition
        {
            get => _audioPosition;
            set
            {
                SetProperty(ref _audioPosition, value);

                if (player != null)
                {
                    player.Seek(value.Value);
                    _audioPosition = value;
                }
            }
        }

        #endregion Values
    }
}