// project=SimTuning.Core, file=AnwendungViewModel.cs, creation=2020:7:31
// Copyright (c) 2020 tuke productions. All rights reserved.
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
    /// <summary>
    /// Auslass-Anwendung-ViewModel.
    /// </summary>
    /// <seealso cref="MvvmCross.ViewModels.MvxNavigationViewModel" />
    public class AnwendungViewModel : MvxNavigationViewModel
    {
        /// <summary>
        /// Gets or sets the area quantity units.
        /// </summary>
        /// <value>
        /// The area quantity units.
        /// </value>
        public ObservableCollection<UnitListItem> AreaQuantityUnits { get; protected set; }

        /// <summary>
        /// Gets or sets the volume quantity units.
        /// </summary>
        /// <value>
        /// The volume quantity units.
        /// </value>
        public ObservableCollection<UnitListItem> VolumeQuantityUnits { get; protected set; }

        /// <summary>
        /// Gets or sets the length quantity units.
        /// </summary>
        /// <value>
        /// The length quantity units.
        /// </value>
        public ObservableCollection<UnitListItem> LengthQuantityUnits { get; protected set; }

        /// <summary>
        /// Gets or sets the speed quantity units.
        /// </summary>
        /// <value>
        /// The speed quantity units.
        /// </value>
        public ObservableCollection<UnitListItem> SpeedQuantityUnits { get; protected set; }

        /// <summary>
        /// Gets the difference stages.
        /// </summary>
        /// <value>
        /// The difference stages.
        /// </value>
        public List<string> DiffStages { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AnwendungViewModel"/> class.
        /// </summary>
        /// <param name="logProvider">The log provider.</param>
        /// <param name="navigationService">The navigation service.</param>
        public AnwendungViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService)
            : base(logProvider, navigationService)
        {
        }

        #region Methods

        /// <summary>
        /// Prepares this instance.
        /// called after construction.
        /// </summary>
        public override void Prepare()
        {
        }

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        /// <returns>Initilisierung.</returns>
        public override Task Initialize()
        {
            this.Vehicle = new VehiclesModel();
            this.Vehicle.Motor = new MotorModel();
            this.Vehicle.Motor.Auslass = new AuslassModel();
            this.Vehicle.Motor.Auslass.Auspuff = new AuspuffModel();

            this.AreaQuantityUnits = new AreaQuantity();
            this.VolumeQuantityUnits = new VolumeQuantity();
            this.LengthQuantityUnits = new LengthQuantity();
            this.SpeedQuantityUnits = new SpeedQuantity();

            this.UnitSchallG = this.SpeedQuantityUnits.Where(x => x.UnitEnumValue.Equals(SpeedUnit.MeterPerSecond)).First();
            this.UnitAuslassF = this.AreaQuantityUnits.Where(x => x.UnitEnumValue.Equals(AreaUnit.SquareMillimeter)).First();
            this.UnitAuslassD = this.LengthQuantityUnits.Where(x => x.UnitEnumValue.Equals(LengthUnit.Millimeter)).First();
            this.UnitAuslassL = this.LengthQuantityUnits.Where(x => x.UnitEnumValue.Equals(LengthUnit.Millimeter)).First();
            this.UnitEndrohrD = this.LengthQuantityUnits.Where(x => x.UnitEnumValue.Equals(LengthUnit.Millimeter)).First();
            this.UnitEndrohrL = this.LengthQuantityUnits.Where(x => x.UnitEnumValue.Equals(LengthUnit.Millimeter)).First();

            using (var db = new DatabaseContext())
            {
                IList<VehiclesModel> vehicList = db.Vehicles
                    .Include(vehicles => vehicles.Motor)
                    .Include(vehicles => vehicles.Motor.Auslass)
                    .ToList();

                this.HelperVehicles = new ObservableCollection<VehiclesModel>(vehicList);
            }

            this.DiffStages = new List<string>() { "One Stage", "Two Stage", "Three Stage" };

            //Commands
            this.InsertDataCommand = new MvxCommand(InsertData);
            this.DiffusorStageCommand = new MvxCommand<string>(DiffusorStage);

            return base.Initialize();
        }

        /// <summary>
        /// Fügt Vehicle-Helper ein.
        /// </summary>
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

        /// <summary>
        /// Diffusors the stage.
        /// </summary>
        /// <param name="obj">The object.</param>
        public void DiffusorStage(object obj)
        {
            this.Vehicle.Motor.Auslass.Auspuff.DiffusorStage = Convert.ToInt32(obj);
        }

        /// <summary>
        /// Berechnet den Auspuff.
        /// </summary>
        /// <returns>Auspuff-Bild als Stream.</returns>
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
            Stream stream = SimTuning.Core.Business.Converts.SKBitmapToStream(AuslassLogic.Auspuff(ref vehicle));
            this.Vehicle = vehicle;
            this.RaisePropertyChanged("Vehicle");

            return stream;
        }

        #endregion Methods

        #region Values

        #region Commands

        /// <summary>
        /// Gets or sets the calculate command.
        /// </summary>
        /// <value>
        /// The calculate command.
        /// </value>
        public IMvxAsyncCommand CalculateCommand { get; set; }

        /// <summary>
        /// Gets or sets the diffusor stage command.
        /// </summary>
        /// <value>
        /// The diffusor stage command.
        /// </value>
        public IMvxCommand DiffusorStageCommand { get; set; }

        /// <summary>
        /// Gets or sets the insert data command.
        /// </summary>
        /// <value>
        /// The insert data command.
        /// </value>
        public IMvxCommand InsertDataCommand { get; set; }

        #endregion Commands

        #region Hilfsdaten

        private ObservableCollection<VehiclesModel> _helperVehicles;

        public ObservableCollection<VehiclesModel> HelperVehicles
        {
            get => this._helperVehicles;
            set { this.SetProperty(ref this._helperVehicles, value); }
        }

        private VehiclesModel _helperVehicle;

        public VehiclesModel HelperVehicle
        {
            get => this._helperVehicle;
            set { this.SetProperty(ref this._helperVehicle, value); }
        }

        #endregion Hilfsdaten

        private VehiclesModel _vehicle;

        public VehiclesModel Vehicle
        {
            get => this._vehicle;
            set { this.SetProperty(ref this._vehicle, value); }
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