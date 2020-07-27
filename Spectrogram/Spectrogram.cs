using SkiaSharp;
using System;
using System.Collections.Generic;

namespace Spectrogram
{
    public class Spectrogram
    {
        public readonly Settings.FftSettings FftSettings;
        public readonly Settings.DisplaySettings DisplaySettings;

        public List<float[]> FftList = new List<float[]>();
        public List<float> Signal = new List<float>();

        public int NextIndex;
        public float[] LatestFFT;

        public Spectrogram(int sampleRate = 8000, int fftSize = 1024, int? step = null)
        {
            if (step == null)
                step = sampleRate;
            FftSettings = new Settings.FftSettings(sampleRate, fftSize, (int)step);
            DisplaySettings = new Settings.DisplaySettings();
            DisplaySettings.fftResolution = FftSettings.fftResolution;
            DisplaySettings.freqHigh = FftSettings.maxFreq;
        }

        public override string ToString()
        {
            return $"Spectrogram ({FftSettings.sampleRate} Hz) " +
                "with {ffts.Count} segments in memory " +
                "({fftSettings.fftSize} points each)";
        }

        public string GetFftInfo()
        {
            return FftSettings.ToString();
        }

        public void AddExtend(float[] values)
        {
            Signal.AddRange(values);
            ProcessNewSegments(scroll: false, fixedSize: null);
        }

        public void AddCircular(float[] values, int fixedSize)
        {
            Signal.AddRange(values);
            ProcessNewSegments(scroll: false, fixedSize: fixedSize);
        }

        public void AddScroll(float[] values, int fixedSize)
        {
            Signal.AddRange(values);
            ProcessNewSegments(scroll: true, fixedSize: fixedSize);
        }

        private void ProcessNewSegments(bool scroll, int? fixedSize)
        {
            int segmentsRemaining = (Signal.Count - FftSettings.fftSize) / FftSettings.step;
            float[] segment = new float[FftSettings.fftSize];

            while (Signal.Count > (FftSettings.fftSize + FftSettings.step))
            {
                int remainingSegments = (Signal.Count - FftSettings.fftSize) / FftSettings.step;
                if (remainingSegments % 10 == 0)
                {
                    Console.WriteLine(string.Format("Processing segment {0} of {1} ({2:0.0}%)",
                        FftList.Count + 1, segmentsRemaining, 100.0 * (FftList.Count + 1) / segmentsRemaining));
                }

                Signal.CopyTo(0, segment, 0, FftSettings.fftSize);
                Signal.RemoveRange(0, FftSettings.step);

                LatestFFT = Operations.FFT(segment);

                if (fixedSize == null)
                    AddNewFftExtend(LatestFFT);
                else
                    AddNewFftFixed(LatestFFT, (int)fixedSize, scroll);
            }

            DisplaySettings.width = FftList.Count;
            //displaySettings.renderNeeded = true;
        }

        private void AddNewFftExtend(float[] fft)
        {
            FftList.Add(fft);
        }

        private void AddNewFftFixed(float[] fft, int fixedSize, bool scroll)
        {
            while (FftList.Count < fixedSize)
                FftList.Add(null);
            while (FftList.Count > fixedSize)
                FftList.RemoveAt(FftList.Count - 1);

            if (scroll)
            {
                FftList.Add(fft);
                FftList.RemoveAt(0);
            }
            else
            {
                NextIndex = Math.Min(NextIndex, FftList.Count - 1);
                FftList[NextIndex] = fft;
                NextIndex += 1;
                if (NextIndex >= FftList.Count)
                    NextIndex = 0;
            }
        }

        public SKBitmap GetBitmap(
            double? intensity = null,
            bool decibels = false,
            //bool vertical = false,
            Colormap? colormap = null,
            bool? showTicks = null,
            double? tickSpacingHz = null,
            double? tickSpacingSec = null,
            double? freqLow = null,
            double? freqHigh = null,
            bool highlightLatestColumn = false
            )
        {
            if (FftList.Count == 0)
                return null;

            if (DisplaySettings.height < 1)
                throw new ArgumentException("FFT frequency range is too small");

            if (intensity != null)
                DisplaySettings.brightness = (float)intensity;

            DisplaySettings.decibels = decibels;
            DisplaySettings.colormap = (colormap == null) ? DisplaySettings.colormap : (Colormap)colormap;
            DisplaySettings.freqLow = (freqLow == null) ? 0 : (double)freqLow;
            DisplaySettings.freqHigh = (freqHigh == null) ? FftSettings.maxFreq : (double)freqHigh;
            DisplaySettings.showTicks = (showTicks == null) ? DisplaySettings.showTicks : (bool)showTicks;
            DisplaySettings.tickSpacingHz = (tickSpacingHz == null) ? DisplaySettings.tickSpacingHz : (double)tickSpacingHz;
            DisplaySettings.tickSpacingSec = (tickSpacingSec == null) ? DisplaySettings.tickSpacingSec : (double)tickSpacingSec;

            if (highlightLatestColumn)
                DisplaySettings.highlightColumn = NextIndex;
            else
                DisplaySettings.highlightColumn = null;

            //SKBitmap bmpIndexed;
            SKBitmap bmpRgb = Image.BitmapFromFFTs(FftList.ToArray(), DisplaySettings);

            //using (var benchmark = new Benchmark(true))
            //{
            //    bmpIndexed = Image.BitmapFromFFTs(fftList.ToArray(), displaySettings);
            //    if (vertical)
            //    {
            //        //var rotated = new SKBitmap(bmpIndexed.Height, bmpIndexed.Width);

            //        //using (var surface = new SKCanvas(rotated))
            //        //{
            //        //    surface.Translate(rotated.Width, 0);
            //        //    surface.RotateDegrees(90);
            //        //    surface.DrawBitmap(bmpIndexed, 0, 0);
            //        //}
            //    }
            //    bmpRgb = bmpIndexed;
            //    displaySettings.lastRenderMsec = benchmark.elapsedMilliseconds;
            //}

            return bmpRgb;
        }

        //public double GetLastRenderTime()
        //{
        //    return displaySettings.lastRenderMsec;
        //}
    }
}