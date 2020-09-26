using System;

namespace Spectrogram
{
    public static class Tools
    {
        //private static string FindFile(string filePath)
        //{
        //    // look for it in this folder
        //    string pathFileHere = System.IO.Path.GetFullPath(filePath);
        //    if (System.IO.File.Exists(pathFileHere))
        //        return pathFileHere;
        //    else
        //        Console.WriteLine($"File not found in same folder: {pathFileHere}");

        // // look for it in the package data folder string fileName =
        // System.IO.Path.GetFileName(filePath); string pathDataFolder =
        // System.IO.Path.GetFullPath("../../../../data/"); string pathInDataFolder =
        // System.IO.Path.Combine(pathDataFolder, fileName); if
        // (System.IO.File.Exists(pathInDataFolder)) return pathInDataFolder; else
        // Console.WriteLine($"File not found in data folder: {pathInDataFolder}");

        //    throw new ArgumentException($"Could not locate {filePath}");
        //}

        public static float[] FloatsFromBytesINT16(byte[] bytes, int skipFirstBytes = 0)
        {
            float[] pcm = new float[(bytes.Length - skipFirstBytes) / 2];
            for (int i = skipFirstBytes; i < bytes.Length - 2; i += 2)
            {
                if (i / 2 >= pcm.Length)
                    break;
                pcm[i / 2] = BitConverter.ToInt16(bytes, i);
            }
            return pcm;
        }

        public static float[] ReadWav(string wavFilePath)
        {
            // quick and drity WAV file reader (only for 16-bit signed mono files)
            if (wavFilePath == null || !System.IO.File.Exists(wavFilePath))
            {
                throw new ArgumentException("file not found: " + wavFilePath);
            }
            if (!wavFilePath.EndsWith(".wav"))
            {
                throw new ArgumentException("file not supported: " + wavFilePath);
            }

            byte[] bytes = System.IO.File.ReadAllBytes(wavFilePath);

            return FloatsFromBytesINT16(bytes, skipFirstBytes: 44);
        }
    }
}