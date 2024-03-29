﻿// Copyright (c) 2021 tuke productions. All rights reserved.
namespace SimTuning.Data
{
    using Plugin.Settings;
    using Plugin.Settings.Abstractions;
    using System;
    using System.IO;
    using Xamarin.Essentials;

    /// <summary>
    /// Konstanten für die Datenbank-Abwicklung.
    /// </summary>
    public static class DatabaseSettings
    {
        /// <summary>
        /// The database name.
        /// </summary>
        public const string DatabaseName = "simtuning.db";

        private static string _databasePath;
        private static string _fileDirectory;

        /// <summary>
        /// Gets or sets the database path.
        /// </summary>
        /// <value>The database path.</value>
        public static string DatabasePath
        {
            get
            {
                if (string.IsNullOrEmpty(_databasePath))
                {
                    _databasePath = Path.Combine(FileDirectory, DatabaseName);
                    DatabasePath = _databasePath;
                }

                return AppSettings.GetValueOrDefault(nameof(DatabasePath), _databasePath);
            }
            set
            {
                AppSettings.AddOrUpdateValue(nameof(DatabasePath), value);
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
                if (string.IsNullOrEmpty(_fileDirectory))
                {
                    if (DeviceInfo.Idiom == DeviceIdiom.Phone)
                    {
                        // android: "/data/user/0/com.tuke_productions.SimTuning/files/"
                        _fileDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                    }
                    else
                    {
                        // AppData\Roaming\SimTuning
                        _fileDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "SimTuning");
                    }

                    // ist evtl. anfangs nicht da
                    if (!Directory.Exists(_fileDirectory))
                    {
                        Directory.CreateDirectory(_fileDirectory);
                    }

                    FileDirectory = _fileDirectory;
                }

                return AppSettings.GetValueOrDefault(nameof(FileDirectory), _fileDirectory);
            }

            set
            {
                AppSettings.AddOrUpdateValue(nameof(FileDirectory), value);
            }
        }

        private static ISettings AppSettings => CrossSettings.Current;
    }
}