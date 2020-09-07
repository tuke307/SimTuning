// project=SimTuning.Core, file=AreaQuantity.cs, creation=2020:7:31 Copyright (c) 2020
// tuke productions. All rights reserved.
namespace SimTuning.Core.Models
{
    using System;
    using System.Collections.ObjectModel;
    using System.Linq;
    using UnitsNet;

    /// <summary>
    /// AreaQuantity.
    /// </summary>
    /// <seealso cref="System.Collections.ObjectModel.ObservableCollection{SimTuning.Core.UnitListItem}" />
    public class AreaQuantity : ObservableCollection<UnitListItem>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AreaQuantity" /> class.
        /// </summary>
        public AreaQuantity() : base()
        {
            QuantityInfo quantityInfo = UnitsNet.Quantity.GetInfo(QuantityType.Area);

            foreach (Enum unitValue in quantityInfo.UnitInfos.Select(x => x.Value) /*quantityInfo.Units*/)
            {
                Add(new UnitListItem(unitValue));
            }
        }
    }
}