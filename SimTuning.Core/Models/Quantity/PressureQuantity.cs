// project=SimTuning.Core, file=PressureQuantity.cs, creation=2020:9:7 Copyright (c) 2021
// tuke productions. All rights reserved.
namespace SimTuning.Core.Models.Quantity
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using UnitsNet.Units;

    /// <summary>
    /// PressureQuantity.
    /// </summary>
    /// <seealso cref="System.Collections.ObjectModel.ObservableCollection{SimTuning.Core.UnitListItem}" />
    public class PressureQuantity : ObservableCollection<UnitListItem>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PressureQuantity" /> class.
        /// </summary>
        public PressureQuantity()
            : base()
        {
            var customPressureUnits = new List<Enum>()
            {
                PressureUnit.Millibar,
                PressureUnit.Bar,
                PressureUnit.Kilobar,
                PressureUnit.Megabar,

                PressureUnit.Millipascal,
                PressureUnit.Pascal,
                PressureUnit.Kilopascal,
                PressureUnit.Hectopascal,
                PressureUnit.Megapascal,
            };

            foreach (Enum unitValue in customPressureUnits)
            {
                this.Add(new UnitListItem(unitValue));
            }
        }
    }
}