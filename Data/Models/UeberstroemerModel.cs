using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models
{
    public class UeberstroemerModel : BaseEntityModel
    {
        public double? Hoehe { get; set; }
        public double? Breite { get; set; }
        public double? Flaeche { get; set; }

        public double? SteuerzeitSZ { get; set; }
        public double? Anzahl { get; set; }

        public int MotorId { get; set; }

        [ForeignKey("MotorId")]
        public virtual MotorModel Motor { get; set; }
    }
}