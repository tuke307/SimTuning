using Data;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using SimTuning.Models;
using SimTuning.ModuleLogic;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using UnitsNet.Units;

namespace SimTuning.ViewModels.Motor
{
    public class HubraumViewModel : BaseViewModel
    {
        private readonly EngineLogic hubraum;
        public ObservableCollection<UnitListItem> VolumeQuantityUnits { get; }
        public ObservableCollection<UnitListItem> LengthQuantityUnits { get; }

        public HubraumViewModel()
        {
            hubraum = new EngineLogic();

            VolumeQuantityUnits = new VolumeQuantity();
            LengthQuantityUnits = new LengthQuantity();

            UnitEinbauspiel = LengthQuantityUnits.Where(x => x.UnitEnumValue.Equals(LengthUnit.Millimeter)).First();
            UnitHub = LengthQuantityUnits.Where(x => x.UnitEnumValue.Equals(LengthUnit.Centimeter)).First();
            UnitHubraumV = VolumeQuantityUnits.Where(x => x.UnitEnumValue.Equals(VolumeUnit.CubicCentimeter)).First();
            UnitKolbenD = LengthQuantityUnits.Where(x => x.UnitEnumValue.Equals(LengthUnit.Centimeter)).First();
            UnitBohrungD = LengthQuantityUnits.Where(x => x.UnitEnumValue.Equals(LengthUnit.Centimeter)).First();

            using (var db = new DatabaseContext())
            {
                IList<VehiclesModel> vehicList = db.Vehicles
                    .Include(vehicles => vehicles.Motor)
                    .Include(vehicles => vehicles.Motor.Einlass)
                    .Include(vehicles => vehicles.Motor.Auslass)
                    .Include(vehicles => vehicles.Motor.Ueberstroemer)
                    .ToList();

                Vehicles = new ObservableCollection<VehiclesModel>(vehicList);
            }
        }

        public GrindingDiametersModel GrindingDiameters
        {
            get => Get<GrindingDiametersModel>();
            set => Set(value);
        }

        public ObservableCollection<VehiclesModel> Vehicles
        {
            get => Get<ObservableCollection<VehiclesModel>>();
            set => Set(value);
        }

        public VehiclesModel Vehicle
        {
            get => Get<VehiclesModel>();
            set
            {
                Set(value);

                if (value.Motor.BohrungD.HasValue)
                    GrindingDiameters = hubraum.Get_GrindingDiameters(value.Motor.BohrungD.Value);
            }
        }

        public UnitListItem UnitEinbauspiel
        {
            get => Get<UnitListItem>();
            set
            {
                Einbauspiel = Business.Functions.UpdateValue(Einbauspiel, UnitEinbauspiel, value);

                Set(value);
            }
        }

        public double? Einbauspiel
        {
            get => Get<double?>();
            set { Set(value); Refresh_all(); }
        }

        public UnitListItem UnitKolbenD
        {
            get => Get<UnitListItem>();
            set
            {
                KolbenD = Business.Functions.UpdateValue(KolbenD, UnitKolbenD, value);

                Set(value);
            }
        }

        public double? KolbenD
        {
            get => Get<double?>();
            set => Set(value);
        }

        public UnitListItem UnitBohrungD
        {
            get => Get<UnitListItem>();
            set
            {
                BohrungD = Business.Functions.UpdateValue(BohrungD, UnitBohrungD, value);

                Set(value);
            }
        }

        public double? BohrungD
        {
            get => Get<double?>();
            set => Set(value);
        }

        public UnitListItem UnitHub
        {
            get => Get<UnitListItem>();
            set
            {
                Hub = Business.Functions.UpdateValue(Hub, UnitHub, value);

                Set(value);
            }
        }

        public double? Hub
        {
            get => Get<double?>();
            set { Set(value); Refresh_all(); }
        }

        public UnitListItem UnitHubraumV
        {
            get => Get<UnitListItem>();
            set
            {
                HubraumV = Business.Functions.UpdateValue(HubraumV, UnitHubraumV, value);

                Set(value);
            }
        }

        public double? HubraumV
        {
            get => Get<double?>();
            set { Set(value); Refresh_all(); }
        }

        private void Refresh_all()
        {
            if (Hub.HasValue && HubraumV.HasValue)
            {
                if (!Einbauspiel.HasValue)
                    Einbauspiel = 0.03;

                BohrungD = hubraum.Get_BohrungsDurchmesser(
                    UnitsNet.UnitConverter.Convert(
                    HubraumV.Value,
                    UnitHubraumV.UnitEnumValue,
                    VolumeUnit.CubicCentimeter),

                    UnitsNet.UnitConverter.Convert(
                    Hub.Value,
                    UnitHub.UnitEnumValue,
                    LengthUnit.Centimeter));

                KolbenD = hubraum.Get_KolbenDurchmesser(
                    UnitsNet.UnitConverter.Convert(
                    BohrungD.Value,
                    UnitBohrungD.UnitEnumValue,
                    LengthUnit.Centimeter),

                    UnitsNet.UnitConverter.Convert(
                    Einbauspiel.Value,
                    UnitEinbauspiel.UnitEnumValue,
                    LengthUnit.Millimeter));
            }
        }
    }
}