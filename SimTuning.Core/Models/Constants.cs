// project=SimTuning.Core, file=Constants.cs, creation=2020:9:7 Copyright (c) 2020 tuke
// productions. All rights reserved.
namespace SimTuning.Core
{
    using System;
    using System.IO;

    /// <summary>
    /// Allgemeine-SimTuning Konstanten.
    /// </summary>
    public static class Constants
    {
        /// <summary>
        /// The audio file.
        /// </summary>
        public static readonly string AudioFile = "DynoAudio.wav";

        /// <summary>
        /// for Encryption.
        /// </summary>
        public static readonly string user_authent = "UsEr_AuThEnTiCaTiOn_Key_7744";

        /// <summary>
        /// Gets the audio file path.
        /// </summary>
        /// <value>The audio file path.</value>
        public static string AudioFilePath
        {
            get
            {
                return Path.Combine(FileDirectory, AudioFile);
            }
        }

        /// <summary>
        /// Gets or sets the file directory.
        /// </summary>
        /// <value>The file directory.</value>
        public static string FileDirectory { get; set; }
    }
}