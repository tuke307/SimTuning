// project=SimTuning.Core, file=PressureQuantity.cs, creation=2020:9:7 Copyright (c) 2020
// tuke productions. All rights reserved.
namespace SimTuning.Core.Models.Quantity
{
    using System;
    using System.Collections.ObjectModel;
    using System.Linq;
    using UnitsNet;

    /// <summary>
    /// PressureQuantity.
    /// </summary>
    /// <seealso cref="System.Collections.ObjectModel.ObservableCollection{SimTuning.Core.UnitListItem}" />
    public class PressureQuantity : ObservableCollection<UnitListItem>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PressureQuantity" /> class.
        /// </summary>
        public PressureQuantity() : base()
        {
            QuantityInfo quantityInfo = UnitsNet.Quantity.GetInfo(QuantityType.Pressure);

            foreach (Enum unitValue in quantityInfo.UnitInfos.Select(x => x.Value) /*quantityInfo.Units*/)
            {
                Add(new UnitListItem(unitValue));
            }
        }
    }
}