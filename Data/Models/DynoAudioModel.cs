using System.ComponentModel.DataAnnotations;

namespace Data.Models
{
    public class DynoAudioModel : BaseEntityModel
    {
        [Required]
        public double X { get; set; }

        [Required]
        public double Y { get; set; }

        public virtual DynoModel Dyno { get; set; }
    }
}