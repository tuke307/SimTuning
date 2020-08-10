﻿// project=Data, file=EnvironmentModel.cs, creation=2020:6:28
// Copyright (c) 2020 tuke productions. All rights reserved.
namespace Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// UmgebungsModel.
    /// </summary>
    /// <seealso cref="Data.Models.BaseEntityModel" />
    public class EnvironmentModel : BaseEntityModel
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
        /// Gets or sets the temperatur t.
        /// </summary>
        /// <value>
        /// The temperatur t.
        /// </value>
        public double? TemperaturT { get; set; }

        /// <summary>
        /// Gets or sets the luftdruck p.
        /// </summary>
        /// <value>
        /// The luftdruck p.
        /// </value>
        public double? LuftdruckP { get; set; }

        /// <summary>
        /// Gets or sets the dyno.
        /// </summary>
        /// <value>
        /// The dyno.
        /// </value>
        public virtual IList<DynoModel> Dyno { get; set; }
    }
}