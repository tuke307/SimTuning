// project=SimTuning.Core, file=SteuerdiagrammViewModel.cs, creation=2020:7:31
// Copyright (c) 2020 tuke productions. All rights reserved.
using Data;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using SimTuning.Core.ModuleLogic;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SimTuning.Core.ViewModels.Motor
{
    public class SteuerdiagrammViewModel : MvxNavigationViewModel
    {
        public SteuerdiagrammViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService) : base(logProvider, navigationService)
        {
            //Lokale liste kreieren
            MotorSteuerzeiten = new ObservableCollection<MotorModel>(){
                new MotorModel()
                {
                    Name = "sehr kurz",
                    Einlass = new EinlassModel() { SteuerzeitSZ = 100 },
                    Auslass = new AuslassModel() { SteuerzeitSZ = 125 },
                    Ueberstroemer = new UeberstroemerModel() { SteuerzeitSZ = 100 }
                },
                new MotorModel()
                {
                    Name = "kurz",
                    Einlass = new EinlassModel() { SteuerzeitSZ = 120 },
                    Auslass = new AuslassModel() { SteuerzeitSZ = 145 },
                    Ueberstroemer = new UeberstroemerModel() { SteuerzeitSZ = 110 }
                },
                new MotorModel()
                {
                    Name = "mittel",
                    Einlass = new EinlassModel() { SteuerzeitSZ = 140 },
                    Auslass = new AuslassModel() { SteuerzeitSZ = 165 },
                    Ueberstroemer = new UeberstroemerModel() { SteuerzeitSZ = 120 }
                },
                new MotorModel()
                {
                    Name = "lang",
                    Einlass = new EinlassModel() { SteuerzeitSZ = 160 },
                    Auslass = new AuslassModel() { SteuerzeitSZ = 185 },
                    Ueberstroemer = new UeberstroemerModel() { SteuerzeitSZ = 130 }
                },
                new MotorModel()
                {
                    Name = "sehr lang",
                    Einlass = new EinlassModel() { SteuerzeitSZ = 180 },
                    Auslass = new AuslassModel() { SteuerzeitSZ = 205 },
                    Ueberstroemer = new UeberstroemerModel() { SteuerzeitSZ = 140 }
                }
            };

            using (var db = new DatabaseContext())
            {
                IList<VehiclesModel> vehicList = db.Vehicles
                    .Include(vehicles => vehicles.Motor)
                    .Include(vehicles => vehicles.Motor.Einlass)
                    .Include(vehicles => vehicles.Motor.Auslass)
                    .Include(vehicles => vehicles.Motor.Ueberstroemer)
                    .ToList();

                HelperVehicles = new ObservableCollection<VehiclesModel>(vehicList);
            }

            InsertReferenceCommand = new MvxCommand(InsertReference);
            InsertVehicleCommand = new MvxCommand(InsertVehicle);
        }

        public IMvxCommand InsertVehicleCommand { get; set; }
        public IMvxCommand InsertReferenceCommand { get; set; }

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

        protected virtual Stream RefreshSteuerzeit()
        {
            if (Einlass_Steuerzeit.HasValue)
            {
                Einlass_Steuerwinkel_oeffnen = EngineLogic.GetSteuerwinkelOeffnet(Einlass_Steuerzeit.Value, 0, 0);
                Einlass_Steuerwinkel_schließen = EngineLogic.GetSteuerwinkelSchließt(Einlass_Steuerzeit.Value, 0, 0);
            }
            if (Auslass_Steuerzeit.HasValue)
            {
                Auslass_Steuerwinkel_oeffnen = EngineLogic.GetSteuerwinkelOeffnet(0, Auslass_Steuerzeit.Value, 0);
                Auslass_Steuerwinkel_schließen = EngineLogic.GetSteuerwinkelSchließt(0, Auslass_Steuerzeit.Value, 0);
            }
            if (Ueberstroemer_Steuerzeit.HasValue)
            {
                Ueberstroemer_Steuerwinkel_oeffnen = EngineLogic.GetSteuerwinkelOeffnet(0, 0, Ueberstroemer_Steuerzeit.Value);
                Ueberstroemer_Steuerwinkel_schließen = EngineLogic.GetSteuerwinkelSchließt(0, 0, Ueberstroemer_Steuerzeit.Value);
            }

            if (Ueberstroemer_Steuerzeit.HasValue && Auslass_Steuerzeit.HasValue)
            {
                Vorauslass_Steuerzeit = EngineLogic.GetVorauslass(Auslass_Steuerzeit.Value, Ueberstroemer_Steuerzeit.Value);
            }

            if (Einlass_Steuerzeit.HasValue && Ueberstroemer_Steuerzeit.HasValue && Auslass_Steuerzeit.HasValue)
            {
                Stream stream = SimTuning.Core.Business.Converts.SKBitmapToStream(EngineLogic.GetPortTimingCircle(this.Einlass_Steuerzeit.Value, this.Auslass_Steuerzeit.Value, this.Ueberstroemer_Steuerzeit.Value));
                return stream;
            }
            else
            {
                return null;
            }
        }

        protected void InsertVehicle()
        {
            Einlass_Steuerzeit = HelperVehicle.Motor.Einlass.SteuerzeitSZ;
            Auslass_Steuerzeit = HelperVehicle.Motor.Auslass.SteuerzeitSZ;
            Ueberstroemer_Steuerzeit = HelperVehicle.Motor.Ueberstroemer.SteuerzeitSZ;
        }

        protected void InsertReference()
        {
            if (this.MotorSteuerzeit.Einlass.SteuerzeitSZ.HasValue)
            {
                this.Einlass_Steuerzeit = this.MotorSteuerzeit.Einlass.SteuerzeitSZ.Value;
            }

            if (this.MotorSteuerzeit.Auslass.SteuerzeitSZ.HasValue)
            {
                this.Auslass_Steuerzeit = this.MotorSteuerzeit.Auslass.SteuerzeitSZ.Value;
            }

            if (this.MotorSteuerzeit.Ueberstroemer.SteuerzeitSZ.HasValue)
            {
                this.Ueberstroemer_Steuerzeit = this.MotorSteuerzeit.Ueberstroemer.SteuerzeitSZ.Value;
            }
        }

        #endregion Commands

        #region Values

        private ObservableCollection<VehiclesModel> _helperVehicles;

        public ObservableCollection<VehiclesModel> HelperVehicles
        {
            get => _helperVehicles;
            set { SetProperty(ref _helperVehicles, value); }
        }

        private VehiclesModel _helperVehicle;

        public VehiclesModel HelperVehicle
        {
            get => _helperVehicle;
            set { SetProperty(ref _helperVehicle, value); }
        }

        private Data.Models.MotorModel _motorSteuerzeit;

        public Data.Models.MotorModel MotorSteuerzeit
        {
            get => _motorSteuerzeit;
            set { SetProperty(ref _motorSteuerzeit, value); }
        }

        private ObservableCollection<Data.Models.MotorModel> _motorSteuerzeiten;

        public ObservableCollection<Data.Models.MotorModel> MotorSteuerzeiten
        {
            get => _motorSteuerzeiten;
            set { SetProperty(ref _motorSteuerzeiten, value); }
        }

        private double? _einlass_Steuerzeit;

        public virtual double? Einlass_Steuerzeit
        {
            get => _einlass_Steuerzeit;
            set { SetProperty(ref _einlass_Steuerzeit, value); }
        }

        private double? _auslass_Steuerzeit;

        public virtual double? Auslass_Steuerzeit
        {
            get => _auslass_Steuerzeit;
            set { SetProperty(ref _auslass_Steuerzeit, value); }
        }

        private double? _ueberstroemer_Steuerzeit;

        public virtual double? Ueberstroemer_Steuerzeit
        {
            get => _ueberstroemer_Steuerzeit;
            set { SetProperty(ref _ueberstroemer_Steuerzeit, value); }
        }

        private double? _einlass_Steuerwinkel_oeffnen;

        public double? Einlass_Steuerwinkel_oeffnen
        {
            get => _einlass_Steuerwinkel_oeffnen;
            set { SetProperty(ref _einlass_Steuerwinkel_oeffnen, value); }
        }

        private double? _auslass_Steuerwinkel_oeffnen;

        public double? Auslass_Steuerwinkel_oeffnen
        {
            get => _auslass_Steuerwinkel_oeffnen;
            set { SetProperty(ref _auslass_Steuerwinkel_oeffnen, value); }
        }

        private double? _ueberstroemer_Steuerwinkel_oeffnen;

        public double? Ueberstroemer_Steuerwinkel_oeffnen
        {
            get => _ueberstroemer_Steuerwinkel_oeffnen;
            set { SetProperty(ref _ueberstroemer_Steuerwinkel_oeffnen, value); }
        }

        private double? _einlass_Steuerwinkel_schließen;

        public double? Einlass_Steuerwinkel_schließen
        {
            get => _einlass_Steuerwinkel_schließen;
            set { SetProperty(ref _einlass_Steuerwinkel_schließen, value); }
        }

        private double? _auslass_Steuerwinkel_schließen;

        public double? Auslass_Steuerwinkel_schließen
        {
            get => _auslass_Steuerwinkel_schließen;
            set { SetProperty(ref _auslass_Steuerwinkel_schließen, value); }
        }

        private double? _ueberstroemer_Steuerwinkel_schließen;

        public double? Ueberstroemer_Steuerwinkel_schließen
        {
            get => _ueberstroemer_Steuerwinkel_schließen;
            set { SetProperty(ref _ueberstroemer_Steuerwinkel_schließen, value); }
        }

        private double? _vorauslass_Steuerzeit;

        public double? Vorauslass_Steuerzeit
        {
            get => _vorauslass_Steuerzeit;
            set { SetProperty(ref _vorauslass_Steuerzeit, value); }
        }

        #endregion Values
    }
}