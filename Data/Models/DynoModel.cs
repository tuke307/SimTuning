// project=Data, file=DynoModel.cs, creation=2020:6:28
// Copyright (c) 2020 tuke productions. All rights reserved.
namespace Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// DynoModel.
    /// </summary>
    /// <seealso cref="Data.Models.BaseEntityModel" />
    public class DynoModel : BaseEntityModel
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
        /// Gets or sets the active.
        /// </summary>
        /// <value>
        /// The active.
        /// </value>
        public bool? Active { get; set; }

        /// <summary>
        /// Gets or sets the vehicle identifier.
        /// </summary>
        /// <value>
        /// The vehicle identifier.
        /// </value>
        public int VehicleId { get; set; }

        /// <summary>
        /// Gets or sets the vehicle.
        /// </summary>
        /// <value>
        /// The vehicle.
        /// </value>
        [ForeignKey("VehicleId")]
        public virtual VehiclesModel Vehicle { get; set; }

        /// <summary>
        /// Gets or sets the environment.
        /// </summary>
        /// <value>
        /// The environment.
        /// </value>
        public virtual EnvironmentModel Environment { get; set; }

        /// <summary>
        /// Gets or sets the audio.
        /// </summary>
        /// <value>
        /// The audio.
        /// </value>
        public virtual IList<DynoAudioModel> Audio { get; set; }

        /// <summary>
        /// Gets or sets the dyno nm.
        /// </summary>
        /// <value>
        /// The dyno nm.
        /// </value>
        public virtual IList<DynoNmModel> DynoNm { get; set; }

        /// <summary>
        /// Gets or sets the dyno ps.
        /// </summary>
        /// <value>
        /// The dyno ps.
        /// </value>
        public virtual IList<DynoPSModel> DynoPS { get; set; }
    }
}