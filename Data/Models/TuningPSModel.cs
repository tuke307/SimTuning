namespace Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class TuningPSModel : BaseEntityModel
    {
        [Required]
        public double X { get; set; }

        [Required]
        public double Y { get; set; }

        public virtual TuningModel Tuning { get; set; }
    }
}