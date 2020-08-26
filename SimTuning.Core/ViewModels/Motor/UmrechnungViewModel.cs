// project=SimTuning.Core, file=UmrechnungViewModel.cs, creation=2020:7:31 Copyright (c)
// 2020 tuke productions. All rights reserved.
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

namespace SimTuning.Core.ViewModels.Motor
{
    public class UmrechnungViewModel : MvxNavigationViewModel
    {
        public IMvxCommand InsertDataCommand { get; set; }

        public ObservableCollection<UnitListItem> LengthQuantityUnits { get; }

        public ObservableCollection<UnitListItem> VolumeQuantityUnits { get; }

        public UmrechnungViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService) : base(logProvider, navigationService)
        {
            this.

            VolumeQuantityUnits = new VolumeQuantity();
            LengthQuantityUnits = new LengthQuantity();

            UnitAbstandOTlength = LengthQuantityUnits.Where(x => x.UnitEnumValue.Equals(LengthUnit.Millimeter)).First();
            UnitDeachsierung = LengthQuantityUnits.Where(x => x.UnitEnumValue.Equals(LengthUnit.Millimeter)).First();
            UnitHub = LengthQuantityUnits.Where(x => x.UnitEnumValue.Equals(LengthUnit.Millimeter)).First();
            UnitHubR = LengthQuantityUnits.Where(x => x.UnitEnumValue.Equals(LengthUnit.Millimeter)).First();
            UnitPleulL = LengthQuantityUnits.Where(x => x.UnitEnumValue.Equals(LengthUnit.Millimeter)).First();

            //using (var db = new DatabaseContext())
            //{
            //    IList<VehiclesModel> vehicList = db.Vehicles
            //        .Include(vehicles => vehicles.Motor)
            //        .Include(vehicles => vehicles.Motor.Einlass)
            //        .Include(vehicles => vehicles.Motor.Auslass)
            //        .Include(vehicles => vehicles.Motor.Ueberstroemer)
            //        .ToList();

            //    HelperVehicles = new ObservableCollection<VehiclesModel>(vehicList);
            //}

            InsertDataCommand = new MvxCommand(InsertData);
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

        #region Commands

        protected void InsertData()
        {
            if (HelperVehicle.Motor.HubL.HasValue)
                Hub = HelperVehicle.Motor.HubL;

            if (HelperVehicle.Motor.PleulL.HasValue)
                PleulL = HelperVehicle.Motor.PleulL;

            if (HelperVehicle.Motor.DeachsierungL.HasValue)
                Deachsierung = HelperVehicle.Motor.DeachsierungL;
        }

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

        #endregion Commands

        #region Values

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

        public double? AbstandOTlength
        {
            get => _abstandOTlength;
            set { SetProperty(ref _abstandOTlength, value); }
        }

        public double? Deachsierung
        {
            get => _deachsierung;
            set
            {
                SetProperty(ref _deachsierung, value);
                Refresh_hubradius();
            }
        }

        public VehiclesModel HelperVehicle
        {
            get => _helperVehicle;
            set { SetProperty(ref _helperVehicle, value); }
        }

        public ObservableCollection<VehiclesModel> HelperVehicles
        {
            get => _helperVehicles;
            set { SetProperty(ref _helperVehicles, value); }
        }

        public double? Hub
        {
            get => _hub;
            set
            {
                SetProperty(ref _hub, value);
                Refresh_hubradius();
            }
        }

        public double? HubR
        {
            get => _hubR;
            set { SetProperty(ref _hubR, value); }
        }

        public bool Kolbenoberkante_checked
        {
            get => _kolbenoberkante_checked;
            set
            {
                SetProperty(ref _kolbenoberkante_checked, value);
                Refresh_Unterschied();
            }
        }

        public bool Kolbenunterkante_checked
        {
            get => _kolbenunterkante_checked;
            set
            {
                SetProperty(ref _kolbenunterkante_checked, value);
                Refresh_Unterschied();
            }
        }

        public double? NachherSteuerwinkelOeffnet
        {
            get => _nachherSteuerwinkelOeffnet;
            set { SetProperty(ref _nachherSteuerwinkelOeffnet, value); }
        }

        public double? NachherSteuerwinkelSchließt
        {
            get => _nachherSteuerwinkelSchließt;
            set { SetProperty(ref _nachherSteuerwinkelSchließt, value); }
        }

        public double? NachherSteuerzeit
        {
            get => _nachherSteuerzeit;
            set
            {
                SetProperty(ref _nachherSteuerzeit, value);
                Refresh_Unterschied();
            }
        }

        public double? PleulL
        {
            get => _pleulL;
            set
            {
                SetProperty(ref _pleulL, value);
                Refresh_hubradius();
            }
        }

        public double? Steuerzeit
        {
            get => _steuerzeit;
            set
            {
                SetProperty(ref _steuerzeit, value);
                Refresh_kwgrad();
            }
        }

        public UnitListItem UnitAbstandOTlength
        {
            get => _unitAbstandOTlength;
            set
            {
                AbstandOTlength = Business.Functions.UpdateValue(AbstandOTlength, _unitAbstandOTlength, value);

                SetProperty(ref _unitAbstandOTlength, value);
            }
        }

        public UnitListItem UnitDeachsierung
        {
            get => _unitDeachsierung;
            set
            {
                Deachsierung = Business.Functions.UpdateValue(Deachsierung, _unitDeachsierung, value);

                SetProperty(ref _unitDeachsierung, value);
            }
        }

        public UnitListItem UnitHub
        {
            get => _unitHub;
            set
            {
                Hub = Business.Functions.UpdateValue(Hub, _unitHub, value);

                SetProperty(ref _unitHub, value);
            }
        }

        public UnitListItem UnitHubR
        {
            get => _unitHubR;
            set
            {
                HubR = Business.Functions.UpdateValue(HubR, _unitHubR, value);

                SetProperty(ref _unitHubR, value);
            }
        }

        public UnitListItem UnitPleulL
        {
            get => _unitPleulL;
            set
            {
                PleulL = Business.Functions.UpdateValue(PleulL, _unitPleulL, value);

                SetProperty(ref _unitPleulL, value);
            }
        }

        public double? Unterschied_grad
        {
            get => _unterschied_grad;
            set { SetProperty(ref _unterschied_grad, value); }
        }

        public double? Unterschied_mm
        {
            get => _unterschied_mm;
            set { SetProperty(ref _unterschied_mm, value); }
        }

        public double? VorherSteuerwinkelOeffnet
        {
            get => _vorherSteuerwinkelOeffnet;
            set { SetProperty(ref _vorherSteuerwinkelOeffnet, value); }
        }

        public double? VorherSteuerwinkelSchließt
        {
            get => _vorherSteuerwinkelSchließt;
            set { SetProperty(ref _vorherSteuerwinkelSchließt, value); }
        }

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