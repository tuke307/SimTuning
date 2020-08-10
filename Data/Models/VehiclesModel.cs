﻿// project=Data, file=VehiclesModel.cs, creation=2020:6:28
// Copyright (c) 2020 tuke productions. All rights reserved.
namespace Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// FahrzeugModel.
    /// </summary>
    /// <seealso cref="Data.Models.BaseEntityModel" />
    public class VehiclesModel : BaseEntityModel
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the beschreibung.
        /// </summary>
        /// <value>
        /// The beschreibung.
        /// </value>
        public string Beschreibung { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="VehiclesModel"/> is deletable.
        /// </summary>
        /// <value>
        ///   <c>true</c> if deletable; otherwise, <c>false</c>.
        /// </value>
        [Required]
        public bool Deletable { get; set; }

        /// <summary>
        /// Gets or sets the uebersetzung.
        /// </summary>
        /// <value>
        /// The uebersetzung.
        /// </value>
        public double? Uebersetzung { get; set; }

        /// <summary>
        /// Gets or sets the gewicht.
        /// </summary>
        /// <value>
        /// The gewicht.
        /// </value>
        public double? Gewicht { get; set; }

        /// <summary>
        /// Gets or sets the front a.
        /// </summary>
        /// <value>
        /// The front a.
        /// </value>
        public double? FrontA { get; set; }

        /// <summary>
        /// Gets or sets the cw.
        /// </summary>
        /// <value>
        /// The cw.
        /// </value>
        public double? Cw { get; set; }

        // public double? Ansaugleitungslaenge { get; set; }

        /// <summary>
        /// Gets or sets the motor identifier.
        /// </summary>
        /// <value>
        /// The motor identifier.
        /// </value>
        public int? MotorId { get; set; }

        /// <summary>
        /// Gets or sets the motor.
        /// </summary>
        /// <value>
        /// The motor.
        /// </value>
        [ForeignKey("MotorId")]
        public virtual MotorModel Motor { get; set; }

        /// <summary>
        /// Gets or sets the tuning.
        /// </summary>
        /// <value>
        /// The tuning.
        /// </value>
        public virtual TuningModel Tuning { get; set; }

        /// <summary>
        /// Gets or sets the dyno.
        /// </summary>
        /// <value>
        /// The dyno.
        /// </value>
        public virtual DynoModel Dyno { get; set; }
    }
}