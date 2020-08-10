// project=SimTuning.Core, file=Constants.cs, creation=2020:7:31
// Copyright (c) 2020 tuke productions. All rights reserved.
namespace SimTuning.Core
{
    using System;
    using System.IO;
    using Plugin.DeviceInfo.Abstractions;

    /// <summary>
    /// Allgemeine-SimTuning Konstanten.
    /// </summary>
    public static class Constants
    {
        /// <summary>
        /// for Encryption.
        /// </summary>
        public static readonly string user_authent = "UsEr_AuThEnTiCaTiOn_Key_7744";

        /// <summary>
        /// Gets or sets the platform.
        /// </summary>
        /// <value>
        /// The platform.
        /// </value>
        public static Platform Platform { get; set; }

        /// <summary>
        /// The audio file.
        /// </summary>
        public static readonly string AudioFile = "DynoAudio.wav";

        /// <summary>
        /// Gets the file directory.
        /// </summary>
        /// <value>
        /// The file directory.
        /// </value>
        public static string FileDirectory
        {
            get
            {
                switch (Constants.Platform)
                {
                    case Plugin.DeviceInfo.Abstractions.Platform.Windows:
                        return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "SimTuning"); // appdata-local-simtunig

                    case Plugin.DeviceInfo.Abstractions.Platform.iOS:
                        return Environment.GetFolderPath(Environment.SpecialFolder.Personal); // interner speicher

                    case Plugin.DeviceInfo.Abstractions.Platform.Android:
                        return Environment.GetFolderPath(Environment.SpecialFolder.Personal); // interner speicher

                    default:
                        return null;
                }
            }
        }

        /// <summary>
        /// Gets the audio file path.
        /// </summary>
        /// <value>
        /// The audio file path.
        /// </value>
        public static string AudioFilePath
        {
            get
            {
                return Path.Combine(FileDirectory, AudioFile);
            }
        }
    }
}