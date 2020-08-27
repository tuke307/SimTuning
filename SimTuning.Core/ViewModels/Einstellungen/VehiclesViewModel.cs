// project=SimTuning.Core, file=VehiclesViewModel.cs, creation=2020:7:31 Copyright (c)
// 2020 tuke productions. All rights reserved.
using Microsoft.EntityFrameworkCore;
using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using SimTuning.Core.Models;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Resources;
using System.Threading.Tasks;
using UnitsNet.Units;
using WordPressPCL.Models;

namespace SimTuning.Core.ViewModels.Einstellungen
{
    /// <summary>
    /// VehiclesViewModel.
    /// </summary>
    /// <seealso cref="MvvmCross.ViewModels.MvxNavigationViewModel{SimTuning.Core.Models.UserModel}" />
    public class VehiclesViewModel : MvxNavigationViewModel<UserModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VehiclesViewModel" /> class.
        /// </summary>
        /// <param name="logProvider">The log provider.</param>
        /// <param name="navigationService">The navigation service.</param>
        public VehiclesViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService)
            : base(logProvider, navigationService)
        {
            this.AreaQuantityUnits = new AreaQuantity();
            this.VolumeQuantityUnits = new VolumeQuantity();
            this.LengthQuantityUnits = new LengthQuantity();
            this.MassQuantityUnits = new MassQuantity();
            this.SpeedQuantityUnits = new SpeedQuantity();

            this.ShowSaveButtonCommand = new MvxCommand(() => this.SaveButton = true);
        }

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        /// <returns>Initilisierung.</returns>
        public override Task Initialize()
        {
            this.rm = new ResourceManager(typeof(SimTuning.Core.resources));

            this.ReloadData();

            return base.Initialize();
        }

        /// <summary>
        /// Prepares the specified user.
        /// </summary>
        /// <param name="_user">The user.</param>
        public override void Prepare(UserModel _user)
        {
            this.User = _user;

            base.Prepare();
        }

        private void SetUnits()
        {
            this.Vehicle.FrontAUnit = AreaUnit.SquareMillimeter;
            this.Vehicle.GewichtUnit = MassUnit.Kilogram;
            this.Vehicle.Motor.Einlass.LaengeLUnit = LengthUnit.Millimeter;
            this.Vehicle.Motor.Einlass.FlaecheAUnit = AreaUnit.SquareMillimeter;
            this.Vehicle.Motor.Einlass.BreiteBUnit = LengthUnit.Millimeter;
            this.Vehicle.Motor.Einlass.HoeheHUnit = LengthUnit.Millimeter;
            this.Vehicle.Motor.Einlass.Vergaser.DurchmesserDUnit = LengthUnit.Millimeter;
            this.Vehicle.Motor.Auslass.FlaecheAUnit = AreaUnit.SquareMillimeter;
            this.Vehicle.Motor.Auslass.BreiteBUnit = LengthUnit.Millimeter;
            this.Vehicle.Motor.Auslass.DurchmesserDUnit = LengthUnit.Millimeter;
            this.Vehicle.Motor.Auslass.HoeheHUnit = LengthUnit.Millimeter;
            this.Vehicle.Motor.Auslass.LaengeLUnit = LengthUnit.Millimeter;

            // must be implemented
            this.Vehicle.Motor.Auslass.Auspuff.AbgasTUnit = TemperatureUnit.DegreeCelsius;
            this.Vehicle.Motor.Auslass.Auspuff.AbgasVUnit = SpeedUnit.KilometerPerHour;
            this.Vehicle.Motor.Auslass.Auspuff.KruemmerDUnit = LengthUnit.Millimeter;
            this.Vehicle.Motor.Auslass.Auspuff.KruemmerLUnit = LengthUnit.Millimeter;
            this.Vehicle.Motor.Auslass.Auspuff.DiffusorD1Unit = LengthUnit.Millimeter;
            this.Vehicle.Motor.Auslass.Auspuff.DiffusorD2Unit = LengthUnit.Millimeter;
            this.Vehicle.Motor.Auslass.Auspuff.DiffusorD3Unit = LengthUnit.Millimeter;
            this.Vehicle.Motor.Auslass.Auspuff.DiffusorDUnit = LengthUnit.Millimeter;
            this.Vehicle.Motor.Auslass.Auspuff.DiffusorL1Unit = LengthUnit.Millimeter;
            this.Vehicle.Motor.Auslass.Auspuff.DiffusorL2Unit = LengthUnit.Millimeter;
            this.Vehicle.Motor.Auslass.Auspuff.DiffusorL3Unit = LengthUnit.Millimeter;
            this.Vehicle.Motor.Auslass.Auspuff.DiffusorLUnit = LengthUnit.Millimeter;
            this.Vehicle.Motor.Auslass.Auspuff.MittelteilDUnit = LengthUnit.Millimeter;
            this.Vehicle.Motor.Auslass.Auspuff.MittelteilLUnit = LengthUnit.Millimeter;
            this.Vehicle.Motor.Auslass.Auspuff.GegenkonusDUnit = LengthUnit.Millimeter;
            this.Vehicle.Motor.Auslass.Auspuff.GegenkonusLUnit = LengthUnit.Millimeter;
            this.Vehicle.Motor.Auslass.Auspuff.EndrohrDUnit = LengthUnit.Millimeter;
            this.Vehicle.Motor.Auslass.Auspuff.EndrohrLUnit = LengthUnit.Millimeter;
            this.Vehicle.Motor.Auslass.Auspuff.ResonanzLUnit = LengthUnit.Millimeter;
            this.Vehicle.Motor.Auslass.Auspuff.GesamtLUnit = LengthUnit.Millimeter;

            this.Vehicle.Motor.Ueberstroemer.FlaecheAUnit = AreaUnit.SquareMillimeter;
            this.Vehicle.Motor.Ueberstroemer.BreiteBUnit = LengthUnit.Millimeter;
            this.Vehicle.Motor.Ueberstroemer.HoeheHUnit = LengthUnit.Millimeter;
            this.Vehicle.Motor.BohrungDUnit = LengthUnit.Millimeter;
            this.Vehicle.Motor.BrennraumVUnit = VolumeUnit.CubicMillimeter;
            this.Vehicle.Motor.HubraumVUnit = VolumeUnit.CubicMillimeter;
            this.Vehicle.Motor.KurbelgehaeuseVUnit = VolumeUnit.CubicMillimeter;
            this.Vehicle.Motor.KolbenGUnit = SpeedUnit.KilometerPerHour;
            this.Vehicle.Motor.DeachsierungLUnit = LengthUnit.Millimeter;
            this.Vehicle.Motor.HubLUnit = LengthUnit.Millimeter;
            this.Vehicle.Motor.PleulLUnit = LengthUnit.Millimeter;
        }

        #region Methods

        /// <summary>
        /// Deletes the vehicle.
        /// </summary>
        protected virtual void DeleteVehicle()
        {
            if (this.Vehicle.Deletable)
            {
                // in Datenbank löschen
                if (this.Vehicle.Id != 0)
                {
                    using (var db = new Data.DatabaseContext())
                    {
                        // in Datenbank löschen
                        db.Vehicles.Remove(this.Vehicle);

                        db.SaveChanges();
                    }

                    this.Vehicles.Remove(this.Vehicles.Where(v => v.Id == this.Vehicle.Id).First());
                }

                // in lokaler liste löschen
                this.Vehicles.Remove(this.Vehicle);

                this.Vehicle = null;
            }
        }

        /// <summary>
        /// Creates new vehicle.
        /// </summary>
        protected virtual void NewVehicle()
        {
            // Vordefinieren des neuen Fahrzeugs
            this.Vehicles.Add(new Data.Models.VehiclesModel()
            {
                Name = "Neues Fahrzeug",
                Beschreibung = "Erstellt am " + DateTime.Now + " über Fahrzeug-Modul",
                Deletable = true,
            });

            this.Vehicle = this.Vehicles.Last();

            this.SaveButton = true;
        }

        /// <summary>
        /// Saves the vehicle.
        /// </summary>
        protected virtual void SaveVehicle()
        {
            using (var db = new Data.DatabaseContext())
            {
                db.Vehicles.Attach(Vehicle);

                db.SaveChanges();
            }

            this.SaveButton = false;
        }

        /// <summary>
        /// Loads the vehicle.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        private Data.Models.VehiclesModel LoadVehicle(Data.Models.VehiclesModel value)
        {
            using (var db = new Data.DatabaseContext())
            {
                // wenn bereits in Datenbank
                if (value.Id != 0)
                {
                    // Vehicle+Motor+Dyno
                    return db.Vehicles
                  .Where(v => v.Id == value.Id)
                  .Include(v => v.Dyno)
                  .Include(v => v.Tuning)
                  .Include(v => v.Motor)
                  .Include(v => v.Motor.Auslass)
                    .ThenInclude(a => a.Auspuff)
                  .Include(v => v.Motor.Einlass)
                    .ThenInclude(e => e.Vergaser)
                  .Include(v => v.Motor.Ueberstroemer)

                  .First();
                }
                else
                {
                    return value;
                }
            }
        }

        /// <summary>
        /// Reloads the data.
        /// </summary>
        private void ReloadData()
        {
            using (var db = new Data.DatabaseContext())
            {
                this.Vehicles = new ObservableCollection<Data.Models.VehiclesModel>(db.Vehicles.ToList());

                this.Engines = new ObservableCollection<Data.Models.MotorModel>(db.Motor.ToList());
            }
        }

        #endregion Methods

        #region Values

        protected ResourceManager rm;

        private Data.Models.MotorModel _engine;

        private ObservableCollection<Data.Models.MotorModel> _engines;

        private bool _saveButton;

        private Data.Models.VehiclesModel _vehicle;

        private ObservableCollection<Data.Models.VehiclesModel> _vehicles;

        public ObservableCollection<UnitListItem> AreaQuantityUnits { get; }

        /// <summary>
        /// Gets or sets the delete vehicle command.
        /// </summary>
        /// <value>The delete vehicle command.</value>
        public IMvxCommand DeleteVehicleCommand { get; set; }

        public Data.Models.MotorModel Engine
        {
            get => this._engine;
            set
            {
                if (this.Vehicle != null)
                {
                    // wenn Vehicle geladen wird; motor setzen für dropdown
                    if (this.Vehicle.MotorId.HasValue)
                    {
                        value = this.Engines.Where(m => m.Id == this.Vehicle.MotorId.Value).First();
                    }

                    if (value != null)
                    {
                        if (value.Id != this.Vehicle.MotorId)
                        {
                            // wenn beim Vehicle ein neuer Motor ausgewählt wird
                            this.Vehicle.Motor = value;
                            this.RaisePropertyChanged("Vehicle"); // Motor-Werte für UI updaten
                        }
                    }
                }

                this.SetProperty(ref this._engine, value);
            }
        }

        public ObservableCollection<Data.Models.MotorModel> Engines
        {
            get => this._engines;
            set { SetProperty(ref this._engines, value); }
        }

        public ObservableCollection<UnitListItem> LengthQuantityUnits { get; }

        public ObservableCollection<UnitListItem> MassQuantityUnits { get; }

        /// <summary>
        /// Creates new vehiclecommand.
        /// </summary>
        /// <value>The new vehicle command.</value>
        public IMvxCommand NewVehicleCommand { get; set; }

        public bool SaveButton
        {
            get => this._saveButton;
            set { this.SetProperty(ref this._saveButton, value); }
        }

        /// <summary>
        /// Gets or sets the save vehicle command.
        /// </summary>
        /// <value>The save vehicle command.</value>
        public IMvxCommand SaveVehicleCommand { get; set; }

        /// <summary>
        /// Gets or sets the show save button command.
        /// </summary>
        /// <value>The show save button command.</value>
        public IMvxCommand ShowSaveButtonCommand { get; set; }

        public ObservableCollection<UnitListItem> SpeedQuantityUnits { get; }

        public UserModel User { get; protected set; }

        public Data.Models.VehiclesModel Vehicle
        {
            get => this._vehicle;
            set
            {
                if (value != null)
                {
                    // Laden des kompletten Datensatzes
                    this.LoadVehicle(value);
                }
                else
                {
                    // gerade gelöscht => letztes Vehicle neu laden
                    if (this.Vehicles.Count != 0)
                    {
                        this.Vehicle = this.Vehicles.Last();
                    }
                }

                //Einfügen
                this.SetProperty(ref this._vehicle, value);

                // Units
                this.SetUnits();

                // nix mehr zu speichern
                this.SaveButton = false;

                // Motor refreshen
                this.Engine = null;
            }
        }

        public ObservableCollection<Data.Models.VehiclesModel> Vehicles
        {
            get => this._vehicles;
            set { SetProperty(ref this._vehicles, value); }
        }

        public ObservableCollection<UnitListItem> VolumeQuantityUnits { get; }

        #region Units

        public double? VehicleFrontA
        {
            get => this.Vehicle?.FrontA;
            set
            {
                this.Vehicle.FrontA = value;
                this.SaveButton = true;
            }
        }

        public UnitListItem VehicleFrontAUnit
        {
            get => this.AreaQuantityUnits.SingleOrDefault(x => x.UnitEnumValue.Equals(this.Vehicle?.FrontAUnit)) ?? this.AreaQuantityUnits.Single(x => x.UnitEnumValue.Equals(Data.Models.VehiclesModel.FrontABaseUnit));
            set
            {
                this.Vehicle.FrontAUnit = (UnitsNet.Units.AreaUnit)value?.UnitEnumValue;
                this.RaisePropertyChanged(() => VehicleFrontA);
            }
        }

        public double? VehicleGewicht
        {
            get => this.Vehicle?.Gewicht;
            set
            {
                this.Vehicle.Gewicht = value;
                this.SaveButton = true;
            }
        }

        public UnitListItem VehicleGewichtUnit
        {
            get => this.MassQuantityUnits.SingleOrDefault(x => x.UnitEnumValue.Equals(this.Vehicle?.GewichtUnit)) ?? this.MassQuantityUnits.Single(x => x.UnitEnumValue.Equals(Data.Models.VehiclesModel.GewichtBaseUnit));
            set
            {
                this.Vehicle.GewichtUnit = (UnitsNet.Units.MassUnit)value?.UnitEnumValue;
                this.RaisePropertyChanged(() => VehicleGewicht);
            }
        }

        public double? VehicleMotorAuslassBreiteB
        {
            get => this.Vehicle?.Motor?.Auslass?.BreiteB;
            set
            {
                this.Vehicle.Motor.Auslass.BreiteB = value;
                this.SaveButton = true;
            }
        }

        public UnitListItem VehicleMotorAuslassBreiteBUnit
        {
            get => this.LengthQuantityUnits.SingleOrDefault(x => x.UnitEnumValue.Equals(this.Vehicle?.Motor?.Auslass?.BreiteBUnit)) ?? this.LengthQuantityUnits.Single(x => x.UnitEnumValue.Equals(Data.Models.AuslassModel.BreiteBBaseUnit));
            set
            {
                this.Vehicle.Motor.Auslass.BreiteBUnit = (UnitsNet.Units.LengthUnit)value?.UnitEnumValue;
                this.RaisePropertyChanged(() => VehicleMotorAuslassBreiteB);
            }
        }

        public double? VehicleMotorAuslassFlaecheA
        {
            get => this.Vehicle?.Motor?.Auslass?.FlaecheA;
            set
            {
                this.Vehicle.Motor.Auslass.FlaecheA = value;
                this.SaveButton = true;
            }
        }

        public UnitListItem VehicleMotorAuslassFlaecheAUnit
        {
            get => this.AreaQuantityUnits.SingleOrDefault(x => x.UnitEnumValue.Equals(this.Vehicle?.Motor?.Auslass?.FlaecheAUnit)) ?? this.AreaQuantityUnits.Single(x => x.UnitEnumValue.Equals(Data.Models.AuslassModel.FlaecheABaseUnit));
            set
            {
                this.Vehicle.Motor.Auslass.FlaecheAUnit = (UnitsNet.Units.AreaUnit)value?.UnitEnumValue;
                this.RaisePropertyChanged(() => VehicleMotorAuslassFlaecheA);
            }
        }

        public double? VehicleMotorAuslassHoeheH
        {
            get => this.Vehicle?.Motor?.Auslass?.HoeheH;
            set
            {
                this.Vehicle.Motor.Auslass.HoeheH = value;
                this.SaveButton = true;
            }
        }

        public UnitListItem VehicleMotorAuslassHoeheHUnit
        {
            get => this.LengthQuantityUnits.SingleOrDefault(x => x.UnitEnumValue.Equals(this.Vehicle?.Motor?.Auslass?.HoeheHUnit)) ?? this.LengthQuantityUnits.Single(x => x.UnitEnumValue.Equals(Data.Models.AuslassModel.HoeheHBaseUnit));
            set
            {
                this.Vehicle.Motor.Auslass.HoeheHUnit = (UnitsNet.Units.LengthUnit)value?.UnitEnumValue;
                this.RaisePropertyChanged(() => VehicleMotorAuslassHoeheH);
            }
        }

        public double? VehicleMotorBohrungD
        {
            get => this.Vehicle?.Motor?.BohrungD;
            set
            {
                this.Vehicle.Motor.BohrungD = value;
                this.SaveButton = true;
            }
        }

        public UnitListItem VehicleMotorBohrungDUnit
        {
            get => this.LengthQuantityUnits.SingleOrDefault(x => x.UnitEnumValue.Equals(this.Vehicle?.Motor?.BohrungDUnit)) ?? this.LengthQuantityUnits.Single(x => x.UnitEnumValue.Equals(Data.Models.MotorModel.BohrungDBaseUnit));
            set
            {
                this.Vehicle.Motor.BohrungDUnit = (UnitsNet.Units.LengthUnit)value?.UnitEnumValue;
                this.RaisePropertyChanged(() => VehicleMotorBohrungD);
            }
        }

        public double? VehicleMotorBrennraumV
        {
            get => this.Vehicle?.Motor?.BrennraumV;
            set
            {
                this.Vehicle.Motor.BrennraumV = value;
                this.SaveButton = true;
            }
        }

        public UnitListItem VehicleMotorBrennraumVUnit
        {
            get => this.VolumeQuantityUnits.SingleOrDefault(x => x.UnitEnumValue.Equals(this.Vehicle?.Motor?.BrennraumVUnit)) ?? this.VolumeQuantityUnits.Single(x => x.UnitEnumValue.Equals(Data.Models.MotorModel.BrennraumVBaseUnit));
            set
            {
                this.Vehicle.Motor.BrennraumVUnit = (UnitsNet.Units.VolumeUnit)value?.UnitEnumValue;
                this.RaisePropertyChanged(() => VehicleMotorBrennraumV);
            }
        }

        public double? VehicleMotorDeachsierungL
        {
            get => this.Vehicle?.Motor?.DeachsierungL;
            set
            {
                this.Vehicle.Motor.DeachsierungL = value;
                this.SaveButton = true;
            }
        }

        public UnitListItem VehicleMotorDeachsierungLUnit
        {
            get => this.LengthQuantityUnits.SingleOrDefault(x => x.UnitEnumValue.Equals(this.Vehicle?.Motor?.DeachsierungLUnit)) ?? this.LengthQuantityUnits.Single(x => x.UnitEnumValue.Equals(Data.Models.MotorModel.DeachsierungLBaseUnit));
            set
            {
                this.Vehicle.Motor.DeachsierungLUnit = (UnitsNet.Units.LengthUnit)value?.UnitEnumValue;
                this.RaisePropertyChanged(() => VehicleMotorDeachsierungL);
            }
        }

        public double? VehicleMotorEinlassBreiteB
        {
            get => this.Vehicle?.FrontA;
            set
            {
                this.Vehicle.FrontA = value;
                this.SaveButton = true;
            }
        }

        public UnitListItem VehicleMotorEinlassBreiteBUnit
        {
            get => this.LengthQuantityUnits.SingleOrDefault(x => x.UnitEnumValue.Equals(this.Vehicle?.Motor?.Einlass?.BreiteBUnit)) ?? this.AreaQuantityUnits.Single(x => x.UnitEnumValue.Equals(Data.Models.VehiclesModel.FrontABaseUnit));
            set
            {
                this.Vehicle.FrontAUnit = (UnitsNet.Units.AreaUnit)value?.UnitEnumValue;
                this.RaisePropertyChanged(() => VehicleMotorEinlassBreiteB);
            }
        }

        public double? VehicleMotorEinlassFlaecheA
        {
            get => this.Vehicle?.FrontA;
            set
            {
                this.Vehicle.FrontA = value;
                this.SaveButton = true;
            }
        }

        public UnitListItem VehicleMotorEinlassFlaecheAUnit
        {
            get => this.AreaQuantityUnits.SingleOrDefault(x => x.UnitEnumValue.Equals(this.Vehicle?.Motor?.Einlass?.FlaecheAUnit)) ?? this.AreaQuantityUnits.Single(x => x.UnitEnumValue.Equals(Data.Models.VehiclesModel.FrontABaseUnit));
            set
            {
                this.Vehicle.FrontAUnit = (UnitsNet.Units.AreaUnit)value?.UnitEnumValue;
                this.RaisePropertyChanged(() => VehicleMotorEinlassFlaecheA);
            }
        }

        public double? VehicleMotorEinlassHoeheH
        {
            get => this.Vehicle?.FrontA;
            set
            {
                this.Vehicle.FrontA = value;
                this.SaveButton = true;
            }
        }

        public UnitListItem VehicleMotorEinlassHoeheHUnit
        {
            get => this.LengthQuantityUnits.SingleOrDefault(x => x.UnitEnumValue.Equals(this.Vehicle?.Motor?.Einlass?.HoeheHUnit)) ?? this.AreaQuantityUnits.Single(x => x.UnitEnumValue.Equals(Data.Models.VehiclesModel.FrontABaseUnit));
            set
            {
                this.Vehicle.FrontAUnit = (UnitsNet.Units.AreaUnit)value?.UnitEnumValue;
                this.RaisePropertyChanged(() => VehicleMotorEinlassHoeheH);
            }
        }

        public double? VehicleMotorEinlassLaengeL
        {
            get => this.Vehicle?.FrontA;
            set
            {
                this.Vehicle.FrontA = value;
                this.SaveButton = true;
            }
        }

        public UnitListItem VehicleMotorEinlassLaengeLUnit
        {
            get => this.LengthQuantityUnits.SingleOrDefault(x => x.UnitEnumValue.Equals(this.Vehicle?.Motor?.Einlass?.LaengeLUnit)) ?? this.AreaQuantityUnits.Single(x => x.UnitEnumValue.Equals(Data.Models.VehiclesModel.FrontABaseUnit));
            set
            {
                this.Vehicle.FrontAUnit = (UnitsNet.Units.AreaUnit)value?.UnitEnumValue;
                this.RaisePropertyChanged(() => VehicleMotorEinlassLaengeL);
            }
        }

        public double? VehicleMotorEinlassVergaserDurchmesserD
        {
            get => this.Vehicle?.FrontA;
            set
            {
                this.Vehicle.FrontA = value;
                this.SaveButton = true;
            }
        }

        public UnitListItem VehicleMotorEinlassVergaserDurchmesserDUnit
        {
            get => this.LengthQuantityUnits.SingleOrDefault(x => x.UnitEnumValue.Equals(this.Vehicle?.Motor?.Einlass?.Vergaser?.DurchmesserDUnit)) ?? this.AreaQuantityUnits.Single(x => x.UnitEnumValue.Equals(Data.Models.VehiclesModel.FrontABaseUnit));
            set
            {
                this.Vehicle.FrontAUnit = (UnitsNet.Units.AreaUnit)value?.UnitEnumValue;
                this.RaisePropertyChanged(() => VehicleMotorEinlassVergaserDurchmesserD);
            }
        }

        public double? VehicleMotorHubL
        {
            get => this.Vehicle?.FrontA;
            set
            {
                this.Vehicle.FrontA = value;
                this.SaveButton = true;
            }
        }

        public UnitListItem VehicleMotorHubLUnit
        {
            get => this.LengthQuantityUnits.SingleOrDefault(x => x.UnitEnumValue.Equals(this.Vehicle?.Motor?.HubLUnit)) ?? this.AreaQuantityUnits.Single(x => x.UnitEnumValue.Equals(Data.Models.VehiclesModel.FrontABaseUnit));
            set
            {
                this.Vehicle.FrontAUnit = (UnitsNet.Units.AreaUnit)value?.UnitEnumValue;
                this.RaisePropertyChanged(() => VehicleMotorHubL);
            }
        }

        public double? VehicleMotorHubraumV
        {
            get => this.Vehicle?.FrontA;
            set
            {
                this.Vehicle.FrontA = value;
                this.SaveButton = true;
            }
        }

        public UnitListItem VehicleMotorHubraumVUnit
        {
            get => this.VolumeQuantityUnits.SingleOrDefault(x => x.UnitEnumValue.Equals(this.Vehicle?.Motor?.HubraumVUnit)) ?? this.AreaQuantityUnits.Single(x => x.UnitEnumValue.Equals(Data.Models.VehiclesModel.FrontABaseUnit));
            set
            {
                this.Vehicle.FrontAUnit = (UnitsNet.Units.AreaUnit)value?.UnitEnumValue;
                this.RaisePropertyChanged(() => VehicleMotorHubraumV);
            }
        }

        public double? VehicleMotorKolbenG
        {
            get => this.Vehicle?.FrontA;
            set
            {
                this.Vehicle.FrontA = value;
                this.SaveButton = true;
            }
        }

        public UnitListItem VehicleMotorKolbenGUnit
        {
            get => this.SpeedQuantityUnits.SingleOrDefault(x => x.UnitEnumValue.Equals(this.Vehicle?.Motor?.KolbenGUnit)) ?? this.AreaQuantityUnits.Single(x => x.UnitEnumValue.Equals(Data.Models.VehiclesModel.FrontABaseUnit));
            set
            {
                this.Vehicle.FrontAUnit = (UnitsNet.Units.AreaUnit)value?.UnitEnumValue;
                this.RaisePropertyChanged(() => VehicleMotorKolbenG);
            }
        }

        public double? VehicleMotorKurbelgehaeuseV
        {
            get => this.Vehicle?.FrontA;
            set
            {
                this.Vehicle.FrontA = value;
                this.SaveButton = true;
            }
        }

        public UnitListItem VehicleMotorKurbelgehaeuseVUnit
        {
            get => this.VolumeQuantityUnits.SingleOrDefault(x => x.UnitEnumValue.Equals(this.Vehicle?.Motor?.KurbelgehaeuseVUnit)) ?? this.AreaQuantityUnits.Single(x => x.UnitEnumValue.Equals(Data.Models.VehiclesModel.FrontABaseUnit));

            set
            {
                this.Vehicle.FrontAUnit = (UnitsNet.Units.AreaUnit)value?.UnitEnumValue;
                this.RaisePropertyChanged(() => VehicleMotorKurbelgehaeuseV);
            }
        }

        public double? VehicleMotorPleulL
        {
            get => this.Vehicle?.FrontA;
            set
            {
                this.Vehicle.FrontA = value;
                this.SaveButton = true;
            }
        }

        public UnitListItem VehicleMotorPleulLUnit
        {
            get => this.LengthQuantityUnits.SingleOrDefault(x => x.UnitEnumValue.Equals(this.Vehicle?.Motor?.PleulLUnit)) ?? this.AreaQuantityUnits.Single(x => x.UnitEnumValue.Equals(Data.Models.VehiclesModel.FrontABaseUnit));
            set
            {
                this.Vehicle.FrontAUnit = (UnitsNet.Units.AreaUnit)value?.UnitEnumValue;
                this.RaisePropertyChanged(() => VehicleMotorPleulL);
            }
        }

        public double? VehicleMotorUeberstroemerBreiteB
        {
            get => this.Vehicle?.FrontA;
            set
            {
                this.Vehicle.FrontA = value;
                this.SaveButton = true;
            }
        }

        public UnitListItem VehicleMotorUeberstroemerBreiteBUnit
        {
            get => this.LengthQuantityUnits.SingleOrDefault(x => x.UnitEnumValue.Equals(this.Vehicle?.Motor?.Ueberstroemer?.BreiteBUnit)) ?? this.AreaQuantityUnits.Single(x => x.UnitEnumValue.Equals(Data.Models.VehiclesModel.FrontABaseUnit));

            set
            {
                this.Vehicle.FrontAUnit = (UnitsNet.Units.AreaUnit)value?.UnitEnumValue;
                this.RaisePropertyChanged(() => VehicleMotorUeberstroemerBreiteB);
            }
        }

        public double? VehicleMotorUeberstroemerFlaecheA
        {
            get => this.Vehicle?.FrontA;
            set
            {
                this.Vehicle.FrontA = value;
                this.SaveButton = true;
            }
        }

        public UnitListItem VehicleMotorUeberstroemerFlaecheAUnit
        {
            get => this.AreaQuantityUnits.SingleOrDefault(x => x.UnitEnumValue.Equals(this.Vehicle?.Motor?.Ueberstroemer?.FlaecheAUnit)) ?? this.AreaQuantityUnits.Single(x => x.UnitEnumValue.Equals(Data.Models.VehiclesModel.FrontABaseUnit));
            set
            {
                this.Vehicle.FrontAUnit = (UnitsNet.Units.AreaUnit)value?.UnitEnumValue;
                this.RaisePropertyChanged(() => VehicleMotorUeberstroemerFlaecheA);
            }
        }

        public double? VehicleMotorUeberstroemerHoeheH
        {
            get => this.Vehicle?.FrontA;
            set
            {
                this.Vehicle.FrontA = value;
                this.SaveButton = true;
            }
        }

        public UnitListItem VehicleMotorUeberstroemerHoeheHUnit
        {
            get => this.LengthQuantityUnits.SingleOrDefault(x => x.UnitEnumValue.Equals(this.Vehicle?.Motor?.Ueberstroemer?.HoeheHUnit)) ?? this.AreaQuantityUnits.Single(x => x.UnitEnumValue.Equals(Data.Models.VehiclesModel.FrontABaseUnit));
            set
            {
                this.Vehicle.FrontAUnit = (UnitsNet.Units.AreaUnit)value?.UnitEnumValue;
                this.RaisePropertyChanged(() => VehicleMotorUeberstroemerHoeheH);
            }
        }

        #endregion Units

        #endregion Values
    }
}