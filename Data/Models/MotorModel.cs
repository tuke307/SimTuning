﻿// project=Data, file=MotorModel.cs, creation=2020:6:28 Copyright (c) 2020 tuke
// productions. All rights reserved.
namespace Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// MotorModel.
    /// </summary>
    /// <seealso cref="Data.Models.BaseEntityModel" />
    public class MotorModel : BaseEntityModel
    {
        /// <summary>
        /// Gets the bohrung d base unit.
        /// </summary>
        /// <value>The bohrung d base unit.</value>
        [NotMapped]
        public static UnitsNet.Units.LengthUnit BohrungDBaseUnit { get => UnitsNet.Units.LengthUnit.Millimeter; }

        /// <summary>
        /// Gets the brennraum v base unit.
        /// </summary>
        /// <value>The brennraum v base unit.</value>
        [NotMapped]
        public static UnitsNet.Units.VolumeUnit BrennraumVBaseUnit { get => UnitsNet.Units.VolumeUnit.CubicMillimeter; }

        /// <summary>
        /// Gets the deachsierung l base unit.
        /// </summary>
        /// <value>The deachsierung l base unit.</value>
        [NotMapped]
        public static UnitsNet.Units.LengthUnit DeachsierungLBaseUnit { get => UnitsNet.Units.LengthUnit.Millimeter; }

        /// <summary>
        /// Gets the hub l base unit.
        /// </summary>
        /// <value>The hub l base unit.</value>
        [NotMapped]
        public static UnitsNet.Units.LengthUnit HubLBaseUnit { get => UnitsNet.Units.LengthUnit.Millimeter; }

        /// <summary>
        /// Gets the hubraum v base unit.
        /// </summary>
        /// <value>The hubraum v base unit.</value>
        [NotMapped]
        public static UnitsNet.Units.VolumeUnit HubraumVBaseUnit { get => UnitsNet.Units.VolumeUnit.CubicMillimeter; }

        /// <summary>
        /// Gets the kolben g base unit.
        /// </summary>
        /// <value>The kolben g base unit.</value>
        [NotMapped]
        public static UnitsNet.Units.SpeedUnit KolbenGBaseUnit { get => UnitsNet.Units.SpeedUnit.MeterPerSecond; }

        /// <summary>
        /// Gets the kurbelgehaeuse v base unit.
        /// </summary>
        /// <value>The kurbelgehaeuse v base unit.</value>
        [NotMapped]
        public static UnitsNet.Units.VolumeUnit KurbelgehaeuseVBaseUnit { get => UnitsNet.Units.VolumeUnit.CubicMillimeter; }

        /// <summary>
        /// Gets the pleul l base unit.
        /// </summary>
        /// <value>The pleul l base unit.</value>
        [NotMapped]
        public static UnitsNet.Units.LengthUnit PleulLBaseUnit { get => UnitsNet.Units.LengthUnit.Millimeter; }

        /// <summary>
        /// Gets or sets the auslass.
        /// </summary>
        /// <value>The auslass.</value>
        [Required]
        public virtual AuslassModel Auslass { get; set; }

        /// <summary>
        /// Gets or sets the bohrung d.
        /// </summary>
        /// <value>The bohrung d.</value>
        public double? BohrungD { get; set; }

        /// <summary>
        /// Gets or sets the bohrung d unit.
        /// </summary>
        /// <value>The bohrung d unit.</value>
        [NotMapped]
        public UnitsNet.Units.LengthUnit BohrungDUnit { get; set; }

        /// <summary>
        /// Gets or sets the brennraum v.
        /// </summary>
        /// <value>The brennraum v.</value>
        public double? BrennraumV { get; set; }

        /// <summary>
        /// Gets or sets the brennraum v unit.
        /// </summary>
        /// <value>The brennraum v unit.</value>
        [NotMapped]
        public UnitsNet.Units.VolumeUnit BrennraumVUnit { get; set; }

        /// <summary>
        /// Gets or sets the deachsierung l.
        /// </summary>
        /// <value>The deachsierung l.</value>
        public double? DeachsierungL { get; set; }

        /// <summary>
        /// Gets or sets the deachsierung l unit.
        /// </summary>
        /// <value>The deachsierung l unit.</value>
        [NotMapped]
        public UnitsNet.Units.LengthUnit DeachsierungLUnit { get; set; }

        /// <summary>
        /// Gets or sets the einlass.
        /// </summary>
        /// <value>The einlass.</value>
        [Required]
        public virtual EinlassModel Einlass { get; set; }

        /// <summary>
        /// Gets or sets the heizwert u.
        /// </summary>
        /// <value>The heizwert u.</value>
        public double? HeizwertU { get; set; }

        /// <summary>
        /// Gets or sets the hub l.
        /// </summary>
        /// <value>The hub l.</value>
        public double? HubL { get; set; }

        /// <summary>
        /// Gets or sets the hub l unit.
        /// </summary>
        /// <value>The hub l unit.</value>
        [NotMapped]
        public UnitsNet.Units.LengthUnit HubLUnit { get; set; }

        /// <summary>
        /// Gets or sets the hubraum v.
        /// </summary>
        /// <value>The hubraum v.</value>
        public double? HubraumV { get; set; }

        /// <summary>
        /// Gets or sets the hubraum v unit.
        /// </summary>
        /// <value>The hubraum v unit.</value>
        [NotMapped]
        public UnitsNet.Units.VolumeUnit HubraumVUnit { get; set; }

        /// <summary>
        /// Gets or sets the kolben g.
        /// </summary>
        /// <value>The kolben g.</value>
        public double? KolbenG { get; set; }

        /// <summary>
        /// Gets or sets the kolben g unit.
        /// </summary>
        /// <value>The kolben g unit.</value>
        [NotMapped]
        public UnitsNet.Units.SpeedUnit KolbenGUnit { get; set; }

        /// <summary>
        /// Gets or sets the kurbelgehaeuse v.
        /// </summary>
        /// <value>The kurbelgehaeuse v.</value>
        public double? KurbelgehaeuseV { get; set; }

        /// <summary>
        /// Gets or sets the kurbelgehaeuse v unit.
        /// </summary>
        /// <value>The kurbelgehaeuse v unit.</value>
        [NotMapped]
        public UnitsNet.Units.VolumeUnit KurbelgehaeuseVUnit { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the pleul l.
        /// </summary>
        /// <value>The pleul l.</value>
        public double? PleulL { get; set; }

        /// <summary>
        /// Gets or sets the pleul l unit.
        /// </summary>
        /// <value>The pleul l unit.</value>
        [NotMapped]
        public UnitsNet.Units.LengthUnit PleulLUnit { get; set; }

        /// <summary>
        /// Gets or sets the resonanz u.
        /// </summary>
        /// <value>The resonanz u.</value>
        public double? ResonanzU { get; set; }

        /// <summary>
        /// Gets or sets the ueberstroemer.
        /// </summary>
        /// <value>The ueberstroemer.</value>
        [Required]
        public virtual UeberstroemerModel Ueberstroemer { get; set; }

        /// <summary>
        /// Gets or sets the vehicles.
        /// </summary>
        /// <value>The vehicles.</value>
        public virtual IList<VehiclesModel> Vehicles { get; set; }

        /// <summary>
        /// Gets or sets the verdichtung v.
        /// </summary>
        /// <value>The verdichtung v.</value>
        public double? VerdichtungV { get; set; }

        /// <summary>
        /// Gets or sets the zuendzeitpunkt.
        /// </summary>
        /// <value>The zuendzeitpunkt.</value>
        public double? Zuendzeitpunkt { get; set; }

        /// <summary>
        /// Gets or sets the zylinder anz.
        /// </summary>
        /// <value>The zylinder anz.</value>
        public double? ZylinderAnz { get; set; }
    }
}