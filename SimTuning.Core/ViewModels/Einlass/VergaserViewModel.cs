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

namespace SimTuning.Core.ViewModels.Einlass
{
    public class VergaserViewModel : MvxViewModel
    {
        private readonly EinlassLogic einlass;
        public ObservableCollection<UnitListItem> LengthQuantityUnits { get; }
        public ObservableCollection<UnitListItem> VolumeQuantityUnits { get; }

        public VergaserViewModel()
        {
            einlass = new EinlassLogic();

            VolumeQuantityUnits = new VolumeQuantity();
            LengthQuantityUnits = new LengthQuantity();

            UnitHubvolumen = VolumeQuantityUnits.Where(x => x.UnitEnumValue.Equals(VolumeUnit.CubicCentimeter)).First();
            UnitVergasergroeße = LengthQuantityUnits.Where(x => x.UnitEnumValue.Equals(LengthUnit.Millimeter)).First();
            UnitHauptdueseD = LengthQuantityUnits.Where(x => x.UnitEnumValue.Equals(LengthUnit.Micrometer)).First();

            using (var db = new DatabaseContext())
            {
                IList<VehiclesModel> vehicList = db.Vehicles
                    .Include(vehicles => vehicles.Motor)
                    .ToList();

                HelperVehicles = new ObservableCollection<VehiclesModel>(vehicList);
            }

            InsertDataCommand = new MvxCommand(InsertData);
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

        public void InsertData()
        {
            if (HelperVehicle.Motor.HubraumV.HasValue)
                Hubvolumen = HelperVehicle.Motor.HubraumV;

            if (HelperVehicle.Motor.ResonanzU.HasValue)
                Resonanzdrehzahl = HelperVehicle.Motor.ResonanzU;
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

        private UnitListItem _unitHauptdueseD;

        public UnitListItem UnitHauptdueseD
        {
            get => _unitHauptdueseD;
            set
            {
                HauptdueseD = Business.Functions.UpdateValue(HauptdueseD, UnitHauptdueseD, value);

                SetProperty(ref _unitHauptdueseD, value);
            }
        }

        private double? _hauptdueseD;

        public double? HauptdueseD
        {
            get => _hauptdueseD;
            set { SetProperty(ref _hauptdueseD, value); }
        }

        private double? _resonanzdrehzahl;

        public double? Resonanzdrehzahl
        {
            get => _resonanzdrehzahl;
            set
            {
                SetProperty(ref _resonanzdrehzahl, value);
                Refresh_Vergasergroeße();
            }
        }

        private UnitListItem _unitHubvolumen;

        public UnitListItem UnitHubvolumen
        {
            get => _unitHubvolumen;
            set
            {
                Hubvolumen = Business.Functions.UpdateValue(Hubvolumen, UnitHubvolumen, value);

                SetProperty(ref _unitHubvolumen, value);
            }
        }

        private double? _hubvolumen;

        public double? Hubvolumen
        {
            get => _hubvolumen;
            set
            {
                SetProperty(ref _hubvolumen, value);
                Refresh_Vergasergroeße();
            }
        }

        private UnitListItem _unitVergasergroeße;

        public UnitListItem UnitVergasergroeße
        {
            get => _unitVergasergroeße;
            set
            {
                Vergasergroeße = Business.Functions.UpdateValue(Vergasergroeße, UnitVergasergroeße, value);

                SetProperty(ref _unitVergasergroeße, value);
            }
        }

        private double? _vergasergroeße;

        public double? Vergasergroeße
        {
            get => _vergasergroeße;
            set
            {
                SetProperty(ref _vergasergroeße, value);
                Refresh_Hauptduesendurchmesser();
            }
        }

        #endregion Values
    }
}