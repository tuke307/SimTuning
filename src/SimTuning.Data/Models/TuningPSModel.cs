// Copyright (c) 2021 tuke productions. All rights reserved.
namespace SimTuning.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// TuningPS Model.
    /// </summary>
    /// <seealso cref="Data.Models.BaseEntityModel" />
    public class TuningPSModel : BaseEntityModel
    {
        /// <summary>
        /// Gets or sets the tuning.
        /// </summary>
        /// <value>The tuning.</value>
        public virtual TuningModel Tuning { get; set; }

        /// <summary>
        /// Gets or sets the x.
        /// </summary>
        /// <value>The x.</value>
        [Required]
        public double X { get; set; }

        /// <summary>
        /// Gets or sets the y.
        /// </summary>
        /// <value>The y.</value>
        [Required]
        public double Y { get; set; }
    }
}