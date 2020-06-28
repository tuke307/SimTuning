using Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace SimTuning.ViewModels.Tuning
{
    public class DataViewModel : BaseViewModel
    {
        public DataViewModel()
        {
            using (var Data = new Data.DatabaseContext())
            {
                IList<Data.Models.VehiclesModel> vehicList = Data.Vehicles.ToList();
                Vehicles = new ObservableCollection<Data.Models.VehiclesModel>(vehicList);

                IList<Data.Models.TuningModel> TuningList = Data.Tuning.ToList();
                Tunings = new ObservableCollection<Data.Models.TuningModel>(TuningList);

                Tuning = Tunings.Where(d => d.Active == true).FirstOrDefault();
            }
        }

        public ICommand SaveTuningCommand { get; set; }
        public ICommand ShowSaveButtonCommand { get; set; }
        public ICommand NewTuningCommand { get; set; }
        public ICommand DeleteTuningCommand { get; set; }

        protected virtual void NewTuning()
        {
            //Fahrzeug zurücksetzen
            Vehicle = null;

            TuningModel Tuning = new TuningModel()
            {
                Name = "Tuning-Durchgang",
                Beschreibung = "Erstellt am " + DateTime.Now.ToString()
            };

            //in Collection hinzufügen
            Tunings.Add(Tuning);

            //vorauswahl
            Tuning = Tunings.Last();
            CreateNewVehicle = true;

            SaveButton = true;
        }

        protected virtual void DeleteTuning()
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
        }

        protected virtual void ShowSave(object obj)
        {
            SaveButton = true;
        }

        protected virtual void SaveTuning()
        {
            if (Tuning != null)
            {
                //Vehicle zuweisen
                if (TakeExistingVehicle)
                {
                    //kein Vehicle ausgewählt
                    if (Vehicle == null)
                    {
                        //Snackbar message

                        return;
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
        }

        public bool SaveButton
        {
            get => Get<bool>();
            set => Set(value);
        }

        public bool CreateNewVehicle
        {
            get => Get<bool>();
            set => Set(value);
        }

        public bool TakeExistingVehicle
        {
            get => Get<bool>();
            set => Set(value);
        }

        public ObservableCollection<Data.Models.VehiclesModel> Vehicles
        {
            get => Get<ObservableCollection<Data.Models.VehiclesModel>>();
            set => Set(value);
        }

        public Data.Models.VehiclesModel Vehicle
        {
            get => Get<Data.Models.VehiclesModel>();
            set { Set(value); SaveButton = true; }
        }

        public Data.Models.TuningModel Tuning
        {
            get => Get<Data.Models.TuningModel>();
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

                    Set(value);
                }
                else
                {
                    CreateNewVehicle = false;
                    TakeExistingVehicle = false;

                    Set(value);

                    ////gerade gelöscht => letztes, in Datenbank gespeichertes Vehicle neu laden
                    //if (Tunings.Count != 0)
                    //{
                    //    //Tuning = Tunings.LastOrDefault();
                    //}
                    //else
                    //{
                    //    //wenn lsite leer ist

                    //}
                }

                //Da werte in UI geändert wurden, wird save-button angezeigt, daher nach dem laden wieder disablen
                SaveButton = false;
            }
        }

        public ObservableCollection<Data.Models.TuningModel> Tunings
        {
            get => Get<ObservableCollection<Data.Models.TuningModel>>();
            set => Set(value);
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
    }
}