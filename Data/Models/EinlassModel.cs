﻿// project=Data, file=EinlassModel.cs, creation=2020:6:28
// Copyright (c) 2020 tuke productions. All rights reserved.
namespace Data.Models
{
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// EinlassModel.
    /// </summary>
    /// <seealso cref="Data.Models.BaseEntityModel" />
    public class EinlassModel : BaseEntityModel
    {
        /// <summary>
        /// Gets or sets the hoehe h.
        /// </summary>
        /// <value>
        /// The hoehe h.
        /// </value>
        public double? HoeheH { get; set; }

        /// <summary>
        /// Gets or sets the breite b.
        /// </summary>
        /// <value>
        /// The breite b.
        /// </value>
        public double? BreiteB { get; set; }

        /// <summary>
        /// Gets or sets the durchmesser d.
        /// </summary>
        /// <value>
        /// The durchmesser d.
        /// </value>
        public double? DurchmesserD { get; set; }

        /// <summary>
        /// Gets or sets the flaeche a.
        /// </summary>
        /// <value>
        /// The flaeche a.
        /// </value>
        public double? FlaecheA { get; set; }

        /// <summary>
        /// Gets or sets the steuerzeit sz.
        /// </summary>
        /// <value>
        /// The steuerzeit sz.
        /// </value>
        public double? SteuerzeitSZ { get; set; }

        /// <summary>
        /// Gets or sets the laenge l.
        /// </summary>
        /// <value>
        /// The laenge l.
        /// </value>
        public double? LaengeL { get; set; }

        /// <summary>
        /// Gets or sets the luft bedarf.
        /// </summary>
        /// <value>
        /// The luft bedarf.
        /// </value>
        public double? LuftBedarf { get; set; }

        /// <summary>
        /// Gets or sets the vergaser.
        /// </summary>
        /// <value>
        /// The vergaser.
        /// </value>
        public virtual VergaserModel Vergaser { get; set; }

        /// <summary>
        /// Gets or sets the motor identifier.
        /// </summary>
        /// <value>
        /// The motor identifier.
        /// </value>
        public int MotorId { get; set; }

        /// <summary>
        /// Gets or sets the motor.
        /// </summary>
        /// <value>
        /// The motor.
        /// </value>
        [ForeignKey("MotorId")]
        public virtual MotorModel Motor { get; set; }
    }
}