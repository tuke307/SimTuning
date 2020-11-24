// project=SimTuning.Core, file=DiagnosisViewModel.cs, creation=2020:7:31 Copyright (c)
// 2020 tuke productions. All rights reserved.
namespace SimTuning.Core.ViewModels.Dyno
{
    using Data;
    using Data.Models;
    using Microsoft.EntityFrameworkCore;
    using MvvmCross.Commands;
    using MvvmCross.Logging;
    using MvvmCross.Navigation;
    using MvvmCross.ViewModels;
    using OxyPlot;
    using SimTuning.Core.Models;
    using SimTuning.Core.Models.Quantity;
    using SimTuning.Core.ModuleLogic;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Resources;
    using System.Threading.Tasks;

    /// <summary>
    /// DiagnosisViewModel.
    /// </summary>
    /// <seealso cref="MvvmCross.ViewModels.MvxNavigationViewModel" />
    public class DiagnosisViewModel : MvxNavigationViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DiagnosisViewModel" /> class.
        /// </summary>
        /// <param name="logProvider">The log provider.</param>
        /// <param name="navigationService">The navigation service.</param>
        /// <param name="messenger">The messenger.</param>
        public DiagnosisViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService, MvvmCross.Plugin.Messenger.IMvxMessenger messenger)
                                            : base(logProvider, navigationService)
        {
            //this.AreaQuantityUnits = new AreaQuantity();
            //this.TemperatureQuantityUnits = new TemperatureQuantity();
            //this.PressureQuantityUnits = new PressureQuantity();
            this.MassQuantityUnits = new MassQuantity();

            this.InsertVehicleCommand = new MvxCommand(this.InsertVehicle);
            //this.InsertEnvironmentCommand = new MvxCommand(this.InsertEnvironment);
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
        /// Refreshes the plot.
        /// </summary>
        protected virtual void RefreshPlot()
        {
            try
            {
                DynoLogic.GetLeistungsGraph(this.Dyno.Vehicle.Gewicht.Value, out List<DynoPsModel> ps/*, out List<DynoNmModel> nm*/);
                this.Dyno.DynoPS = ps;

                using (var db = new DatabaseContext())
                {
                    // in Datenbank einfügen
                    db.Dyno.Attach(this.Dyno);
                    db.SaveChanges();
                }

                this.RaisePropertyChanged(() => this.PlotStrength);
            }
            catch (Exception exc)
            {
                this.Log.ErrorException("Fehler bei RefreshPlot: ", exc);
            }
        }

        /// <summary>
        /// Reloads the data.
        /// </summary>
        /// <param name="mvxReloaderMessage">The MVX reloader message.</param>
        protected void ReloadData(Models.MvxReloaderMessage mvxReloaderMessage = null)
        {
            try
            {
                using (var db = new DatabaseContext())
                {
                    IList<VehiclesModel> vehicList = db.Vehicles.ToList();
                    this.HelperVehicles = new ObservableCollection<VehiclesModel>(vehicList);

                    IList<EnvironmentModel> environmList = db.Environment.ToList();
                    this.HelperEnvironments = new ObservableCollection<EnvironmentModel>(environmList);

                    this.Dyno = db.Dyno.Single(d => d.Active == true); // .Include(v => v.Audio);
                    //this.NewEnvironment();
                }
            }
            catch (Exception exc)
            {
                this.Log.ErrorException("Fehler bei ReloadData: ", exc);
            }
        }

        /// <summary>
        /// Inserts the environment.
        /// </summary>
        //private void InsertEnvironment()
        //{
        //    if (this.HelperEnvironment.LuftdruckP.HasValue)
        //    {
        //        this.DynoEnvironmentLuftdruckP = this.HelperEnvironment.LuftdruckP;
        //        this.RaisePropertyChanged(() => this.DynoEnvironmentLuftdruckP);
        //    }

        //    if (this.HelperEnvironment.TemperaturT.HasValue)
        //    {
        //        this.DynoEnvironmentTemperaturT = this.HelperEnvironment.TemperaturT;
        //        this.RaisePropertyChanged(() => this.DynoEnvironmentTemperaturT);
        //    }
        //}

        /// <summary>
        /// Inserts the vehicle.
        /// </summary>
        private void InsertVehicle()
        {
            if (this.HelperVehicle.Gewicht.HasValue)
            {
                this.DynoVehicleGewicht = this.HelperVehicle.Gewicht;
                this.RaisePropertyChanged(() => this.DynoVehicleGewicht);
            }

            //if (this.HelperVehicle.Cw.HasValue)
            //{
            //    this.DynoVehicleCw = this.HelperVehicle.Cw;
            //    this.RaisePropertyChanged(() => this.DynoVehicleCw);
            //}

            //if (this.HelperVehicle.FrontA.HasValue)
            //{
            //    this.DynoVehicleFrontA = this.HelperVehicle.FrontA;
            //    this.RaisePropertyChanged(() => this.DynoVehicleFrontA);
            //}
        }

        /// <summary>
        /// Creates new environment.
        /// </summary>
        //private void NewEnvironment()
        //{
        //    if (this.Dyno.Environment == null)
        //    {
        //        this.Dyno.Environment = new EnvironmentModel()
        //        {
        //            Name = "Automatisch erstellt " + DateTime.Now,
        //        };
        //        this.RaisePropertyChanged(() => Dyno);
        //    }
        //}

        #endregion Methods

        #region Values

        private DynoModel _dyno;

        //public ObservableCollection<UnitListItem> AreaQuantityUnits { get; }

        public DynoModel Dyno
        {
            get => _dyno;
            set => SetProperty(ref _dyno, value);
        }

        //public double? DynoEnvironmentLuftdruckP
        //{
        //    get => Dyno?.Environment?.LuftdruckP;
        //    set
        //    {
        //        if (this.Dyno?.Environment == null)
        //        {
        //            return;
        //        }

        //        this.Dyno.Environment.LuftdruckP = value;
        //    }
        //}

        //public UnitListItem DynoEnvironmentLuftdruckPUnit
        //{
        //    get => this.PressureQuantityUnits.SingleOrDefault(x => x.UnitEnumValue.Equals(this.Dyno?.Environment?.LuftdruckPUnit));
        //    set
        //    {
        //        if (this.Dyno?.Environment == null)
        //        {
        //            return;
        //        }

        //        this.Dyno.Environment.LuftdruckPUnit = (UnitsNet.Units.PressureUnit)value?.UnitEnumValue;
        //        this.RaisePropertyChanged(() => this.DynoEnvironmentLuftdruckP);
        //    }
        //}

        //public double? DynoEnvironmentTemperaturT
        //{
        //    get => Dyno?.Environment?.TemperaturT;
        //    set
        //    {
        //        if (this.Dyno?.Environment == null)
        //        {
        //            return;
        //        }

        //        this.Dyno.Environment.TemperaturT = value;
        //    }
        //}

        //public UnitListItem DynoEnvironmentTemperaturTUnit
        //{
        //    get => this.TemperatureQuantityUnits.SingleOrDefault(x => x.UnitEnumValue.Equals(this.Dyno?.Environment?.TemperaturTUnit));
        //    set
        //    {
        //        if (this.Dyno?.Environment == null)
        //        {
        //            return;
        //        }

        //        this.Dyno.Environment.TemperaturTUnit = (UnitsNet.Units.TemperatureUnit)value?.UnitEnumValue;
        //        this.RaisePropertyChanged(() => this.DynoEnvironmentTemperaturT);
        //    }
        //}

        //public double? DynoVehicleCw
        //{
        //    get => Dyno?.Vehicle?.Cw;
        //    set
        //    {
        //        if (this.Dyno?.Vehicle == null)
        //        {
        //            return;
        //        }

        //        this.Dyno.Vehicle.Cw = value;
        //    }
        //}

        //public double? DynoVehicleFrontA
        //{
        //    get => Dyno?.Vehicle?.FrontA;
        //    set
        //    {
        //        if (this.Dyno?.Vehicle == null)
        //        {
        //            return;
        //        }

        //        this.Dyno.Vehicle.FrontA = value;
        //    }
        //}

        //public UnitListItem DynoVehicleFrontAUnit
        //{
        //    get => this.AreaQuantityUnits.SingleOrDefault(x => x.UnitEnumValue.Equals(this.Dyno?.Vehicle?.FrontAUnit));
        //    set
        //    {
        //        if (this.Dyno?.Vehicle == null)
        //        {
        //            return;
        //        }

        //        this.Dyno.Vehicle.FrontAUnit = (UnitsNet.Units.AreaUnit)value?.UnitEnumValue;
        //        this.RaisePropertyChanged(() => this.DynoVehicleFrontAUnit);
        //    }
        //}

        public double? DynoVehicleGewicht
        {
            get => Dyno?.Vehicle?.Gewicht;
            set
            {
                if (this.Dyno?.Vehicle == null)
                {
                    return;
                }

                this.Dyno.Vehicle.Gewicht = value;
            }
        }

        public UnitListItem DynoVehicleGewichtUnit
        {
            get => this.MassQuantityUnits.SingleOrDefault(x => x.UnitEnumValue.Equals(this.Dyno?.Vehicle?.GewichtUnit));
            set
            {
                if (this.Dyno?.Vehicle == null)
                {
                    return;
                }

                this.Dyno.Vehicle.GewichtUnit = (UnitsNet.Units.MassUnit)value?.UnitEnumValue;
                this.RaisePropertyChanged(() => this.DynoVehicleGewicht);
            }
        }

        //public double? DynoVehicleUebersetzung
        //{
        //    get => Dyno?.Vehicle?.Uebersetzung;
        //    set
        //    {
        //        if (this.Dyno?.Vehicle == null)
        //        {
        //            return;
        //        }

        //        this.Dyno.Vehicle.Uebersetzung = value;
        //    }
        //}

        /// <summary>
        /// Gets or sets the insert environment command.
        /// </summary>
        /// <value>The insert environment command.</value>
        //public IMvxCommand InsertEnvironmentCommand { get; set; }

        /// <summary>
        /// Gets or sets the insert vehicle command.
        /// </summary>
        /// <value>The insert vehicle command.</value>
        public IMvxCommand InsertVehicleCommand { get; set; }

        public ObservableCollection<UnitListItem> MassQuantityUnits { get; }

        public PlotModel PlotStrength
        {
            get => DynoLogic.PlotLeistung;
        }

        //public ObservableCollection<UnitListItem> PressureQuantityUnits { get; }

        /// <summary>
        /// Gets or sets the refresh plot command.
        /// </summary>
        /// <value>The refresh plot command.</value>
        public IMvxAsyncCommand RefreshPlotCommand { get; set; }

        /// <summary>
        /// Gets or sets the show save command.
        /// </summary>
        /// <value>The show save command.</value>
        public IMvxCommand ShowSaveCommand { get; set; }

        //public ObservableCollection<UnitListItem> TemperatureQuantityUnits { get; }

        #region Hilfs-Daten

        public Data.Models.VehiclesModel _helperVehicle;
        private Data.Models.EnvironmentModel _helperEnvironment;
        private ObservableCollection<Data.Models.EnvironmentModel> _helperEnvironments;

        private ObservableCollection<Data.Models.VehiclesModel> _helperVehicles;

        public Data.Models.EnvironmentModel HelperEnvironment
        {
            get => _helperEnvironment;
            set { SetProperty(ref _helperEnvironment, value); }
        }

        public ObservableCollection<Data.Models.EnvironmentModel> HelperEnvironments
        {
            get => _helperEnvironments;
            set { SetProperty(ref _helperEnvironments, value); }
        }

        public Data.Models.VehiclesModel HelperVehicle
        {
            get => _helperVehicle;
            set { SetProperty(ref _helperVehicle, value); }
        }

        public ObservableCollection<Data.Models.VehiclesModel> HelperVehicles
        {
            get => _helperVehicles;
            set { SetProperty(ref _helperVehicles, value); }
        }

        #endregion Hilfs-Daten

        #endregion Values
    }
}