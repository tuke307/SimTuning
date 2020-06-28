using System;
using System.Collections.ObjectModel;
using UnitsNet;

namespace SimTuning.Models
{
    public class VolumeQuantity : ObservableCollection<UnitListItem>
    {
        public VolumeQuantity() : base()
        {
            QuantityInfo quantityInfo = Quantity.GetInfo(QuantityType.Volume);

            foreach (Enum unitValue in quantityInfo.Units)
            {
                Add(new UnitListItem(unitValue));
            }
        }
    }
}