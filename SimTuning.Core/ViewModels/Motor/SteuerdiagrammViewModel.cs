// project=SimTuning.Core, file=SteuerdiagrammViewModel.cs, creation=2020:7:31 Copyright
// (c) 2020 tuke productions. All rights reserved.
namespace SimTuning.Core.ViewModels.Motor
{
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

    /// <summary>
    /// SteuerdiagrammViewModel.
    /// </summary>
    /// <seealso cref="MvvmCross.ViewModels.MvxNavigationViewModel" />
    public class SteuerdiagrammViewModel : MvxNavigationViewModel
    {
        /// <summary>
        /// Gets or sets the insert reference command.
        /// </summary>
        /// <value>The insert reference command.</value>
        public IMvxCommand InsertReferenceCommand { get; set; }

        /// <summary>
        /// Gets or sets the insert vehicle command.
        /// </summary>
        /// <value>The insert vehicle command.</value>
        public IMvxCommand InsertVehicleCommand { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SteuerdiagrammViewModel" />
        /// class.
        /// </summary>
        /// <param name="logProvider">The log provider.</param>
        /// <param name="navigationService">The navigation service.</param>
        public SteuerdiagrammViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService)
            : base(logProvider, navigationService)
        {
            // lokale liste kreieren
            this.MotorSteuerzeiten = new ObservableCollection<MotorModel>()
            {
                new MotorModel()
                {
                    Name = "sehr kurz",
                    Einlass = new EinlassModel() { SteuerzeitSZ = 100 },
                    Auslass = new AuslassModel() { SteuerzeitSZ = 125 },
                    Ueberstroemer = new UeberstroemerModel() { SteuerzeitSZ = 100 },
                },
                new MotorModel()
                {
                    Name = "kurz",
                    Einlass = new EinlassModel() { SteuerzeitSZ = 120 },
                    Auslass = new AuslassModel() { SteuerzeitSZ = 145 },
                    Ueberstroemer = new UeberstroemerModel() { SteuerzeitSZ = 110 },
                },
                new MotorModel()
                {
                    Name = "mittel",
                    Einlass = new EinlassModel() { SteuerzeitSZ = 140 },
                    Auslass = new AuslassModel() { SteuerzeitSZ = 165 },
                    Ueberstroemer = new UeberstroemerModel() { SteuerzeitSZ = 120 },
                },
                new MotorModel()
                {
                    Name = "lang",
                    Einlass = new EinlassModel() { SteuerzeitSZ = 160 },
                    Auslass = new AuslassModel() { SteuerzeitSZ = 185 },
                    Ueberstroemer = new UeberstroemerModel() { SteuerzeitSZ = 130 },
                },
                new MotorModel()
                {
                    Name = "sehr lang",
                    Einlass = new EinlassModel() { SteuerzeitSZ = 180 },
                    Auslass = new AuslassModel() { SteuerzeitSZ = 205 },
                    Ueberstroemer = new UeberstroemerModel() { SteuerzeitSZ = 140 },
                },
            };

            using (var db = new DatabaseContext())
            {
                IList<VehiclesModel> vehicList = db.Vehicles
                    .Include(vehicles => vehicles.Motor)
                    .Include(vehicles => vehicles.Motor.Einlass)
                    .Include(vehicles => vehicles.Motor.Auslass)
                    .Include(vehicles => vehicles.Motor.Ueberstroemer)
                    .ToList();

                this.HelperVehicles = new ObservableCollection<VehiclesModel>(vehicList);
            }

            this.InsertReferenceCommand = new MvxCommand(this.InsertReference);
            this.InsertVehicleCommand = new MvxCommand(this.InsertVehicle);
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
        /// Prepares this instance. called after construction.
        /// </summary>
        public override void Prepare()
        {
        }

        #region Methods

        /// <summary>
        /// Refreshes the steuerzeit.
        /// </summary>
        /// <returns></returns>
        protected virtual Stream RefreshSteuerzeit()
        {
            if (this.SteuerzeitEinlass.HasValue)
            {
                this.Einlass_Steuerwinkel_oeffnen = EngineLogic.GetSteuerwinkelOeffnet(this.SteuerzeitEinlass.Value, 0, 0);
                this.Einlass_Steuerwinkel_schließen = EngineLogic.GetSteuerwinkelSchließt(this.SteuerzeitEinlass.Value, 0, 0);
            }

            if (this.SteuerzeitAuslass.HasValue)
            {
                this.Auslass_Steuerwinkel_oeffnen = EngineLogic.GetSteuerwinkelOeffnet(0, this.SteuerzeitAuslass.Value, 0);
                this.Auslass_Steuerwinkel_schließen = EngineLogic.GetSteuerwinkelSchließt(0, this.SteuerzeitAuslass.Value, 0);
            }

            if (this.SteuerzeitUeberstroemer.HasValue)
            {
                this.Ueberstroemer_Steuerwinkel_oeffnen = EngineLogic.GetSteuerwinkelOeffnet(0, 0, this.SteuerzeitUeberstroemer.Value);
                this.Ueberstroemer_Steuerwinkel_schließen = EngineLogic.GetSteuerwinkelSchließt(0, 0, this.SteuerzeitUeberstroemer.Value);
            }

            if (this.SteuerzeitUeberstroemer.HasValue && this.SteuerzeitAuslass.HasValue)
            {
                this.SteuerzeitVorauslass = EngineLogic.GetVorauslass(this.SteuerzeitAuslass.Value, this.SteuerzeitUeberstroemer.Value);
            }

            if (this.SteuerzeitEinlass.HasValue && this.SteuerzeitUeberstroemer.HasValue && this.SteuerzeitAuslass.HasValue)
            {
                Stream stream = SimTuning.Core.Business.Converts.SKBitmapToStream(bitmap: EngineLogic.GetPortTimingCircle(this.SteuerzeitEinlass.Value, this.SteuerzeitAuslass.Value, this.SteuerzeitUeberstroemer.Value));
                return stream;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Inserts the reference.
        /// </summary>
        private void InsertReference()
        {
            if (this.MotorSteuerzeit.Einlass.SteuerzeitSZ.HasValue)
            {
                this.SteuerzeitEinlass = this.MotorSteuerzeit.Einlass.SteuerzeitSZ.Value;
            }

            if (this.MotorSteuerzeit.Auslass.SteuerzeitSZ.HasValue)
            {
                this.SteuerzeitAuslass = this.MotorSteuerzeit.Auslass.SteuerzeitSZ.Value;
            }

            if (this.MotorSteuerzeit.Ueberstroemer.SteuerzeitSZ.HasValue)
            {
                this.SteuerzeitUeberstroemer = this.MotorSteuerzeit.Ueberstroemer.SteuerzeitSZ.Value;
            }
        }

        /// <summary>
        /// Inserts the vehicle.
        /// </summary>
        private void InsertVehicle()
        {
            if (this.HelperVehicle.Motor.Einlass.SteuerzeitSZ.HasValue)
            {
                this.SteuerzeitEinlass = this.HelperVehicle.Motor.Einlass.SteuerzeitSZ;
            }

            if (this.HelperVehicle.Motor.Auslass.SteuerzeitSZ.HasValue)
            {
                this.SteuerzeitAuslass = this.HelperVehicle.Motor.Auslass.SteuerzeitSZ;
            }

            if (this.HelperVehicle.Motor.Ueberstroemer.SteuerzeitSZ.HasValue)
            {
                this.SteuerzeitUeberstroemer = this.HelperVehicle.Motor.Ueberstroemer.SteuerzeitSZ;
            }
        }

        #endregion Methods

        #region Values

        private double? _auslass_Steuerwinkel_oeffnen;
        private double? _auslass_Steuerwinkel_schließen;
        private double? _einlass_Steuerwinkel_oeffnen;
        private double? _einlass_Steuerwinkel_schließen;
        private VehiclesModel _helperVehicle;
        private ObservableCollection<VehiclesModel> _helperVehicles;
        private Data.Models.MotorModel _motorSteuerzeit;
        private ObservableCollection<Data.Models.MotorModel> _motorSteuerzeiten;
        private double? _steuerzeitAuslass;
        private double? _steuerzeitEinlass;
        private double? _steuerzeitUeberstroemer;
        private double? _steuerzeitVorauslass;
        private double? _ueberstroemer_Steuerwinkel_oeffnen;

        private double? _ueberstroemer_Steuerwinkel_schließen;

        public double? Auslass_Steuerwinkel_oeffnen
        {
            get => this._auslass_Steuerwinkel_oeffnen;
            set => this.SetProperty(ref this._auslass_Steuerwinkel_oeffnen, value);
        }

        public double? Auslass_Steuerwinkel_schließen
        {
            get => this._auslass_Steuerwinkel_schließen;
            set => this.SetProperty(ref this._auslass_Steuerwinkel_schließen, value);
        }

        public double? Einlass_Steuerwinkel_oeffnen
        {
            get => this._einlass_Steuerwinkel_oeffnen;
            set => this.SetProperty(ref this._einlass_Steuerwinkel_oeffnen, value);
        }

        public double? Einlass_Steuerwinkel_schließen
        {
            get => this._einlass_Steuerwinkel_schließen;
            set => this.SetProperty(ref this._einlass_Steuerwinkel_schließen, value);
        }

        public VehiclesModel HelperVehicle
        {
            get => this._helperVehicle;
            set => this.SetProperty(ref this._helperVehicle, value);
        }

        public ObservableCollection<VehiclesModel> HelperVehicles
        {
            get => this._helperVehicles;
            set => this.SetProperty(ref this._helperVehicles, value);
        }

        public Data.Models.MotorModel MotorSteuerzeit
        {
            get => this._motorSteuerzeit;
            set => this.SetProperty(ref this._motorSteuerzeit, value);
        }

        public ObservableCollection<Data.Models.MotorModel> MotorSteuerzeiten
        {
            get => this._motorSteuerzeiten;
            set => this.SetProperty(ref this._motorSteuerzeiten, value);
        }

        /// <summary>
        /// Gets or sets the steuerzeit auslass.
        /// </summary>
        /// <value>The steuerzeit auslass.</value>
        public virtual double? SteuerzeitAuslass
        {
            get => this._steuerzeitAuslass;
            set => this.SetProperty(ref this._steuerzeitAuslass, value);
        }

        /// <summary>
        /// Gets or sets the steuerzeit einlass.
        /// </summary>
        /// <value>The steuerzeit einlass.</value>
        public virtual double? SteuerzeitEinlass
        {
            get => this._steuerzeitEinlass;
            set => this.SetProperty(ref this._steuerzeitEinlass, value);
        }

        /// <summary>
        /// Gets or sets the steuerzeit ueberstroemer.
        /// </summary>
        /// <value>The steuerzeit ueberstroemer.</value>
        public virtual double? SteuerzeitUeberstroemer
        {
            get => this._steuerzeitUeberstroemer;
            set => this.SetProperty(ref this._steuerzeitUeberstroemer, value);
        }

        public double? SteuerzeitVorauslass
        {
            get => this._steuerzeitVorauslass;
            set => this.SetProperty(ref this._steuerzeitVorauslass, value);
        }

        public double? Ueberstroemer_Steuerwinkel_oeffnen
        {
            get => this._ueberstroemer_Steuerwinkel_oeffnen;
            set => this.SetProperty(ref this._ueberstroemer_Steuerwinkel_oeffnen, value);
        }

        public double? Ueberstroemer_Steuerwinkel_schließen
        {
            get => this._ueberstroemer_Steuerwinkel_schließen;
            set => this.SetProperty(ref this._ueberstroemer_Steuerwinkel_schließen, value);
        }

        #endregion Values
    }
}