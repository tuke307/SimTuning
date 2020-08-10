// project=Data, file=MotorModel.cs, creation=2020:6:28
// Copyright (c) 2020 tuke productions. All rights reserved.
namespace Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// MotorModel.
    /// </summary>
    /// <seealso cref="Data.Models.BaseEntityModel" />
    public class MotorModel : BaseEntityModel
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
        /// Gets or sets the hub l.
        /// </summary>
        /// <value>
        /// The hub l.
        /// </value>
        public double? HubL { get; set; }

        /// <summary>
        /// Gets or sets the pleul l.
        /// </summary>
        /// <value>
        /// The pleul l.
        /// </value>
        public double? PleulL { get; set; }

        /// <summary>
        /// Gets or sets the kolben g.
        /// </summary>
        /// <value>
        /// The kolben g.
        /// </value>
        public double? KolbenG { get; set; }

        /// <summary>
        /// Gets or sets the deachsierung l.
        /// </summary>
        /// <value>
        /// The deachsierung l.
        /// </value>
        public double? DeachsierungL { get; set; }

        /// <summary>
        /// Gets or sets the bohrung d.
        /// </summary>
        /// <value>
        /// The bohrung d.
        /// </value>
        public double? BohrungD { get; set; }

        /// <summary>
        /// Gets or sets the resonanz u.
        /// </summary>
        /// <value>
        /// The resonanz u.
        /// </value>
        public double? ResonanzU { get; set; }

        /// <summary>
        /// Gets or sets the hubraum v.
        /// </summary>
        /// <value>
        /// The hubraum v.
        /// </value>
        public double? HubraumV { get; set; }

        /// <summary>
        /// Gets or sets the brennraum v.
        /// </summary>
        /// <value>
        /// The brennraum v.
        /// </value>
        public double? BrennraumV { get; set; }

        /// <summary>
        /// Gets or sets the kurbelgehaeuse v.
        /// </summary>
        /// <value>
        /// The kurbelgehaeuse v.
        /// </value>
        public double? KurbelgehaeuseV { get; set; }

        /// <summary>
        /// Gets or sets the verdichtung v.
        /// </summary>
        /// <value>
        /// The verdichtung v.
        /// </value>
        public double? VerdichtungV { get; set; }

        /// <summary>
        /// Gets or sets the zylinder anz.
        /// </summary>
        /// <value>
        /// The zylinder anz.
        /// </value>
        public double? ZylinderAnz { get; set; }

        /// <summary>
        /// Gets or sets the zuendzeitpunkt.
        /// </summary>
        /// <value>
        /// The zuendzeitpunkt.
        /// </value>
        public double? Zuendzeitpunkt { get; set; }

        /// <summary>
        /// Gets or sets the heizwert u.
        /// </summary>
        /// <value>
        /// The heizwert u.
        /// </value>
        public double? HeizwertU { get; set; }

        /// <summary>
        /// Gets or sets the einlass.
        /// </summary>
        /// <value>
        /// The einlass.
        /// </value>
        [Required]
        public virtual EinlassModel Einlass { get; set; }

        /// <summary>
        /// Gets or sets the auslass.
        /// </summary>
        /// <value>
        /// The auslass.
        /// </value>
        [Required]
        public virtual AuslassModel Auslass { get; set; }

        /// <summary>
        /// Gets or sets the ueberstroemer.
        /// </summary>
        /// <value>
        /// The ueberstroemer.
        /// </value>
        [Required]
        public virtual UeberstroemerModel Ueberstroemer { get; set; }

        /// <summary>
        /// Gets or sets the vehicles.
        /// </summary>
        /// <value>
        /// The vehicles.
        /// </value>
        public virtual IList<VehiclesModel> Vehicles { get; set; }
    }
}