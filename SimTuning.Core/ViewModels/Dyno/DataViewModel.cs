// project=SimTuning.Core, file=DataViewModel.cs, creation=2020:7:31
// Copyright (c) 2020 tuke productions. All rights reserved.
namespace SimTuning.Core.ViewModels.Dyno
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Threading.Tasks;
    using Data.Models;
    using Microsoft.EntityFrameworkCore;
    using MvvmCross.Commands;
    using MvvmCross.Logging;
    using MvvmCross.Navigation;
    using MvvmCross.ViewModels;

    /// <summary>
    /// Dyno-Data-ViewModel.
    /// </summary>
    /// <seealso cref="MvvmCross.ViewModels.MvxNavigationViewModel" />
    public class DataViewModel : MvxNavigationViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DataViewModel"/> class.
        /// </summary>
        /// <param name="logProvider">The log provider.</param>
        /// <param name="navigationService">The navigation service.</param>
        public DataViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService)
            : base(logProvider, navigationService)
        {
            using (var data = new Data.DatabaseContext())
            {
                IList<Data.Models.VehiclesModel> vehicList = data.Vehicles.ToList();
                this.Vehicles = new ObservableCollection<Data.Models.VehiclesModel>(vehicList);

                IList<Data.Models.DynoModel> dynoList = data.Dyno.ToList();
                this.Dynos = new ObservableCollection<Data.Models.DynoModel>(dynoList);

                this.Dyno = this.Dynos.Where(d => d.Active == true).FirstOrDefault();
            }

            this.ShowSaveButtonCommand = new MvxCommand(this.ShowSave);
        }

        #region Methods

        /// <summary>
        /// Prepares this instance.
        /// called after construction.
        /// </summary>
        public override void Prepare()
        {
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
        /// Creates new dyno.
        /// </summary>
        protected virtual void NewDyno()
        {
            // Fahrzeug zurücksetzen
            this.Vehicle = null;

            DynoModel dyno = new DynoModel()
            {
                Name = "Dyno-Durchgang",
                Beschreibung = "Erstellt am " + DateTime.Now.ToString()
            };

            // in Collection hinzufügen
            this.Dynos.Add(dyno);

            // vorauswahl
            this.Dyno = Dynos.Last();
            this.CreateNewVehicle = true;

            this.SaveButton = true;
        }

        /// <summary>
        /// Deletes the dyno.
        /// </summary>
        protected virtual void DeleteDyno()
        {
            // in Datenbank löschen
            if (this.Dyno.Id != 0)
            {
                using (var db = new Data.DatabaseContext())
                {
                    // in Datenbank löschen
                    db.Dyno.Remove(this.Dyno);

                    db.SaveChanges();
                }

                this.Dynos.Remove(this.Dynos.Where(d => d.Id == this.Dyno.Id).First());
            }

            // in lokaler liste löschen
            this.Dynos.Remove(this.Dyno);

            // Letzten in liste Laden
            this.Dyno = null;
        }

        /// <summary>
        /// Shows the save.
        /// </summary>
        protected virtual void ShowSave()
        {
            this.SaveButton = true;
        }

        /// <summary>
        /// Saves the dyno.
        /// </summary>
        /// <returns>Status.</returns>
        protected virtual bool SaveDyno()
        {
            try
            {
                if (this.Dyno != null)
                {
                    // Vehicle zuweisen
                    if (this.TakeExistingVehicle)
                    {
                        // kein Vehicle ausgewählt
                        if (this.Vehicle == null)
                        {
                            return false;
                        }

                        this.Dyno.Vehicle = this.Vehicle;
                    }

                    // neues Fahrzeug erstellen
                    else if (this.CreateNewVehicle)
                    {
                        Data.Models.VehiclesModel vehicle = new Data.Models.VehiclesModel()
                        {
                            Name = "Dyno-Fahrzeug",
                            Beschreibung = "Erstellt über Dyno-Modul mit der Option 'Neues Fahrzeug erstellen'",
                            Deletable = true
                        };

                        using (var data = new Data.DatabaseContext())
                        {
                            data.Vehicles.Add(vehicle);

                            data.SaveChanges();
                        }

                        // neues Vehicle Dyno zuweisen
                        this.Dyno.Vehicle = vehicle;

                        // Lokaler list hinzufügen und auswählen
                        this.Vehicles.Add(vehicle);

                        this.Vehicle = this.Vehicles.Last();

                        // Radio buttons zurücksetzen
                        this.CreateNewVehicle = false;
                        this.TakeExistingVehicle = true;
                    }

                    using (var db = new Data.DatabaseContext())
                    {
                        db.Dyno.Update(this.Dyno);

                        db.SaveChanges();
                    }
                }

                this.SaveButton = false;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Loads the dyno.
        /// </summary>
        /// <param name="dyno">The dyno.</param>
        /// <returns>geladenes DynoModel.</returns>
        protected static Data.Models.DynoModel LoadDyno(Data.Models.DynoModel dyno)
        {
            try
            {
                using (var data = new Data.DatabaseContext())
                {
                    // Vehicle+Dyno laden
                    return data.Dyno
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

        /// <summary>
        /// Sets the active dyno.
        /// </summary>
        /// <param name="dyno">The dyno.</param>
        /// <returns>aktualisiertes DynoModel.</returns>
        protected virtual DynoModel SetActiveDyno(DynoModel dyno)
        {
            try
            {
                // Dyno-Datensatz in collection aktiv setzen
                this.Dynos.Where(d => d == dyno).First().Active = true;

                // in Database ändern
                using (var db = new Data.DatabaseContext())
                {
                    dyno.Active = true;

                    db.Dyno.Update(dyno);

                    db.SaveChanges();
                }
            }
            catch
            {
                // z.B. kein Dyno aktiv
                // z.B. nicht in datenbank
            }

            return dyno;
        }

        /// <summary>
        /// Sets the inactive dyno.
        /// </summary>
        protected virtual void SetInactiveDyno()
        {
            try
            {
                // aktive Dyno(s) holen
                var dynos = this.Dynos.Where(d => d.Active == true).ToList();

                // für local collection
                foreach (var item in this.Dynos)
                {
                    item.Active = false;
                }

                // für datenbank
                foreach (var item in dynos)
                {
                    item.Active = false;
                }

                // in Database ändern
                using (var db = new Data.DatabaseContext())
                {
                    db.Dyno.UpdateRange(dynos);

                    db.SaveChanges();
                }
            }
            catch
            {
                // z.B. kein Dyno aktiv
                // z.B. nicht in datenbank
            }
        }

        #endregion Methods

        #region Values

        #region Commands

        public IMvxCommand SaveDynoCommand { get; set; }
        public IMvxCommand ShowSaveButtonCommand { get; set; }
        public IMvxCommand NewDynoCommand { get; set; }
        public IMvxCommand DeleteDynoCommand { get; set; }

        #endregion Commands

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

        private Data.Models.DynoModel _dyno;

        public Data.Models.DynoModel Dyno
        {
            get => _dyno;
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
                }
                else
                {
                    CreateNewVehicle = false;
                    TakeExistingVehicle = false;
                }

                SetProperty(ref _dyno, value);

                //Da werte in UI geändert wurden, wird save-button angezeigt, daher nach dem laden wieder disablen
                SaveButton = false;
            }
        }

        private ObservableCollection<Data.Models.DynoModel> _dynos;

        public ObservableCollection<Data.Models.DynoModel> Dynos
        {
            get => _dynos;
            set { SetProperty(ref _dynos, value); }
        }

        #endregion Values
    }
}