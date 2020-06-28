using Plugin.DeviceInfo.Abstractions;
using System;
using System.IO;

namespace SimTuning
{
    public class Constants
    {
        //Encryption
        public static readonly string user_authent = "UsEr_AuThEnTiCaTiOn_Key_7744";

        public static Platform Platform { get; set; }

        public static readonly string AudioFile = "DynoAudio.wav";

        public static string FileDirectory
        {
            get
            {
                switch (Constants.Platform)
                {
                    case Plugin.DeviceInfo.Abstractions.Platform.Windows:
                        return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "SimTuning"); //appdata-local-simtunig

                    case Plugin.DeviceInfo.Abstractions.Platform.iOS:
                        return Environment.GetFolderPath(Environment.SpecialFolder.Personal); //interner speicher

                    case Plugin.DeviceInfo.Abstractions.Platform.Android:
                        return Environment.GetFolderPath(Environment.SpecialFolder.Personal); //interner speicher

                    default:
                        return null;
                }
            }
        }

        public static string AudioFilePath
        {
            get
            {
                return Path.Combine(FileDirectory, AudioFile);
            }
        }
    }
}