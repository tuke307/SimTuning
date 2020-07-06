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

namespace SimTuning.Core.ViewModels.Auslass
{
    public class TheorieViewModel : MvxViewModel
    {
        private readonly AuslassLogic auslass;
        public ObservableCollection<UnitListItem> AreaQuantityUnits { get; }
        public ObservableCollection<UnitListItem> LengthQuantityUnits { get; }
        public ObservableCollection<UnitListItem> SpeedQuantityUnits { get; }

        public TheorieViewModel()
        {
            auslass = new AuslassLogic();

            AreaQuantityUnits = new AreaQuantity();
            LengthQuantityUnits = new LengthQuantity();
            SpeedQuantityUnits = new SpeedQuantity();

            UnitAbgasV = SpeedQuantityUnits.Where(x => x.UnitEnumValue.Equals(SpeedUnit.MeterPerSecond)).First();
            UnitAuslassA = AreaQuantityUnits.Where(x => x.UnitEnumValue.Equals(AreaUnit.SquareCentimeter)).First();
            UnitKruemmerD = LengthQuantityUnits.Where(x => x.UnitEnumValue.Equals(LengthUnit.Millimeter)).First();
            UnitKruemmerL = LengthQuantityUnits.Where(x => x.UnitEnumValue.Equals(LengthUnit.Centimeter)).First();
            UnitResonanzL = LengthQuantityUnits.Where(x => x.UnitEnumValue.Equals(LengthUnit.Centimeter)).First();

            using (var db = new DatabaseContext())
            {
                IList<VehiclesModel> vehicList = db.Vehicles
                    .Include(vehicles => vehicles.Motor)
                    .Include(vehicles => vehicles.Motor.Auslass)
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

        protected virtual void InsertData()
        {
            if (HelperVehicle.Motor.Auslass.FlaecheA.HasValue)
                AuslassA = HelperVehicle.Motor.Auslass.FlaecheA;

            if (HelperVehicle.Motor.ResonanzU.HasValue)
                ResonanzDrehzahl = HelperVehicle.Motor.ResonanzU;

            if (HelperVehicle.Motor.Auslass.SteuerzeitSZ.HasValue)
                AusslassSteuerwinkel = HelperVehicle.Motor.Auslass.SteuerzeitSZ;
        }

        private void Refresh_KruemmerL()
        {
            if (KruemmerD.HasValue)
            {
                if (!DrehmomentF.HasValue)
                    DrehmomentF = 9;

                KruemmerL = auslass.Get_Kruemmerlaenge(
                     UnitsNet.UnitConverter.Convert(KruemmerD.Value,
                     UnitKruemmerD.UnitEnumValue,
                     LengthUnit.Millimeter),

                     DrehmomentF.Value,

                     0);
            }
        }

        private void Refresh_KruemmerD()
        {
            if (AuslassA.HasValue)
                KruemmerSpannneD =
                    auslass.Get_KruemmerDurchmesser(
                    UnitsNet.UnitConverter.Convert(AuslassA.Value,
                    UnitAuslassA.UnitEnumValue,
                    AreaUnit.SquareCentimeter), 10)
                    +
                    " - "
                    +
                    auslass.Get_KruemmerDurchmesser(
                    UnitsNet.UnitConverter.Convert(AuslassA.Value,
                    UnitAuslassA.UnitEnumValue,
                    AreaUnit.SquareCentimeter), 20);
        }

        private void Refresh_AuspuffGeschwindigkeit()
        {
            if (ModAbgasT.HasValue)
                AbgasV = auslass.Get_Abgasgeschwindigkeit(ModAbgasT.Value);
        }

        private void Refresh_Resonanzlaenge()
        {
            if (AusslassSteuerwinkel.HasValue && AbgasT.HasValue && ResonanzDrehzahl.HasValue)
                ResonanzL = auslass.Get_Resonanzlaenge(AusslassSteuerwinkel.Value, AbgasT.Value, ResonanzDrehzahl.Value);
        }

        #endregion Commands

        #region Values

        #region Hilfsdaten

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

        #endregion Hilfsdaten

        private string _kruemmerSpannneD;

        public string KruemmerSpannneD
        {
            get => _kruemmerSpannneD;
            set { SetProperty(ref _kruemmerSpannneD, value); }
        }

        private UnitListItem _unitAuslassA;

        public UnitListItem UnitAuslassA
        {
            get => _unitAuslassA;
            set
            {
                AuslassA = Business.Functions.UpdateValue(AuslassA, _unitAuslassA, value);

                SetProperty(ref _unitAuslassA, value);
            }
        }

        private double? _auslassA;

        public double? AuslassA
        {
            get => _auslassA;
            set { SetProperty(ref _auslassA, value); }
        }

        private UnitListItem _unitKruemmerD;

        public UnitListItem UnitKruemmerD
        {
            get => _unitKruemmerD;
            set
            {
                KruemmerD = Business.Functions.UpdateValue(KruemmerD, _unitKruemmerD, value);

                SetProperty(ref _unitKruemmerD, value);
            }
        }

        private double? _kruemmerD;

        public double? KruemmerD
        {
            get => _kruemmerD;
            set
            {
                SetProperty(ref _kruemmerD, value);
                Refresh_KruemmerL();
            }
        }

        private double? _drehmomentF;

        public double? DrehmomentF
        {
            get => _drehmomentF;
            set
            {
                SetProperty(ref _drehmomentF, value);
                Refresh_KruemmerL();
            }
        }

        private UnitListItem _unitKruemmerL;

        public UnitListItem UnitKruemmerL
        {
            get => _unitKruemmerL;
            set
            {
                KruemmerL = Business.Functions.UpdateValue(KruemmerL, _unitKruemmerL, value);

                SetProperty(ref _unitKruemmerL, value);
            }
        }

        private double? _kruemmerL;

        public double? KruemmerL
        {
            get => _kruemmerL;
            set { SetProperty(ref _kruemmerL, value); }
        }

        private double? _modAbgasT;

        public double? ModAbgasT
        {
            get => _modAbgasT;
            set
            {
                SetProperty(ref _modAbgasT, value);
                Refresh_AuspuffGeschwindigkeit();
            }
        }

        private UnitListItem _unitAbgasV;

        public UnitListItem UnitAbgasV
        {
            get => _unitAbgasV;
            set
            {
                AbgasV = Business.Functions.UpdateValue(AbgasV, _unitAbgasV, value);

                SetProperty(ref _unitAbgasV, value);
            }
        }

        private double? _abgasV;

        public double? AbgasV
        {
            get => _abgasV;
            set { SetProperty(ref _abgasV, value); }
        }

        private double? _abgasT;

        public double? AbgasT
        {
            get => _abgasT;
            set
            {
                SetProperty(ref _abgasT, value);
                Refresh_Resonanzlaenge();
            }
        }

        private double? _ausslassSteuerwinkel;

        public double? AusslassSteuerwinkel
        {
            get => _ausslassSteuerwinkel;
            set
            {
                SetProperty(ref _ausslassSteuerwinkel, value);
                Refresh_Resonanzlaenge();
            }
        }

        private double? _resonanzDrehzahl;

        public double? ResonanzDrehzahl
        {
            get => _resonanzDrehzahl;
            set
            {
                SetProperty(ref _resonanzDrehzahl, value);
                Refresh_Resonanzlaenge();
            }
        }

        private UnitListItem _unitResonanzL;

        public UnitListItem UnitResonanzL
        {
            get => _unitAuslassA;
            set
            {
                ResonanzL = Business.Functions.UpdateValue(ResonanzL, _unitResonanzL, value);

                SetProperty(ref _unitResonanzL, value);
            }
        }

        private double? _resonanzL;

        public double? ResonanzL
        {
            get => _resonanzL;
            set { SetProperty(ref _resonanzL, value); }
        }

        #endregion Values
    }
}