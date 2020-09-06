// project=SimTuning.Core, file=UmrechnungViewModel.cs, creation=2020:7:31 Copyright (c)
// 2020 tuke productions. All rights reserved.
namespace SimTuning.Core.ViewModels.Motor
{
    using Data;
    using Data.Models;
    using Microsoft.EntityFrameworkCore;
    using MvvmCross.Commands;
    using MvvmCross.Logging;
    using MvvmCross.Navigation;
    using MvvmCross.ViewModels;
    using SimTuning.Core.Models;
    using SimTuning.Core.ModuleLogic;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Threading.Tasks;
    using UnitsNet.Units;

    /// <summary>
    /// UmrechnungViewModel.
    /// </summary>
    /// <seealso cref="MvvmCross.ViewModels.MvxNavigationViewModel" />
    public class UmrechnungViewModel : MvxNavigationViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UmrechnungViewModel" /> class.
        /// </summary>
        /// <param name="logProvider">The log provider.</param>
        /// <param name="navigationService">The navigation service.</param>
        public UmrechnungViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService)
            : base(logProvider, navigationService)
        {
            VolumeQuantityUnits = new VolumeQuantity();
            LengthQuantityUnits = new LengthQuantity();

            UnitAbstandOTlength = LengthQuantityUnits.Where(x => x.UnitEnumValue.Equals(LengthUnit.Millimeter)).First();
            UnitDeachsierung = LengthQuantityUnits.Where(x => x.UnitEnumValue.Equals(LengthUnit.Millimeter)).First();
            UnitHub = LengthQuantityUnits.Where(x => x.UnitEnumValue.Equals(LengthUnit.Millimeter)).First();
            UnitHubR = LengthQuantityUnits.Where(x => x.UnitEnumValue.Equals(LengthUnit.Millimeter)).First();
            UnitPleulL = LengthQuantityUnits.Where(x => x.UnitEnumValue.Equals(LengthUnit.Millimeter)).First();

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

            InsertDataCommand = new MvxCommand(InsertData);
        }

        #region Methods

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

        /// <summary>
        /// Inserts the data.
        /// </summary>
        protected void InsertData()
        {
            if (HelperVehicle.Motor.HubL.HasValue)
                Hub = HelperVehicle.Motor.HubL;

            if (HelperVehicle.Motor.PleulL.HasValue)
                PleulL = HelperVehicle.Motor.PleulL;

            if (HelperVehicle.Motor.DeachsierungL.HasValue)
                Deachsierung = HelperVehicle.Motor.DeachsierungL;
        }

        /// <summary>
        /// Refreshes the hubradius.
        /// </summary>
        private void Refresh_hubradius()
        {
            if (Hub.HasValue && PleulL.HasValue && Deachsierung.HasValue)
                HubR = EngineLogic.GetStrokeRadius(
                     UnitsNet.UnitConverter.Convert(Hub.Value,
                     UnitHub.UnitEnumValue,
                     LengthUnit.Millimeter),

                     UnitsNet.UnitConverter.Convert(PleulL.Value,
                     UnitPleulL.UnitEnumValue,
                     LengthUnit.Millimeter),

                     UnitsNet.UnitConverter.Convert(Deachsierung.Value,
                     UnitDeachsierung.UnitEnumValue,
                     LengthUnit.Millimeter));
        }

        /// <summary>
        /// Refreshes the kwgrad.
        /// </summary>
        private void Refresh_kwgrad()
        {
            if (PleulL.HasValue && HubR.HasValue && Deachsierung.HasValue && Steuerzeit.HasValue)
                AbstandOTlength = EngineLogic.GetDistanceToOT(
                      UnitsNet.UnitConverter.Convert(PleulL.Value,
                      UnitPleulL.UnitEnumValue,
                      LengthUnit.Millimeter),

                      UnitsNet.UnitConverter.Convert(HubR.Value,
                      UnitHubR.UnitEnumValue,
                      LengthUnit.Millimeter),

                      UnitsNet.UnitConverter.Convert(Deachsierung.Value,
                      UnitDeachsierung.UnitEnumValue,
                      LengthUnit.Millimeter),

                      Steuerzeit.Value);
        }

        /// <summary>
        /// Refreshes the unterschied.
        /// </summary>
        private void Refresh_Unterschied()
        {
            if (VorherSteuerzeit.HasValue && NachherSteuerzeit.HasValue && PleulL.HasValue && HubR.HasValue && Deachsierung.HasValue && (Kolbenoberkante_checked || Kolbenunterkante_checked))
            {
                List<double> steuerwinkel = new List<double>();
                steuerwinkel.AddRange(EngineLogic.GetSteuerwinkel(VorherSteuerzeit.Value, NachherSteuerzeit.Value, Kolbenoberkante_checked, Kolbenunterkante_checked));

                VorherSteuerwinkelOeffnet = steuerwinkel[0];
                VorherSteuerwinkelSchließt = steuerwinkel[1];
                NachherSteuerwinkelOeffnet = steuerwinkel[2];
                NachherSteuerwinkelSchließt = steuerwinkel[3];

                Unterschied_grad = EngineLogic.GetPortTimingDifference(false, VorherSteuerzeit.Value, NachherSteuerzeit.Value);

                //verbessern und durschnitt aus öffnen und schließen bilden
                Unterschied_mm = EngineLogic.GetPortTimingDifference(
                    true,

                    this.VorherSteuerwinkelOeffnet.Value,
                    this.NachherSteuerwinkelOeffnet.Value,

                     UnitsNet.UnitConverter.Convert(PleulL.Value,
                    UnitPleulL.UnitEnumValue,
                    LengthUnit.Millimeter),

                     UnitsNet.UnitConverter.Convert(HubR.Value,
                    UnitHubR.UnitEnumValue,
                    LengthUnit.Millimeter),

                     UnitsNet.UnitConverter.Convert(Deachsierung.Value,
                    UnitDeachsierung.UnitEnumValue,
                    LengthUnit.Millimeter));
            }
        }

        #endregion Methods

        #region Values

        #region private

        private double? _abstandOTlength;
        private double? _deachsierung;
        private VehiclesModel _helperVehicle;
        private ObservableCollection<VehiclesModel> _helperVehicles;

        private double? _hub;

        private double? _hubR;

        private bool _kolbenoberkante_checked;

        private bool _kolbenunterkante_checked;

        private double? _nachherSteuerwinkelOeffnet;

        private double? _nachherSteuerwinkelSchließt;

        private double? _nachherSteuerzeit;

        private double? _pleulL;

        private double? _steuerzeit;

        private UnitListItem _unitAbstandOTlength;

        private UnitListItem _unitDeachsierung;

        private UnitListItem _unitHub;

        private UnitListItem _unitHubR;

        private UnitListItem _unitPleulL;

        private double? _unterschied_grad;

        private double? _unterschied_mm;

        private double? _vorherSteuerwinkelOeffnet;

        private double? _vorherSteuerwinkelSchließt;

        private double? _vorherSteuerzeit;

        #endregion private

        /// <summary>
        /// Gets or sets the abstand o tlength.
        /// </summary>
        /// <value>The abstand o tlength.</value>
        public double? AbstandOTlength
        {
            get => _abstandOTlength;
            set { SetProperty(ref _abstandOTlength, value); }
        }

        /// <summary>
        /// Gets or sets the deachsierung.
        /// </summary>
        /// <value>The deachsierung.</value>
        public double? Deachsierung
        {
            get => _deachsierung;
            set
            {
                SetProperty(ref _deachsierung, value);
                Refresh_hubradius();
            }
        }

        /// <summary>
        /// Gets or sets the helper vehicle.
        /// </summary>
        /// <value>The helper vehicle.</value>
        public VehiclesModel HelperVehicle
        {
            get => _helperVehicle;
            set { SetProperty(ref _helperVehicle, value); }
        }

        /// <summary>
        /// Gets or sets the helper vehicles.
        /// </summary>
        /// <value>The helper vehicles.</value>
        public ObservableCollection<VehiclesModel> HelperVehicles
        {
            get => _helperVehicles;
            set { SetProperty(ref _helperVehicles, value); }
        }

        /// <summary>
        /// Gets or sets the hub.
        /// </summary>
        /// <value>The hub.</value>
        public double? Hub
        {
            get => _hub;
            set
            {
                SetProperty(ref _hub, value);
                Refresh_hubradius();
            }
        }

        /// <summary>
        /// Gets or sets the hub r.
        /// </summary>
        /// <value>The hub r.</value>
        public double? HubR
        {
            get => _hubR;
            set { SetProperty(ref _hubR, value); }
        }

        /// <summary>
        /// Gets or sets the insert data command.
        /// </summary>
        /// <value>The insert data command.</value>
        public IMvxCommand InsertDataCommand { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [kolbenoberkante checked].
        /// </summary>
        /// <value>
        /// <c>true</c> if [kolbenoberkante checked]; otherwise, <c>false</c>.
        /// </value>
        public bool Kolbenoberkante_checked
        {
            get => _kolbenoberkante_checked;
            set
            {
                SetProperty(ref _kolbenoberkante_checked, value);
                Refresh_Unterschied();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [kolbenunterkante checked].
        /// </summary>
        /// <value>
        /// <c>true</c> if [kolbenunterkante checked]; otherwise, <c>false</c>.
        /// </value>
        public bool Kolbenunterkante_checked
        {
            get => _kolbenunterkante_checked;
            set
            {
                SetProperty(ref _kolbenunterkante_checked, value);
                Refresh_Unterschied();
            }
        }

        /// <summary>
        /// Gets the length quantity units.
        /// </summary>
        /// <value>The length quantity units.</value>
        public ObservableCollection<UnitListItem> LengthQuantityUnits { get; }

        /// <summary>
        /// Gets or sets the nachher steuerwinkel oeffnet.
        /// </summary>
        /// <value>The nachher steuerwinkel oeffnet.</value>
        public double? NachherSteuerwinkelOeffnet
        {
            get => _nachherSteuerwinkelOeffnet;
            set { SetProperty(ref _nachherSteuerwinkelOeffnet, value); }
        }

        /// <summary>
        /// Gets or sets the nachher steuerwinkel schließt.
        /// </summary>
        /// <value>The nachher steuerwinkel schließt.</value>
        public double? NachherSteuerwinkelSchließt
        {
            get => _nachherSteuerwinkelSchließt;
            set { SetProperty(ref _nachherSteuerwinkelSchließt, value); }
        }

        /// <summary>
        /// Gets or sets the nachher steuerzeit.
        /// </summary>
        /// <value>The nachher steuerzeit.</value>
        public double? NachherSteuerzeit
        {
            get => _nachherSteuerzeit;
            set
            {
                SetProperty(ref _nachherSteuerzeit, value);
                Refresh_Unterschied();
            }
        }

        /// <summary>
        /// Gets or sets the pleul l.
        /// </summary>
        /// <value>The pleul l.</value>
        public double? PleulL
        {
            get => _pleulL;
            set
            {
                SetProperty(ref _pleulL, value);
                Refresh_hubradius();
            }
        }

        /// <summary>
        /// Gets or sets the steuerzeit.
        /// </summary>
        /// <value>The steuerzeit.</value>
        public double? Steuerzeit
        {
            get => _steuerzeit;
            set
            {
                SetProperty(ref _steuerzeit, value);
                Refresh_kwgrad();
            }
        }

        /// <summary>
        /// Gets or sets the unit abstand o tlength.
        /// </summary>
        /// <value>The unit abstand o tlength.</value>
        public UnitListItem UnitAbstandOTlength
        {
            get => _unitAbstandOTlength;
            set
            {
                AbstandOTlength = Business.Functions.UpdateValue(AbstandOTlength, _unitAbstandOTlength, value);

                SetProperty(ref _unitAbstandOTlength, value);
            }
        }

        /// <summary>
        /// Gets or sets the unit deachsierung.
        /// </summary>
        /// <value>The unit deachsierung.</value>
        public UnitListItem UnitDeachsierung
        {
            get => _unitDeachsierung;
            set
            {
                Deachsierung = Business.Functions.UpdateValue(Deachsierung, _unitDeachsierung, value);

                SetProperty(ref _unitDeachsierung, value);
            }
        }

        /// <summary>
        /// Gets or sets the unit hub.
        /// </summary>
        /// <value>The unit hub.</value>
        public UnitListItem UnitHub
        {
            get => _unitHub;
            set
            {
                Hub = Business.Functions.UpdateValue(Hub, _unitHub, value);

                SetProperty(ref _unitHub, value);
            }
        }

        /// <summary>
        /// Gets or sets the unit hub r.
        /// </summary>
        /// <value>The unit hub r.</value>
        public UnitListItem UnitHubR
        {
            get => _unitHubR;
            set
            {
                HubR = Business.Functions.UpdateValue(HubR, _unitHubR, value);

                SetProperty(ref _unitHubR, value);
            }
        }

        /// <summary>
        /// Gets or sets the unit pleul l.
        /// </summary>
        /// <value>The unit pleul l.</value>
        public UnitListItem UnitPleulL
        {
            get => _unitPleulL;
            set
            {
                PleulL = Business.Functions.UpdateValue(PleulL, _unitPleulL, value);

                SetProperty(ref _unitPleulL, value);
            }
        }

        /// <summary>
        /// Gets or sets the unterschied grad.
        /// </summary>
        /// <value>The unterschied grad.</value>
        public double? Unterschied_grad
        {
            get => _unterschied_grad;
            set { SetProperty(ref _unterschied_grad, value); }
        }

        /// <summary>
        /// Gets or sets the unterschied mm.
        /// </summary>
        /// <value>The unterschied mm.</value>
        public double? Unterschied_mm
        {
            get => _unterschied_mm;
            set { SetProperty(ref _unterschied_mm, value); }
        }

        /// <summary>
        /// Gets the volume quantity units.
        /// </summary>
        /// <value>The volume quantity units.</value>
        public ObservableCollection<UnitListItem> VolumeQuantityUnits { get; }

        /// <summary>
        /// Gets or sets the vorher steuerwinkel oeffnet.
        /// </summary>
        /// <value>The vorher steuerwinkel oeffnet.</value>
        public double? VorherSteuerwinkelOeffnet
        {
            get => _vorherSteuerwinkelOeffnet;
            set { SetProperty(ref _vorherSteuerwinkelOeffnet, value); }
        }

        /// <summary>
        /// Gets or sets the vorher steuerwinkel schließt.
        /// </summary>
        /// <value>The vorher steuerwinkel schließt.</value>
        public double? VorherSteuerwinkelSchließt
        {
            get => _vorherSteuerwinkelSchließt;
            set { SetProperty(ref _vorherSteuerwinkelSchließt, value); }
        }

        /// <summary>
        /// Gets or sets the vorher steuerzeit.
        /// </summary>
        /// <value>The vorher steuerzeit.</value>
        public double? VorherSteuerzeit
        {
            get => _vorherSteuerzeit;
            set
            {
                SetProperty(ref _vorherSteuerzeit, value);
                Refresh_Unterschied();
            }
        }

        #endregion Values
    }
}