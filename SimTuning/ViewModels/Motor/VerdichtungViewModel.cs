using Data;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using SimTuning.Models;
using SimTuning.ModuleLogic;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using UnitsNet.Units;

namespace SimTuning.ViewModels.Motor
{
    public class VerdichtungViewModel : BaseViewModel
    {
        private EngineLogic verdichtung;
        public ObservableCollection<UnitListItem> VolumeQuantityUnits { get; }
        public ObservableCollection<UnitListItem> LengthQuantityUnits { get; }

        public VerdichtungViewModel()
        {
            //InsertDataCommand = new ActionCommand(InsertData);
            verdichtung = new EngineLogic();

            VolumeQuantityUnits = new VolumeQuantity();
            LengthQuantityUnits = new LengthQuantity();

            UnitAbdrehenLength = LengthQuantityUnits.Where(x => x.UnitEnumValue.Equals(LengthUnit.Millimeter)).First();
            UnitBohrungD = LengthQuantityUnits.Where(x => x.UnitEnumValue.Equals(LengthUnit.Millimeter)).First();
            UnitBrennraumV = VolumeQuantityUnits.Where(x => x.UnitEnumValue.Equals(VolumeUnit.CubicMillimeter)).First();
            UnitHubraumV = VolumeQuantityUnits.Where(x => x.UnitEnumValue.Equals(VolumeUnit.CubicMillimeter)).First();

            Zielverdichtungen = new List<double?>() { 8, 8.5, 9, 9.5, 10, 10.5, 11, 11.5, 12 };
            Zielverdichtung = Zielverdichtungen[5];

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

        public ICommand InsertDataCommand { get; set; }

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

        protected void InsertData(object parameter)
        {
            if (Vehicle.Motor.HubraumV.HasValue)
                HubraumV = Vehicle.Motor.HubraumV;

            if (Vehicle.Motor.BrennraumV.HasValue)
                BrennraumV = Vehicle.Motor.BrennraumV;

            if (Vehicle.Motor.BohrungD.HasValue)
                BohrungD = Vehicle.Motor.BohrungD;
        }

        private void Refresh_verdichtung()
        {
            if (HubraumV.HasValue && BrennraumV.HasValue && BohrungD.HasValue)
            {
                derzeitige_verdichtung = verdichtung.Get_Verdichtung(
                    UnitsNet.UnitConverter.Convert(HubraumV.Value,
                    UnitHubraumV.UnitEnumValue,
                    VolumeUnit.CubicMillimeter),

                    UnitsNet.UnitConverter.Convert(BrennraumV.Value,
                    UnitBrennraumV.UnitEnumValue,
                    VolumeUnit.CubicMillimeter),

                    UnitsNet.UnitConverter.Convert(BohrungD.Value,
                    UnitBohrungD.UnitEnumValue,
                    LengthUnit.Centimeter));
            }
        }

        private void Refresh_zielverdichtung()
        {
            if (HubraumV.HasValue && BrennraumV.HasValue && BohrungD.HasValue && Zielverdichtung != 0)
            {
                AbdrehenLength = verdichtung.Get_Abdrehen_mm(
                    UnitsNet.UnitConverter.Convert(HubraumV.Value,
                    UnitHubraumV.UnitEnumValue,
                    VolumeUnit.CubicMillimeter),

                    UnitsNet.UnitConverter.Convert(BrennraumV.Value,
                    UnitBrennraumV.UnitEnumValue,
                    VolumeUnit.CubicMillimeter),

                    UnitsNet.UnitConverter.Convert(BohrungD.Value,
                    UnitBohrungD.UnitEnumValue,
                    LengthUnit.Millimeter),

                    Zielverdichtung.Value);
            }
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
            set { Set(value); Refresh_verdichtung(); }
        }

        public UnitListItem UnitBrennraumV
        {
            get => Get<UnitListItem>();
            set
            {
                BrennraumV = Business.Functions.UpdateValue(BrennraumV, UnitBrennraumV, value);

                Set(value);
            }
        }

        public double? BrennraumV
        {
            get => Get<double?>();
            set { Set(value); Refresh_verdichtung(); }
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
            set { Set(value); Refresh_verdichtung(); }
        }

        public double? derzeitige_verdichtung
        {
            get => Get<double?>();
            set => Set(value);
        }

        public UnitListItem UnitAbdrehenLength
        {
            get => Get<UnitListItem>();
            set
            {
                AbdrehenLength = Business.Functions.UpdateValue(AbdrehenLength, UnitAbdrehenLength, value);

                Set(value);
            }
        }

        public double? AbdrehenLength
        {
            get => Get<double?>();
            set => Set(value);
        }

        public List<double?> Zielverdichtungen
        {
            get => Get<List<double?>>();
            set { Set(value); Refresh_zielverdichtung(); }
        }

        public double? Zielverdichtung
        {
            get => Get<double?>();
            set { Set(value); Refresh_zielverdichtung(); }
        }
    }
}