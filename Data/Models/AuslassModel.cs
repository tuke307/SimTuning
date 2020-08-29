// project=Data, file=AuslassModel.cs, creation=2020:6:28 Copyright (c) 2020 tuke
// productions. All rights reserved.
namespace Data.Models
{
    using Microsoft.EntityFrameworkCore.Storage;
    using System;
    using System.ComponentModel.DataAnnotations.Schema;
    using UnitsNet.Units;

    /// <summary>
    /// AuslassModel.
    /// </summary>
    /// <seealso cref="Data.Models.BaseEntityModel" />
    public class AuslassModel : BaseEntityModel
    {
        //[NotMapped]
        //private LengthUnit? _DurchmesserDUnit;

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
        public static UnitsNet.Units.LengthUnit FlaecheABaseUnit { get => UnitsNet.Units.LengthUnit.Millimeter; }

        /// <summary>
        /// Gets the hoehe h base unit.
        /// </summary>
        /// <value>The hoehe h base unit.</value>
        [NotMapped]
        public static UnitsNet.Units.LengthUnit HoeheHBaseUnit { get => UnitsNet.Units.LengthUnit.Millimeter; }

        /// <summary>
        /// Gets the laenge l base unit.
        /// </summary>
        /// <value>The laenge l base unit.</value>
        [NotMapped]
        public static UnitsNet.Units.LengthUnit LaengeLBaseUnit { get => UnitsNet.Units.LengthUnit.Millimeter; }

        /// <summary>
        /// Gets or sets the auspuff.
        /// </summary>
        /// <value>The auspuff.</value>
        public virtual AuspuffModel Auspuff { get; set; }

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
        public UnitsNet.Units.LengthUnit? BreiteBUnit { get; set; }

        //{
        //    get => this._BreiteBUnit ?? BreiteBBaseUnit;
        //    set
        //    {
        //        UnitsNet.UnitConverter.TryConvert(
        //        this.BreiteB.Value,
        //        this._BreiteBUnit,
        //        value,
        //        out double convertedValue);

        // if (UnitSettings.Default.RoundOnUnitChange) { this.BreiteB =
        // Math.Round(convertedValue, UnitSettings.Default.RoundingAccuracy); } else {
        // this.BreiteB = convertedValue; }

        //        this._BreiteBUnit = value;
        //    }
        //}

        /// <summary>
        /// Gets or sets the durchmesser d.
        /// </summary>
        /// <value>The durchmesser d.</value>
        public double? DurchmesserD { get; set; }

        /// <summary>
        /// Gets or sets the durchmesser d unit.
        /// </summary>
        /// <value>The durchmesser d unit.</value>
        [NotMapped]
        public UnitsNet.Units.LengthUnit? DurchmesserDUnit { get; set; }

        //{
        //get => this._DurchmesserDUnit ?? DurchmesserDBaseUnit;
        //set
        //{
        //    UnitsNet.UnitConverter.TryConvert(
        //    this.DurchmesserD.Value,
        //    this._BreiteBUnit,
        //    value,
        //    out double convertedValue);

        // if (UnitSettings.Default.RoundOnUnitChange) { this.DurchmesserD =
        // Math.Round(convertedValue, UnitSettings.Default.RoundingAccuracy); } else {
        // this.DurchmesserD = convertedValue; }

        //    this._DurchmesserDUnit = value;
        //}
        //}

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
        /// Gets or sets the breite b unit.
        /// </summary>
        /// <value>The breite b unit.</value>
        //[NotMapped]
        //private UnitsNet.Units.LengthUnit? _BreiteBUnit { get; set; }
    }
}