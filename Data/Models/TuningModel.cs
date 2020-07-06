using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models
{
    public class TuningModel : BaseEntityModel
    {
        [Required]
        public string Name { get; set; }

        public string Beschreibung { get; set; }

        public bool? Active { get; set; }
        public double? DiagrammeU { get; set; }

        public int VehicleId { get; set; }

        [ForeignKey("VehicleId")]
        public virtual VehiclesModel Vehicle { get; set; }

        public virtual EnvironmentModel Environment { get; set; }

        public virtual IList<TuningPSModel> Tuning { get; set; }
    }
}