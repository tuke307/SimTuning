// project=Data, file=VergaserModel.cs, creation=2020:6:28
// Copyright (c) 2020 tuke productions. All rights reserved.
namespace Data.Models
{
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// VergaserModel.
    /// </summary>
    /// <seealso cref="Data.Models.BaseEntityModel" />
    public class VergaserModel : BaseEntityModel
    {
        /// <summary>
        /// Gets or sets the durchmesser d.
        /// </summary>
        /// <value>
        /// The durchmesser d.
        /// </value>
        public double? DurchmesserD { get; set; }

        /// <summary>
        /// Gets or sets the benzin luft f.
        /// </summary>
        /// <value>
        /// The benzin luft f.
        /// </value>
        public double? BenzinLuftF { get; set; }

        /// <summary>
        /// Gets or sets the einlass identifier.
        /// </summary>
        /// <value>
        /// The einlass identifier.
        /// </value>
        public int EinlassId { get; set; }

        /// <summary>
        /// Gets or sets the einlass.
        /// </summary>
        /// <value>
        /// The einlass.
        /// </value>
        [ForeignKey("EinlassId")]
        public virtual EinlassModel Einlass { get; set; }
    }
}