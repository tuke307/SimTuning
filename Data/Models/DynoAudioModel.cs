﻿// project=Data, file=DynoAudioModel.cs, creation=2020:6:28
// Copyright (c) 2020 tuke productions. All rights reserved.
namespace Data.Models
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// DynoAudio.
    /// </summary>
    /// <seealso cref="Data.Models.BaseEntityModel" />
    public class DynoAudioModel : BaseEntityModel
    {
        /// <summary>
        /// Gets or sets the x.
        /// </summary>
        /// <value>
        /// The x.
        /// </value>
        [Required]
        public double X { get; set; }

        /// <summary>
        /// Gets or sets the y.
        /// </summary>
        /// <value>
        /// The y.
        /// </value>
        [Required]
        public double Y { get; set; }

        /// <summary>
        /// Gets or sets the dyno.
        /// </summary>
        /// <value>
        /// The dyno.
        /// </value>
        public virtual DynoModel Dyno { get; set; }
    }
}