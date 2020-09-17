// project=Data, file=DynoAudioModel.cs, creation=2020:6:28 Copyright (c) 2020 tuke
// productions. All rights reserved.
namespace Data.Models
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// DynoAudio.
    /// </summary>
    /// <seealso cref="Data.Models.BaseEntityModel" />
    public class DrehzahlModel : BaseEntityModel
    {
        /// <summary>
        /// Gets or sets the drehzahl.
        /// </summary>
        /// <value>The drehzahl.</value>
        [Required]
        public double Drehzahl { get; set; }

        /// <summary>
        /// Gets or sets the dyno.
        /// </summary>
        /// <value>The dyno.</value>
        public virtual DynoModel Dyno { get; set; }

        /// <summary>
        /// Gets or sets the zeit.
        /// </summary>
        /// <value>The zeit.</value>
        [Required]
        public double Zeit { get; set; }
    }
}