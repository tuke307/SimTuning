using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SimTuning.Test
{
    public static class Constants
    {
        public static readonly string DynoAudioFile = @"C:\Users\Tony\AppData\Roaming\SimTuning\DynoAudio.wav";
        private static readonly string DirectoryName = "UnitTests";

        public static string Directory
        {
            get
            {
                return Path.Combine(RootDirectory, DirectoryName);
            }
        }

        public static string RootDirectory
        {
            get
            {
                return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "SimTuning"); // appdata-local-simtuning
            }
        }
    }
}