// project=SimTuning.Core, file=AreaQuantity.cs, creation=2020:7:31 Copyright (c) 2020
// tuke productions. All rights reserved.
namespace SimTuning.Core.Models
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using UnitsNet.Units;

    /// <summary>
    /// AreaQuantity.
    /// </summary>
    /// <seealso cref="System.Collections.ObjectModel.ObservableCollection{SimTuning.Core.UnitListItem}" />
    public class AreaQuantity : ObservableCollection<UnitListItem>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AreaQuantity" /> class.
        /// </summary>
        public AreaQuantity()
            : base()
        {
            var customAreaUnits = new List<Enum>()
            {
                AreaUnit.SquareMillimeter,
                AreaUnit.SquareCentimeter,
                AreaUnit.SquareDecimeter,
                AreaUnit.SquareMeter,
            };

            foreach (Enum unitValue in customAreaUnits)
            {
                this.Add(new UnitListItem(unitValue));
            }
        }
    }
}