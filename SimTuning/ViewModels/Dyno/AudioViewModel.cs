using Data;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using SimTuning.ModuleLogic;
using SkiaSharp;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SimTuning.ViewModels.Dyno
{
    public class AudioViewModel : BaseViewModel
    {
        protected AudioLogic audioLogic;

        public AudioViewModel()
        {
            audioLogic = new AudioLogic();
            BadgeFileOpen = false;

            using (var db = new DatabaseContext())
            {
                try
                {
                    Dyno = db.Dyno.Where(d => d.Active == true).Include(v => v.Audio).First();
                }
                catch { }
            }
        }

        public ICommand OpenFileCommand { get; set; }
        public ICommand StopCommand { get; set; }
        public ICommand PauseCommand { get; set; }
        public ICommand PlayCommand { get; set; }
        public ICommand CutBeginnCommand { get; set; }
        public ICommand CutEndCommand { get; set; }

        public DynoModel Dyno
        {
            get => Get<DynoModel>();
            set => Set(value);
        }

        protected virtual void OpenFileDialog()
        {
        }

        protected virtual void Play(object parameter)
        {
        }

        protected virtual void Pause(object parameter)
        {
        }

        protected virtual void Stop(object parameter)
        {
        }

        protected bool BadgeFileOpen
        {
            get => Get<bool>();
            set => Set(value);
        }

        protected Stream ReloadImageAudioSpectrogram()
        {
            SKBitmap spec = audioLogic.GetSpectrogram(SimTuning.Constants.AudioFilePath);
            Stream stream = SimTuning.Business.Converts.SKBitmapToStream(spec);

            return stream;
        }

        protected virtual void OpenFile()
        {
        }

        protected virtual void CutBeginn()
        {
        }

        protected virtual void CutEnd()
        {
        }

        /// <summary>
        /// Schneidet die Audio Datei zurecht
        /// speichert den geschnittenen Schnipsel in einem Stream und überschreibt diese dann
        /// </summary>
        /// <param name="cut_start">The cut start.</param>
        /// <param name="cut_end">The cut end.</param>
        protected virtual void TrimAudio(double cut_start, double cut_end)
        {
            //string tempFile = Path.Combine(SimTuning.Constants.FileDirectory, "temp.wav");
            Stream cuttedFileStream = new MemoryStream();
            //FileStream fsSource = new FileStream(SimTuning.Constants.AudioFilePath, FileMode.Open, FileAccess.Read);

            if (cut_start > 0)
                SimTuning.Business.AudioUtils.TrimWavFile(TimeSpan.FromSeconds(cut_start), TimeSpan.FromSeconds(0), outStream: ref cuttedFileStream, inPath: SimTuning.Constants.AudioFilePath);
            else if (cut_end > 0)
                SimTuning.Business.AudioUtils.TrimWavFile(TimeSpan.FromSeconds(0), TimeSpan.FromSeconds(cut_end), outStream: ref cuttedFileStream, inPath: SimTuning.Constants.AudioFilePath);

            //löscht alte Datei
            File.Delete(SimTuning.Constants.AudioFilePath);

            //neue gecuttete temp-Datei für alte Datei ersetzen
            //File.Create(tempFile, SimTuning.Constants.AudioFilePath);

            using (var fileStream = File.Create(SimTuning.Constants.AudioFilePath))
            {
                cuttedFileStream.Seek(0, SeekOrigin.Begin);
                cuttedFileStream.CopyTo(fileStream);
            }
        }
    }
}