using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using UnitsNet;

namespace SimTuning.Models
{
    public class AreaQuantity : ObservableCollection<UnitListItem>
    {
        public AreaQuantity() : base()
        {
            QuantityInfo quantityInfo = Quantity.GetInfo(QuantityType.Area);

            foreach (Enum unitValue in quantityInfo.Units)
            {
                Add(new UnitListItem(unitValue));
            }
        }
    }
}