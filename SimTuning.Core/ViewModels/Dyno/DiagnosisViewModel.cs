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
using System.Reflection;
using System.Resources;
using System.Threading.Tasks;

namespace SimTuning.Core.ViewModels.Dyno
{
    public class DiagnosisViewModel : MvxNavigationViewModel
    {
        protected DynoLogic dynoLogic;
        protected readonly ResourceManager rm;

        public DiagnosisViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService) : base(logProvider, navigationService)
        {
            this.dynoLogic = new DynoLogic();

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

            this.rm = new ResourceManager(typeof(SimTuning.Core.resources));

            this.InsertVehicleCommand = new MvxCommand(this.InsertVehicle);
            this.InsertEnvironmentCommand = new MvxCommand(this.InsertEnvironment);
        }

        public IMvxAsyncCommand RefreshPlotCommand { get; set; }
        public IMvxCommand InsertVehicleCommand { get; set; }
        public IMvxCommand InsertEnvironmentCommand { get; set; }
        public IMvxCommand ShowSaveCommand { get; set; }

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

        #region Commands

        public void InsertVehicle()
        {
            if (HelperVehicle.Gewicht.HasValue)
                Dyno.Vehicle.Gewicht = HelperVehicle.Gewicht;

            if (HelperVehicle.Cw.HasValue)
                Dyno.Vehicle.Cw = HelperVehicle.Cw;

            if (HelperVehicle.FrontA.HasValue)
                Dyno.Vehicle.FrontA = HelperVehicle.FrontA;

            RaisePropertyChanged("Dyno");
        }

        public void InsertEnvironment()
        {
            if (HelperEnvironment.LuftdruckP.HasValue)
                Dyno.Environment.LuftdruckP = HelperEnvironment.LuftdruckP;

            if (HelperEnvironment.TemperaturT.HasValue)
                Dyno.Environment.TemperaturT = HelperEnvironment.TemperaturT;

            RaisePropertyChanged("Dyno");
        }

        protected virtual async Task RefreshPlot()
        {
            this.dynoLogic.CalculateStrengthPlot(this.Dyno, out List<DynoPSModel> ps, out List<DynoNmModel> nm);
            this.Dyno.DynoPS = ps;
            this.Dyno.DynoNm = nm;

            using (var db = new DatabaseContext())
            {
                //in Datenbank einfügen
                db.Dyno.Attach(this.Dyno);
                db.SaveChanges();
            }

            await RaisePropertyChanged("PlotStrength");
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

        public PlotModel PlotStrength
        {
            get { return dynoLogic.PlotStrength; }
        }

        private DynoModel _dyno;

        public DynoModel Dyno
        {
            get => _dyno;
            set { SetProperty(ref _dyno, value); }
        }

        private double? _gesamtuebersetzung;

        public double? Gesamtuebersetzung
        {
            get => _gesamtuebersetzung;
            set { SetProperty(ref _gesamtuebersetzung, value); }
        }

        private double? _gewicht;

        public double? Gewicht
        {
            get => _gewicht;
            set { SetProperty(ref _gewicht, value); }
        }

        private double? _frontflaeche;

        public double? frontflaeche
        {
            get => _frontflaeche;
            set { SetProperty(ref _frontflaeche, value); }
        }

        private double? _cw_wert;

        public double? Cw_wert
        {
            get => _cw_wert;
            set { SetProperty(ref _cw_wert, value); }
        }

        private double? _temperatur;

        public double? temperatur
        {
            get => _temperatur;
            set { SetProperty(ref _temperatur, value); }
        }

        private double? _luftdruck;

        public double? luftdruck
        {
            get => _luftdruck;
            set { SetProperty(ref _luftdruck, value); }
        }

        #endregion Values

        #region Hilfs-Daten

        private ObservableCollection<Data.Models.EnvironmentModel> _helperEnvironments;

        public ObservableCollection<Data.Models.EnvironmentModel> HelperEnvironments
        {
            get => _helperEnvironments;
            set { SetProperty(ref _helperEnvironments, value); }
        }

        private Data.Models.EnvironmentModel _helperEnvironment;

        public Data.Models.EnvironmentModel HelperEnvironment
        {
            get => _helperEnvironment;
            set { SetProperty(ref _helperEnvironment, value); }
        }

        private ObservableCollection<Data.Models.VehiclesModel> _helperVehicles;

        public ObservableCollection<Data.Models.VehiclesModel> HelperVehicles
        {
            get => _helperVehicles;
            set { SetProperty(ref _helperVehicles, value); }
        }

        public Data.Models.VehiclesModel _helperVehicle;

        public Data.Models.VehiclesModel HelperVehicle
        {
            get => _helperVehicle;
            set { SetProperty(ref _helperVehicle, value); }
        }

        #endregion Hilfs-Daten
    }
}