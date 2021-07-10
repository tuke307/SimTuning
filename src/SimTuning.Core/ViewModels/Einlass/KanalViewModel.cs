﻿// project=SimTuning.Core, file=KanalViewModel.cs, creation=2020:7:31 Copyright (c) 2021
// tuke productions. All rights reserved.
namespace SimTuning.Core.ViewModels.Einlass
{
    using Data;
    using Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;
    using MvvmCross.Commands;
    using MvvmCross.Navigation;
    using MvvmCross.ViewModels;
    using SimTuning.Core.Models;
    using SimTuning.Core.ModuleLogic;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Threading.Tasks;
    using UnitsNet.Units;

    /// <summary>
    /// KanalViewModel.
    /// </summary>
    /// <seealso cref="MvvmCross.ViewModels.MvxNavigationViewModel" />
    public class KanalViewModel : MvxNavigationViewModel
    {
        public KanalViewModel(ILoggerFactory logFactory, IMvxNavigationService navigationService)
            : base(logFactory, navigationService)
        {
            this.AreaQuantityUnits = new AreaQuantity();
            this.VolumeQuantityUnits = new VolumeQuantity();
            this.LengthQuantityUnits = new LengthQuantity();

            // Vehicle Creation
            this.Vehicle = new VehiclesModel();
            this.Vehicle.Motor = new MotorModel();
            this.Vehicle.Motor.Einlass = new EinlassModel();

            this.UnitResonanzlaenge = this.LengthQuantityUnits.Where(x => x.UnitEnumValue.Equals(LengthUnit.Millimeter)).First();

            using (var db = new DatabaseContext())
            {
                IList<VehiclesModel> vehicList = db.Vehicles
                    .Include(vehicles => vehicles.Motor)
                    .Include(vehicles => vehicles.Motor.Einlass)
                    .Include(vehicles => vehicles.Motor.Auslass)
                    .Include(vehicles => vehicles.Motor.Ueberstroemer)
                    .ToList();

                this.HelperVehicles = new ObservableCollection<VehiclesModel>(vehicList);
            }

            this.InsertDataCommand = new MvxCommand(this.InsertData);
        }

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        /// <returns>Initilisierung.</returns>
        public override Task Initialize()
        {
            return base.Initialize();
        }

        /// <summary>
        /// Prepares this instance. called after construction.
        /// </summary>
        public override void Prepare()
        {
        }

        #region Methods

        /// <summary>
        /// Inserts the data.
        /// </summary>
        private void InsertData()
        {
            if (this.HelperVehicle.Motor.Einlass.FlaecheA.HasValue)
            {
                this.VehicleMotorEinlassFlaecheA = this.HelperVehicle.Motor.Einlass.FlaecheA;
                this.RaisePropertyChanged(() => this.VehicleMotorEinlassFlaecheA);
            }

            if (this.HelperVehicle.Motor.Einlass.SteuerzeitSZ.HasValue)
            {
                this.Einlasssteuerwinkel = this.HelperVehicle.Motor.Einlass.SteuerzeitSZ;
                this.RaisePropertyChanged(() => this.Einlasssteuerwinkel);
            }

            if (this.HelperVehicle.Motor.ResonanzU.HasValue)
            {
                this.VehicleMotorResonanzU = this.HelperVehicle.Motor.ResonanzU;
                this.RaisePropertyChanged(() => this.VehicleMotorResonanzU);
            }

            if (this.HelperVehicle.Motor.KurbelgehaeuseV.HasValue)
            {
                this.VehicleMotorKurbelgehaeuseV = this.HelperVehicle.Motor.KurbelgehaeuseV;
                this.RaisePropertyChanged(() => this.VehicleMotorKurbelgehaeuseV);
            }

            if (this.HelperVehicle.Motor.Einlass.DurchmesserD.HasValue)
            {
                this.VehicleMotorEinlassDurchmesserD = this.HelperVehicle.Motor.Einlass.DurchmesserD;
                this.RaisePropertyChanged(() => this.VehicleMotorEinlassDurchmesserD);
            }
        }

        private void RefreshResonanzlaenge()
        {
            if (this.VehicleMotorEinlassFlaecheA.HasValue && this.Einlasssteuerwinkel.HasValue && this.VehicleMotorKurbelgehaeuseV.HasValue && this.VehicleMotorResonanzU.HasValue && this.VehicleMotorEinlassDurchmesserD.HasValue)
            {
                this.Resonanzlaenge = EinlassLogic.GetResonanzLaenge(
                    UnitsNet.UnitConverter.Convert(this.VehicleMotorEinlassFlaecheA.Value, this.VehicleMotorEinlassFlaecheAUnit.UnitEnumValue, AreaUnit.SquareCentimeter),
                    this.Einlasssteuerwinkel.Value,
                    UnitsNet.UnitConverter.Convert(this.VehicleMotorKurbelgehaeuseV.Value, this.VehicleMotorKurbelgehaeuseVUnit.UnitEnumValue, VolumeUnit.CubicCentimeter),
                    this.VehicleMotorResonanzU.Value,
                    UnitsNet.UnitConverter.Convert(this.VehicleMotorEinlassDurchmesserD.Value, this.VehicleMotorEinlassDurchmesserDUnit.UnitEnumValue, LengthUnit.Centimeter));
            }
        }

        #endregion Methods

        #region Values

        private double? _einlasssteuerwinkel;

        private VehiclesModel _helperVehicle;

        private ObservableCollection<VehiclesModel> _helperVehicles;

        private double? _resonanzlaenge;

        private UnitListItem _unitResonanzlaenge;

        private VehiclesModel _vehicle;

        /// <summary>
        /// Gets the area quantity units.
        /// </summary>
        /// <value>The area quantity units.</value>
        public ObservableCollection<UnitListItem> AreaQuantityUnits { get; }

        public double? Einlasssteuerwinkel
        {
            get => _einlasssteuerwinkel;
            set
            {
                SetProperty(ref _einlasssteuerwinkel, value);
                RefreshResonanzlaenge();
            }
        }

        public VehiclesModel HelperVehicle
        {
            get => _helperVehicle;
            set { SetProperty(ref _helperVehicle, value); }
        }

        public ObservableCollection<VehiclesModel> HelperVehicles
        {
            get => _helperVehicles;
            set { SetProperty(ref _helperVehicles, value); }
        }

        /// <summary>
        /// Gets or sets the insert data command.
        /// </summary>
        /// <value>The insert data command.</value>
        public IMvxCommand InsertDataCommand { get; set; }

        /// <summary>
        /// Gets the length quantity units.
        /// </summary>
        /// <value>The length quantity units.</value>
        public ObservableCollection<UnitListItem> LengthQuantityUnits { get; }

        public double? Resonanzlaenge
        {
            get => _resonanzlaenge;
            set { SetProperty(ref _resonanzlaenge, value); }
        }

        public UnitListItem UnitResonanzlaenge
        {
            get => _unitResonanzlaenge;
            set
            {
                Resonanzlaenge = Business.Functions.UpdateValue(Resonanzlaenge, _unitResonanzlaenge, value);

                SetProperty(ref _unitResonanzlaenge, value);
            }
        }

        /// <summary>
        /// Gets or sets the vehicle.
        /// </summary>
        /// <value>The vehicle.</value>
        public VehiclesModel Vehicle
        {
            get => this._vehicle;
            set => this.SetProperty(ref this._vehicle, value);
        }

        public double? VehicleMotorEinlassDurchmesserD
        {
            get => this.Vehicle?.Motor?.Einlass?.DurchmesserD;
            set
            {
                if (this.Vehicle?.Motor?.Einlass == null)
                {
                    return;
                }

                this.Vehicle.Motor.Einlass.DurchmesserD = value;
                RefreshResonanzlaenge();
            }
        }

        public UnitListItem VehicleMotorEinlassDurchmesserDUnit
        {
            get => this.LengthQuantityUnits.SingleOrDefault(x => x.UnitEnumValue.Equals(this.Vehicle?.Motor?.Einlass?.DurchmesserDUnit));
            set
            {
                if (this.Vehicle?.Motor?.Einlass == null)
                {
                    return;
                }

                this.Vehicle.Motor.Einlass.DurchmesserDUnit = (UnitsNet.Units.LengthUnit)value?.UnitEnumValue;
                this.RaisePropertyChanged(() => this.VehicleMotorEinlassDurchmesserD);
            }
        }

        public double? VehicleMotorEinlassFlaecheA
        {
            get => this.Vehicle?.Motor?.Einlass?.FlaecheA;
            set
            {
                if (this.Vehicle?.Motor?.Einlass == null)
                {
                    return;
                }

                this.Vehicle.Motor.Einlass.FlaecheA = value;
                RefreshResonanzlaenge();
            }
        }

        public UnitListItem VehicleMotorEinlassFlaecheAUnit
        {
            get => this.AreaQuantityUnits.SingleOrDefault(x => x.UnitEnumValue.Equals(this.Vehicle?.Motor?.Einlass?.FlaecheAUnit));
            set
            {
                if (this.Vehicle?.Motor?.Einlass == null)
                {
                    return;
                }

                this.Vehicle.Motor.Einlass.FlaecheAUnit = (UnitsNet.Units.AreaUnit)value?.UnitEnumValue;
                this.RaisePropertyChanged(() => this.VehicleMotorEinlassFlaecheA);
            }
        }

        public double? VehicleMotorKurbelgehaeuseV
        {
            get => this.Vehicle?.Motor?.KurbelgehaeuseV;
            set
            {
                if (this.Vehicle?.Motor?.Einlass == null)
                {
                    return;
                }

                this.Vehicle.Motor.KurbelgehaeuseV = value;
                RefreshResonanzlaenge();
            }
        }

        public UnitListItem VehicleMotorKurbelgehaeuseVUnit
        {
            get => this.VolumeQuantityUnits.SingleOrDefault(x => x.UnitEnumValue.Equals(this.Vehicle?.Motor?.KurbelgehaeuseVUnit));
            set
            {
                if (this.Vehicle?.Motor == null)
                {
                    return;
                }

                this.Vehicle.Motor.KurbelgehaeuseVUnit = (UnitsNet.Units.VolumeUnit)value?.UnitEnumValue;
                this.RaisePropertyChanged(() => this.VehicleMotorKurbelgehaeuseV);
            }
        }

        public double? VehicleMotorResonanzU
        {
            get => this.Vehicle?.Motor?.ResonanzU;
            set
            {
                if (this.Vehicle?.Motor == null)
                {
                    return;
                }

                this.Vehicle.Motor.ResonanzU = value;
                RefreshResonanzlaenge();
            }
        }

        /// <summary>
        /// Gets the volume quantity units.
        /// </summary>
        /// <value>The volume quantity units.</value>
        public ObservableCollection<UnitListItem> VolumeQuantityUnits { get; }

        #endregion Values
    }
}