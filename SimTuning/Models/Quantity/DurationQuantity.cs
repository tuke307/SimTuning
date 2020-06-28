using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using UnitsNet;

namespace SimTuning.Models
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