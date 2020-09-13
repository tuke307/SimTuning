// project=SimTuning.Core, file=TheorieViewModel.cs, creation=2020:7:31 Copyright (c) 2020
// tuke productions. All rights reserved.
namespace SimTuning.Core.ViewModels.Auslass
{
    using Data;
    using Data.Models;
    using Microsoft.EntityFrameworkCore;
    using MvvmCross.Commands;
    using MvvmCross.Logging;
    using MvvmCross.Navigation;
    using MvvmCross.ViewModels;
    using SimTuning.Core.Models;
    using SimTuning.Core.Models.Quantity;
    using SimTuning.Core.ModuleLogic;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Threading.Tasks;
    using UnitsNet.Units;

    /// <summary>
    /// Einlass-Theorie-ViewModel.
    /// </summary>
    /// <seealso cref="MvvmCross.ViewModels.MvxNavigationViewModel" />
    public class TheorieViewModel : MvxNavigationViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TheorieViewModel" /> class.
        /// </summary>
        /// <param name="logProvider">The log provider.</param>
        /// <param name="navigationService">The navigation service.</param>
        public TheorieViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService)
            : base(logProvider, navigationService)
        {
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

            using (var db = new DatabaseContext())
            {
                IList<VehiclesModel> vehicList = db.Vehicles
                    .Include(vehicles => vehicles.Motor)
                    .Include(vehicles => vehicles.Motor.Auslass)
                    .ToList();

                this.HelperVehicles = new ObservableCollection<VehiclesModel>(vehicList);
            }

            // Commands
            this.InsertDataCommand = new MvxCommand(this.InsertData);
        }

        #region Methods

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

        /// <summary>
        /// Inserts the data.
        /// </summary>
        protected virtual void InsertData()
        {
            if (this.HelperVehicle.Motor.Auslass.FlaecheA.HasValue)
            {
                this.VehicleMotorAuslassFlaecheA = this.HelperVehicle.Motor.Auslass.FlaecheA;
                this.RaisePropertyChanged(() => this.VehicleMotorAuslassFlaecheA);
            }

            if (this.HelperVehicle.Motor.ResonanzU.HasValue)
            {
                this.VehicleMotorResonanzU = this.HelperVehicle.Motor.ResonanzU;
                this.RaisePropertyChanged(() => this.VehicleMotorResonanzU);
            }

            if (this.HelperVehicle.Motor.Auslass.SteuerzeitSZ.HasValue)
            {
                this.VehicleMotorAuslassSteuerzeitSZ = this.HelperVehicle.Motor.Auslass.SteuerzeitSZ;
                this.RaisePropertyChanged(() => this.VehicleMotorAuslassSteuerzeitSZ);
            }
        }

        /// <summary>
        /// Refreshes the auspuff geschwindigkeit.
        /// </summary>
        private void Refresh_AuspuffGeschwindigkeit()
        {
            if (this.ModAuspuffAbgasT.HasValue)
            {
                this.ModAuspuffAbgasV = AuslassLogic.GetGasVelocity(this.ModAuspuffAbgasT.Value);
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
                    AuslassLogic.GetManifoldDiameter(
                    UnitsNet.UnitConverter.Convert(
                        this.VehicleMotorAuslassFlaecheA.Value,
                        this.VehicleMotorAuslassFlaecheAUnit.UnitEnumValue,
                        AreaUnit.SquareCentimeter), 10)
                    +
                    " - "
                    +
                    AuslassLogic.GetManifoldDiameter(
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
                this.VehicleMotorAuslassAuspuffKruemmerL = AuslassLogic.GetManifoldLength(
                     UnitsNet.UnitConverter.Convert(
                         this.VehicleMotorAuslassAuspuffKruemmerD.Value,
                         this.VehicleMotorAuslassAuspuffKruemmerDUnit.UnitEnumValue,
                         LengthUnit.Millimeter),
                     this.VehicleMotorAuslassAuspuffKruemmerF.Value,
                     0);

                this.RaisePropertyChanged(() => this.VehicleMotorAuslassAuspuffKruemmerL);
            }
        }

        /// <summary>
        /// Refreshes the resonanzlaenge.
        /// </summary>
        private void Refresh_Resonanzlaenge()
        {
            if (this.VehicleMotorAuslassSteuerzeitSZ.HasValue && this.VehicleMotorAuslassAuspuffAbgasT.HasValue && this.VehicleMotorResonanzU.HasValue)
            {
                this.VehicleMotorAuslassAuspuffResonanzL = AuslassLogic.GetResonanceLength(this.VehicleMotorAuslassSteuerzeitSZ.Value, this.VehicleMotorAuslassAuspuffAbgasT.Value, this.VehicleMotorResonanzU.Value);
                this.RaisePropertyChanged(() => this.VehicleMotorAuslassAuspuffResonanzL);
            }
        }

        #endregion Methods

        #region Values

        #region Commands

        public IMvxCommand InsertDataCommand { get; set; }

        #endregion Commands

        private VehiclesModel _helperVehicle;
        private ObservableCollection<VehiclesModel> _helperVehicles;

        private string _kruemmerSpannneD;

        private AuspuffModel _modAuspuff;
        private VehiclesModel _vehicle;

        /// <summary>
        /// Gets the area quantity units.
        /// </summary>
        /// <value>The area quantity units.</value>
        public ObservableCollection<UnitListItem> AreaQuantityUnits { get; }

        /// <summary>
        /// Gets or sets the helper vehicle.
        /// </summary>
        /// <value>The helper vehicle.</value>
        public VehiclesModel HelperVehicle
        {
            get => _helperVehicle;
            set => SetProperty(ref _helperVehicle, value);
        }

        /// <summary>
        /// Gets or sets the helper vehicles.
        /// </summary>
        /// <value>The helper vehicles.</value>
        public ObservableCollection<VehiclesModel> HelperVehicles
        {
            get => _helperVehicles;
            set => SetProperty(ref _helperVehicles, value);
        }

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
                this.RaisePropertyChanged(() => this.ModAuspuffAbgasT);
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
                this.RaisePropertyChanged(() => this.ModAuspuffAbgasV);
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
                this.RaisePropertyChanged(() => this.VehicleMotorAuslassAuspuffAbgasT);
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
                this.RaisePropertyChanged(() => this.VehicleMotorAuslassAuspuffKruemmerD);
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
                this.RaisePropertyChanged(() => this.VehicleMotorAuslassAuspuffKruemmerL);
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
                this.RaisePropertyChanged(() => this.VehicleMotorAuslassAuspuffResonanzL);
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
                this.RaisePropertyChanged(() => this.VehicleMotorAuslassFlaecheA);
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
            }
        }

        #endregion Values
    }
}