// project=Data, file=AuspuffModel.cs, creation=2020:6:28 Copyright (c) 2020 tuke
// productions. All rights reserved.
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
        /// Gets the abgas t base unit.
        /// </summary>
        /// <value>The abgas t base unit.</value>
        [NotMapped]
        public static UnitsNet.Units.TemperatureUnit AbgasTBaseUnit { get => UnitsNet.Units.TemperatureUnit.DegreeCelsius; }

        /// <summary>
        /// Gets the abgas v base unit.
        /// </summary>
        /// <value>The abgas v base unit.</value>
        [NotMapped]
        public static UnitsNet.Units.SpeedUnit AbgasVBaseUnit { get => UnitsNet.Units.SpeedUnit.MeterPerSecond; }

        /// <summary>
        /// Gets the diffusor d1 base unit.
        /// </summary>
        /// <value>The diffusor d1 base unit.</value>
        [NotMapped]
        public static UnitsNet.Units.LengthUnit DiffusorD1BaseUnit { get => UnitsNet.Units.LengthUnit.Millimeter; }

        /// <summary>
        /// Gets the diffusor d2 base unit.
        /// </summary>
        /// <value>The diffusor d2 base unit.</value>
        [NotMapped]
        public static UnitsNet.Units.LengthUnit DiffusorD2BaseUnit { get => UnitsNet.Units.LengthUnit.Millimeter; }

        /// <summary>
        /// Gets the diffusor d3 base unit.
        /// </summary>
        /// <value>The diffusor d3 base unit.</value>
        [NotMapped]
        public static UnitsNet.Units.LengthUnit DiffusorD3BaseUnit { get => UnitsNet.Units.LengthUnit.Millimeter; }

        /// <summary>
        /// Gets the diffusor d base unit.
        /// </summary>
        /// <value>The diffusor d base unit.</value>
        [NotMapped]
        public static UnitsNet.Units.LengthUnit DiffusorDBaseUnit { get => UnitsNet.Units.LengthUnit.Millimeter; }

        /// <summary>
        /// Gets the diffusor l1 base unit.
        /// </summary>
        /// <value>The diffusor l1 base unit.</value>
        [NotMapped]
        public static UnitsNet.Units.LengthUnit DiffusorL1BaseUnit { get => UnitsNet.Units.LengthUnit.Millimeter; }

        /// <summary>
        /// Gets the diffusor l2 base unit.
        /// </summary>
        /// <value>The diffusor l2 base unit.</value>
        [NotMapped]
        public static UnitsNet.Units.LengthUnit DiffusorL2BaseUnit { get => UnitsNet.Units.LengthUnit.Millimeter; }

        /// <summary>
        /// Gets the diffusor l3 base unit.
        /// </summary>
        /// <value>The diffusor l3 base unit.</value>
        [NotMapped]
        public static UnitsNet.Units.LengthUnit DiffusorL3BaseUnit { get => UnitsNet.Units.LengthUnit.Millimeter; }

        /// <summary>
        /// Gets the diffusor l base unit.
        /// </summary>
        /// <value>The diffusor l base unit.</value>
        [NotMapped]
        public static UnitsNet.Units.LengthUnit DiffusorLBaseUnit { get => UnitsNet.Units.LengthUnit.Millimeter; }

        /// <summary>
        /// Gets the endrohr d base unit.
        /// </summary>
        /// <value>The endrohr d base unit.</value>
        [NotMapped]
        public static UnitsNet.Units.LengthUnit EndrohrDBaseUnit { get => UnitsNet.Units.LengthUnit.Millimeter; }

        /// <summary>
        /// Gets the endrohr l base unit.
        /// </summary>
        /// <value>The endrohr l base unit.</value>
        [NotMapped]
        public static UnitsNet.Units.LengthUnit EndrohrLBaseUnit { get => UnitsNet.Units.LengthUnit.Millimeter; }

        /// <summary>
        /// Gets the gegenkonus d base unit.
        /// </summary>
        /// <value>The gegenkonus d base unit.</value>
        [NotMapped]
        public static UnitsNet.Units.LengthUnit GegenkonusDBaseUnit { get => UnitsNet.Units.LengthUnit.Millimeter; }

        /// <summary>
        /// Gets the gegenkonus l base unit.
        /// </summary>
        /// <value>The gegenkonus l base unit.</value>
        [NotMapped]
        public static UnitsNet.Units.LengthUnit GegenkonusLBaseUnit { get => UnitsNet.Units.LengthUnit.Millimeter; }

        /// <summary>
        /// Gets the gesamt l base unit.
        /// </summary>
        /// <value>The gesamt l base unit.</value>
        [NotMapped]
        public static UnitsNet.Units.LengthUnit GesamtLBaseUnit { get => UnitsNet.Units.LengthUnit.Centimeter; }

        /// <summary>
        /// Gets the kruemmer d base unit.
        /// </summary>
        /// <value>The kruemmer d base unit.</value>
        [NotMapped]
        public static UnitsNet.Units.LengthUnit KruemmerDBaseUnit { get => UnitsNet.Units.LengthUnit.Millimeter; }

        /// <summary>
        /// Gets the kruemmer l base unit.
        /// </summary>
        /// <value>The kruemmer l base unit.</value>
        [NotMapped]
        public static UnitsNet.Units.LengthUnit KruemmerLBaseUnit { get => UnitsNet.Units.LengthUnit.Millimeter; }

        /// <summary>
        /// Gets the mittelteil base unit.
        /// </summary>
        /// <value>The mittelteil base unit.</value>
        [NotMapped]
        public static UnitsNet.Units.LengthUnit MittelteilBaseUnit { get => UnitsNet.Units.LengthUnit.Millimeter; }

        /// <summary>
        /// Gets the mittelteil l base unit.
        /// </summary>
        /// <value>The mittelteil l base unit.</value>
        [NotMapped]
        public static UnitsNet.Units.LengthUnit MittelteilLBaseUnit { get => UnitsNet.Units.LengthUnit.Millimeter; }

        /// <summary>
        /// Gets the resonanz l base unit.
        /// </summary>
        /// <value>The resonanz l base unit.</value>
        [NotMapped]
        public static UnitsNet.Units.LengthUnit ResonanzLBaseUnit { get => UnitsNet.Units.LengthUnit.Millimeter; }

        /// <summary>
        /// Gets or sets the abgas t.
        /// </summary>
        /// <value>The abgas t.</value>
        public double? AbgasT { get; set; }

        /// <summary>
        /// Gets or sets the abgas t unit.
        /// </summary>
        /// <value>The abgas t unit.</value>
        [NotMapped]
        public UnitsNet.Units.TemperatureUnit AbgasTUnit { get; set; }

        /// <summary>
        /// Gets or sets the abgas v.
        /// </summary>
        /// <value>The abgas v.</value>
        public double? AbgasV { get; set; }

        /// <summary>
        /// Gets or sets the abgas v unit.
        /// </summary>
        /// <value>The abgas v unit.</value>
        [NotMapped]
        public UnitsNet.Units.SpeedUnit AbgasVUnit { get; set; }

        /// <summary>
        /// Gets or sets the auslass.
        /// </summary>
        /// <value>The auslass.</value>
        [ForeignKey("AuslassId")]
        public virtual AuslassModel Auslass { get; set; }

        /// <summary>
        /// Gets or sets the auslass identifier.
        /// </summary>
        /// <value>The auslass identifier.</value>
        public int AuslassId { get; set; }

        /// <summary>
        /// Gets or sets the diffusor d.
        /// </summary>
        /// <value>The diffusor d.</value>
        public double? DiffusorD { get; set; }

        /// <summary>
        /// Gets or sets the diffusor d1.
        /// </summary>
        /// <value>The diffusor d1.</value>
        public double? DiffusorD1 { get; set; }

        /// <summary>
        /// Gets or sets the diffusor d1 unit.
        /// </summary>
        /// <value>The diffusor d1 unit.</value>
        [NotMapped]
        public UnitsNet.Units.LengthUnit DiffusorD1Unit { get; set; }

        /// <summary>
        /// Gets or sets the diffusor d2.
        /// </summary>
        /// <value>The diffusor d2.</value>
        public double? DiffusorD2 { get; set; }

        /// <summary>
        /// Gets or sets the diffusor d2 unit.
        /// </summary>
        /// <value>The diffusor d2 unit.</value>
        [NotMapped]
        public UnitsNet.Units.LengthUnit DiffusorD2Unit { get; set; }

        /// <summary>
        /// Gets or sets the diffusor d3.
        /// </summary>
        /// <value>The diffusor d3.</value>
        public double? DiffusorD3 { get; set; }

        /// <summary>
        /// Gets or sets the diffusor d3 unit.
        /// </summary>
        /// <value>The diffusor d3 unit.</value>
        [NotMapped]
        public UnitsNet.Units.LengthUnit DiffusorD3Unit { get; set; }

        /// <summary>
        /// Gets or sets the diffusor d unit.
        /// </summary>
        /// <value>The diffusor d unit.</value>
        [NotMapped]
        public UnitsNet.Units.LengthUnit DiffusorDUnit { get; set; }

        /// <summary>
        /// Gets or sets the diffusor l.
        /// </summary>
        /// <value>The diffusor l.</value>
        public double? DiffusorL { get; set; }

        /// <summary>
        /// Gets or sets the diffusor l1.
        /// </summary>
        /// <value>The diffusor l1.</value>
        public double? DiffusorL1 { get; set; }

        /// <summary>
        /// Gets or sets the diffusor l1 unit.
        /// </summary>
        /// <value>The diffusor l1 unit.</value>
        [NotMapped]
        public UnitsNet.Units.LengthUnit DiffusorL1Unit { get; set; }

        /// <summary>
        /// Gets or sets the diffusor l2.
        /// </summary>
        /// <value>The diffusor l2.</value>
        public double? DiffusorL2 { get; set; }

        /// <summary>
        /// Gets or sets the diffusor l2 unit.
        /// </summary>
        /// <value>The diffusor l2 unit.</value>
        [NotMapped]
        public UnitsNet.Units.LengthUnit DiffusorL2Unit { get; set; }

        /// <summary>
        /// Gets or sets the diffusor l3.
        /// </summary>
        /// <value>The diffusor l3.</value>
        public double? DiffusorL3 { get; set; }

        /// <summary>
        /// Gets or sets the diffusor l3 unit.
        /// </summary>
        /// <value>The diffusor l3 unit.</value>
        [NotMapped]
        public UnitsNet.Units.LengthUnit DiffusorL3Unit { get; set; }

        /// <summary>
        /// Gets or sets the diffusor l unit.
        /// </summary>
        /// <value>The diffusor l unit.</value>
        [NotMapped]
        public UnitsNet.Units.LengthUnit DiffusorLUnit { get; set; }

        /// <summary>
        /// Gets or sets the diffusor stage.
        /// </summary>
        /// <value>The diffusor stage.</value>
        public int DiffusorStage { get; set; }

        /// <summary>
        /// Gets or sets the diffusor w.
        /// </summary>
        /// <value>The diffusor w.</value>
        public double? DiffusorW { get; set; }

        /// <summary>
        /// Gets or sets the diffusor w1.
        /// </summary>
        /// <value>The diffusor w1.</value>
        public double? DiffusorW1 { get; set; }

        /// <summary>
        /// Gets or sets the diffusor w2.
        /// </summary>
        /// <value>The diffusor w2.</value>
        public double? DiffusorW2 { get; set; }

        /// <summary>
        /// Gets or sets the diffusor w3.
        /// </summary>
        /// <value>The diffusor w3.</value>
        public double? DiffusorW3 { get; set; }

        /// <summary>
        /// Gets or sets the endrohr d.
        /// </summary>
        /// <value>The endrohr d.</value>
        public double? EndrohrD { get; set; }

        /// <summary>
        /// Gets or sets the endrohr d unit.
        /// </summary>
        /// <value>The endrohr d unit.</value>
        [NotMapped]
        public UnitsNet.Units.LengthUnit EndrohrDUnit { get; set; }

        /// <summary>
        /// Gets or sets the endrohr l.
        /// </summary>
        /// <value>The endrohr l.</value>
        public double? EndrohrL { get; set; }

        /// <summary>
        /// Gets or sets the endrohr l unit.
        /// </summary>
        /// <value>The endrohr l unit.</value>
        [NotMapped]
        public UnitsNet.Units.LengthUnit EndrohrLUnit { get; set; }

        /// <summary>
        /// Gets or sets the gegenkonus d.
        /// </summary>
        /// <value>The gegenkonus d.</value>
        public double? GegenkonusD { get; set; }

        /// <summary>
        /// Gets or sets the gegenkonus d unit.
        /// </summary>
        /// <value>The gegenkonus d unit.</value>
        [NotMapped]
        public UnitsNet.Units.LengthUnit GegenkonusDUnit { get; set; }

        /// <summary>
        /// Gets or sets the gegenkonus l.
        /// </summary>
        /// <value>The gegenkonus l.</value>
        public double? GegenkonusL { get; set; }

        /// <summary>
        /// Gets or sets the gegenkonus l unit.
        /// </summary>
        /// <value>The gegenkonus l unit.</value>
        [NotMapped]
        public UnitsNet.Units.LengthUnit GegenkonusLUnit { get; set; }

        /// <summary>
        /// Gets or sets the gegen konus w.
        /// </summary>
        /// <value>The gegen konus w.</value>
        public double? GegenKonusW { get; set; }

        /// <summary>
        /// Gets or sets the gesamt l.
        /// </summary>
        /// <value>The gesamt l.</value>
        public double? GesamtL { get; set; }

        /// <summary>
        /// Gets or sets the gesamt l unit.
        /// </summary>
        /// <value>The gesamt l unit.</value>
        [NotMapped]
        public UnitsNet.Units.LengthUnit GesamtLUnit { get; set; }

        /// <summary>
        /// Gets or sets the kruemmer d.
        /// </summary>
        /// <value>The kruemmer d.</value>
        public double? KruemmerD { get; set; }

        /// <summary>
        /// Gets or sets the kruemmer d unit.
        /// </summary>
        /// <value>The kruemmer d unit.</value>
        [NotMapped]
        public UnitsNet.Units.LengthUnit KruemmerDUnit { get; set; }

        /// <summary>
        /// Gets or sets the kruemmer f.
        /// </summary>
        /// <value>The kruemmer f.</value>
        public double? KruemmerF { get; set; }

        /// <summary>
        /// Gets or sets the kruemmer l.
        /// </summary>
        /// <value>The kruemmer l.</value>
        public double? KruemmerL { get; set; }

        /// <summary>
        /// Gets or sets the kruemmer l unit.
        /// </summary>
        /// <value>The kruemmer l unit.</value>
        [NotMapped]
        public UnitsNet.Units.LengthUnit KruemmerLUnit { get; set; }

        /// <summary>
        /// Gets or sets the kruemmer w.
        /// </summary>
        /// <value>The kruemmer w.</value>
        public double? KruemmerW { get; set; }

        /// <summary>
        /// Gets or sets the mittelteil d.
        /// </summary>
        /// <value>The mittelteil d.</value>
        public double? MittelteilD { get; set; }

        /// <summary>
        /// Gets or sets the mittelteil d unit.
        /// </summary>
        /// <value>The mittelteil d unit.</value>
        [NotMapped]
        public UnitsNet.Units.LengthUnit MittelteilDUnit { get; set; }

        /// <summary>
        /// Gets or sets the mittelteil f.
        /// </summary>
        /// <value>The mittelteil f.</value>
        public double? MittelteilF { get; set; }

        /// <summary>
        /// Gets or sets the mittelteil l.
        /// </summary>
        /// <value>The mittelteil l.</value>
        public double? MittelteilL { get; set; }

        /// <summary>
        /// Gets or sets the mittelteil l unit.
        /// </summary>
        /// <value>The mittelteil l unit.</value>
        [NotMapped]
        public UnitsNet.Units.LengthUnit MittelteilLUnit { get; set; }

        /// <summary>
        /// Gets or sets the resonanz l.
        /// </summary>
        /// <value>The resonanz l.</value>
        public double? ResonanzL { get; set; }

        /// <summary>
        /// Gets or sets the resonanz l unit.
        /// </summary>
        /// <value>The resonanz l unit.</value>
        [NotMapped]
        public UnitsNet.Units.LengthUnit ResonanzLUnit { get; set; }
    }
}