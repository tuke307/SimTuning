using Data;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using SimTuning.Models;
using SimTuning.ModuleLogic;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows.Input;
using UnitsNet.Units;

namespace SimTuning.ViewModels.Auslass
{
    public class AnwendungViewModel : BaseViewModel
    {
        protected readonly AuslassLogic auslass;
        public ObservableCollection<UnitListItem> AreaQuantityUnits { get; }
        public ObservableCollection<UnitListItem> VolumeQuantityUnits { get; }
        public ObservableCollection<UnitListItem> LengthQuantityUnits { get; }
        public ObservableCollection<UnitListItem> SpeedQuantityUnits { get; }
        public List<string> DiffStages { get; private set; }

        public AnwendungViewModel()
        {
            auslass = new AuslassLogic();

            Vehicle = new VehiclesModel();
            Vehicle.Motor = new MotorModel();
            Vehicle.Motor.Auslass = new AuslassModel();
            Vehicle.Motor.Auslass.Auspuff = new AuspuffModel();

            //InputExhaust = new SimTuning.Models.InputExhaustModel();
            //OutputExhaust = new SimTuning.Models.OutputExhaustModel();

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
        }

        public ICommand CalculateCommand { get; set; }
        public ICommand DiffusorStageCommand { get; set; }
        public ICommand InsertDataCommand { get; set; }

        public void InsertData(object obj)
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

        public ObservableCollection<VehiclesModel> HelperVehicles
        {
            get => Get<ObservableCollection<VehiclesModel>>();
            set => Set(value);
        }

        public VehiclesModel HelperVehicle
        {
            get => Get<VehiclesModel>();
            set => Set(value);
        }

        public VehiclesModel Vehicle
        {
            get => Get<VehiclesModel>();
            set => Set(value);
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
            Stream stream = SimTuning.Business.Converts.SKBitmapToStream(auslass.Auspuff(ref vehicle));
            Vehicle = vehicle;
            OnPropertyChanged("Vehicle");

            return stream;
            //SimTuning.Models.OutputExhaustModel _OutputExhaust;
            //Stream stream = SimTuning.Business.Converts.SKBitmapToStream(auslass.Auspuff(InputExhaust, out _OutputExhaust));
            //OutputExhaust = _OutputExhaust;
            //PngBitmapDecoder decoder = new PngBitmapDecoder(stream, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.Default);
            //Auspuff = decoder.Frames[0];
        }

        public UnitListItem UnitAuslassD
        {
            get => Get<UnitListItem>();
            set
            {
                Vehicle.Motor.Auslass.DurchmesserD = Business.Functions.UpdateValue(Vehicle.Motor.Auslass.DurchmesserD, UnitAuslassD, value);
                OnPropertyChanged("InputExhaust");

                Set(value);
            }
        }

        public UnitListItem UnitSchallG
        {
            get => Get<UnitListItem>();
            set
            {
                Vehicle.Motor.Auslass.Auspuff.AbgasV = Business.Functions.UpdateValue(Vehicle.Motor.Auslass.Auspuff.AbgasV, UnitSchallG, value);
                OnPropertyChanged("InputExhaust");

                Set(value);
            }
        }

        public UnitListItem UnitAuslassL
        {
            get => Get<UnitListItem>();
            set
            {
                Vehicle.Motor.Auslass.LaengeL = Business.Functions.UpdateValue(Vehicle.Motor.Auslass.LaengeL, UnitAuslassL, value);
                OnPropertyChanged("InputExhaust");

                Set(value);
            }
        }

        public UnitListItem UnitAuslassF
        {
            get => Get<UnitListItem>();
            set
            {
                Vehicle.Motor.Auslass.FlaecheA = Business.Functions.UpdateValue(Vehicle.Motor.Auslass.FlaecheA, UnitAuslassF, value);
                OnPropertyChanged("InputExhaust");

                Set(value);
            }
        }

        public UnitListItem UnitEndrohrD
        {
            get => Get<UnitListItem>();
            set
            {
                Vehicle.Motor.Auslass.Auspuff.EndrohrD = Business.Functions.UpdateValue(Vehicle.Motor.Auslass.Auspuff.EndrohrD, UnitEndrohrD, value);
                OnPropertyChanged("InputExhaust");

                Set(value);
            }
        }

        public UnitListItem UnitEndrohrL
        {
            get => Get<UnitListItem>();
            set
            {
                Vehicle.Motor.Auslass.Auspuff.EndrohrL = Business.Functions.UpdateValue(Vehicle.Motor.Auslass.Auspuff.EndrohrL, UnitEndrohrL, value);
                OnPropertyChanged("InputExhaust");

                Set(value);
            }
        }

        //public SimTuning.Models.InputExhaustModel InputExhaust
        //{
        //    get => Get<SimTuning.Models.InputExhaustModel>();
        //    set => Set(value);
        //}

        //public SimTuning.Models.OutputExhaustModel OutputExhaust
        //{
        //    get => Get<SimTuning.Models.OutputExhaustModel>();
        //    set => Set(value);
        //}
    }
}