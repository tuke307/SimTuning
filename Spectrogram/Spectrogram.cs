﻿using SkiaSharp;
using System;
using System.Collections.Generic;

namespace Spectrogram
{
    public class Spectrogram
    {
        public readonly Settings.FftSettings fftSettings;
        public readonly Settings.DisplaySettings displaySettings;

        public List<float[]> fftList = new List<float[]>();
        public List<float> signal = new List<float>();

        public int nextIndex;
        public float[] latestFFT;

        public Spectrogram(int sampleRate = 8000, int fftSize = 1024, int? step = null)
        {
            if (step == null)
                step = sampleRate;
            fftSettings = new Settings.FftSettings(sampleRate, fftSize, (int)step);
            displaySettings = new Settings.DisplaySettings();
            displaySettings.fftResolution = fftSettings.fftResolution;
            displaySettings.freqHigh = fftSettings.maxFreq;
        }

        public override string ToString()
        {
            return $"Spectrogram ({fftSettings.sampleRate} Hz) " +
                "with {ffts.Count} segments in memory " +
                "({fftSettings.fftSize} points each)";
        }

        public string GetFftInfo()
        {
            return fftSettings.ToString();
        }

        public void AddExtend(float[] values)
        {
            signal.AddRange(values);
            ProcessNewSegments(scroll: false, fixedSize: null);
        }

        public void AddCircular(float[] values, int fixedSize)
        {
            signal.AddRange(values);
            ProcessNewSegments(scroll: false, fixedSize: fixedSize);
        }

        public void AddScroll(float[] values, int fixedSize)
        {
            signal.AddRange(values);
            ProcessNewSegments(scroll: true, fixedSize: fixedSize);
        }

        private void ProcessNewSegments(bool scroll, int? fixedSize)
        {
            int segmentsRemaining = (signal.Count - fftSettings.fftSize) / fftSettings.step;
            float[] segment = new float[fftSettings.fftSize];

            while (signal.Count > (fftSettings.fftSize + fftSettings.step))
            {
                int remainingSegments = (signal.Count - fftSettings.fftSize) / fftSettings.step;
                if (remainingSegments % 10 == 0)
                {
                    Console.WriteLine(string.Format("Processing segment {0} of {1} ({2:0.0}%)",
                        fftList.Count + 1, segmentsRemaining, 100.0 * (fftList.Count + 1) / segmentsRemaining));
                }

                signal.CopyTo(0, segment, 0, fftSettings.fftSize);
                signal.RemoveRange(0, fftSettings.step);

                latestFFT = Operations.FFT(segment);

                if (fixedSize == null)
                    AddNewFftExtend(latestFFT);
                else
                    AddNewFftFixed(latestFFT, (int)fixedSize, scroll);
            }

            displaySettings.width = fftList.Count;
            //displaySettings.renderNeeded = true;
        }

        private void AddNewFftExtend(float[] fft)
        {
            fftList.Add(fft);
        }

        private void AddNewFftFixed(float[] fft, int fixedSize, bool scroll)
        {
            while (fftList.Count < fixedSize)
                fftList.Add(null);
            while (fftList.Count > fixedSize)
                fftList.RemoveAt(fftList.Count - 1);

            if (scroll)
            {
                fftList.Add(fft);
                fftList.RemoveAt(0);
            }
            else
            {
                nextIndex = Math.Min(nextIndex, fftList.Count - 1);
                fftList[nextIndex] = fft;
                nextIndex += 1;
                if (nextIndex >= fftList.Count)
                    nextIndex = 0;
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
            if (fftList.Count == 0)
                return null;

            if (displaySettings.height < 1)
                throw new ArgumentException("FFT frequency range is too small");

            if (intensity != null)
                displaySettings.brightness = (float)intensity;

            displaySettings.decibels = decibels;
            displaySettings.colormap = (colormap == null) ? displaySettings.colormap : (Colormap)colormap;
            displaySettings.freqLow = (freqLow == null) ? 0 : (double)freqLow;
            displaySettings.freqHigh = (freqHigh == null) ? fftSettings.maxFreq : (double)freqHigh;
            displaySettings.showTicks = (showTicks == null) ? displaySettings.showTicks : (bool)showTicks;
            displaySettings.tickSpacingHz = (tickSpacingHz == null) ? displaySettings.tickSpacingHz : (double)tickSpacingHz;
            displaySettings.tickSpacingSec = (tickSpacingSec == null) ? displaySettings.tickSpacingSec : (double)tickSpacingSec;

            if (highlightLatestColumn)
                displaySettings.highlightColumn = nextIndex;
            else
                displaySettings.highlightColumn = null;

            //SKBitmap bmpIndexed;
            SKBitmap bmpRgb = Image.BitmapFromFFTs(fftList.ToArray(), displaySettings);

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