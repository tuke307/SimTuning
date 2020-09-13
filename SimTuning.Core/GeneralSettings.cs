// project=SimTuning.Core, file=Constants.cs, creation=2020:9:7 Copyright (c) 2020 tuke
// productions. All rights reserved.
namespace SimTuning.Core
{
    using Plugin.Settings;
    using Plugin.Settings.Abstractions;
    using System.IO;

    /// <summary>
    /// Allgemeine-SimTuning Konstanten.
    /// </summary>
    public static class GeneralSettings
    {
        /// <summary>
        /// The user authent.
        /// </summary>
        public const string UserAuthent = "UsEr_AuThEnTiCaTiOn_Key_7744";

        /// <summary>
        /// Gets or sets the audio file.
        /// </summary>
        /// <value>The audio file.</value>
        public static string AudioFile
        {
            get
            {
                return AppSettings.GetValueOrDefault(nameof(AudioFile), "DynoAudio.wav");
            }

            set
            {
                AppSettings.AddOrUpdateValue(nameof(AudioFile), value);
            }
        }

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
        public static string FileDirectory
        {
            get
            {
                return AppSettings.GetValueOrDefault(nameof(FileDirectory), null);
            }

            set
            {
                AppSettings.AddOrUpdateValue(nameof(FileDirectory), value);
            }
        }

        private static ISettings AppSettings => CrossSettings.Current;
    }
}