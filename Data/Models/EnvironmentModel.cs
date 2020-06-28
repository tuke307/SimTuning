using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Data.Models
{
    public class EnvironmentModel : BaseEntityModel
    {
        [Required]
        public string Name { get; set; }

        public double? TemperaturT { get; set; }

        public double? LuftdruckP { get; set; }

        public virtual IList<DynoModel> Dyno { get; set; }
    }
}