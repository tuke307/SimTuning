using Data;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using MvvmCross.Commands;
using MvvmCross.ViewModels;
using SimTuning.Core.Models;
using SimTuning.Core.ModuleLogic;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using UnitsNet.Units;

namespace SimTuning.Core.ViewModels.Motor
{
    public class VerdichtungViewModel : MvxViewModel
    {
        private readonly EngineLogic engineLogic;
        public ObservableCollection<UnitListItem> VolumeQuantityUnits { get; }
        public ObservableCollection<UnitListItem> LengthQuantityUnits { get; }

        public VerdichtungViewModel()
        {
            engineLogic = new EngineLogic();

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

                HelperVehicles = new ObservableCollection<VehiclesModel>(vehicList);
            }
        }

        public IMvxCommand InsertDataCommand { get; set; }

        public override void Prepare()
        {
            // This is the first method to be called after construction
        }

        public override Task Initialize()
        {
            // Async initialization

            return base.Initialize();
        }

        #region Commands

        protected void InsertData()
        {
            if (HelperVehicle.Motor.HubraumV.HasValue)
                HubraumV = HelperVehicle.Motor.HubraumV;

            if (HelperVehicle.Motor.BrennraumV.HasValue)
                BrennraumV = HelperVehicle.Motor.BrennraumV;

            if (HelperVehicle.Motor.BohrungD.HasValue)
                BohrungD = HelperVehicle.Motor.BohrungD;
        }

        private void Refresh_verdichtung()
        {
            if (HubraumV.HasValue && BrennraumV.HasValue && BohrungD.HasValue)
            {
                Derzeitige_verdichtung = engineLogic.Get_Verdichtung(
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
                AbdrehenLength = engineLogic.Get_Abdrehen_mm(
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

        #endregion Commands

        #region Values

        private ObservableCollection<VehiclesModel> _helperVehicles;

        public ObservableCollection<VehiclesModel> HelperVehicles
        {
            get => _helperVehicles;
            set { SetProperty(ref _helperVehicles, value); }
        }

        private VehiclesModel _helperVehicle;

        public VehiclesModel HelperVehicle
        {
            get => _helperVehicle;
            set { SetProperty(ref _helperVehicle, value); }
        }

        private UnitListItem _unitHubraumV;

        public UnitListItem UnitHubraumV
        {
            get => _unitHubraumV;
            set
            {
                HubraumV = Business.Functions.UpdateValue(HubraumV, _unitHubraumV, value);

                SetProperty(ref _unitHubraumV, value);
            }
        }

        private double? _hubraumV;

        public double? HubraumV
        {
            get => _hubraumV;
            set
            {
                SetProperty(ref _hubraumV, value);
                Refresh_verdichtung();
            }
        }

        private UnitListItem _unitBrennraum;

        public UnitListItem UnitBrennraumV
        {
            get => _unitBrennraum;
            set
            {
                BrennraumV = Business.Functions.UpdateValue(BrennraumV, _unitBrennraum, value);

                SetProperty(ref _unitBrennraum, value);
            }
        }

        private double? _brennraumV;

        public double? BrennraumV
        {
            get => _brennraumV;
            set
            {
                SetProperty(ref _brennraumV, value);
                Refresh_verdichtung();
            }
        }

        private UnitListItem _unitBohrungD;

        public UnitListItem UnitBohrungD
        {
            get => _unitBohrungD;
            set
            {
                BohrungD = Business.Functions.UpdateValue(BohrungD, _unitBohrungD, value);

                SetProperty(ref _unitBohrungD, value);
            }
        }

        private double? _bohrungD;

        public double? BohrungD
        {
            get => _bohrungD;
            set
            {
                SetProperty(ref _bohrungD, value);
                Refresh_verdichtung();
            }
        }

        private double? _derzeitige_verdichtung;

        public double? Derzeitige_verdichtung
        {
            get => _derzeitige_verdichtung;
            set { SetProperty(ref _derzeitige_verdichtung, value); }
        }

        private UnitListItem _unitAbdrehenLength;

        public UnitListItem UnitAbdrehenLength
        {
            get => _unitAbdrehenLength;
            set
            {
                AbdrehenLength = Business.Functions.UpdateValue(AbdrehenLength, _unitAbdrehenLength, value);

                SetProperty(ref _unitAbdrehenLength, value);
            }
        }

        private double? _abdrehenLength;

        public double? AbdrehenLength
        {
            get => _abdrehenLength;
            set { SetProperty(ref _abdrehenLength, value); }
        }

        private List<double?> _zielverdichtungen;

        public List<double?> Zielverdichtungen
        {
            get => _zielverdichtungen;
            set
            {
                SetProperty(ref _zielverdichtungen, value);
                Refresh_zielverdichtung();
            }
        }

        private double? _zielverdichtung;

        public double? Zielverdichtung
        {
            get => _zielverdichtung;
            set
            {
                SetProperty(ref _zielverdichtung, value);
                Refresh_zielverdichtung();
            }
        }

        #endregion Values
    }
}