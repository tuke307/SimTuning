// project=Data, file=Constants.cs, creation=2020:6:28
// Copyright (c) 2020 tuke productions. All rights reserved.
namespace Data
{
    /// <summary>
    /// Konstanten für die Datenbank-Abwicklung.
    /// </summary>
    public static class Constants
    {
        /// <summary>
        /// The database name.
        /// </summary>
        public const string DatabaseName = "simtuning.db";

        /// <summary>
        /// Gets or sets the database path.
        /// </summary>
        /// <value>
        /// The database path.
        /// </value>
        public static string DatabasePath { get; set; }
    }
}