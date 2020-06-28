using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Data.Models
{
    public class VergaserModel : BaseEntityModel
    {
        public double? DurchmesserD { get; set; }
        public double? BenzinLuftF { get; set; }

        public int EinlassId { get; set; }

        [ForeignKey("EinlassId")]
        public virtual EinlassModel Einlass { get; set; }
    }
}
