// project=Data, file=TuningModel.cs, creation=2020:6:28 Copyright (c) 2021 tuke
// productions. All rights reserved.
namespace Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// TuningModel.
    /// </summary>
    /// <seealso cref="Data.Models.BaseEntityModel" />
    public class TuningModel : BaseEntityModel
    {
        /// <summary>
        /// Gets or sets the active.
        /// </summary>
        /// <value>The active.</value>
        public bool? Active { get; set; }

        /// <summary>
        /// Gets or sets the beschreibung.
        /// </summary>
        /// <value>The beschreibung.</value>
        public string Beschreibung { get; set; }

        /// <summary>
        /// Gets or sets the diagramme u.
        /// </summary>
        /// <value>The diagramme u.</value>
        public double? DiagrammeU { get; set; }

        /// <summary>
        /// Gets or sets the environment.
        /// </summary>
        /// <value>The environment.</value>
        public virtual EnvironmentModel Environment { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the tuning.
        /// </summary>
        /// <value>The tuning.</value>
        public virtual IList<TuningPSModel> Tuning { get; set; }

        /// <summary>
        /// Gets or sets the vehicle.
        /// </summary>
        /// <value>The vehicle.</value>
        [ForeignKey("VehicleId")]
        public virtual VehiclesModel Vehicle { get; set; }

        /// <summary>
        /// Gets or sets the vehicle identifier.
        /// </summary>
        /// <value>The vehicle identifier.</value>
        public int VehicleId { get; set; }
    }
}