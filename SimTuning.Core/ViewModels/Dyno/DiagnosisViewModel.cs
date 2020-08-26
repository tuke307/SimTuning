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
        protected readonly ResourceManager rm;
        protected DynoLogic dynoLogic;

        /// <summary>
        /// Gets or sets the insert environment command.
        /// </summary>
        /// <value>The insert environment command.</value>
        public IMvxCommand InsertEnvironmentCommand { get; set; }

        /// <summary>
        /// Gets or sets the insert vehicle command.
        /// </summary>
        /// <value>The insert vehicle command.</value>
        public IMvxCommand InsertVehicleCommand { get; set; }

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

        /// <summary>
        /// Initializes a new instance of the <see cref="DiagnosisViewModel" /> class.
        /// </summary>
        /// <param name="logProvider">The log provider.</param>
        /// <param name="navigationService">The navigation service.</param>
        /// <param name="messenger">The messenger.</param>
        public DiagnosisViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService, MvvmCross.Plugin.Messenger.IMvxMessenger messenger)
                                            : base(logProvider, navigationService)
        {
            this.dynoLogic = new DynoLogic();

            this.rm = new ResourceManager(typeof(SimTuning.Core.resources));

            this.InsertVehicleCommand = new MvxCommand(this.InsertVehicle);
            this.InsertEnvironmentCommand = new MvxCommand(this.InsertEnvironment);
        }

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
        /// <param name="mvxReloaderMessage">The MVX reloader message.</param>
        protected void ReloadData(Models.MvxReloaderMessage mvxReloaderMessage = null)
        {
            using (var db = new DatabaseContext())
            {
                IList<VehiclesModel> vehicList = db.Vehicles.ToList();
                this.HelperVehicles = new ObservableCollection<VehiclesModel>(vehicList);

                IList<EnvironmentModel> environmList = db.Environment.ToList();
                this.HelperEnvironments = new ObservableCollection<EnvironmentModel>(environmList);

                try
                {
                    this.Dyno = db.Dyno.Where(d => d.Active == true).Include(v => v.Audio).First();
                    this.NewEnvironment();
                }
                catch { }
            }
        }

        #region Commands

        protected virtual void RefreshPlot()
        {
            this.dynoLogic.CalculateStrengthPlot(this.Dyno, out List<DynoPSModel> ps, out List<DynoNmModel> nm);
            this.Dyno.DynoPS = ps;
            this.Dyno.DynoNm = nm;

            using (var db = new DatabaseContext())
            {
                // in Datenbank einfügen
                db.Dyno.Attach(this.Dyno);
                db.SaveChanges();
            }

            this.RaisePropertyChanged("PlotStrength");
        }

        private void InsertEnvironment()
        {
            if (this.HelperEnvironment.LuftdruckP.HasValue)
            {
                this.Dyno.Environment.LuftdruckP = HelperEnvironment.LuftdruckP;
            }

            if (this.HelperEnvironment.TemperaturT.HasValue)
            {
                this.Dyno.Environment.TemperaturT = HelperEnvironment.TemperaturT;
            }

            this.RaisePropertyChanged("Dyno");
        }

        private void InsertVehicle()
        {
            if (HelperVehicle.Gewicht.HasValue)
                Dyno.Vehicle.Gewicht = HelperVehicle.Gewicht;

            if (HelperVehicle.Cw.HasValue)
                Dyno.Vehicle.Cw = HelperVehicle.Cw;

            if (HelperVehicle.FrontA.HasValue)
                Dyno.Vehicle.FrontA = HelperVehicle.FrontA;

            RaisePropertyChanged("Dyno");
        }

        private void NewEnvironment()
        {
            if (this.Dyno.Environment == null)
            {
                this.Dyno.Environment = new EnvironmentModel()
                {
                    Name = "Automatisch erstellt " + DateTime.Now,
                };
                this.RaisePropertyChanged("Dyno");
            }
        }

        #endregion Commands

        #region Values

        private double? _cw_wert;

        private DynoModel _dyno;

        private double? _frontflaeche;

        private double? _gesamtuebersetzung;

        private double? _gewicht;

        private double? _luftdruck;

        private double? _temperatur;

        public double? Cw_wert
        {
            get => _cw_wert;
            set { SetProperty(ref _cw_wert, value); }
        }

        public DynoModel Dyno
        {
            get => _dyno;
            set { SetProperty(ref _dyno, value); }
        }

        public double? frontflaeche
        {
            get => _frontflaeche;
            set { SetProperty(ref _frontflaeche, value); }
        }

        public double? Gesamtuebersetzung
        {
            get => _gesamtuebersetzung;
            set { SetProperty(ref _gesamtuebersetzung, value); }
        }

        public double? Gewicht
        {
            get => _gewicht;
            set { SetProperty(ref _gewicht, value); }
        }

        public double? luftdruck
        {
            get => _luftdruck;
            set { SetProperty(ref _luftdruck, value); }
        }

        public PlotModel PlotStrength
        {
            get { return dynoLogic.PlotStrength; }
        }

        public double? temperatur
        {
            get => _temperatur;
            set { SetProperty(ref _temperatur, value); }
        }

        #endregion Values

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
    }
}