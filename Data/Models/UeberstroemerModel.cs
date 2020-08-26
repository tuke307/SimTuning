// project=Data, file=UeberstroemerModel.cs, creation=2020:6:28 Copyright (c) 2020 tuke
// productions. All rights reserved.
namespace Data.Models
{
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// ÜberströmerModel.
    /// </summary>
    /// <seealso cref="Data.Models.BaseEntityModel" />
    public class UeberstroemerModel : BaseEntityModel
    {
        /// <summary>
        /// Gets or sets the anzahl.
        /// </summary>
        /// <value>The anzahl.</value>
        public double? Anzahl { get; set; }

        /// <summary>
        /// Gets or sets the breite.
        /// </summary>
        /// <value>The breite.</value>
        public double? BreiteB { get; set; }

        /// <summary>
        /// Gets or sets the breite b unit.
        /// </summary>
        /// <value>The breite b unit.</value>
        [NotMapped]
        public UnitsNet.Units.LengthUnit BreiteBUnit { get; set; }

        /// <summary>
        /// Gets or sets the flaeche.
        /// </summary>
        /// <value>The flaeche.</value>
        public double? FlaecheA { get; set; }

        /// <summary>
        /// Gets or sets the flaeche a unit.
        /// </summary>
        /// <value>The flaeche a unit.</value>
        [NotMapped]
        public UnitsNet.Units.AreaUnit FlaecheAUnit { get; set; }

        /// <summary>
        /// Gets or sets the hoehe.
        /// </summary>
        /// <value>The hoehe.</value>
        public double? HoeheH { get; set; }

        /// <summary>
        /// Gets or sets the hoehe h unit.
        /// </summary>
        /// <value>The hoehe h unit.</value>
        [NotMapped]
        public UnitsNet.Units.LengthUnit HoeheHUnit { get; set; }

        /// <summary>
        /// Gets or sets the motor.
        /// </summary>
        /// <value>The motor.</value>
        [ForeignKey("MotorId")]
        public virtual MotorModel Motor { get; set; }

        /// <summary>
        /// Gets or sets the motor identifier.
        /// </summary>
        /// <value>The motor identifier.</value>
        public int MotorId { get; set; }

        /// <summary>
        /// Gets or sets the steuerzeit sz.
        /// </summary>
        /// <value>The steuerzeit sz.</value>
        public double? SteuerzeitSZ { get; set; }
    }
}