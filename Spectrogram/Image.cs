using SciColorMaps.Portable;
using SkiaSharp;
using System;
using System.Linq;
using System.Runtime.InteropServices;

namespace Spectrogram
{
    internal class Image
    {
        public static SKBitmap BitmapFromFFTs(float[][] ffts, Settings.DisplaySettings displaySettings)
        {
            if (ffts == null || ffts.Length == 0)
                throw new ArgumentException("ffts must contain float arrays");

            SKBitmap bmp = new SKBitmap(ffts.Length, displaySettings.height, SKColorType.Alpha8, SKAlphaType.Opaque);//8-bit alpha-only color

            byte[] pixels = bmp.Bytes;

            //jede Spalte durchgehen
            for (int col = 0; col < bmp.Width; col++)
            {
                if (col >= bmp.Width)
                    continue;

                //Spalte hervorheben
                if (col == displaySettings.highlightColumn)
                {
                    //Zeilen durchgehen
                    for (int row = 0; row < bmp.Height; row++)
                    {
                        int bytePosition = (bmp.Height - 1 - row) * bmp.Width + col;

                        //bmp.SetPixel(col, row, (byte)(255));
                        pixels[bytePosition] = (byte)(255);
                    }
                    continue;
                }

                if (ffts[col] == null)
                    continue;

                //Zeilen durchgehen
                for (int row = 0; row < bmp.Height; row++)
                {
                    int bytePosition = (bmp.Height - 1 - row) * bmp.Width + col;

                    float pixelValue;
                    pixelValue = ffts[col][row + displaySettings.pixelLower];
                    if (displaySettings.decibels)
                        pixelValue = (float)(Math.Log10(pixelValue) * 20);
                    pixelValue = (pixelValue * displaySettings.brightness);
                    pixelValue = Math.Max(0, pixelValue);
                    pixelValue = Math.Min(255, pixelValue);

                    pixels[bytePosition] = (byte)(pixelValue);
                    //bmp.SetPixel(col, row, (byte)(pixelValue));
                }
            }

            IntPtr pix = bmp.GetPixels();
            Marshal.Copy(pixels, 0, pix, pixels.Length);
            bmp.SetPixels(pix);

            //Colormap
            bmp = ApplyColormap(bmp, displaySettings.colormap);

            return bmp;
        }

        public static SKBitmap ApplyColormap(SKBitmap bmp, Colormap colormap)
        {
            //farbtabellen erstellen
            byte[] atable = new byte[256];
            byte[] rtable = new byte[256];
            byte[] gtable = new byte[256];
            byte[] btable = new byte[256];

            //Map erstellen
            ColorMap colorMap = new ColorMap(colormap.ToString());

            //farben zuweisen
            var colors = colorMap.Colors().ToList();

            //farbtabellen zuweisen
            for (int i = 0; i < 256; i++)
            {
                atable[i] = (byte)255;
                rtable[i] = colors[i][0];
                gtable[i] = colors[i][1];
                btable[i] = colors[i][2];
            }

            //Colormap anwenden
            var coloredBitmap = new SKBitmap(bmp.Width, bmp.Height);

            var surface = new SKCanvas(coloredBitmap);
            SKPaint paint = new SKPaint();
            paint.ColorFilter = SKColorFilter.CreateTable(rtable, rtable, gtable, btable);

            surface.Translate(coloredBitmap.Width / 2, coloredBitmap.Height / 2);
            surface.DrawBitmap(bmp, bmp.Width, bmp.Height, paint: paint);

            return coloredBitmap;

            //using (var image = SKImage.FromBitmap(bmp))
            //using (var data = image.Encode())
            //{
            //    // save the data to a stream
            //    using (var stream = File.OpenWrite("C:\\Users\\Tony\\Desktop\\image.png"))
            //    {
            //        data.SaveTo(stream);
            //    }
            //}
        }
    }
}