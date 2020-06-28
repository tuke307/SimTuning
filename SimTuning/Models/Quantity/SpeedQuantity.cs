using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using UnitsNet;

namespace SimTuning.Models
{
    public class SpeedQuantity : ObservableCollection<UnitListItem>
    {
        public SpeedQuantity() : base()
        {
            QuantityInfo quantityInfo = Quantity.GetInfo(QuantityType.Speed);

            foreach (Enum unitValue in quantityInfo.Units)
            {
                Add(new UnitListItem(unitValue));
            }
        }
    }
}