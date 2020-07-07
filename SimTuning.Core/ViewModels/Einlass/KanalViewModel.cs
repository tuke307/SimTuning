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
    public class KanalViewModel : MvxViewModel
    {
        private readonly EinlassLogic einlass;
        public ObservableCollection<UnitListItem> AreaQuantityUnits { get; }
        public ObservableCollection<UnitListItem> VolumeQuantityUnits { get; }
        public ObservableCollection<UnitListItem> LengthQuantityUnits { get; }

        public KanalViewModel()
        {
            einlass = new EinlassLogic();

            AreaQuantityUnits = new AreaQuantity();
            VolumeQuantityUnits = new VolumeQuantity();
            LengthQuantityUnits = new LengthQuantity();

            //Vorausgewählte Einheiten
            UnitEinlassA = AreaQuantityUnits.Where(x => x.UnitEnumValue.Equals(AreaUnit.SquareMillimeter)).First();
            UnitKurbelgehauseV = VolumeQuantityUnits.Where(x => x.UnitEnumValue.Equals(VolumeUnit.CubicCentimeter)).First();
            UnitAnsaugleitungD = LengthQuantityUnits.Where(x => x.UnitEnumValue.Equals(LengthUnit.Centimeter)).First();
            UnitResonanzlaenge = LengthQuantityUnits.Where(x => x.UnitEnumValue.Equals(LengthUnit.Centimeter)).First();

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
            if (HelperVehicle.Motor.Einlass.FlaecheA.HasValue)
                EinlassA = HelperVehicle.Motor.Einlass.FlaecheA;

            if (HelperVehicle.Motor.Einlass.SteuerzeitSZ.HasValue)
                Einlasssteuerwinkel = HelperVehicle.Motor.Einlass.SteuerzeitSZ;

            if (HelperVehicle.Motor.ResonanzU.HasValue)
                Resonanzdrehzahl = HelperVehicle.Motor.ResonanzU;

            if (HelperVehicle.Motor.KurbelgehaeuseV.HasValue)
                KurbelgehauseV = HelperVehicle.Motor.KurbelgehaeuseV;

            if (HelperVehicle.Motor.Einlass.LaengeL.HasValue)
                AnsaugleitungD = HelperVehicle.Motor.Einlass.LaengeL;
        }

        private void RefreshResonanzlaenge()
        {
            if (EinlassA.HasValue && Einlasssteuerwinkel.HasValue && KurbelgehauseV.HasValue && Resonanzdrehzahl.HasValue && AnsaugleitungD.HasValue)
            {
                Resonanzlaenge = einlass.Get_Resonanzlaenge(
                    UnitsNet.UnitConverter.Convert(EinlassA.Value, UnitEinlassA.UnitEnumValue, AreaUnit.SquareCentimeter),
                    Einlasssteuerwinkel.Value,
                    UnitsNet.UnitConverter.Convert(KurbelgehauseV.Value, UnitKurbelgehauseV.UnitEnumValue, VolumeUnit.CubicCentimeter),
                    Resonanzdrehzahl.Value,
                    UnitsNet.UnitConverter.Convert(AnsaugleitungD.Value, UnitAnsaugleitungD.UnitEnumValue, LengthUnit.Centimeter));
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

        private UnitListItem _unitResonanzlaenge;

        public UnitListItem UnitResonanzlaenge
        {
            get => _unitResonanzlaenge;
            set
            {
                Resonanzlaenge = Business.Functions.UpdateValue(Resonanzlaenge, _unitResonanzlaenge, value);

                SetProperty(ref _unitResonanzlaenge, value);
            }
        }

        private double? _resonanzlaenge;

        public double? Resonanzlaenge
        {
            get => _resonanzlaenge;
            set { SetProperty(ref _resonanzlaenge, value); }
        }

        private UnitListItem _unitEinlassA;

        public UnitListItem UnitEinlassA
        {
            get => _unitEinlassA;
            set
            {
                EinlassA = Business.Functions.UpdateValue(EinlassA, UnitEinlassA, value);

                SetProperty(ref _unitEinlassA, value);
            }
        }

        private double? _einlassA;

        public double? EinlassA
        {
            get => _einlassA;
            set
            {
                SetProperty(ref _einlassA, value);
                RefreshResonanzlaenge();
            }
        }

        private double? _einlasssteuerwinkel;

        public double? Einlasssteuerwinkel
        {
            get => _einlasssteuerwinkel;
            set
            {
                SetProperty(ref _einlasssteuerwinkel, value);
                RefreshResonanzlaenge();
            }
        }

        private double? _resonanzdrehzahl;

        public double? Resonanzdrehzahl
        {
            get => _resonanzdrehzahl;
            set
            {
                SetProperty(ref _resonanzdrehzahl, value);
                RefreshResonanzlaenge();
            }
        }

        private UnitListItem _unitKurbelgehauseV;

        public UnitListItem UnitKurbelgehauseV
        {
            get => _unitKurbelgehauseV;
            set
            {
                KurbelgehauseV = Business.Functions.UpdateValue(KurbelgehauseV, _unitKurbelgehauseV, value);

                SetProperty(ref _unitKurbelgehauseV, value);
            }
        }

        private double? _kurbelgehauseV;

        public double? KurbelgehauseV
        {
            get => _kurbelgehauseV;
            set
            {
                SetProperty(ref _kurbelgehauseV, value);
                RefreshResonanzlaenge();
            }
        }

        private UnitListItem _unitAnsaugleitungD;

        public UnitListItem UnitAnsaugleitungD
        {
            get => _unitAnsaugleitungD;
            set
            {
                AnsaugleitungD = Business.Functions.UpdateValue(AnsaugleitungD, _unitAnsaugleitungD, value);

                SetProperty(ref _unitAnsaugleitungD, value);
            }
        }

        private double? _ansaugleitungD;

        public double? AnsaugleitungD
        {
            get => _ansaugleitungD;
            set
            {
                SetProperty(ref _ansaugleitungD, value);
                RefreshResonanzlaenge();
            }
        }

        #endregion Values
    }
}