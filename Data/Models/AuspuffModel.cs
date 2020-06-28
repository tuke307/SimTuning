using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Data.Models
{
    public class AuspuffModel : BaseEntityModel
    {
        public double? KruemmerF { get; set; }
        public double? KruemmerW { get; set; }
        public double? KruemmerL { get; set; }
        public double? KruemmerD { get; set; }
        public int DiffusorStage { get; set; }
        public double? DiffusorW { get; set; }
        public double? DiffusorW1 { get; set; }
        public double? DiffusorW2 { get; set; }
        public double? DiffusorW3 { get; set; } 
        public double? DiffusorL { get; set; }
        public double? DiffusorD { get; set; }
        public double? DiffusorL1 { get; set; }
        public double? DiffusorD1 { get; set; }
        public double? DiffusorL2 { get; set; }
        public double? DiffusorD2 { get; set; }
        public double? DiffusorL3 { get; set; }
        public double? DiffusorD3 { get; set; }
        public double? MittelteilF { get; set; }
        public double? MittelteilL { get; set; }
        public double? MittelteilD { get; set; }
        public double? GegenKonusW { get; set; }
        public double? GegenkonusL { get; set; }
        public double? GegenkonusD { get; set; }
        public double? EndrohrL { get; set; }
        public double? EndrohrD { get; set; }
        public double? GesamtL { get; set; }
        public double? ResonanzL { get; set; }
        public double? AbgasT { get; set; }
        public double? AbgasV { get; set; }

        public int AuslassId { get; set; }

        [ForeignKey("AuslassId")]
        public virtual AuslassModel Auslass { get; set; }
    }
}
