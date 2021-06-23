// project=SimTuning.Core, file=DataViewModel.cs, creation=2020:7:31 Copyright (c) 2020
// tuke productions. All rights reserved.
namespace SimTuning.Core.ViewModels.Dyno
{
    using Data.Models;
    using Microsoft.Extensions.Logging;
    using MvvmCross.Commands;
    using MvvmCross.Navigation;
    using MvvmCross.Plugin.Messenger;
    using MvvmCross.ViewModels;
    using Newtonsoft.Json;
    using SimTuning.Core.Business;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.IO;
    using System.IO.Compression;
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
        /// <param name="logFactory">The log provider.</param>
        /// <param name="navigationService">The navigation service.</param>
        /// <param name="messenger">The messenger.</param>
        public DataViewModel(ILoggerFactory logFactory, IMvxNavigationService navigationService, IMvxMessenger messenger)
            : base(logFactory, navigationService)
        {
            this._messenger = messenger;

            this.ShowSaveButtonCommand = new MvxCommand(() => this.SaveButton = true);
            this.ExportDynoCommand = new MvxCommand(this.ExportDyno);
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
            try
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
            catch (Exception exc)
            {
                this.Log.LogError("Fehler bei ReloadData: ", exc);
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
                this.Log.LogError("Fehler bei DeleteDyno: ", exc);
            }
        }

        /// <summary>
        /// Exports the dyno.
        /// </summary>
        protected virtual void ExportDyno()
        {
            try
            {
                // erstellen der json.
                // TODO: reference test check
                string json = JsonConvert.SerializeObject(this.Dyno, Formatting.Indented,
                new JsonSerializerSettings()
                {
                    //ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                    //ReferenceLoopHandling = ReferenceLoopHandling.Serialize,
                    PreserveReferencesHandling = PreserveReferencesHandling.Objects,
                });

                File.WriteAllText(GeneralSettings.DataExportFilePath, json);

                // Dateien die gepackt werden sollen
                var list = new List<string>()
                {
                    GeneralSettings.DataExportFilePath,
                    GeneralSettings.AudioAccelerationFilePath,
                    GeneralSettings.AudioRolloutFilePath,
                };

                // ZIP archiv erstellen
                Functions.CreateZipFile(GeneralSettings.DataExportArchivePath, list);
            }
            catch (Exception exc)
            {
                this.Log.LogError("Fehler bei ExportDyno: ", exc);
            }
        }

        /// <summary>
        /// Imports the dyno.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        protected virtual async Task ImportDyno(string fileName)
        {
            //bool status = false;

            // zip extrahieren
            if (File.Exists(SimTuning.Core.GeneralSettings.DataExportFilePath))
            {
                File.Delete(SimTuning.Core.GeneralSettings.DataExportFilePath);
            }
            if (File.Exists(SimTuning.Core.GeneralSettings.AudioAccelerationFilePath))
            {
                File.Delete(SimTuning.Core.GeneralSettings.AudioAccelerationFilePath);
            }
            ZipFile.ExtractToDirectory(fileName, SimTuning.Core.GeneralSettings.FileDirectory);

            // wenn Datei ausgewählt
            //using (FileStream sourceStream = File.Open(fileName, FileMode.OpenOrCreate))
            //{
            //    status = SimTuning.Core.Business.AudioUtils.AudioCopy(SimTuning.Core.GeneralSettings.AudioFile, sourceStream);
            //}

            //if (status)
            //{
            //await this.RefreshAudioFileAsync().ConfigureAwait(true);
            //}

            // TODO: only for testing
            if (File.Exists(SimTuning.Core.GeneralSettings.DataExportFilePath))
            {
                string json = File.ReadAllText(SimTuning.Core.GeneralSettings.DataExportFilePath);
                DynoModel _dyno = JsonConvert.DeserializeObject<DynoModel>(json);
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
                    db.Entry(_dyno).Collection(v => v.Geschwindigkeit).Load();
                    db.Entry(_dyno).Collection(v => v.DynoPS).Load();

                    return _dyno;
                }
            }
            catch (Exception exc)
            {
                this.Log.LogWarning("Fehler bei LoadDyno: ", exc);
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
                this.Log.LogError("Fehler bei NewDyno: ", exc);
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
                this.Log.LogError("Fehler bei SaveDyno: ", exc);

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
                this.Log.LogWarning("Fehler bei SetActiveDyno: ", exc);

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
                this.Log.LogWarning("Fehler bei SetInactiveDyno: ", exc);

                // z.B. keine Werte in Dynos z.B. kein Dyno aktiv z.B. nicht in datenbank
            }
        }

        #endregion Methods

        #region Values

        #region Commands

        /// <summary>
        /// Gets or sets the delete dyno command.
        /// </summary>
        /// <value>The delete dyno command.</value>
        public IMvxCommand DeleteDynoCommand { get; set; }

        /// <summary>
        /// Gets or sets the export dyno command.
        /// </summary>
        /// <value>The export dyno command.</value>
        public IMvxCommand ExportDynoCommand { get; set; }

        /// <summary>
        /// Gets or sets the import dyno command.
        /// </summary>
        /// <value>The import dyno command.</value>
        public IMvxAsyncCommand ImportDynoCommand { get; set; }

        /// <summary>
        /// Creates new dynocommand.
        /// </summary>
        /// <value>The new dyno command.</value>
        public IMvxCommand NewDynoCommand { get; set; }

        /// <summary>
        /// Gets or sets the save dyno command.
        /// </summary>
        /// <value>The save dyno command.</value>
        public IMvxCommand SaveDynoCommand { get; set; }

        /// <summary>
        /// Gets or sets the show save button command.
        /// </summary>
        /// <value>The show save button command.</value>
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