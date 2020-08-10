// project=SimTuning.Core, file=DurationQuantity.cs, creation=2020:7:31
// Copyright (c) 2020 tuke productions. All rights reserved.
using System;
using System.Collections.ObjectModel;
using System.Linq;
using UnitsNet;

namespace SimTuning.Core.Models
{
    public class DurationQuantity : ObservableCollection<UnitListItem>
    {
        public DurationQuantity() : base()
        {
            QuantityInfo quantityInfo = Quantity.GetInfo(QuantityType.Duration);

            foreach (Enum unitValue in quantityInfo.UnitInfos.Select(x => x.Value) /*quantityInfo.Units*/)
            {
                Add(new UnitListItem(unitValue));
            }
        }
    }
}