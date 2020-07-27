using Data.Models;
using Microsoft.EntityFrameworkCore;
using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace SimTuning.Core.ViewModels.Tuning
{
    public class DataViewModel : MvxNavigationViewModel
    {
        public DataViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService) : base(logProvider, navigationService)
        {
            using (var Data = new Data.DatabaseContext())
            {
                IList<Data.Models.VehiclesModel> vehicList = Data.Vehicles.ToList();
                Vehicles = new ObservableCollection<Data.Models.VehiclesModel>(vehicList);

                IList<Data.Models.TuningModel> TuningList = Data.Tuning.ToList();
                Tunings = new ObservableCollection<Data.Models.TuningModel>(TuningList);

                Tuning = Tunings.Where(d => d.Active == true).FirstOrDefault();
            }

            ShowSaveButtonCommand = new MvxCommand(ShowSave);
        }

        public IMvxCommand SaveTuningCommand { get; set; }
        public IMvxCommand ShowSaveButtonCommand { get; set; }
        public IMvxCommand NewTuningCommand { get; set; }
        public IMvxCommand DeleteTuningCommand { get; set; }

        public override void Prepare()
        {
            // This is the first method to be called after construction
        }

        public override Task Initialize()
        {
            // Async initialization

            return base.Initialize();
        }

        #region Commands

        protected virtual void NewTuning()
        {
            //Fahrzeug zurücksetzen
            Vehicle = null;

            TuningModel tuning = new TuningModel()
            {
                Name = "Tuning-Durchgang",
                Beschreibung = "Erstellt am " + DateTime.Now.ToString()
            };

            //in Collection hinzufügen
            Tunings.Add(tuning);

            //vorauswahl
            tuning = Tunings.Last();
            CreateNewVehicle = true;

            SaveButton = true;
        }

        protected virtual bool DeleteTuning()
        {
            try
            {
                //in Datenbank löschen
                if (Tuning.Id != 0)
                {
                    using (var Data = new Data.DatabaseContext())
                    {
                        //in Datenbank löschen
                        Data.Tuning.Remove(Tuning);

                        Data.SaveChanges();
                    }

                    Tunings.Remove(Tunings.Where(d => d.Id == Tuning.Id).First());
                }

                //in lokaler liste löschen
                Tunings.Remove(Tuning);

                //Letzten in liste Laden
                Tuning = null;

                return true;
            }
            catch
            {
                return false;
            }
        }

        protected virtual void ShowSave()
        {
            SaveButton = true;
        }

        protected virtual bool SaveTuning()
        {
            try
            {
                if (Tuning != null)
                {
                    //Vehicle zuweisen
                    if (TakeExistingVehicle)
                    {
                        //kein Vehicle ausgewählt
                        if (Vehicle == null)
                        {
                            return false;
                        }

                        Tuning.Vehicle = Vehicle;
                    }
                    //neues Fahrzeug erstellen
                    else if (CreateNewVehicle)
                    {
                        Data.Models.VehiclesModel vehicle = new Data.Models.VehiclesModel()
                        {
                            Name = "Tuning-Fahrzeug",
                            Beschreibung = "Erstellt über Tuning-Modul mit der Option 'Neues Fahrzeug erstellen'",
                            Deletable = true
                        };

                        using (var Data = new Data.DatabaseContext())
                        {
                            Data.Vehicles.Add(vehicle);

                            Data.SaveChanges();
                        }

                        //neues Vehicle Tuning zuweisen
                        Tuning.Vehicle = vehicle;

                        //Lokaler list hinzufügen und auswählen
                        Vehicles.Add(vehicle);

                        Vehicle = Vehicles.Last();

                        //Radio buttons zurücksetzen
                        CreateNewVehicle = false;
                        TakeExistingVehicle = true;
                    }

                    using (var Data = new Data.DatabaseContext())
                    {
                        Data.Tuning.Update(Tuning);

                        Data.SaveChanges();
                    }
                }

                SaveButton = false;
                return true;
            }
            catch
            {
                return false;
            }
        }

        protected Data.Models.TuningModel LoadTuning(Data.Models.TuningModel Tuning)
        {
            try
            {
                using (var Data = new Data.DatabaseContext())
                {
                    //Vehicle+Tuning laden
                    return Data.Tuning
                      .Where(v => v.Id == Tuning.Id)
                      .Include(v => v.Tuning)
                      .First();
                }
            }
            catch (Exception)
            {
                return Tuning;
                throw;
            }
        }

        protected virtual TuningModel SetActiveTuning(TuningModel Tuning)
        {
            try
            {
                //Tuning-Datensatz in collection aktiv setzen
                Tunings.Where(d => d == Tuning).First().Active = true;

                //in Database ändern
                using (var db = new Data.DatabaseContext())
                {
                    Tuning.Active = true;

                    db.Tuning.Update(Tuning);

                    db.SaveChanges();
                }
            }
            catch
            {
                //z.B. kein Tuning aktiv
                //z.B. nicht in datenbank
            }

            return Tuning;
        }

        protected virtual void SetInactiveTuning()
        {
            try
            {
                //aktive Tuning(s) holen
                var tunings = Tunings.Where(d => d.Active == true).ToList();

                //für local collection
                foreach (var item in Tunings)
                    item.Active = false;

                //für datenbank
                foreach (var item in tunings)
                    item.Active = false;

                //in Database ändern
                using (var db = new Data.DatabaseContext())
                {
                    db.Tuning.UpdateRange(tunings);

                    db.SaveChanges();
                }
            }
            catch
            {
                //z.B. kein Tuning aktiv
                //z.B. nicht in datenbank
            }
        }

        #endregion Commands

        #region Values

        private bool _saveButton;

        public bool SaveButton
        {
            get => _saveButton;
            set { SetProperty(ref _saveButton, value); }
        }

        private bool _createNewVehicle;

        public bool CreateNewVehicle
        {
            get => _createNewVehicle;
            set { SetProperty(ref _createNewVehicle, value); }
        }

        private bool _takeExistingVehicle;

        public bool TakeExistingVehicle
        {
            get => _takeExistingVehicle;
            set { SetProperty(ref _takeExistingVehicle, value); }
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
                SetProperty(ref _vehicle, value);
                SaveButton = true;
            }
        }

        private Data.Models.TuningModel _tuning;

        public Data.Models.TuningModel Tuning
        {
            get => _tuning;
            set
            {
                //alten ausgewälten Tuning-Datensatz inaktiv setzen
                SetInactiveTuning();

                //fahrzeug zurücksetzen
                Vehicle = null;

                if (value != null)
                {
                    //Active setzen
                    value = SetActiveTuning(value);

                    //komplett laden
                    value = LoadTuning(value);

                    //Vehicle laden
                    if (value.Vehicle != null)
                    {
                        CreateNewVehicle = false;
                        TakeExistingVehicle = true;
                        Vehicle = Vehicles.Where(v => v.Id == value.Vehicle.Id).First();
                    }
                    else
                    {
                        CreateNewVehicle = true;
                        TakeExistingVehicle = false;
                    }
                }
                else
                {
                    CreateNewVehicle = false;
                    TakeExistingVehicle = false;
                }

                SetProperty(ref _tuning, value);

                //Da werte in UI geändert wurden, wird save-button angezeigt, daher nach dem laden wieder disablen
                SaveButton = false;
            }
        }

        private ObservableCollection<Data.Models.TuningModel> _tunings;

        public ObservableCollection<Data.Models.TuningModel> Tunings
        {
            get => _tunings;
            set { SetProperty(ref _tunings, value); }
        }

        #endregion Values
    }
}