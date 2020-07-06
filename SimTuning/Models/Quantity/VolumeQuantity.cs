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