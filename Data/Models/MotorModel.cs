using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Data.Models
{
    public class MotorModel : BaseEntityModel
    {
        [Required]
        public string Name { get; set; }

        public double? HubL { get; set; }
        public double? PleulL { get; set; }
        public double? KolbenG { get; set; }
        public double? DeachsierungL { get; set; }
        public double? BohrungD { get; set; }
        public double? ResonanzU { get; set; }
        public double? HubraumV { get; set; }
        public double? BrennraumV { get; set; }
        public double? KurbelgehaeuseV { get; set; }
        public double? VerdichtungV { get; set; }
        public double? ZylinderAnz { get; set; }
        public double? Zuendzeitpunkt { get; set; }
        public double? HeizwertU { get; set; }

        [Required]
        public virtual EinlassModel Einlass { get; set; }

        [Required]
        public virtual AuslassModel Auslass { get; set; }

        [Required]
        public virtual UeberstroemerModel Ueberstroemer { get; set; }

        public virtual IList<VehiclesModel> Vehicles { get; set; }
    }
}