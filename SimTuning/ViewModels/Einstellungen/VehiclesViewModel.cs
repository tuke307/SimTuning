using Microsoft.EntityFrameworkCore;
using SimTuning.Models;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using UnitsNet.Units;

namespace SimTuning.ViewModels.Einstellungen
{
    public class VehiclesViewModel : BaseViewModel
    {
        //private readonly MainWindowViewModel mainWindowViewModel;
        public ObservableCollection<UnitListItem> AreaQuantityUnits { get; }

        public ObservableCollection<UnitListItem> MassQuantityUnits { get; }
        public ObservableCollection<UnitListItem> VolumeQuantityUnits { get; }
        public ObservableCollection<UnitListItem> LengthQuantityUnits { get; }

        public VehiclesViewModel(/*MainWindowViewModel mainWindowViewModel*/)
        {
            //this.mainWindowViewModel = mainWindowViewModel;

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

            //NewVehicleCommand = new ActionCommand(NewVehicle);
            //DeleteVehicleCommand = new ActionCommand(DeleteVehicle);
            //SaveVehicleCommand = new ActionCommand(SaveVehicle);
            //ShowSaveButtonCommand = new ActionCommand(ShowSave);
        }

        protected void ShowSave(object obj)
        {
            SaveButton = true;
        }

        //Speichern eines Fahrzeugs
        protected void SaveVehicle()
        {
            using (var db = new Data.DatabaseContext())
            {
                db.Vehicles.Attach(Vehicle);

                db.SaveChanges();
            }

            SaveButton = false;
        }

        protected void NewVehicle()
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

        protected void DeleteVehicle()
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

        public ICommand NewVehicleCommand { get; set; }
        public ICommand DeleteVehicleCommand { get; set; }
        public ICommand SaveVehicleCommand { get; set; }
        public ICommand ShowSaveButtonCommand { get; set; }

        public ObservableCollection<Data.Models.MotorModel> Engines
        {
            get => Get<ObservableCollection<Data.Models.MotorModel>>();
            set => Set(value);
        }

        public Data.Models.MotorModel Engine
        {
            get => Get<Data.Models.MotorModel>();
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
                            OnPropertyChanged("Vehicle"); // Motor-Werte für UI updaten
                        }
                    }
                }

                Set(value);
            }
        }

        public ObservableCollection<Data.Models.VehiclesModel> Vehicles
        {
            get => Get<ObservableCollection<Data.Models.VehiclesModel>>();
            set => Set(value);
        }

        public Data.Models.VehiclesModel Vehicle
        {
            get => Get<Data.Models.VehiclesModel>();
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
                Set(value);

                //nix mehr zu speichern
                SaveButton = false;

                //Motor refreshen
                Engine = null;
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

        public bool SaveButton
        {
            get => Get<bool>();
            set => Set(value);
        }

        public UnitListItem UnitAnsaugleitungL
        {
            get => Get<UnitListItem>();
            set
            {
                if (Vehicle != null)
                    Vehicle.Motor.Einlass.LaengeL = Business.Functions.UpdateValue(Vehicle.Motor.Einlass.LaengeL, UnitAnsaugleitungL, value);

                Set(value);
            }
        }

        public UnitListItem UnitFrontA
        {
            get => Get<UnitListItem>();
            set
            {
                if (Vehicle != null)
                    Vehicle.FrontA = Business.Functions.UpdateValue(Vehicle.FrontA, UnitFrontA, value);

                Set(value);
            }
        }

        public UnitListItem UnitGewicht
        {
            get => Get<UnitListItem>();
            set
            {
                if (Vehicle != null)
                    Vehicle.Gewicht = Business.Functions.UpdateValue(Vehicle.Gewicht, UnitGewicht, value);

                Set(value);
            }
        }

        public UnitListItem UnitHub
        {
            get => Get<UnitListItem>();
            set
            {
                if (Vehicle != null)
                    Vehicle.Motor.HubL = Business.Functions.UpdateValue(Vehicle.Motor.HubL, UnitHub, value);

                Set(value);
            }
        }

        public UnitListItem UnitPleulL
        {
            get => Get<UnitListItem>();
            set
            {
                if (Vehicle != null)
                    Vehicle.Motor.PleulL = Business.Functions.UpdateValue(Vehicle.Motor.PleulL, UnitPleulL, value);

                Set(value);
            }
        }

        public UnitListItem UnitDeachsierung
        {
            get => Get<UnitListItem>();
            set
            {
                if (Vehicle != null)
                    Vehicle.Motor.DeachsierungL = Business.Functions.UpdateValue(Vehicle.Motor.DeachsierungL, UnitDeachsierung, value);

                Set(value);
            }
        }

        public UnitListItem UnitBohrungD
        {
            get => Get<UnitListItem>();
            set
            {
                if (Vehicle != null)
                    Vehicle.Motor.BohrungD = Business.Functions.UpdateValue(Vehicle.Motor.BohrungD, UnitBohrungD, value);

                Set(value);
            }
        }

        public UnitListItem UnitHubraumV
        {
            get => Get<UnitListItem>();
            set
            {
                if (Vehicle != null)
                    Vehicle.Motor.HubraumV = Business.Functions.UpdateValue(Vehicle.Motor.HubraumV, UnitHubraumV, value);

                Set(value);
            }
        }

        public UnitListItem UnitBrennraumV
        {
            get => Get<UnitListItem>();
            set
            {
                if (Vehicle != null)
                    Vehicle.Motor.BrennraumV = Business.Functions.UpdateValue(Vehicle.Motor.BrennraumV, UnitBrennraumV, value);

                Set(value);
            }
        }

        public UnitListItem UnitKurbelgehaeuseV
        {
            get => Get<UnitListItem>();
            set
            {
                if (Vehicle != null)
                    Vehicle.Motor.BrennraumV = Business.Functions.UpdateValue(Vehicle.Motor.KurbelgehaeuseV, UnitKurbelgehaeuseV, value);

                Set(value);
            }
        }

        public UnitListItem UnitEinlassH
        {
            get => Get<UnitListItem>();
            set
            {
                if (Vehicle != null)
                    Vehicle.Motor.Einlass.HoeheH = Business.Functions.UpdateValue(Vehicle.Motor.Einlass.HoeheH, UnitEinlassH, value);

                Set(value);
            }
        }

        public UnitListItem UnitEinlassB
        {
            get => Get<UnitListItem>();
            set
            {
                if (Vehicle != null)
                    Vehicle.Motor.Einlass.BreiteB = Business.Functions.UpdateValue(Vehicle.Motor.Einlass.BreiteB, UnitEinlassB, value);

                Set(value);
            }
        }

        public UnitListItem UnitEinlassA
        {
            get => Get<UnitListItem>();
            set
            {
                if (Vehicle != null)
                    Vehicle.Motor.Einlass.FlaecheA = Business.Functions.UpdateValue(Vehicle.Motor.Einlass.FlaecheA, UnitEinlassA, value);

                Set(value);
            }
        }

        public UnitListItem UnitAuslassH
        {
            get => Get<UnitListItem>();
            set
            {
                if (Vehicle != null)
                    Vehicle.Motor.Auslass.HoeheH = Business.Functions.UpdateValue(Vehicle.Motor.Auslass.HoeheH, UnitAuslassH, value);

                Set(value);
            }
        }

        public UnitListItem UnitAuslassB
        {
            get => Get<UnitListItem>();
            set
            {
                if (Vehicle != null)
                    Vehicle.Motor.Auslass.BreiteB = Business.Functions.UpdateValue(Vehicle.Motor.Auslass.BreiteB, UnitAuslassB, value);

                Set(value);
            }
        }

        public UnitListItem UnitAuslassA
        {
            get => Get<UnitListItem>();
            set
            {
                if (Vehicle != null)
                    Vehicle.Motor.Auslass.FlaecheA = Business.Functions.UpdateValue(Vehicle.Motor.Auslass.FlaecheA, UnitAuslassA, value);

                Set(value);
            }
        }

        public UnitListItem UnitUeberstroemerH
        {
            get => Get<UnitListItem>();
            set
            {
                if (Vehicle != null)
                    Vehicle.Motor.Ueberstroemer.Hoehe = Business.Functions.UpdateValue(Vehicle.Motor.Ueberstroemer.Hoehe, UnitUeberstroemerH, value);

                Set(value);
            }
        }

        public UnitListItem UnitUeberstroemerB
        {
            get => Get<UnitListItem>();
            set
            {
                if (Vehicle != null)
                    Vehicle.Motor.Ueberstroemer.Breite = Business.Functions.UpdateValue(Vehicle.Motor.Ueberstroemer.Breite, UnitUeberstroemerB, value);

                Set(value);
            }
        }

        public UnitListItem UnitUeberstroemerA
        {
            get => Get<UnitListItem>();
            set
            {
                if (Vehicle != null)
                    Vehicle.Motor.Ueberstroemer.Flaeche = Business.Functions.UpdateValue(Vehicle.Motor.Ueberstroemer.Flaeche, UnitUeberstroemerA, value);

                Set(value);
            }
        }
    }
}