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
        /// Gets or sets the data export archive.
        /// </summary>
        /// <value>The data export archive.</value>
        public static string DataExportArchive
        {
            get
            {
                return AppSettings.GetValueOrDefault(nameof(DataExportArchive), "DataExport.zip");
            }

            set
            {
                AppSettings.AddOrUpdateValue(nameof(DataExportArchive), value);
            }
        }

        /// <summary>
        /// Gets the data export archive path.
        /// </summary>
        /// <value>The data export archive path.</value>
        public static string DataExportArchivePath
        {
            get
            {
                return Path.Combine(FileDirectory, DataExportArchive);
            }
        }

        /// <summary>
        /// Gets or sets the data export file.
        /// </summary>
        /// <value>The data export file.</value>
        public static string DataExportFile
        {
            get
            {
                return AppSettings.GetValueOrDefault(nameof(DataExportFile), "DataExport.json");
            }

            set
            {
                AppSettings.AddOrUpdateValue(nameof(DataExportFile), value);
            }
        }

        /// <summary>
        /// Gets the data export file path.
        /// </summary>
        /// <value>The data export file path.</value>
        public static string DataExportFilePath
        {
            get
            {
                return Path.Combine(FileDirectory, DataExportFile);
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

        /// <summary>
        /// Gets or sets the release notes file.
        /// </summary>
        /// <value>The release notes file.</value>
        public static string ReleaseNotesFile
        {
            get
            {
                return AppSettings.GetValueOrDefault(nameof(ReleaseNotesFile), "releasenotes.txt");
            }

            set
            {
                AppSettings.AddOrUpdateValue(nameof(ReleaseNotesFile), value);
            }
        }

        /// <summary>
        /// Gets the release notes file path.
        /// </summary>
        /// <value>The release notes file path.</value>
        public static string ReleaseNotesFilePath
        {
            get
            {
                return Path.Combine(FileDirectory, ReleaseNotesFile);
            }
        }

        private static ISettings AppSettings => CrossSettings.Current;
    }
}