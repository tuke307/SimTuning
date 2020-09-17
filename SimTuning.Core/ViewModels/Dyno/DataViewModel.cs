// project=SimTuning.Core, file=DataViewModel.cs, creation=2020:7:31 Copyright (c) 2020
// tuke productions. All rights reserved.
namespace SimTuning.Core.ViewModels.Dyno
{
    using Data.Models;
    using MvvmCross.Commands;
    using MvvmCross.Logging;
    using MvvmCross.Navigation;
    using MvvmCross.Plugin.Messenger;
    using MvvmCross.ViewModels;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    /// Dyno-Data-ViewModel.
    /// </summary>
    /// <seealso cref="MvvmCross.ViewModels.MvxNavigationViewModel" />
    public class DataViewModel : MvxNavigationViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DataViewModel" /> class.
        /// </summary>
        /// <param name="logProvider">The log provider.</param>
        /// <param name="navigationService">The navigation service.</param>
        /// <param name="messenger">The messenger.</param>
        public DataViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService, IMvxMessenger messenger)
            : base(logProvider, navigationService)
        {
            this._messenger = messenger;

            this.ShowSaveButtonCommand = new MvxCommand(() => this.SaveButton = true);
        }

        #region Methods

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        /// <returns>Initilisierung.</returns>
        public override Task Initialize()
        {
            this.ReloadData();

            return base.Initialize();
        }

        /// <summary>
        /// Prepares this instance. called after construction.
        /// </summary>
        public override void Prepare()
        {
            base.Prepare();
        }

        /// <summary>
        /// Reloads the data.
        /// </summary>
        public void ReloadData()
        {
            using (var db = new Data.DatabaseContext())
            {
                IList<Data.Models.VehiclesModel> vehicList = db.Vehicles.ToList();
                this.Vehicles = new ObservableCollection<Data.Models.VehiclesModel>(vehicList);

                IList<Data.Models.DynoModel> dynoList = db.Dyno.ToList();
                this.Dynos = new ObservableCollection<Data.Models.DynoModel>(dynoList);

                this.Dyno = this.Dynos.Where(d => d.Active == true).FirstOrDefault();
            }
        }

        /// <summary>
        /// Deletes the dyno.
        /// </summary>
        protected virtual void DeleteDyno()
        {
            try
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

                    this.Dynos.Remove(this.Dynos.Single(d => d.Id == this.Dyno.Id));
                }

                // in lokaler liste löschen
                this.Dynos.Remove(this.Dyno);

                // Letzten in liste Laden
                this.Dyno = null;
            }
            catch (Exception exc)
            {
                this.Log.WarnException("Fehler beim Löschen des Dyno: ", exc);
            }
        }

        /// <summary>
        /// Loads the dyno.
        /// </summary>
        /// <param name="dyno">The dyno.</param>
        /// <returns>geladenes DynoModel oder das übergebene.</returns>
        protected Data.Models.DynoModel LoadDyno(Data.Models.DynoModel dyno)
        {
            if (dyno?.Id == null)
            {
                return dyno;
            }

            try
            {
                using (var db = new Data.DatabaseContext())
                {
                    // Vehicle+Dyno laden
                    var _dyno = db.Dyno.Find(dyno.Id);

                    db.Entry(_dyno).Reference(v => v.Vehicle).Load();
                    db.Entry(_dyno).Collection(v => v.Drehzahl).Load();
                    db.Entry(_dyno).Collection(v => v.DynoNm).Load();
                    db.Entry(_dyno).Collection(v => v.DynoPS).Load();

                    return _dyno;
                }
            }
            catch (Exception exc)
            {
                this.Log.WarnException("Fehler beim Löschen des Dyno: ", exc);
                return dyno;
            }
        }

        /// <summary>
        /// Creates new dyno.
        /// </summary>
        protected virtual void NewDyno()
        {
            try
            {
                // Fahrzeug zurücksetzen
                this.Vehicle = null;

                DynoModel dyno = new DynoModel()
                {
                    Name = "Dyno-Durchgang",
                    Beschreibung = $"Erstellt am {DateTime.Now}",
                };

                // in Collection hinzufügen
                this.Dynos.Add(dyno);

                // vorauswahl
                this.Dyno = this.Dynos.Last();
                this.CreateNewVehicle = true;

                this.SaveButton = true;
            }
            catch (Exception exc)
            {
                this.Log.WarnException("Fehler beim Löschen des Dyno: ", exc);
            }
        }

        /// <summary>
        /// Saves the dyno.
        /// </summary>
        /// <returns>Status.</returns>
        protected virtual bool SaveDyno()
        {
            if (this.Dyno == null)
            {
                this.SaveButton = false;
                return false;
            }

            try
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
                        Deletable = true,
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

                // Refresh aller Dyno-Datensätze im Dyno-Modul
                var message = new Core.Models.MvxReloaderMessage(this, this.Dyno);

                this._messenger.Publish(message);

                this.SaveButton = false;
                return true;
            }
            catch (Exception exc)
            {
                this.Log.WarnException("Fehler beim Speichern des Dyno: ", exc);

                return false;
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
                // in collection
                this.Dynos.Single(d => d.Equals(dyno)).Active = true;

                // lokal
                dyno.Active = true;

                // keine Speicherung für NewDyno()
                if (dyno.Id == null)
                {
                    return dyno;
                }

                // in Database ändern
                using (var db = new Data.DatabaseContext())
                {
                    db.Dyno.Update(dyno);

                    db.SaveChanges();
                }
            }
            catch (Exception exc)
            {
                this.Log.WarnException("Fehler beim Aktiv-setzem des Dyno: ", exc);

                // z.B. kein Dyno aktiv z.B. nicht in datenbank
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
                // aktives Dyno holen
                var dyno = this.Dynos.Single(d => d.Active == true);

                // local collection
                foreach (var item in this.Dynos)
                {
                    item.Active = false;
                }

                // Datenbank
                dyno.Active = false;

                if (dyno.Id == null)
                {
                    return;
                }

                using (var db = new Data.DatabaseContext())
                {
                    db.Dyno.Update(dyno);

                    db.SaveChanges();
                }
            }
            catch (Exception exc)
            {
                this.Log.WarnException("Fehler beim InAktiv-setzen des Dyno", exc);

                // z.B. keine Werte in Dynos z.B. kein Dyno aktiv z.B. nicht in datenbank
            }
        }

        #endregion Methods

        #region Values

        #region Commands

        public IMvxCommand DeleteDynoCommand { get; set; }

        public IMvxCommand NewDynoCommand { get; set; }

        public IMvxCommand SaveDynoCommand { get; set; }

        public IMvxCommand ShowSaveButtonCommand { get; set; }

        #endregion Commands

        protected readonly IMvxMessenger _messenger;
        private bool _createNewVehicle;
        private Data.Models.DynoModel _dyno;
        private ObservableCollection<Data.Models.DynoModel> _dynos;
        private bool _saveButton;

        private bool _takeExistingVehicle;

        private Data.Models.VehiclesModel _vehicle;

        private ObservableCollection<Data.Models.VehiclesModel> _vehicles;

        /// <summary>
        /// Gets or sets a value indicating whether [create new vehicle].
        /// </summary>
        /// <value><c>true</c> if [create new vehicle]; otherwise, <c>false</c>.</value>
        public bool CreateNewVehicle
        {
            get => this._createNewVehicle;
            set => this.SetProperty(ref this._createNewVehicle, value);
        }

        /// <summary>
        /// Gets or sets the dyno.
        /// </summary>
        /// <value>The dyno.</value>
        public Data.Models.DynoModel Dyno
        {
            get => this._dyno;
            set
            {
                // alten ausgewälten Dyno-Datensatz inaktiv setzen
                this.SetInactiveDyno();

                // fahrzeug zurücksetzen
                this.Vehicle = null;

                if (value != null)
                {
                    // Active setzen
                    value = this.SetActiveDyno(value);

                    // komplett laden
                    value = this.LoadDyno(value);

                    this.RaisePropertyChanged(() => this.DynoBeschreibung);
                    this.RaisePropertyChanged(() => this.DynoName);

                    // Vehicle laden
                    if (value.Vehicle != null)
                    {
                        this.CreateNewVehicle = false;
                        this.TakeExistingVehicle = true;
                        this.Vehicle = this.Vehicles.Single(v => v.Id == value.Vehicle.Id);
                    }
                    else
                    {
                        this.CreateNewVehicle = true;
                        this.TakeExistingVehicle = false;
                    }
                }
                else
                {
                    this.CreateNewVehicle = false;
                    this.TakeExistingVehicle = false;
                }

                this.SetProperty(ref this._dyno, value);

                // Da werte in UI geändert wurden, wird save-button angezeigt, daher nach
                // dem laden wieder disablen
                this.SaveButton = false;
            }
        }

        /// <summary>
        /// Gets or sets the dyno beschreibung.
        /// </summary>
        /// <value>The dyno beschreibung.</value>
        public string DynoBeschreibung
        {
            get => this.Dyno?.Beschreibung;
            set
            {
                if (this.Dyno == null)
                {
                    return;
                }

                this.Dyno.Beschreibung = value;
            }
        }

        /// <summary>
        /// Gets or sets the name of the dyno.
        /// </summary>
        /// <value>The name of the dyno.</value>
        public string DynoName
        {
            get => this.Dyno?.Name;
            set
            {
                if (this.Dyno == null)
                {
                    return;
                }

                this.Dyno.Name = value;
            }
        }

        /// <summary>
        /// Gets or sets the dynos.
        /// </summary>
        /// <value>The dynos.</value>
        public ObservableCollection<Data.Models.DynoModel> Dynos
        {
            get => this._dynos;
            set => this.SetProperty(ref this._dynos, value);
        }

        /// <summary>
        /// Gets or sets a value indicating whether [save button].
        /// </summary>
        /// <value><c>true</c> if [save button]; otherwise, <c>false</c>.</value>
        public bool SaveButton
        {
            get => this._saveButton;
            set => this.SetProperty(ref this._saveButton, value);
        }

        /// <summary>
        /// Gets or sets a value indicating whether [take existing vehicle].
        /// </summary>
        /// <value><c>true</c> if [take existing vehicle]; otherwise, <c>false</c>.</value>
        public bool TakeExistingVehicle
        {
            get => this._takeExistingVehicle;
            set => this.SetProperty(ref this._takeExistingVehicle, value);
        }

        /// <summary>
        /// Gets or sets the vehicle.
        /// </summary>
        /// <value>The vehicle.</value>
        public Data.Models.VehiclesModel Vehicle
        {
            get => this._vehicle;
            set
            {
                SetProperty(ref this._vehicle, value);
                this.SaveButton = true;
            }
        }

        /// <summary>
        /// Gets or sets the vehicles.
        /// </summary>
        /// <value>The vehicles.</value>
        public ObservableCollection<Data.Models.VehiclesModel> Vehicles
        {
            get => _vehicles;
            set => SetProperty(ref _vehicles, value);
        }

        #endregion Values
    }
}