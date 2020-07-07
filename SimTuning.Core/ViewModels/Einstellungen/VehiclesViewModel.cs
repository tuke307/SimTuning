using Microsoft.EntityFrameworkCore;
using MvvmCross.Commands;
using MvvmCross.ViewModels;
using SimTuning.Core.Models;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using UnitsNet.Units;
using WordPressPCL.Models;

namespace SimTuning.Core.ViewModels.Einstellungen
{
    public class VehiclesViewModel : MvxViewModel<UserModel>
    {
        public UserModel User;
        public ObservableCollection<UnitListItem> AreaQuantityUnits { get; }
        public ObservableCollection<UnitListItem> MassQuantityUnits { get; }
        public ObservableCollection<UnitListItem> VolumeQuantityUnits { get; }
        public ObservableCollection<UnitListItem> LengthQuantityUnits { get; }

        public VehiclesViewModel()
        {
            AreaQuantityUnits = new AreaQuantity();
            VolumeQuantityUnits = new VolumeQuantity();
            LengthQuantityUnits = new LengthQuantity();
            MassQuantityUnits = new MassQuantity();

            UnitAnsaugleitungL = LengthQuantityUnits.Where(x => x.UnitEnumValue.Equals(LengthUnit.Millimeter)).First();
            UnitAuslassA = AreaQuantityUnits.Where(x => x.UnitEnumValue.Equals(AreaUnit.SquareMillimeter)).First();
            UnitAuslassB = LengthQuantityUnits.Where(x => x.UnitEnumValue.Equals(LengthUnit.Millimeter)).First();
            UnitAuslassH = LengthQuantityUnits.Where(x => x.UnitEnumValue.Equals(LengthUnit.Millimeter)).First();
            UnitBohrungD = LengthQuantityUnits.Where(x => x.UnitEnumValue.Equals(LengthUnit.Millimeter)).First();
            UnitBrennraumV = VolumeQuantityUnits.Where(x => x.UnitEnumValue.Equals(VolumeUnit.CubicMillimeter)).First();
            UnitDeachsierung = LengthQuantityUnits.Where(x => x.UnitEnumValue.Equals(LengthUnit.Millimeter)).First();
            UnitEinlassA = AreaQuantityUnits.Where(x => x.UnitEnumValue.Equals(AreaUnit.SquareMillimeter)).First();
            UnitEinlassB = LengthQuantityUnits.Where(x => x.UnitEnumValue.Equals(LengthUnit.Millimeter)).First();
            UnitEinlassH = LengthQuantityUnits.Where(x => x.UnitEnumValue.Equals(LengthUnit.Millimeter)).First();
            UnitFrontA = AreaQuantityUnits.Where(x => x.UnitEnumValue.Equals(AreaUnit.SquareMillimeter)).First();
            UnitGewicht = MassQuantityUnits.Where(x => x.UnitEnumValue.Equals(MassUnit.Kilogram)).First();
            UnitHub = LengthQuantityUnits.Where(x => x.UnitEnumValue.Equals(LengthUnit.Millimeter)).First();
            UnitHubraumV = VolumeQuantityUnits.Where(x => x.UnitEnumValue.Equals(VolumeUnit.CubicMillimeter)).First();
            UnitKurbelgehaeuseV = VolumeQuantityUnits.Where(x => x.UnitEnumValue.Equals(VolumeUnit.CubicMillimeter)).First();
            UnitPleulL = LengthQuantityUnits.Where(x => x.UnitEnumValue.Equals(LengthUnit.Millimeter)).First();
            UnitUeberstroemerA = AreaQuantityUnits.Where(x => x.UnitEnumValue.Equals(AreaUnit.SquareMillimeter)).First();
            UnitUeberstroemerB = LengthQuantityUnits.Where(x => x.UnitEnumValue.Equals(LengthUnit.Millimeter)).First();
            UnitUeberstroemerH = LengthQuantityUnits.Where(x => x.UnitEnumValue.Equals(LengthUnit.Millimeter)).First();

            using (var db = new Data.DatabaseContext())
            {
                Vehicles = new ObservableCollection<Data.Models.VehiclesModel>(db.Vehicles.ToList());

                Engines = new ObservableCollection<Data.Models.MotorModel>(db.Motor.ToList());
            }
        }

        public IMvxCommand NewVehicleCommand { get; set; }
        public IMvxCommand DeleteVehicleCommand { get; set; }
        public IMvxCommand SaveVehicleCommand { get; set; }
        public IMvxCommand ShowSaveButtonCommand { get; set; }

        public override void Prepare(UserModel _user)
        {
            // This is the first method to be called after construction

            User = _user;
        }

        public override Task Initialize()
        {
            // Async initialization

            return base.Initialize();
        }

        #region Commands

        protected void ShowSave()
        {
            SaveButton = true;
        }

        protected virtual void SaveVehicle()
        {
            using (var db = new Data.DatabaseContext())
            {
                db.Vehicles.Attach(Vehicle);

                db.SaveChanges();
            }

            SaveButton = false;
        }

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
                    return value;
            }
        }

        #endregion Commands

        #region Values

        private ObservableCollection<Data.Models.MotorModel> _engines;

        public ObservableCollection<Data.Models.MotorModel> Engines
        {
            get => _engines;
            set { SetProperty(ref _engines, value); }
        }

        private Data.Models.MotorModel _engine;

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

        private ObservableCollection<Data.Models.VehiclesModel> _vehicles;

        public ObservableCollection<Data.Models.VehiclesModel> Vehicles
        {
            get => _vehicles;
            set { SetProperty(ref _vehicles, value); }
        }

        private Data.Models.VehiclesModel _vehicle;

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

        private bool _saveButton;

        public bool SaveButton
        {
            get => _saveButton;
            set { SetProperty(ref _saveButton, value); }
        }

        #region Units

        private UnitListItem _unitAnsaugleitungL;

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

        private UnitListItem _unitFrontA;

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

        private UnitListItem _unitGewicht;

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

        private UnitListItem _unitHub;

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

        private UnitListItem _unitPleulL;

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

        private UnitListItem _unitDeachsierung;

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

        private UnitListItem _unitBohrungD;

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

        private UnitListItem _unitHubraumV;

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

        private UnitListItem _unitBrennraumV;

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

        private UnitListItem _unitKurbelgehaeuseV;

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

        private UnitListItem _unitEinlassH;

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

        public UnitListItem _unitEinlassB;

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

        public UnitListItem _unitEinlassA;

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

        private UnitListItem _unitAuslassH;

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

        private UnitListItem _unitAuslassB;

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

        private UnitListItem _unitAuslassA;

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

        private UnitListItem _unitUeberstroemerH;

        public UnitListItem UnitUeberstroemerH
        {
            get => _unitUeberstroemerH;
            set
            {
                if (Vehicle != null)
                    Vehicle.Motor.Ueberstroemer.Hoehe = Business.Functions.UpdateValue(Vehicle.Motor.Ueberstroemer.Hoehe, _unitUeberstroemerH, value);

                SetProperty(ref _unitUeberstroemerH, value);
            }
        }

        private UnitListItem _unitUeberstroemerB;

        public UnitListItem UnitUeberstroemerB
        {
            get => _unitUeberstroemerB;
            set
            {
                if (Vehicle != null)
                    Vehicle.Motor.Ueberstroemer.Breite = Business.Functions.UpdateValue(Vehicle.Motor.Ueberstroemer.Breite, _unitUeberstroemerB, value);

                SetProperty(ref _unitUeberstroemerB, value);
            }
        }

        private UnitListItem _unitUeberstroemerA;

        public UnitListItem UnitUeberstroemerA
        {
            get => _unitUeberstroemerA;
            set
            {
                if (Vehicle != null)
                    Vehicle.Motor.Ueberstroemer.Flaeche = Business.Functions.UpdateValue(Vehicle.Motor.Ueberstroemer.Flaeche, _unitUeberstroemerA, value);

                SetProperty(ref _unitUeberstroemerA, value);
            }
        }

        #endregion Units

        #endregion Values
    }
}