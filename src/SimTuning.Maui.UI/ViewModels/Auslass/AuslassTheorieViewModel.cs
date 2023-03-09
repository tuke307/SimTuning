﻿// Copyright (c) 2021 tuke productions. All rights reserved.
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Logging;
using SimTuning.Core;
using SimTuning.Core.Models;
using SimTuning.Core.Models.Quantity;
using SimTuning.Core.ModuleLogic;
using SimTuning.Core.Services;
using SimTuning.Data.Models;
using SimTuning.Maui.UI.Services;
using System.Collections.ObjectModel;
using UnitsNet.Units;

namespace SimTuning.Maui.UI.ViewModels
{
    public class AuslassTheorieViewModel : ViewModelBase
    {
        public AuslassTheorieViewModel(
            ILogger<AuslassTheorieViewModel> logger,
            IVehicleService vehicleService)
        {
            this._logger = logger;
            this._vehicleService = vehicleService;

            this.AreaQuantityUnits = new AreaQuantity();
            this.LengthQuantityUnits = new LengthQuantity();
            this.SpeedQuantityUnits = new SpeedQuantity();
            this.TemperatureQuantityUnits = new TemperatureQuantity();

            // Vehicle Creation
            this.Vehicle = new VehiclesModel();
            this.Vehicle.Motor = new MotorModel();
            this.Vehicle.Motor.Auslass = new AuslassModel();
            this.Vehicle.Motor.Auslass.Auspuff = new AuspuffModel();

            this.ModAuspuff = new AuspuffModel();

            // andere unit vorbelegen
            VehicleMotorAuslassFlaecheAUnit = this.AreaQuantityUnits.SingleOrDefault(x => x.UnitEnumValue.Equals(AreaUnit.SquareCentimeter));
        }

        #region Methods

        /// <summary>
        /// Inserts the data.
        /// </summary>
        public void InsertHelperVehicle(VehiclesModel helperVehicle)
        {
            if (helperVehicle.Motor.Auslass.FlaecheA.HasValue)
            {
                this.VehicleMotorAuslassFlaecheA = helperVehicle.Motor.Auslass.FlaecheA;
            }

            if (helperVehicle.Motor.ResonanzU.HasValue)
            {
                this.VehicleMotorResonanzU = helperVehicle.Motor.ResonanzU;
            }

            if (helperVehicle.Motor.Auslass.SteuerzeitSZ.HasValue)
            {
                this.VehicleMotorAuslassSteuerzeitSZ = helperVehicle.Motor.Auslass.SteuerzeitSZ;
            }
        }

        /// <summary>
        /// Refreshes the auspuff geschwindigkeit.
        /// </summary>
        private void Refresh_AuspuffGeschwindigkeit()
        {
            if (this.ModAuspuffAbgasT.HasValue)
            {
                this.ModAuspuffAbgasV = AuslassLogic.GetGasGeschwindigkeit(this.ModAuspuffAbgasT.Value);
                this.OnPropertyChanged(nameof(this.ModAuspuffAbgasV));
            }
        }

        /// <summary>
        /// Refreshes the kruemmer d.
        /// </summary>
        private void Refresh_KruemmerD()
        {
            if (this.VehicleMotorAuslassFlaecheA.HasValue)
            {
                this.KruemmerSpannneD =
                    AuslassLogic.GetKruemmerDurchmesser(
                    UnitsNet.UnitConverter.Convert(
                        this.VehicleMotorAuslassFlaecheA.Value,
                        this.VehicleMotorAuslassFlaecheAUnit.UnitEnumValue,
                        AreaUnit.SquareCentimeter), 10)
                    +
                    " - "
                    +
                    AuslassLogic.GetKruemmerDurchmesser(
                    UnitsNet.UnitConverter.Convert(
                        this.VehicleMotorAuslassFlaecheA.Value,
                        this.VehicleMotorAuslassFlaecheAUnit.UnitEnumValue,
                        AreaUnit.SquareCentimeter), 20);
            }
        }

        /// <summary>
        /// Refreshes the kruemmer l.
        /// </summary>
        private void Refresh_KruemmerL()
        {
            if (this.VehicleMotorAuslassAuspuffKruemmerD.HasValue && this.VehicleMotorAuslassAuspuffKruemmerF.HasValue)
            {
                this.VehicleMotorAuslassAuspuffKruemmerL = AuslassLogic.GetKruemmerLaenge(
                     UnitsNet.UnitConverter.Convert(
                         this.VehicleMotorAuslassAuspuffKruemmerD.Value,
                         this.VehicleMotorAuslassAuspuffKruemmerDUnit.UnitEnumValue,
                         LengthUnit.Millimeter),
                     this.VehicleMotorAuslassAuspuffKruemmerF.Value,
                     0);

                this.OnPropertyChanged(nameof(this.VehicleMotorAuslassAuspuffKruemmerL));
            }
        }

        /// <summary>
        /// Refreshes the resonanzlaenge.
        /// </summary>
        private void Refresh_Resonanzlaenge()
        {
            if (this.VehicleMotorAuslassSteuerzeitSZ.HasValue && this.VehicleMotorAuslassAuspuffAbgasT.HasValue && this.VehicleMotorResonanzU.HasValue)
            {
                this.VehicleMotorAuslassAuspuffResonanzL = AuslassLogic.GetResonanzLaenge(this.VehicleMotorAuslassSteuerzeitSZ.Value, this.VehicleMotorAuslassAuspuffAbgasT.Value, this.VehicleMotorResonanzU.Value);
                this.OnPropertyChanged(nameof(this.VehicleMotorAuslassAuspuffResonanzL));
            }
        }

        #endregion Methods

        #region Values

        private readonly ILogger<AuslassTheorieViewModel> _logger;
        private readonly IVehicleService _vehicleService;

        private string _kruemmerSpannneD;

        private AuspuffModel _modAuspuff;
        private VehiclesModel _vehicle;

        /// <summary>
        /// Gets the area quantity units.
        /// </summary>
        /// <value>The area quantity units.</value>
        public ObservableCollection<UnitListItem> AreaQuantityUnits { get; }

        /// <summary>
        /// Gets or sets the kruemmer spannne d.
        /// </summary>
        /// <value>The kruemmer spannne d.</value>
        public string KruemmerSpannneD
        {
            get => this._kruemmerSpannneD;
            set => this.SetProperty(ref this._kruemmerSpannneD, value);
        }

        /// <summary>
        /// Gets the length quantity units.
        /// </summary>
        /// <value>The length quantity units.</value>
        public ObservableCollection<UnitListItem> LengthQuantityUnits { get; }

        /// <summary>
        /// Gets or sets the mod auspuff.
        /// </summary>
        /// <value>The mod auspuff.</value>
        public AuspuffModel ModAuspuff
        {
            get => this._modAuspuff;
            set => this.SetProperty(ref this._modAuspuff, value);
        }

        /// <summary>
        /// Gets or sets the mod abgas t.
        /// </summary>
        /// <value>The mod abgas t.</value>
        public double? ModAuspuffAbgasT
        {
            get => this.ModAuspuff?.AbgasT;
            set
            {
                if (this.ModAuspuff == null)
                {
                    return;
                }

                this.ModAuspuff.AbgasT = value;
                this.Refresh_AuspuffGeschwindigkeit();
            }
        }

        /// <summary>
        /// Gets or sets the mod auspuff abgas t unit.
        /// </summary>
        /// <value>The mod auspuff abgas t unit.</value>
        public UnitListItem ModAuspuffAbgasTUnit
        {
            get => this.TemperatureQuantityUnits.SingleOrDefault(x => x.UnitEnumValue.Equals(this.ModAuspuff.AbgasTUnit));
            set
            {
                if (this.ModAuspuff == null)
                {
                    return;
                }

                this.ModAuspuff.AbgasTUnit = (UnitsNet.Units.TemperatureUnit)value?.UnitEnumValue;
                this.OnPropertyChanged(nameof(this.ModAuspuffAbgasT));
            }
        }

        /// <summary>
        /// Gets or sets the mod abgas v.
        /// </summary>
        /// <value>The mod abgas v.</value>
        public double? ModAuspuffAbgasV
        {
            get => this.ModAuspuff?.AbgasV;
            set
            {
                if (this.ModAuspuff == null)
                {
                    return;
                }

                this.ModAuspuff.AbgasV = value;
            }
        }

        /// <summary>
        /// Gets or sets the mod abgas v unit.
        /// </summary>
        /// <value>The mod abgas v unit.</value>
        public UnitListItem ModAuspuffAbgasVUnit
        {
            get => this.SpeedQuantityUnits.SingleOrDefault(x => x.UnitEnumValue.Equals(this.ModAuspuff.AbgasVUnit));
            set
            {
                if (this.ModAuspuff == null)
                {
                    return;
                }

                this.ModAuspuff.AbgasVUnit = (UnitsNet.Units.SpeedUnit)value?.UnitEnumValue;
                this.OnPropertyChanged(nameof(this.ModAuspuffAbgasV));
            }
        }

        /// <summary>
        /// Gets the speed quantity units.
        /// </summary>
        /// <value>The speed quantity units.</value>
        public ObservableCollection<UnitListItem> SpeedQuantityUnits { get; }

        /// <summary>
        /// Gets the temperature quantity units.
        /// </summary>
        /// <value>The temperature quantity units.</value>
        public ObservableCollection<UnitListItem> TemperatureQuantityUnits { get; }

        /// <summary>
        /// Gets or sets the vehicle.
        /// </summary>
        /// <value>The vehicle.</value>
        public VehiclesModel Vehicle
        {
            get => this._vehicle;
            set => this.SetProperty(ref this._vehicle, value);
        }

        /// <summary>
        /// Gets or sets the vehicle motor auslass auspuff abgas t.
        /// </summary>
        /// <value>The vehicle motor auslass auspuff abgas t.</value>
        public double? VehicleMotorAuslassAuspuffAbgasT
        {
            get => this.Vehicle?.Motor?.Auslass?.Auspuff?.AbgasT;
            set
            {
                if (this.Vehicle?.Motor?.Auslass?.Auspuff == null)
                {
                    return;
                }

                this.Vehicle.Motor.Auslass.Auspuff.AbgasT = value;
                this.Refresh_Resonanzlaenge();
            }
        }

        /// <summary>
        /// Gets or sets the vehicle motor auslass auspuff abgas t unit.
        /// </summary>
        /// <value>The vehicle motor auslass auspuff abgas t unit.</value>
        public UnitListItem VehicleMotorAuslassAuspuffAbgasTUnit
        {
            get => this.TemperatureQuantityUnits.SingleOrDefault(x => x.UnitEnumValue.Equals(this.Vehicle?.Motor?.Auslass?.Auspuff?.AbgasTUnit));
            set
            {
                if (this.Vehicle?.Motor?.Auslass?.Auspuff == null)
                {
                    return;
                }

                this.Vehicle.Motor.Auslass.Auspuff.AbgasTUnit = (UnitsNet.Units.TemperatureUnit)value?.UnitEnumValue;
                this.OnPropertyChanged(nameof(this.VehicleMotorAuslassAuspuffAbgasT));
            }
        }

        /// <summary>
        /// Gets or sets the vehicle motor auslass auspuff kruemmer d.
        /// </summary>
        /// <value>The vehicle motor auslass auspuff kruemmer d.</value>
        public double? VehicleMotorAuslassAuspuffKruemmerD
        {
            get => this.Vehicle?.Motor?.Auslass?.Auspuff?.KruemmerD;
            set
            {
                if (this.Vehicle?.Motor?.Auslass?.Auspuff == null)
                {
                    return;
                }

                this.Vehicle.Motor.Auslass.Auspuff.KruemmerD = value;
                this.Refresh_KruemmerL();
            }
        }

        /// <summary>
        /// Gets or sets the vehicle motor auslass auspuff kruemmer d unit.
        /// </summary>
        /// <value>The vehicle motor auslass auspuff kruemmer d unit.</value>
        public UnitListItem VehicleMotorAuslassAuspuffKruemmerDUnit
        {
            get => this.LengthQuantityUnits.SingleOrDefault(x => x.UnitEnumValue.Equals(this.Vehicle?.Motor?.Auslass?.Auspuff?.KruemmerDUnit));
            set
            {
                if (this.Vehicle?.Motor?.Auslass?.Auspuff == null)
                {
                    return;
                }

                this.Vehicle.Motor.Auslass.Auspuff.KruemmerDUnit = (UnitsNet.Units.LengthUnit)value?.UnitEnumValue;
                this.OnPropertyChanged(nameof(this.VehicleMotorAuslassAuspuffKruemmerD));
            }
        }

        /// <summary>
        /// Gets or sets the vehicle motor auslass auspuff kruemmer f.
        /// </summary>
        /// <value>The vehicle motor auslass auspuff kruemmer f.</value>
        public double? VehicleMotorAuslassAuspuffKruemmerF
        {
            get => this.Vehicle?.Motor?.Auslass?.Auspuff?.KruemmerF;
            set
            {
                if (this.Vehicle?.Motor?.Auslass?.Auspuff == null)
                {
                    return;
                }

                this.Vehicle.Motor.Auslass.Auspuff.KruemmerF = value;
                this.Refresh_KruemmerL();
            }
        }

        /// <summary>
        /// Gets or sets the vehicle motor auslass auspuff kruemmer l.
        /// </summary>
        /// <value>The vehicle motor auslass auspuff kruemmer l.</value>
        public double? VehicleMotorAuslassAuspuffKruemmerL
        {
            get => this.Vehicle?.Motor?.Auslass?.Auspuff?.KruemmerL;
            set
            {
                if (this.Vehicle?.Motor?.Auslass?.Auspuff == null)
                {
                    return;
                }

                this.Vehicle.Motor.Auslass.Auspuff.KruemmerL = value;
            }
        }

        /// <summary>
        /// Gets or sets the vehicle motor auslass auspuff kruemmer ld unit.
        /// </summary>
        /// <value>The vehicle motor auslass auspuff kruemmer ld unit.</value>
        public UnitListItem VehicleMotorAuslassAuspuffKruemmerLUnit
        {
            get => this.LengthQuantityUnits.SingleOrDefault(x => x.UnitEnumValue.Equals(this.Vehicle?.Motor?.Auslass?.Auspuff?.KruemmerLUnit));
            set
            {
                if (this.Vehicle?.Motor?.Auslass?.Auspuff == null)
                {
                    return;
                }

                this.Vehicle.Motor.Auslass.Auspuff.KruemmerLUnit = (UnitsNet.Units.LengthUnit)value?.UnitEnumValue;
                this.OnPropertyChanged(nameof(this.VehicleMotorAuslassAuspuffKruemmerL));
            }
        }

        /// <summary>
        /// Gets or sets the vehicle motor auslass auspuff resonanz l.
        /// </summary>
        /// <value>The vehicle motor auslass auspuff resonanz l.</value>
        public double? VehicleMotorAuslassAuspuffResonanzL
        {
            get => this.Vehicle?.Motor?.Auslass?.Auspuff?.ResonanzL;
            set
            {
                if (this.Vehicle?.Motor?.Auslass?.Auspuff == null)
                {
                    return;
                }

                this.Vehicle.Motor.Auslass.Auspuff.ResonanzL = value;
            }
        }

        /// <summary>
        /// Gets or sets the vehicle motor auslass auspuff resonanz l unit.
        /// </summary>
        /// <value>The vehicle motor auslass auspuff resonanz l unit.</value>
        public UnitListItem VehicleMotorAuslassAuspuffResonanzLUnit
        {
            get => this.LengthQuantityUnits.SingleOrDefault(x => x.UnitEnumValue.Equals(this.Vehicle?.Motor?.Auslass?.Auspuff?.ResonanzLUnit));
            set
            {
                if (this.Vehicle?.Motor?.Auslass?.Auspuff == null)
                {
                    return;
                }

                this.Vehicle.Motor.Auslass.Auspuff.ResonanzLUnit = (UnitsNet.Units.LengthUnit)value?.UnitEnumValue;
                this.OnPropertyChanged(nameof(this.VehicleMotorAuslassAuspuffResonanzL));
            }
        }

        /// <summary>
        /// Gets or sets the vehicle motor auslass flaeche a.
        /// </summary>
        /// <value>The vehicle motor auslass flaeche a.</value>
        public double? VehicleMotorAuslassFlaecheA
        {
            get => this.Vehicle?.Motor?.Auslass?.FlaecheA;
            set
            {
                if (this.Vehicle?.Motor?.Auslass == null)
                {
                    return;
                }

                this.Refresh_KruemmerD();
                this.Vehicle.Motor.Auslass.FlaecheA = value;
                this.OnPropertyChanged(nameof(this.VehicleMotorAuslassFlaecheA));
            }
        }

        /// <summary>
        /// Gets or sets the vehicle motor auslass flaeche a unit.
        /// </summary>
        /// <value>The vehicle motor auslass flaeche a unit.</value>
        public UnitListItem VehicleMotorAuslassFlaecheAUnit
        {
            get => this.AreaQuantityUnits.SingleOrDefault(x => x.UnitEnumValue.Equals(this.Vehicle?.Motor?.Auslass?.FlaecheAUnit));
            set
            {
                if (this.Vehicle?.Motor?.Auslass == null)
                {
                    return;
                }

                this.Vehicle.Motor.Auslass.FlaecheAUnit = (UnitsNet.Units.AreaUnit)value?.UnitEnumValue;
                this.OnPropertyChanged(nameof(this.VehicleMotorAuslassFlaecheA));
            }
        }

        /// <summary>
        /// Gets or sets the vehicle motor auslass steuerzeit sz.
        /// </summary>
        /// <value>The vehicle motor auslass steuerzeit sz.</value>
        public double? VehicleMotorAuslassSteuerzeitSZ
        {
            get => this.Vehicle?.Motor?.Auslass?.SteuerzeitSZ;
            set
            {
                if (this.Vehicle?.Motor?.Auslass == null)
                {
                    return;
                }

                this.Refresh_Resonanzlaenge();
                this.Vehicle.Motor.Auslass.SteuerzeitSZ = value;
                this.OnPropertyChanged(nameof(this.VehicleMotorAuslassSteuerzeitSZ));
            }
        }

        /// <summary>
        /// Gets or sets the vehicle motor resonanz u.
        /// </summary>
        /// <value>The vehicle motor resonanz u.</value>
        public double? VehicleMotorResonanzU
        {
            get => this.Vehicle?.Motor?.ResonanzU;
            set
            {
                if (this.Vehicle?.Motor == null)
                {
                    return;
                }

                this.Refresh_Resonanzlaenge();
                this.Vehicle.Motor.ResonanzU = value;
                this.OnPropertyChanged(nameof(this.VehicleMotorResonanzU));
            }
        }

        #endregion Values
    }
}