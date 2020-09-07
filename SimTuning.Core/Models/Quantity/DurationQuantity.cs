﻿// project=SimTuning.Core, file=DurationQuantity.cs, creation=2020:7:31 Copyright (c) 2020
// tuke productions. All rights reserved.
namespace SimTuning.Core.Models
{
    using System;
    using System.Collections.ObjectModel;
    using System.Linq;
    using UnitsNet;

    /// <summary>
    /// DurationQuantity.
    /// </summary>
    /// <seealso cref="System.Collections.ObjectModel.ObservableCollection{SimTuning.Core.UnitListItem}" />
    public class DurationQuantity : ObservableCollection<UnitListItem>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DurationQuantity" /> class.
        /// </summary>
        public DurationQuantity() : base()
        {
            QuantityInfo quantityInfo = UnitsNet.Quantity.GetInfo(QuantityType.Duration);

            foreach (Enum unitValue in quantityInfo.UnitInfos.Select(x => x.Value) /*quantityInfo.Units*/)
            {
                Add(new UnitListItem(unitValue));
            }
        }
    }
}