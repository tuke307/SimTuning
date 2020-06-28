using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using UnitsNet;

namespace SimTuning.Models
{
    public class MassQuantity : ObservableCollection<UnitListItem>
    {
        public MassQuantity() : base()
        {
            QuantityInfo quantityInfo = Quantity.GetInfo(QuantityType.Mass);

            foreach (Enum unitValue in quantityInfo.Units)
            {
                Add(new UnitListItem(unitValue));
            }
        }
    }
}