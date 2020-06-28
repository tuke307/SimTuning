using Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace SimTuning.ViewModels.Dyno
{
    public class DataViewModel : BaseViewModel
    {
        public DataViewModel()
        {
            using (var Data = new Data.DatabaseContext())
            {
                IList<Data.Models.VehiclesModel> vehicList = Data.Vehicles.ToList();
                Vehicles = new ObservableCollection<Data.Models.VehiclesModel>(vehicList);

                IList<Data.Models.DynoModel> dynoList = Data.Dyno.ToList();
                Dynos = new ObservableCollection<Data.Models.DynoModel>(dynoList);

                Dyno = Dynos.Where(d => d.Active == true).FirstOrDefault();
            }
        }

        public ICommand SaveDynoCommand { get; set; }
        public ICommand ShowSaveButtonCommand { get; set; }
        public ICommand NewDynoCommand { get; set; }
        public ICommand DeleteDynoCommand { get; set; }

        protected virtual void NewDyno()
        {
            //Fahrzeug zurücksetzen
            Vehicle = null;

            DynoModel dyno = new DynoModel()
            {
                Name = "Dyno-Durchgang",
                Beschreibung = "Erstellt am " + DateTime.Now.ToString()
            };

            //in Collection hinzufügen
            Dynos.Add(dyno);

            //vorauswahl
            Dyno = Dynos.Last();
            CreateNewVehicle = true;

            SaveButton = true;
        }

        protected virtual void DeleteDyno()
        {
            //in Datenbank löschen
            if (Dyno.Id != 0)
            {
                using (var Data = new Data.DatabaseContext())
                {
                    //in Datenbank löschen
                    Data.Dyno.Remove(Dyno);

                    Data.SaveChanges();
                }

                Dynos.Remove(Dynos.Where(d => d.Id == Dyno.Id).First());
            }

            //in lokaler liste löschen
            Dynos.Remove(Dyno);

            //Letzten in liste Laden
            Dyno = null;
        }

        protected virtual void ShowSave(object obj)
        {
            SaveButton = true;
        }

        protected virtual void SaveDyno()
        {
            if (Dyno != null)
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

                    Dyno.Vehicle = Vehicle;
                }
                //neues Fahrzeug erstellen
                else if (CreateNewVehicle)
                {
                    Data.Models.VehiclesModel vehicle = new Data.Models.VehiclesModel()
                    {
                        Name = "Dyno-Fahrzeug",
                        Beschreibung = "Erstellt über Dyno-Modul mit der Option 'Neues Fahrzeug erstellen'",
                        Deletable = true
                    };

                    using (var Data = new Data.DatabaseContext())
                    {
                        Data.Vehicles.Add(vehicle);

                        Data.SaveChanges();
                    }

                    //neues Vehicle Dyno zuweisen
                    Dyno.Vehicle = vehicle;

                    //Lokaler list hinzufügen und auswählen
                    Vehicles.Add(vehicle);

                    Vehicle = Vehicles.Last();

                    //Radio buttons zurücksetzen
                    CreateNewVehicle = false;
                    TakeExistingVehicle = true;
                }

                using (var Data = new Data.DatabaseContext())
                {
                    Data.Dyno.Update(Dyno);

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

        public Data.Models.DynoModel Dyno
        {
            get => Get<Data.Models.DynoModel>();
            set
            {
                //alten ausgewälten Dyno-Datensatz inaktiv setzen
                SetInactiveDyno();

                //fahrzeug zurücksetzen
                Vehicle = null;

                if (value != null)
                {
                    //Active setzen
                    value = SetActiveDyno(value);

                    //komplett laden
                    value = LoadDyno(value);

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
                    //if (Dynos.Count != 0)
                    //{
                    //    //Dyno = Dynos.LastOrDefault();
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

        public ObservableCollection<Data.Models.DynoModel> Dynos
        {
            get => Get<ObservableCollection<Data.Models.DynoModel>>();
            set => Set(value);
        }

        protected Data.Models.DynoModel LoadDyno(Data.Models.DynoModel dyno)
        {
            try
            {
                using (var Data = new Data.DatabaseContext())
                {
                        //Vehicle+Dyno laden
                        return Data.Dyno
                          .Where(v => v.Id == dyno.Id)
                          .Include(v => v.Vehicle)
                          .Include(v => v.Audio)
                          .Include(v => v.DynoNm)
                          .Include(v => v.DynoPS)
                          .First();
                   
                }
            }
            catch (Exception)
            {
                return dyno;
                throw;
            }
        }

        protected virtual DynoModel SetActiveDyno(DynoModel dyno)
        {
            try
            {
                //Dyno-Datensatz in collection aktiv setzen
                Dynos.Where(d => d == dyno).First().Active = true;

                //in Database ändern
                using (var db = new Data.DatabaseContext())
                {
                    dyno.Active = true;

                    db.Dyno.Update(dyno);

                    db.SaveChanges();
                }
            }
            catch
            {
                //z.B. kein Dyno aktiv
                //z.B. nicht in datenbank
            }

            return dyno;
        }

        protected virtual void SetInactiveDyno()
        {
            try
            {
                //aktive Dyno(s) holen
                var dynos = Dynos.Where(d => d.Active == true).ToList();

                //für local collection
                foreach (var item in Dynos)
                    item.Active = false;

                //für datenbank
                foreach (var item in dynos)
                    item.Active = false;

                //in Database ändern
                using (var db = new Data.DatabaseContext())
                {
                    db.Dyno.UpdateRange(dynos);

                    db.SaveChanges();
                }
            }
            catch
            {
                //z.B. kein Dyno aktiv
                //z.B. nicht in datenbank
            }
        }
    }
}