using System;
using System.Collections.ObjectModel;
using System.Linq;
using UnitsNet;

namespace SimTuning.Core.Models
{
    public class SpeedQuantity : ObservableCollection<UnitListItem>
    {
        public SpeedQuantity() : base()
        {
            QuantityInfo quantityInfo = Quantity.GetInfo(QuantityType.Speed);

            foreach (Enum unitValue in quantityInfo.UnitInfos.Select(x => x.Value) /*quantityInfo.Units*/)
            {
                Add(new UnitListItem(unitValue));
            }
        }
    }
}