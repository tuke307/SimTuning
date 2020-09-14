﻿// project=Data, file=EnvironmentModel.cs, creation=2020:6:28 Copyright (c) 2020 tuke
// productions. All rights reserved.
namespace Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using UnitsNet.Units;

    /// <summary>
    /// UmgebungsModel.
    /// </summary>
    /// <seealso cref="Data.Models.BaseEntityModel" />
    public class EnvironmentModel : BaseEntityModel
    {
        /// <summary>
        /// Gets the luftdruck p base unit.
        /// </summary>
        /// <value>The luftdruck p base unit.</value>
        [NotMapped]
        public static PressureUnit LuftdruckPBaseUnit { get => PressureUnit.Hectopascal; }

        /// <summary>
        /// Gets the temperatur t base unit.
        /// </summary>
        /// <value>The temperatur t base unit.</value>
        [NotMapped]
        public static UnitsNet.Units.TemperatureUnit TemperaturTBaseUnit { get => UnitsNet.Units.TemperatureUnit.DegreeCelsius; }

        /// <summary>
        /// Gets or sets the dyno.
        /// </summary>
        /// <value>The dyno.</value>
        public virtual IList<DynoModel> Dyno { get; set; }

        /// <summary>
        /// Gets or sets the luftdruck p.
        /// </summary>
        /// <value>The luftdruck p.</value>
        public double? LuftdruckP { get; set; }

        /// <summary>
        /// Gets or sets the luftdruck p unit.
        /// </summary>
        /// <value>The luftdruck p unit.</value>
        [NotMapped]
        public UnitsNet.Units.PressureUnit? LuftdruckPUnit
        {
            get => this._LuftdruckPUnit ?? LuftdruckPBaseUnit;
            set
            {
                if (this.LuftdruckP.HasValue)
                {
                    UnitsNet.UnitConverter.TryConvert(
               this.LuftdruckP.Value,
               this.LuftdruckPUnit,
               value,
               out double convertedValue);

                    if (UnitSettings.RoundOnUnitChange)
                    {
                        this.LuftdruckP = Math.Round(convertedValue, UnitSettings.RoundingAccuracy);
                    }
                    else
                    {
                        this.LuftdruckP = convertedValue;
                    }
                }

                this._LuftdruckPUnit = value;
            }
        }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the temperatur t.
        /// </summary>
        /// <value>The temperatur t.</value>
        public double? TemperaturT { get; set; }

        /// <summary>
        /// Gets or sets the temperatur t unit.
        /// </summary>
        /// <value>The temperatur t unit.</value>
        [NotMapped]
        public UnitsNet.Units.TemperatureUnit? TemperaturTUnit
        {
            get => this._TemperaturTUnit ?? TemperaturTBaseUnit;
            set
            {
                if (this.TemperaturT.HasValue)
                {
                    UnitsNet.UnitConverter.TryConvert(
               this.TemperaturT.Value,
               this.TemperaturTUnit,
               value,
               out double convertedValue);

                    if (UnitSettings.RoundOnUnitChange)
                    {
                        this.TemperaturT = Math.Round(convertedValue, UnitSettings.RoundingAccuracy);
                    }
                    else
                    {
                        this.TemperaturT = convertedValue;
                    }
                }

                this._TemperaturTUnit = value;
            }
        }

        /// <summary>
        /// Gets or sets the luftdruck p unit.
        /// </summary>
        /// <value>The luftdruck p unit.</value>
        [NotMapped]
        private PressureUnit? _LuftdruckPUnit { get; set; }

        /// <summary>
        /// Gets or sets the temperatur t unit.
        /// </summary>
        /// <value>The temperatur t unit.</value>
        [NotMapped]
        private TemperatureUnit? _TemperaturTUnit { get; set; }
    }
}