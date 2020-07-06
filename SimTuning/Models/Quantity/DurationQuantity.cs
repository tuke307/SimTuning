using System;
using System.Collections.ObjectModel;
using UnitsNet;

namespace SimTuning.Core.Models
{
    public class DurationQuantity : ObservableCollection<UnitListItem>
    {
        public DurationQuantity() : base()
        {
            QuantityInfo quantityInfo = Quantity.GetInfo(QuantityType.Duration);

            foreach (Enum unitValue in quantityInfo.Units)
            {
                Add(new UnitListItem(unitValue));
            }
        }
    }
}