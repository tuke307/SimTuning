// Copyright (c) 2021 tuke productions. All rights reserved.
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.Plugin.Messenger;
using MvvmCross.ViewModels;
using SimTuning.Data.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace SimTuning.Core.ViewModels.Tuning
{
    /// <summary>
    /// DataViewModel.
    /// </summary>
    /// <seealso cref="MvvmCross.ViewModels.MvxViewModel" />
    public class DataViewModel : MvxViewModel
    {
        /// <summary> Initializes a new instance of the <see cref="DataViewModel"/> class. </summary> <param name="logger"><inheritdoc cref="ILogger" path="/summary/node()" /></param> <param
        /// name="navigationService"><inheritdoc cref="IMvxNavigationService" path="/summary/node()" /></param <param name="messenger"><inheritdoc cref="IMvxMessenger" path="/summary/node()"
        /// /></param>
        public DataViewModel(
            ILogger<DataViewModel> logger,
            IMvxNavigationService navigationService,
            IMvxMessenger messenger)
        {
            this._logger = logger;
            this._messenger = messenger;
        }

        #region Commands

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        /// <returns>Initilisierung.</returns>
        public override Task Initialize()
        {
            this.ShowSaveButtonCommand = new MvxCommand(() => this.SaveButton = true);

            ReloadData();

            return base.Initialize();
        }

        /// <summary>
        /// Prepares this instance. called after construction.
        /// </summary>
        public override void Prepare()
        {
            base.Prepare();
        }

        public void ReloadData()
        {
            using (var data = new Data.DatabaseContext())
            {
                IList<VehiclesModel> vehicList = data.Vehicles.ToList();
                this.Vehicles = new ObservableCollection<Data.Models.VehiclesModel>(vehicList);

                IList<Data.Models.TuningModel> tuningList = data.Tuning.ToList();
                this.Tunings = new
                ObservableCollection<Data.Models.TuningModel>(tuningList);

                this.Tuning = this.Tunings.Where(d => d.Active == true).FirstOrDefault();
            }
        }

        protected virtual bool DeleteTuning()
        {
            try
            {
                // in Datenbank löschen
                if (Tuning.Id != 0)
                {
                    using (var data = new Data.DatabaseContext())
                    {
                        // in Datenbank löschen
                        data.Tuning.Remove(Tuning);

                        data.SaveChanges();
                    }

                    Tunings.Remove(Tunings.Where(d => d.Id == Tuning.Id).First());
                }

                // in lokaler liste löschen
                Tunings.Remove(Tuning);

                // Letzten in liste Laden
                Tuning = null;

                return true;
            }
            catch
            {
                return false;
            }
        }

        protected TuningModel LoadTuning(TuningModel tuning)
        {
            try
            {
                using (var data = new Data.DatabaseContext())
                {
                    // Vehicle+Tuning laden
                    return data.Tuning
                      .Where(v => v.Id == tuning.Id)
                      .Include(v => v.Tuning)
                      .First();
                }
            }
            catch (Exception)
            {
                return tuning;
                throw;
            }
        }

        protected virtual void NewTuning()
        {
            // Fahrzeug zurücksetzen
            Vehicle = null;

            TuningModel tuning = new TuningModel()
            {
                Name = "Tuning-Durchgang",
                Beschreibung = "Erstellt am " + DateTime.Now.ToString(),
            };

            // in Collection hinzufügen
            Tunings.Add(tuning);

            // vorauswahl
            tuning = Tunings.Last();
            CreateNewVehicle = true;

            SaveButton = true;
        }

        protected virtual bool SaveTuning()
        {
            try
            {
                if (Tuning != null)
                {
                    // Vehicle zuweisen
                    if (TakeExistingVehicle)
                    {
                        // kein Vehicle ausgewählt
                        if (Vehicle == null)
                        {
                            return false;
                        }

                        Tuning.Vehicle = Vehicle;
                    }
                    // neues Fahrzeug erstellen
                    else if (CreateNewVehicle)
                    {
                        Data.Models.VehiclesModel vehicle = new Data.Models.VehiclesModel()
                        {
                            Name = "Tuning-Fahrzeug",
                            Beschreibung = "Erstellt über Tuning-Modul mit der Option 'Neues Fahrzeug erstellen'",
                            Deletable = true,
                        };

                        using (var data = new Data.DatabaseContext())
                        {
                            data.Vehicles.Add(vehicle);

                            data.SaveChanges();
                        }

                        // neues Vehicle Tuning zuweisen
                        Tuning.Vehicle = vehicle;

                        // Lokaler list hinzufügen und auswählen
                        Vehicles.Add(vehicle);

                        Vehicle = Vehicles.Last();

                        // Radio buttons zurücksetzen
                        CreateNewVehicle = false;
                        TakeExistingVehicle = true;
                    }

                    using (var data = new Data.DatabaseContext())
                    {
                        data.Tuning.Update(Tuning);

                        data.SaveChanges();
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

        protected virtual TuningModel SetActiveTuning(TuningModel tuning)
        {
            try
            {
                // Tuning-Datensatz in collection aktiv setzen
                Tunings.Where(d => d == tuning).First().Active = true;

                // in Database ändern
                using (var db = new Data.DatabaseContext())
                {
                    tuning.Active = true;

                    db.Tuning.Update(tuning);

                    db.SaveChanges();
                }
            }
            catch
            {
                // z.B. kein Tuning aktiv z.B. nicht in datenbank
            }

            return tuning;
        }

        protected virtual void SetInactiveTuning()
        {
            try
            {
                // aktive Tuning(s) holen
                var tunings = Tunings.Where(d => d.Active == true).ToList();

                // für local collection
                foreach (var item in Tunings)
                    item.Active = false;

                // für datenbank
                foreach (var item in tunings)
                    item.Active = false;

                // in Database ändern
                using (var db = new Data.DatabaseContext())
                {
                    db.Tuning.UpdateRange(tunings);

                    db.SaveChanges();
                }
            }
            catch
            {
                // z.B. kein Tuning aktiv z.B. nicht in datenbank
            }
        }

        #endregion Commands

        #region Values

        protected readonly IMvxMessenger _messenger;

        private readonly ILogger<DataViewModel> _logger;

        private bool _createNewVehicle;

        private bool _saveButton;

        private bool _takeExistingVehicle;

        private Data.Models.TuningModel _tuning;

        private ObservableCollection<Data.Models.TuningModel> _tunings;

        private Data.Models.VehiclesModel _vehicle;

        private ObservableCollection<Data.Models.VehiclesModel> _vehicles;

        public bool CreateNewVehicle
        {
            get => _createNewVehicle;
            set { SetProperty(ref _createNewVehicle, value); }
        }

        public IMvxCommand DeleteTuningCommand { get; set; }

        public IMvxCommand NewTuningCommand { get; set; }

        public bool SaveButton
        {
            get => _saveButton;
            set { SetProperty(ref _saveButton, value); }
        }

        public IMvxCommand SaveTuningCommand { get; set; }

        public IMvxCommand ShowSaveButtonCommand { get; set; }

        public bool TakeExistingVehicle
        {
            get => _takeExistingVehicle;
            set { SetProperty(ref _takeExistingVehicle, value); }
        }

        public Data.Models.TuningModel Tuning
        {
            get => _tuning;
            set
            {
                // alten ausgewälten Tuning-Datensatz inaktiv setzen
                SetInactiveTuning();

                // fahrzeug zurücksetzen
                Vehicle = null;

                if (value != null)
                {
                    // Active setzen
                    value = SetActiveTuning(value);

                    // komplett laden
                    value = LoadTuning(value);

                    // Vehicle laden
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

                // Da werte in UI geändert wurden, wird save-button angezeigt, daher nach dem laden wieder disablen
                SaveButton = false;
            }
        }

        public ObservableCollection<Data.Models.TuningModel> Tunings
        {
            get => _tunings;
            set { SetProperty(ref _tunings, value); }
        }

        public Data.Models.VehiclesModel Vehicle
        {
            get => _vehicle;
            set
            {
                SetProperty(ref _vehicle, value);
                SaveButton = true;
            }
        }

        public ObservableCollection<Data.Models.VehiclesModel> Vehicles
        {
            get => _vehicles;
            set { SetProperty(ref _vehicles, value); }
        }

        #endregion Values
    }
}