using Data;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using SimTuning.Models;
using SimTuning.ModuleLogic;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using UnitsNet.Units;

namespace SimTuning.ViewModels.Einlass
{
    public class VergaserViewModel : BaseViewModel
    {
        private readonly EinlassLogic einlass = new EinlassLogic();
        public ObservableCollection<UnitListItem> LengthQuantityUnits { get; }
        public ObservableCollection<UnitListItem> VolumeQuantityUnits { get; }

        public VergaserViewModel()
        {
            VolumeQuantityUnits = new VolumeQuantity();
            LengthQuantityUnits = new LengthQuantity();

            //InsertDataCommand = new ActionCommand(InsertData);
            UnitHubvolumen = VolumeQuantityUnits.Where(x => x.UnitEnumValue.Equals(VolumeUnit.CubicCentimeter)).First();
            UnitVergasergroeße = LengthQuantityUnits.Where(x => x.UnitEnumValue.Equals(LengthUnit.Millimeter)).First();
            UnitHauptdueseD = LengthQuantityUnits.Where(x => x.UnitEnumValue.Equals(LengthUnit.Micrometer)).First();

            using (var db = new DatabaseContext())
            {
                IList<VehiclesModel> vehicList = db.Vehicles
                    .Include(vehicles => vehicles.Motor)
                    .ToList();

                Vehicles = new ObservableCollection<VehiclesModel>(vehicList);
            }
        }

        public ICommand InsertDataCommand { get; set; }

        public void InsertData(object obj)
        {
            if (Vehicle.Motor.HubraumV.HasValue)
                Hubvolumen = Vehicle.Motor.HubraumV;

            if (Vehicle.Motor.ResonanzU.HasValue)
                Resonanzdrehzahl = Vehicle.Motor.ResonanzU;
        }

        public ObservableCollection<VehiclesModel> Vehicles
        {
            get => Get<ObservableCollection<VehiclesModel>>();
            set => Set(value);
        }

        public VehiclesModel Vehicle
        {
            get => Get<VehiclesModel>();
            set => Set(value);
        }

        private void Refresh_Vergasergroeße()
        {
            if (Hubvolumen.HasValue && Resonanzdrehzahl.HasValue)
            {
                Vergasergroeße = einlass.Get_Vergasergroeße(
                    UnitsNet.UnitConverter.Convert(Hubvolumen.Value,
                    UnitHubvolumen.UnitEnumValue,
                    VolumeUnit.CubicCentimeter),
                    Resonanzdrehzahl.Value);
            }
        }

        private void Refresh_Hauptduesendurchmesser()
        {
            if (Vergasergroeße.HasValue)
            {
                HauptdueseD = einlass.Get_Hauptduesendurchmesser(
                    UnitsNet.UnitConverter.Convert(Vergasergroeße.Value,
                    UnitVergasergroeße.UnitEnumValue,
                    LengthUnit.Millimeter));
            }
        }

        public UnitListItem UnitHauptdueseD
        {
            get => Get<UnitListItem>();
            set
            {
                HauptdueseD = Business.Functions.UpdateValue(HauptdueseD, UnitHauptdueseD, value);

                Set(value);
            }
        }

        public double? HauptdueseD
        {
            get => Get<double?>();
            set => Set(value);
        }

        public double? Resonanzdrehzahl
        {
            get => Get<double?>();
            set { Set(value); Refresh_Vergasergroeße(); }
        }

        public UnitListItem UnitHubvolumen
        {
            get => Get<UnitListItem>();
            set
            {
                Hubvolumen = Business.Functions.UpdateValue(Hubvolumen, UnitHubvolumen, value);

                Set(value);
            }
        }

        public double? Hubvolumen
        {
            get => Get<double?>();
            set { Set(value); Refresh_Vergasergroeße(); }
        }

        public UnitListItem UnitVergasergroeße
        {
            get => Get<UnitListItem>();
            set
            {
                Vergasergroeße = Business.Functions.UpdateValue(Vergasergroeße, UnitVergasergroeße, value);

                Set(value);
            }
        }

        public double? Vergasergroeße
        {
            get => Get<double?>();
            set { Set(value); Refresh_Hauptduesendurchmesser(); }
        }
    }
}