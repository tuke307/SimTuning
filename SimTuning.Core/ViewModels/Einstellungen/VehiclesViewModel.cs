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

            this.UnitAnsaugleitungL = this.LengthQuantityUnits.Where(x => x.UnitEnumValue.Equals(LengthUnit.Millimeter)).First();
            this.UnitAuslassA = this.AreaQuantityUnits.Where(x => x.UnitEnumValue.Equals(AreaUnit.SquareMillimeter)).First();
            this.UnitAuslassB = this.LengthQuantityUnits.Where(x => x.UnitEnumValue.Equals(LengthUnit.Millimeter)).First();
            this.UnitAuslassH = this.LengthQuantityUnits.Where(x => x.UnitEnumValue.Equals(LengthUnit.Millimeter)).First();
            this.UnitBohrungD = this.LengthQuantityUnits.Where(x => x.UnitEnumValue.Equals(LengthUnit.Millimeter)).First();
            this.UnitBrennraumV = this.VolumeQuantityUnits.Where(x => x.UnitEnumValue.Equals(VolumeUnit.CubicMillimeter)).First();
            this.UnitDeachsierung = this.LengthQuantityUnits.Where(x => x.UnitEnumValue.Equals(LengthUnit.Millimeter)).First();
            this.UnitEinlassA = this.AreaQuantityUnits.Where(x => x.UnitEnumValue.Equals(AreaUnit.SquareMillimeter)).First();
            this.UnitEinlassB = this.LengthQuantityUnits.Where(x => x.UnitEnumValue.Equals(LengthUnit.Millimeter)).First();
            this.UnitEinlassH = this.LengthQuantityUnits.Where(x => x.UnitEnumValue.Equals(LengthUnit.Millimeter)).First();
            this.UnitFrontA = this.AreaQuantityUnits.Where(x => x.UnitEnumValue.Equals(AreaUnit.SquareMillimeter)).First();
            this.UnitGewicht = this.MassQuantityUnits.Where(x => x.UnitEnumValue.Equals(MassUnit.Kilogram)).First();
            this.UnitHub = this.LengthQuantityUnits.Where(x => x.UnitEnumValue.Equals(LengthUnit.Millimeter)).First();
            this.UnitHubraumV = this.VolumeQuantityUnits.Where(x => x.UnitEnumValue.Equals(VolumeUnit.CubicMillimeter)).First();
            this.UnitKurbelgehaeuseV = this.VolumeQuantityUnits.Where(x => x.UnitEnumValue.Equals(VolumeUnit.CubicMillimeter)).First();
            this.UnitPleulL = this.LengthQuantityUnits.Where(x => x.UnitEnumValue.Equals(LengthUnit.Millimeter)).First();
            this.UnitUeberstroemerA = this.AreaQuantityUnits.Where(x => x.UnitEnumValue.Equals(AreaUnit.SquareMillimeter)).First();
            this.UnitUeberstroemerB = this.LengthQuantityUnits.Where(x => x.UnitEnumValue.Equals(LengthUnit.Millimeter)).First();
            this.UnitUeberstroemerH = this.LengthQuantityUnits.Where(x => x.UnitEnumValue.Equals(LengthUnit.Millimeter)).First();

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

        #region Methods

        /// <summary>
        /// Deletes the vehicle.
        /// </summary>
        protected virtual void DeleteVehicle()
        {
            if (Vehicle.Deletable)
            {
                //in Datenbank löschen
                if (Vehicle.Id != 0)
                {
                    using (var db = new Data.DatabaseContext())
                    {
                        //in Datenbank löschen
                        db.Vehicles.Remove(Vehicle);

                        db.SaveChanges();
                    }

                    Vehicles.Remove(Vehicles.Where(v => v.Id == Vehicle.Id).First());
                }

                //in lokaler liste löschen
                Vehicles.Remove(Vehicle);

                Vehicle = null;
            }
        }

        /// <summary>
        /// Creates new vehicle.
        /// </summary>
        protected virtual void NewVehicle()
        {
            //Vordefinieren des neuen Fahrzeugs
            Vehicles.Add(new Data.Models.VehiclesModel()
            {
                Name = "Neues Fahrzeug",
                Beschreibung = "Erstellt am " + DateTime.Now + " über Fahrzeug-Modul",
                Deletable = true
            });

            Vehicle = Vehicles.Last();

            SaveButton = true;
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

            SaveButton = false;
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
                //wenn bereits in Datenbank
                if (value.Id != 0)
                {
                    //Vehicle+Motor+Dyno
                    return db.Vehicles
                  .Where(v => v.Id == value.Id)
                  .Include(v => v.Motor)
                  .Include(v => v.Motor.Einlass)
                  .Include(v => v.Motor.Auslass)
                  .Include(v => v.Motor.Ueberstroemer)
                  .Include(v => v.Dyno)
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
            get => _engine;
            set
            {
                if (Vehicle != null)
                {
                    //wenn Vehicle geladen wird; motor setzen für dropdown
                    if (Vehicle.MotorId.HasValue)
                        value = Engines.Where(m => m.Id == Vehicle.MotorId.Value).First();

                    if (value != null)
                    {
                        if (value.Id != Vehicle.MotorId)
                        {
                            //wenn beim Vehicle ein neuer Motor ausgewählt wird
                            Vehicle.Motor = value;
                            RaisePropertyChanged("Vehicle"); // Motor-Werte für UI updaten
                        }
                    }
                }

                SetProperty(ref _engine, value);
            }
        }

        public ObservableCollection<Data.Models.MotorModel> Engines
        {
            get => _engines;
            set { SetProperty(ref _engines, value); }
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
            get => _saveButton;
            set { SetProperty(ref _saveButton, value); }
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
            get => _vehicle;
            set
            {
                if (value != null)
                {
                    //Laden des kompletten Datensatzes
                    LoadVehicle(value);
                }
                else
                {
                    //gerade gelöscht => letztes Vehicle neu laden
                    if (Vehicles.Count != 0)
                        Vehicle = Vehicles.Last();
                }

                //Einfügen
                SetProperty(ref _vehicle, value);

                //nix mehr zu speichern
                SaveButton = false;

                //Motor refreshen
                Engine = null;
            }
        }

        public ObservableCollection<Data.Models.VehiclesModel> Vehicles
        {
            get => _vehicles;
            set { SetProperty(ref _vehicles, value); }
        }

        public ObservableCollection<UnitListItem> VolumeQuantityUnits { get; }

        #region Units

        public UnitListItem _unitEinlassA;
        public UnitListItem _unitEinlassB;
        private UnitListItem _unitAnsaugleitungL;

        private UnitListItem _unitAuslassA;

        private UnitListItem _unitAuslassB;

        private UnitListItem _unitAuslassH;

        private UnitListItem _unitBohrungD;

        private UnitListItem _unitBrennraumV;

        private UnitListItem _unitDeachsierung;

        private UnitListItem _unitEinlassH;

        private UnitListItem _unitFrontA;

        private UnitListItem _unitGewicht;

        private UnitListItem _unitHub;

        private UnitListItem _unitHubraumV;

        private UnitListItem _unitKurbelgehaeuseV;

        private UnitListItem _unitPleulL;

        private UnitListItem _unitUeberstroemerA;

        private UnitListItem _unitUeberstroemerB;

        private UnitListItem _unitUeberstroemerH;

        public UnitListItem UnitAnsaugleitungL
        {
            get => _unitAnsaugleitungL;
            set
            {
                if (Vehicle != null)
                    Vehicle.Motor.Einlass.LaengeL = Business.Functions.UpdateValue(Vehicle.Motor.Einlass.LaengeL, _unitAnsaugleitungL, value);

                SetProperty(ref _unitAnsaugleitungL, value);
            }
        }

        public UnitListItem UnitAuslassA
        {
            get => _unitAuslassA;
            set
            {
                if (Vehicle != null)
                    Vehicle.Motor.Auslass.FlaecheA = Business.Functions.UpdateValue(Vehicle.Motor.Auslass.FlaecheA, _unitAuslassA, value);

                SetProperty(ref _unitAuslassA, value);
            }
        }

        public UnitListItem UnitAuslassB
        {
            get => _unitAuslassB;
            set
            {
                if (Vehicle != null)
                    Vehicle.Motor.Auslass.BreiteB = Business.Functions.UpdateValue(Vehicle.Motor.Auslass.BreiteB, _unitAuslassB, value);

                SetProperty(ref _unitAuslassB, value);
            }
        }

        public UnitListItem UnitAuslassH
        {
            get => _unitAuslassH;
            set
            {
                if (Vehicle != null)
                    Vehicle.Motor.Auslass.HoeheH = Business.Functions.UpdateValue(Vehicle.Motor.Auslass.HoeheH, _unitAuslassH, value);

                SetProperty(ref _unitAuslassH, value);
            }
        }

        public UnitListItem UnitBohrungD
        {
            get => _unitBohrungD;
            set
            {
                if (Vehicle != null)
                    Vehicle.Motor.BohrungD = Business.Functions.UpdateValue(Vehicle.Motor.BohrungD, _unitBohrungD, value);

                SetProperty(ref _unitBohrungD, value);
            }
        }

        public UnitListItem UnitBrennraumV
        {
            get => _unitBrennraumV;
            set
            {
                if (Vehicle != null)
                    Vehicle.Motor.BrennraumV = Business.Functions.UpdateValue(Vehicle.Motor.BrennraumV, _unitBrennraumV, value);

                SetProperty(ref _unitBrennraumV, value);
            }
        }

        public UnitListItem UnitDeachsierung
        {
            get => _unitDeachsierung;
            set
            {
                if (Vehicle != null)
                    Vehicle.Motor.DeachsierungL = Business.Functions.UpdateValue(Vehicle.Motor.DeachsierungL, _unitDeachsierung, value);

                SetProperty(ref _unitDeachsierung, value);
            }
        }

        public UnitListItem UnitEinlassA
        {
            get => _unitEinlassA;
            set
            {
                if (Vehicle != null)
                    Vehicle.Motor.Einlass.FlaecheA = Business.Functions.UpdateValue(Vehicle.Motor.Einlass.FlaecheA, _unitEinlassA, value);

                SetProperty(ref _unitEinlassA, value);
            }
        }

        public UnitListItem UnitEinlassB
        {
            get => _unitEinlassB;
            set
            {
                if (Vehicle != null)
                    Vehicle.Motor.Einlass.BreiteB = Business.Functions.UpdateValue(Vehicle.Motor.Einlass.BreiteB, _unitEinlassB, value);

                SetProperty(ref _unitEinlassB, value);
            }
        }

        public UnitListItem UnitEinlassH
        {
            get => _unitEinlassH;
            set
            {
                if (Vehicle != null)
                    Vehicle.Motor.Einlass.HoeheH = Business.Functions.UpdateValue(Vehicle.Motor.Einlass.HoeheH, _unitEinlassH, value);

                SetProperty(ref _unitEinlassH, value);
            }
        }

        public UnitListItem UnitFrontA
        {
            get => _unitFrontA;
            set
            {
                if (Vehicle != null)
                    Vehicle.FrontA = Business.Functions.UpdateValue(Vehicle.FrontA, _unitFrontA, value);

                SetProperty(ref _unitFrontA, value);
            }
        }

        public UnitListItem UnitGewicht
        {
            get => _unitGewicht;
            set
            {
                if (Vehicle != null)
                    Vehicle.Gewicht = Business.Functions.UpdateValue(Vehicle.Gewicht, _unitGewicht, value);

                SetProperty(ref _unitGewicht, value);
            }
        }

        public UnitListItem UnitHub
        {
            get => _unitHub;
            set
            {
                if (Vehicle != null)
                    Vehicle.Motor.HubL = Business.Functions.UpdateValue(Vehicle.Motor.HubL, _unitHub, value);

                SetProperty(ref _unitHub, value);
            }
        }

        public UnitListItem UnitHubraumV
        {
            get => _unitHubraumV;
            set
            {
                if (Vehicle != null)
                    Vehicle.Motor.HubraumV = Business.Functions.UpdateValue(Vehicle.Motor.HubraumV, _unitHubraumV, value);

                SetProperty(ref _unitHubraumV, value);
            }
        }

        public UnitListItem UnitKurbelgehaeuseV
        {
            get => _unitKurbelgehaeuseV;
            set
            {
                if (Vehicle != null)
                    Vehicle.Motor.BrennraumV = Business.Functions.UpdateValue(Vehicle.Motor.KurbelgehaeuseV, _unitKurbelgehaeuseV, value);

                SetProperty(ref _unitKurbelgehaeuseV, value);
            }
        }

        public UnitListItem UnitPleulL
        {
            get => _unitPleulL;
            set
            {
                if (Vehicle != null)
                    Vehicle.Motor.PleulL = Business.Functions.UpdateValue(Vehicle.Motor.PleulL, _unitPleulL, value);

                SetProperty(ref _unitPleulL, value);
            }
        }

        public UnitListItem UnitUeberstroemerA
        {
            get => _unitUeberstroemerA;
            set
            {
                if (Vehicle != null)
                    Vehicle.Motor.Ueberstroemer.FlaecheA = Business.Functions.UpdateValue(Vehicle.Motor.Ueberstroemer.FlaecheA, _unitUeberstroemerA, value);

                SetProperty(ref _unitUeberstroemerA, value);
            }
        }

        public UnitListItem UnitUeberstroemerB
        {
            get => _unitUeberstroemerB;
            set
            {
                if (Vehicle != null)
                    Vehicle.Motor.Ueberstroemer.BreiteB = Business.Functions.UpdateValue(Vehicle.Motor.Ueberstroemer.BreiteB, _unitUeberstroemerB, value);

                SetProperty(ref _unitUeberstroemerB, value);
            }
        }

        public UnitListItem UnitUeberstroemerH
        {
            get => _unitUeberstroemerH;
            set
            {
                if (Vehicle != null)
                    Vehicle.Motor.Ueberstroemer.HoeheH = Business.Functions.UpdateValue(Vehicle.Motor.Ueberstroemer.HoeheH, _unitUeberstroemerH, value);

                SetProperty(ref _unitUeberstroemerH, value);
            }
        }

        #endregion Units

        #endregion Values
    }
}