using Data;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using SimTuning.Core.Models;
using SimTuning.Core.ModuleLogic;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using UnitsNet.Units;
using WooCommerceNET.WooCommerce.v3;

namespace SimTuning.Core.ViewModels.Auslass
{
    public class AnwendungViewModel : MvxNavigationViewModel
    {
        protected AuslassLogic auslass;
        public ObservableCollection<UnitListItem> AreaQuantityUnits { get; protected set; }
        public ObservableCollection<UnitListItem> VolumeQuantityUnits { get; protected set; }
        public ObservableCollection<UnitListItem> LengthQuantityUnits { get; protected set; }
        public ObservableCollection<UnitListItem> SpeedQuantityUnits { get; protected set; }
        public List<string> DiffStages { get; private set; }

        public AnwendungViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService) : base(logProvider, navigationService)
        {
        }

        public override void Prepare()
        {
            // This is the first method to be called after construction
        }

        public override Task Initialize()
        {
            // Async initialization

            auslass = new AuslassLogic();

            Vehicle = new VehiclesModel();
            Vehicle.Motor = new MotorModel();
            Vehicle.Motor.Auslass = new AuslassModel();
            Vehicle.Motor.Auslass.Auspuff = new AuspuffModel();

            AreaQuantityUnits = new AreaQuantity();
            VolumeQuantityUnits = new VolumeQuantity();
            LengthQuantityUnits = new LengthQuantity();
            SpeedQuantityUnits = new SpeedQuantity();

            UnitSchallG = SpeedQuantityUnits.Where(x => x.UnitEnumValue.Equals(SpeedUnit.MeterPerSecond)).First();
            UnitAuslassF = AreaQuantityUnits.Where(x => x.UnitEnumValue.Equals(AreaUnit.SquareMillimeter)).First();
            UnitAuslassD = LengthQuantityUnits.Where(x => x.UnitEnumValue.Equals(LengthUnit.Millimeter)).First();
            UnitAuslassL = LengthQuantityUnits.Where(x => x.UnitEnumValue.Equals(LengthUnit.Millimeter)).First();
            UnitEndrohrD = LengthQuantityUnits.Where(x => x.UnitEnumValue.Equals(LengthUnit.Millimeter)).First();
            UnitEndrohrL = LengthQuantityUnits.Where(x => x.UnitEnumValue.Equals(LengthUnit.Millimeter)).First();

            using (var db = new DatabaseContext())
            {
                IList<VehiclesModel> vehicList = db.Vehicles
                    .Include(vehicles => vehicles.Motor)
                    .Include(vehicles => vehicles.Motor.Auslass)
                    .ToList();

                HelperVehicles = new ObservableCollection<VehiclesModel>(vehicList);
            }

            DiffStages = new List<string>() { "One Stage", "Two Stage", "Three Stage" };

            //Commands
            InsertDataCommand = new MvxCommand(InsertData);
            DiffusorStageCommand = new MvxCommand<string>(DiffusorStage);

            return base.Initialize();
        }

        public IMvxCommand CalculateCommand { get; set; }
        public IMvxCommand DiffusorStageCommand { get; set; }
        public IMvxCommand InsertDataCommand { get; set; }

        #region Commands

        public void InsertData()
        {
            if (HelperVehicle.Motor.Auslass.FlaecheA.HasValue)
                Vehicle.Motor.Auslass.FlaecheA = UnitsNet.UnitConverter.Convert(
                   HelperVehicle.Motor.Auslass.FlaecheA.Value,
                   AreaUnit.SquareMillimeter,
                   UnitAuslassF.UnitEnumValue);

            if (HelperVehicle.Motor.ResonanzU.HasValue)
                Vehicle.Motor.ResonanzU = HelperVehicle.Motor.ResonanzU;

            if (HelperVehicle.Motor.Auslass.SteuerzeitSZ.HasValue)
                Vehicle.Motor.Auslass.SteuerzeitSZ = HelperVehicle.Motor.Auslass.SteuerzeitSZ;
        }

        public void DiffusorStage(object obj)
        {
            Vehicle.Motor.Auslass.Auspuff.DiffusorStage = Convert.ToInt32(obj);
        }

        protected virtual Stream Calculate()
        {
            //Convert
            Vehicle.Motor.Auslass.Auspuff.AbgasV = UnitsNet.UnitConverter.Convert(
                   Vehicle.Motor.Auslass.Auspuff.AbgasV.Value,
                  UnitSchallG.UnitEnumValue,
                  SpeedUnit.MeterPerSecond);

            Vehicle.Motor.Auslass.FlaecheA = UnitsNet.UnitConverter.Convert(
                 Vehicle.Motor.Auslass.FlaecheA.Value,
                UnitAuslassF.UnitEnumValue,
                AreaUnit.SquareMillimeter);

            Vehicle.Motor.Auslass.DurchmesserD = UnitsNet.UnitConverter.Convert(
                    Vehicle.Motor.Auslass.DurchmesserD.Value,
                    UnitAuslassD.UnitEnumValue,
                    LengthUnit.Millimeter);

            Vehicle.Motor.Auslass.LaengeL = UnitsNet.UnitConverter.Convert(
                   Vehicle.Motor.Auslass.LaengeL.Value,
                   UnitAuslassL.UnitEnumValue,
                   LengthUnit.Millimeter);

            Vehicle.Motor.Auslass.Auspuff.EndrohrD = UnitsNet.UnitConverter.Convert(
                 Vehicle.Motor.Auslass.Auspuff.EndrohrD.Value,
                 UnitEndrohrD.UnitEnumValue,
                 LengthUnit.Millimeter);

            Vehicle.Motor.Auslass.Auspuff.EndrohrL = UnitsNet.UnitConverter.Convert(
                 Vehicle.Motor.Auslass.Auspuff.EndrohrL.Value,
                 UnitEndrohrL.UnitEnumValue,
                 LengthUnit.Millimeter);

            VehiclesModel vehicle = Vehicle;
            Stream stream = SimTuning.Core.Business.Converts.SKBitmapToStream(auslass.Auspuff(ref vehicle));
            Vehicle = vehicle;
            RaisePropertyChanged("Vehicle");

            return stream;
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

        private VehiclesModel _vehicle;

        public VehiclesModel Vehicle
        {
            get => _vehicle;
            set { SetProperty(ref _vehicle, value); }
        }

        #region Units

        private UnitListItem _unitAuslassD;

        public UnitListItem UnitAuslassD
        {
            get => _unitAuslassD;
            set
            {
                Vehicle.Motor.Auslass.DurchmesserD = Business.Functions.UpdateValue(Vehicle.Motor.Auslass.DurchmesserD, UnitAuslassD, value);
                RaisePropertyChanged(() => Vehicle);

                SetProperty(ref _unitAuslassD, value);
            }
        }

        private UnitListItem _unitSchallG;

        public UnitListItem UnitSchallG
        {
            get => _unitSchallG;
            set
            {
                Vehicle.Motor.Auslass.Auspuff.AbgasV = Business.Functions.UpdateValue(Vehicle.Motor.Auslass.Auspuff.AbgasV, UnitSchallG, value);
                RaisePropertyChanged(() => Vehicle);

                SetProperty(ref _unitSchallG, value);
            }
        }

        private UnitListItem _unitAuslassL;

        public UnitListItem UnitAuslassL
        {
            get => _unitAuslassL;
            set
            {
                Vehicle.Motor.Auslass.LaengeL = Business.Functions.UpdateValue(Vehicle.Motor.Auslass.LaengeL, UnitAuslassL, value);
                RaisePropertyChanged(() => Vehicle);

                SetProperty(ref _unitAuslassL, value);
            }
        }

        private UnitListItem _unitAuslassF;

        public UnitListItem UnitAuslassF
        {
            get => _unitAuslassF;
            set
            {
                Vehicle.Motor.Auslass.FlaecheA = Business.Functions.UpdateValue(Vehicle.Motor.Auslass.FlaecheA, UnitAuslassF, value);
                RaisePropertyChanged(() => Vehicle);

                SetProperty(ref _unitAuslassF, value);
            }
        }

        private UnitListItem _unitEndrohrD;

        public UnitListItem UnitEndrohrD
        {
            get => _unitEndrohrD;
            set
            {
                Vehicle.Motor.Auslass.Auspuff.EndrohrD = Business.Functions.UpdateValue(Vehicle.Motor.Auslass.Auspuff.EndrohrD, UnitEndrohrD, value);
                RaisePropertyChanged(() => Vehicle);

                SetProperty(ref _unitEndrohrD, value);
            }
        }

        private UnitListItem _unitEndrohrL;

        public UnitListItem UnitEndrohrL
        {
            get => _unitEndrohrL;
            set
            {
                Vehicle.Motor.Auslass.Auspuff.EndrohrL = Business.Functions.UpdateValue(Vehicle.Motor.Auslass.Auspuff.EndrohrL, UnitEndrohrL, value);
                RaisePropertyChanged(() => Vehicle);

                SetProperty(ref _unitEndrohrL, value);
            }
        }

        #endregion Units

        #endregion Values
    }
}