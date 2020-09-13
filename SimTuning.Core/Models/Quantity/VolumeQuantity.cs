// project=SimTuning.Core, file=VolumeQuantity.cs, creation=2020:7:31 Copyright (c) 2020
// tuke productions. All rights reserved.
namespace SimTuning.Core.Models
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using UnitsNet.Units;

    /// <summary>
    /// VolumeQuantity.
    /// </summary>
    /// <seealso cref="System.Collections.ObjectModel.ObservableCollection{SimTuning.Core.UnitListItem}" />
    public class VolumeQuantity : ObservableCollection<UnitListItem>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VolumeQuantity" /> class.
        /// </summary>
        public VolumeQuantity()
            : base()
        {
            var customVolumeUnits = new List<Enum>()
            {
                VolumeUnit.CubicMicrometer,
                VolumeUnit.CubicMillimeter,
                VolumeUnit.CubicCentimeter,
                VolumeUnit.CubicDecimeter,
                VolumeUnit.Milliliter,
                VolumeUnit.Liter,
            };

            foreach (Enum unitValue in customVolumeUnits)
            {
                this.Add(new UnitListItem(unitValue));
            }
        }
    }
}