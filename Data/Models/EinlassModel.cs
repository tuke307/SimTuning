// project=Data, file=EinlassModel.cs, creation=2020:6:28 Copyright (c) 2020 tuke
// productions. All rights reserved.
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
        /// Gets the breite b base unit.
        /// </summary>
        /// <value>The breite b base unit.</value>
        [NotMapped]
        public static UnitsNet.Units.LengthUnit BreiteBBaseUnit { get => UnitsNet.Units.LengthUnit.Millimeter; }

        /// <summary>
        /// Gets the durchmesser d base unit.
        /// </summary>
        /// <value>The durchmesser d base unit.</value>
        [NotMapped]
        public static UnitsNet.Units.LengthUnit DurchmesserDBaseUnit { get => UnitsNet.Units.LengthUnit.Millimeter; }

        /// <summary>
        /// Gets the flaeche a base unit.
        /// </summary>
        /// <value>The flaeche a base unit.</value>
        [NotMapped]
        public static UnitsNet.Units.AreaUnit FlaecheABaseUnit { get => UnitsNet.Units.AreaUnit.SquareMillimeter; }

        /// <summary>
        /// Gets the hoehe h base unit.
        /// </summary>
        /// <value>The hoehe h base unit.</value>
        [NotMapped]
        public static UnitsNet.Units.LengthUnit HoeheHBaseUnit { get => UnitsNet.Units.LengthUnit.Millimeter; }

        /// <summary>
        /// Gets the laenge base unit.
        /// </summary>
        /// <value>The laenge base unit.</value>
        [NotMapped]
        public static UnitsNet.Units.LengthUnit LaengeBaseUnit { get => UnitsNet.Units.LengthUnit.Millimeter; }

        /// <summary>
        /// Gets or sets the breite b.
        /// </summary>
        /// <value>The breite b.</value>
        public double? BreiteB { get; set; }

        /// <summary>
        /// Gets or sets the breite b unit.
        /// </summary>
        /// <value>The breite b unit.</value>
        [NotMapped]
        public UnitsNet.Units.LengthUnit BreiteBUnit { get; set; }

        /// <summary>
        /// Gets or sets the durchmesser d.
        /// </summary>
        /// <value>The durchmesser d.</value>
        public double? DurchmesserD { get; set; }

        [NotMapped]
        public UnitsNet.Units.LengthUnit DurchmesserDUnit { get; set; }

        /// <summary>
        /// Gets or sets the flaeche a.
        /// </summary>
        /// <value>The flaeche a.</value>
        public double? FlaecheA { get; set; }

        /// <summary>
        /// Gets or sets the flaeche a unit.
        /// </summary>
        /// <value>The flaeche a unit.</value>
        [NotMapped]
        public UnitsNet.Units.AreaUnit FlaecheAUnit { get; set; }

        /// <summary>
        /// Gets or sets the hoehe h.
        /// </summary>
        /// <value>The hoehe h.</value>
        public double? HoeheH { get; set; }

        /// <summary>
        /// Gets or sets the hoehe h unit.
        /// </summary>
        /// <value>The hoehe h unit.</value>
        [NotMapped]
        public UnitsNet.Units.LengthUnit HoeheHUnit { get; set; }

        /// <summary>
        /// Gets or sets the laenge l.
        /// </summary>
        /// <value>The laenge l.</value>
        public double? LaengeL { get; set; }

        /// <summary>
        /// Gets or sets the laenge l unit.
        /// </summary>
        /// <value>The laenge l unit.</value>
        [NotMapped]
        public UnitsNet.Units.LengthUnit LaengeLUnit { get; set; }

        /// <summary>
        /// Gets or sets the luft bedarf.
        /// </summary>
        /// <value>The luft bedarf.</value>
        public double? LuftBedarf { get; set; }

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

        /// <summary>
        /// Gets or sets the vergaser.
        /// </summary>
        /// <value>The vergaser.</value>
        public virtual VergaserModel Vergaser { get; set; }
    }
}