using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models
{
    public class VehiclesModel : BaseEntityModel
    {
        [Required]
        public string Name { get; set; }

        public string Beschreibung { get; set; }

        [Required]
        public bool Deletable { get; set; }

        public double? Uebersetzung { get; set; }
        public double? Gewicht { get; set; }
        public double? FrontA { get; set; }
        public double? Cw { get; set; }
        //public double? Ansaugleitungslaenge { get; set; }

        public int? MotorId { get; set; }

        [ForeignKey("MotorId")]
        public virtual MotorModel Motor { get; set; }

        public virtual TuningModel Tuning { get; set; }
        public virtual DynoModel Dyno { get; set; }
    }
}