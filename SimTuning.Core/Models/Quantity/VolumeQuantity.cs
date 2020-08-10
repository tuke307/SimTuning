// project=SimTuning.Core, file=VolumeQuantity.cs, creation=2020:7:31
// Copyright (c) 2020 tuke productions. All rights reserved.
using System;
using System.Collections.ObjectModel;
using System.Linq;
using UnitsNet;

namespace SimTuning.Core.Models
{
    public class VolumeQuantity : ObservableCollection<UnitListItem>
    {
        public VolumeQuantity() : base()
        {
            QuantityInfo quantityInfo = Quantity.GetInfo(QuantityType.Volume);

            foreach (Enum unitValue in quantityInfo.UnitInfos.Select(x => x.Value) /*quantityInfo.Units*/)
            {
                Add(new UnitListItem(unitValue));
            }
        }
    }
}