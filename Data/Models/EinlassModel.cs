using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models
{
    public class EinlassModel : BaseEntityModel
    {
        public double? HoeheH { get; set; }
        public double? BreiteB { get; set; }
        public double? DurchmesserD { get; set; }
        public double? FlaecheA { get; set; }
        public double? SteuerzeitSZ { get; set; }
        public double? LaengeL { get; set; }
        public double? LuftBedarf { get; set; }

        public virtual VergaserModel Vergaser { get; set; }

        public int MotorId { get; set; }

        [ForeignKey("MotorId")]
        public virtual MotorModel Motor { get; set; }
    }
}