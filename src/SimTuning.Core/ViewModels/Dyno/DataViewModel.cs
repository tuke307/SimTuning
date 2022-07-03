// Copyright (c) 2021 tuke productions. All rights reserved.
namespace SimTuning.Core.ViewModels.Dyno
{
    using Microsoft.Extensions.Logging;
    using MvvmCross.Commands;
    using MvvmCross.Navigation;
    using MvvmCross.Plugin.Messenger;
    using MvvmCross.ViewModels;
    using Newtonsoft.Json;
    using SimTuning.Core.Helpers;
    using SimTuning.Core.Services;
    using SimTuning.Data.Models;
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
    /// <seealso cref="MvvmCross.ViewModels.MvxViewModel" />
    public class DataViewModel : MvxViewModel
    {
        /// <summary> Initializes a new instance of the <see cref="DataViewModel"/> class. </summary> <param name="logger"><inheritdoc cref="ILogger" path="/summary/node()" /></param> <param
        /// name="navigationService"><inheritdoc cref="IMvxNavigationService" path="/summary/node()" /></param <param name="messenger"><inheritdoc cref="IMvxMessenger" path="/summary/node()"
        /// /></param>
        public DataViewModel(
            ILogger<DataViewModel> logger,
            IMvxNavigationService navigationService,
            IVehicleService vehicleService,
            IMvxMessenger messenger)
        {
            this._logger = logger;
            this._navigationService = navigationService;
            this._vehicleService = vehicleService;
            this._messenger = messenger;
        }

        #region Methods

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        /// <returns>Initilisierung.</returns>
        public override Task Initialize()
        {
            this.ShowSaveButtonCommand = new MvxCommand(() => this.SaveButton = true);
            this.ExportDynoCommand = new MvxCommand(this.ExportDyno);

            this.Dynos = new ObservableCollection<DynoModel>(_vehicleService.RetrieveDynos());
            this.Dyno = _vehicleService.RetrieveOneActive();

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
        /// Deletes the dyno.
        /// </summary>
        protected virtual bool DeleteDyno()
        {
            try
            {
                // in lokale liste aktualisieren
                Dynos.Remove(this.Dyno);
                RaisePropertyChanged(() => Dynos);

                _vehicleService.DeleteOne(this.Dyno);

                // letzten in liste laden.
                this.Dyno = Dynos.Last();

                return true;
            }
            catch (Exception exc)
            {
                _logger.LogError(exc, exc.Message, null);
                return false;
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
                    // ReferenceLoopHandling = ReferenceLoopHandling.Ignore, ReferenceLoopHandling = ReferenceLoopHandling.Serialize,
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
                _logger.LogError(exc, exc.Message, null);
            }
        }

        /// <summary>
        /// Imports the dyno.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        protected virtual async Task ImportDyno(string fileName)
        {
            // bool status = false;

            // zip extrahieren
            if (File.Exists(SimTuning.Core.GeneralSettings.DataExportFilePath))
            {
                File.Delete(SimTuning.Core.GeneralSettings.DataExportFilePath);
            }
            if (File.Exists(SimTuning.Core.GeneralSettings.AudioAccelerationFilePath))
            {
                File.Delete(SimTuning.Core.GeneralSettings.AudioAccelerationFilePath);
            }
            ZipFile.ExtractToDirectory(fileName, Data.DatabaseSettings.FileDirectory);

            // wenn Datei ausgewählt using (FileStream sourceStream = File.Open(fileName, FileMode.OpenOrCreate)) { status =
            // SimTuning.Core.Helpers.AudioUtils.AudioCopy(SimTuning.Core.GeneralSettings.AudioFile, sourceStream); }

            // if (status) { await this.RefreshAudioFileAsync().ConfigureAwait(true); }

            // TODO: only for testing
            if (File.Exists(SimTuning.Core.GeneralSettings.DataExportFilePath))
            {
                string json = File.ReadAllText(SimTuning.Core.GeneralSettings.DataExportFilePath);
                DynoModel dyno = JsonConvert.DeserializeObject<DynoModel>(json);
            }
        }

        /// <summary>
        /// Creates new dyno.
        /// </summary>
        protected virtual bool NewDyno()
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

                // gespeichert wird erst bei SaveDyno() erstmal lokal hinzufügen
                this.Dynos.Add(dyno);

                // vorauswahl
                this.Dyno = this.Dynos.Last();
                this.CreateNewVehicle = true;

                this.SaveButton = true;

                return true;
            }
            catch (Exception exc)
            {
                _logger.LogError(exc, exc.Message, null);
                return false;
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

                    // nur id zuweisen
                    this.Dyno.VehicleId = this.Vehicle.Id.Value;
                }

                // neues Fahrzeug erstellen
                else if (this.CreateNewVehicle)
                {
                    VehiclesModel vehicle = new VehiclesModel()
                    {
                        Name = "Dyno-Fahrzeug",
                        Beschreibung = "Erstellt über Dyno-Modul mit der Option 'Neues Fahrzeug erstellen'",
                        Deletable = true,
                    };

                    vehicle = _vehicleService.CreateOne(vehicle);

                    // Lokaler list auswählen
                    RaisePropertyChanged(() => Vehicles);
                    this.Vehicle = this.Vehicles.Last();

                    // neues Vehicle Dyno zuweisen
                    this.Dyno.Vehicle = vehicle;

                    // Radio buttons zurücksetzen
                    this.CreateNewVehicle = false;
                    this.TakeExistingVehicle = true;
                }

                if (Dyno.Id == null)
                {
                    this.Dyno = _vehicleService.CreateOne(this.Dyno);
                }
                else
                {
                    _vehicleService.UpdateOne(this.Dyno);
                }

                // wenn bereits geladene seiten mit dyno daten Refresh aller Dyno-Datensätze
                if (this._messenger.HasSubscriptionsFor<Core.Models.MvxReloaderMessage>())
                {
                    var message = new Core.Models.MvxReloaderMessage(this, this.Dyno);

                    this._messenger.Publish(message);
                }

                this.SaveButton = false;

                return true;
            }
            catch (Exception exc)
            {
                _logger.LogError(exc, exc.Message, null);

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
                dyno.Active = true;

                // auch in db updaten.
                _vehicleService.UpdateOne(dyno);
            }
            catch (Exception exc)
            {
                _logger.LogError(exc, exc.Message, null);

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
                // aktiven Dyno inaktiv setzen.
                this.Dyno.Active = false;

                // wenn in db, dann auch da updaten.
                _vehicleService.UpdateOne(this.Dyno);

                // in lokaler liste aktualisieren.
                Dynos[_dynoIndex] = this.Dyno;
                // foreach (var dyno in Dynos) { dyno.Active = false; }
            }
            catch (Exception exc)
            {
                _logger.LogError(exc, exc.Message, null);

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
        protected readonly IMvxNavigationService _navigationService;
        private readonly ILogger<DataViewModel> _logger;
        private readonly IVehicleService _vehicleService;
        private bool _createNewVehicle;
        private DynoModel _dyno;
        private int _dynoIndex;
        private ObservableCollection<DynoModel> _dynos;
        private bool _saveButton;

        private bool _takeExistingVehicle;

        private VehiclesModel _vehicle;

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
        public DynoModel Dyno
        {
            get => this._dyno;
            set
            {
                // alten ausgewälten Dyno-Datensatz inaktiv setzen
                if (Dyno != null)
                    this.SetInactiveDyno();

                // fahrzeug zurücksetzen
                this.Vehicle = null;

                if (value != null)
                {
                    _dynoIndex = Dynos.IndexOf(value);

                    // Active setzen
                    value = this.SetActiveDyno(value);

                    // Vehicle laden
                    if (value.VehicleId != 0)
                    {
                        this.CreateNewVehicle = false;
                        this.TakeExistingVehicle = true;
                        this.Vehicle = this.Vehicles.Single(v => v.Id == value.VehicleId);
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

                this.RaisePropertyChanged(() => this.DynoBeschreibung);
                this.RaisePropertyChanged(() => this.DynoName);

                // Da werte in UI geändert wurden, wird save-button angezeigt, daher nach dem laden wieder disablen
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
        public ObservableCollection<DynoModel> Dynos
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
        public VehiclesModel Vehicle
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
        public ObservableCollection<VehiclesModel> Vehicles
        {
            get => new ObservableCollection<VehiclesModel>(_vehicleService.RetrieveVehicles());
        }

        #endregion Values
    }
}