using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using UnitsNet;

namespace SimTuning.Models
{
    public class LengthQuantity : ObservableCollection<UnitListItem>
    {
        public LengthQuantity() : base()
        {
            QuantityInfo quantityInfo = Quantity.GetInfo(QuantityType.Length);

            foreach (Enum unitValue in quantityInfo.Units)
            {
                Add(new UnitListItem(unitValue));
            }
        }
    }
}