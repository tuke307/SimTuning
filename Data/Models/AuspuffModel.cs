// project=Data, file=AuspuffModel.cs, creation=2020:6:28
// Copyright (c) 2020 tuke productions. All rights reserved.
namespace Data.Models
{
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// AuspuffModel.
    /// </summary>
    /// <seealso cref="Data.Models.BaseEntityModel" />
    public class AuspuffModel : BaseEntityModel
    {
        /// <summary>
        /// Gets or sets the kruemmer f.
        /// </summary>
        /// <value>
        /// The kruemmer f.
        /// </value>
        public double? KruemmerF { get; set; }

        /// <summary>
        /// Gets or sets the kruemmer w.
        /// </summary>
        /// <value>
        /// The kruemmer w.
        /// </value>
        public double? KruemmerW { get; set; }

        /// <summary>
        /// Gets or sets the kruemmer l.
        /// </summary>
        /// <value>
        /// The kruemmer l.
        /// </value>
        public double? KruemmerL { get; set; }

        /// <summary>
        /// Gets or sets the kruemmer d.
        /// </summary>
        /// <value>
        /// The kruemmer d.
        /// </value>
        public double? KruemmerD { get; set; }

        /// <summary>
        /// Gets or sets the diffusor stage.
        /// </summary>
        /// <value>
        /// The diffusor stage.
        /// </value>
        public int DiffusorStage { get; set; }

        /// <summary>
        /// Gets or sets the diffusor w.
        /// </summary>
        /// <value>
        /// The diffusor w.
        /// </value>
        public double? DiffusorW { get; set; }

        /// <summary>
        /// Gets or sets the diffusor w1.
        /// </summary>
        /// <value>
        /// The diffusor w1.
        /// </value>
        public double? DiffusorW1 { get; set; }

        /// <summary>
        /// Gets or sets the diffusor w2.
        /// </summary>
        /// <value>
        /// The diffusor w2.
        /// </value>
        public double? DiffusorW2 { get; set; }

        /// <summary>
        /// Gets or sets the diffusor w3.
        /// </summary>
        /// <value>
        /// The diffusor w3.
        /// </value>
        public double? DiffusorW3 { get; set; }

        /// <summary>
        /// Gets or sets the diffusor l.
        /// </summary>
        /// <value>
        /// The diffusor l.
        /// </value>
        public double? DiffusorL { get; set; }

        /// <summary>
        /// Gets or sets the diffusor d.
        /// </summary>
        /// <value>
        /// The diffusor d.
        /// </value>
        public double? DiffusorD { get; set; }

        /// <summary>
        /// Gets or sets the diffusor l1.
        /// </summary>
        /// <value>
        /// The diffusor l1.
        /// </value>
        public double? DiffusorL1 { get; set; }

        /// <summary>
        /// Gets or sets the diffusor d1.
        /// </summary>
        /// <value>
        /// The diffusor d1.
        /// </value>
        public double? DiffusorD1 { get; set; }

        /// <summary>
        /// Gets or sets the diffusor l2.
        /// </summary>
        /// <value>
        /// The diffusor l2.
        /// </value>
        public double? DiffusorL2 { get; set; }

        /// <summary>
        /// Gets or sets the diffusor d2.
        /// </summary>
        /// <value>
        /// The diffusor d2.
        /// </value>
        public double? DiffusorD2 { get; set; }

        /// <summary>
        /// Gets or sets the diffusor l3.
        /// </summary>
        /// <value>
        /// The diffusor l3.
        /// </value>
        public double? DiffusorL3 { get; set; }

        /// <summary>
        /// Gets or sets the diffusor d3.
        /// </summary>
        /// <value>
        /// The diffusor d3.
        /// </value>
        public double? DiffusorD3 { get; set; }

        /// <summary>
        /// Gets or sets the mittelteil f.
        /// </summary>
        /// <value>
        /// The mittelteil f.
        /// </value>
        public double? MittelteilF { get; set; }

        /// <summary>
        /// Gets or sets the mittelteil l.
        /// </summary>
        /// <value>
        /// The mittelteil l.
        /// </value>
        public double? MittelteilL { get; set; }

        /// <summary>
        /// Gets or sets the mittelteil d.
        /// </summary>
        /// <value>
        /// The mittelteil d.
        /// </value>
        public double? MittelteilD { get; set; }

        /// <summary>
        /// Gets or sets the gegen konus w.
        /// </summary>
        /// <value>
        /// The gegen konus w.
        /// </value>
        public double? GegenKonusW { get; set; }

        /// <summary>
        /// Gets or sets the gegenkonus l.
        /// </summary>
        /// <value>
        /// The gegenkonus l.
        /// </value>
        public double? GegenkonusL { get; set; }

        /// <summary>
        /// Gets or sets the gegenkonus d.
        /// </summary>
        /// <value>
        /// The gegenkonus d.
        /// </value>
        public double? GegenkonusD { get; set; }

        /// <summary>
        /// Gets or sets the endrohr l.
        /// </summary>
        /// <value>
        /// The endrohr l.
        /// </value>
        public double? EndrohrL { get; set; }

        /// <summary>
        /// Gets or sets the endrohr d.
        /// </summary>
        /// <value>
        /// The endrohr d.
        /// </value>
        public double? EndrohrD { get; set; }

        /// <summary>
        /// Gets or sets the gesamt l.
        /// </summary>
        /// <value>
        /// The gesamt l.
        /// </value>
        public double? GesamtL { get; set; }

        /// <summary>
        /// Gets or sets the resonanz l.
        /// </summary>
        /// <value>
        /// The resonanz l.
        /// </value>
        public double? ResonanzL { get; set; }

        /// <summary>
        /// Gets or sets the abgas t.
        /// </summary>
        /// <value>
        /// The abgas t.
        /// </value>
        public double? AbgasT { get; set; }

        /// <summary>
        /// Gets or sets the abgas v.
        /// </summary>
        /// <value>
        /// The abgas v.
        /// </value>
        public double? AbgasV { get; set; }

        /// <summary>
        /// Gets or sets the auslass identifier.
        /// </summary>
        /// <value>
        /// The auslass identifier.
        /// </value>
        public int AuslassId { get; set; }

        /// <summary>
        /// Gets or sets the auslass.
        /// </summary>
        /// <value>
        /// The auslass.
        /// </value>
        [ForeignKey("AuslassId")]
        public virtual AuslassModel Auslass { get; set; }
    }
}