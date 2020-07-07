using Data;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using MvvmCross.Commands;
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
    public class UmrechnungViewModel : MvxViewModel
    {
        private readonly EngineLogic engineLogic;
        public ObservableCollection<UnitListItem> VolumeQuantityUnits { get; }
        public ObservableCollection<UnitListItem> LengthQuantityUnits { get; }

        public UmrechnungViewModel()
        {
            engineLogic = new EngineLogic();

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

        public IMvxCommand InsertDataCommand { get; set; }

        public override void Prepare()
        {
            // This is the first method to be called after construction
        }

        public override Task Initialize()
        {
            // Async initialization

            return base.Initialize();
        }

        #region Commands

        private void Refresh_hubradius()
        {
            if (Hub.HasValue && PleulL.HasValue && Deachsierung.HasValue)
                HubR = engineLogic.Get_hubradius(
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

        protected void InsertData()
        {
            if (HelperVehicle.Motor.HubL.HasValue)
                Hub = HelperVehicle.Motor.HubL;

            if (HelperVehicle.Motor.PleulL.HasValue)
                PleulL = HelperVehicle.Motor.PleulL;

            if (HelperVehicle.Motor.DeachsierungL.HasValue)
                Deachsierung = HelperVehicle.Motor.DeachsierungL;
        }

        private void Refresh_kwgrad()
        {
            if (PleulL.HasValue && HubR.HasValue && Deachsierung.HasValue && Steuerzeit.HasValue)
                AbstandOTlength = engineLogic.Get_mmvorot(
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
                steuerwinkel.AddRange(engineLogic.Get_steuerwinkel(VorherSteuerzeit.Value, NachherSteuerzeit.Value, Kolbenoberkante_checked, Kolbenunterkante_checked));

                VorherSteuerwinkelOeffnet = steuerwinkel[0];
                VorherSteuerwinkelSchließt = steuerwinkel[1];
                NachherSteuerwinkelOeffnet = steuerwinkel[2];
                NachherSteuerwinkelSchließt = steuerwinkel[3];

                Unterschied_grad = engineLogic.Get_Unterschied_grad(VorherSteuerzeit.Value, NachherSteuerzeit.Value);

                //verbessern und durschnitt aus öffnen und schließen bilden
                Unterschied_mm = engineLogic.Get_Unterschied_mm(
                    VorherSteuerwinkelOeffnet.Value,
                    NachherSteuerwinkelOeffnet.Value,

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

        private UnitListItem _unitHub;

        public UnitListItem UnitHub
        {
            get => _unitHub;
            set
            {
                Hub = Business.Functions.UpdateValue(Hub, _unitHub, value);

                SetProperty(ref _unitHub, value);
            }
        }

        private double? _hub;

        public double? Hub
        {
            get => _hub;
            set
            {
                SetProperty(ref _hub, value);
                Refresh_hubradius();
            }
        }

        private UnitListItem _unitPleulL;

        public UnitListItem UnitPleulL
        {
            get => _unitPleulL;
            set
            {
                PleulL = Business.Functions.UpdateValue(PleulL, _unitPleulL, value);

                SetProperty(ref _unitPleulL, value);
            }
        }

        private double? _pleulL;

        public double? PleulL
        {
            get => _pleulL;
            set
            {
                SetProperty(ref _pleulL, value);
                Refresh_hubradius();
            }
        }

        private UnitListItem _unitDeachsierung;

        public UnitListItem UnitDeachsierung
        {
            get => _unitDeachsierung;
            set
            {
                Deachsierung = Business.Functions.UpdateValue(Deachsierung, _unitDeachsierung, value);

                SetProperty(ref _unitDeachsierung, value);
            }
        }

        private double? _deachsierung;

        public double? Deachsierung
        {
            get => _deachsierung;
            set
            {
                SetProperty(ref _deachsierung, value);
                Refresh_hubradius();
            }
        }

        private UnitListItem _unitHubR;

        public UnitListItem UnitHubR
        {
            get => _unitHubR;
            set
            {
                HubR = Business.Functions.UpdateValue(HubR, _unitHubR, value);

                SetProperty(ref _unitHubR, value);
            }
        }

        private double? _hubR;

        public double? HubR
        {
            get => _hubR;
            set { SetProperty(ref _hubR, value); }
        }

        private double? _steuerzeit;

        public double? Steuerzeit
        {
            get => _steuerzeit;
            set
            {
                SetProperty(ref _steuerzeit, value);
                Refresh_kwgrad();
            }
        }

        private UnitListItem _unitAbstandOTlength;

        public UnitListItem UnitAbstandOTlength
        {
            get => _unitAbstandOTlength;
            set
            {
                AbstandOTlength = Business.Functions.UpdateValue(AbstandOTlength, _unitAbstandOTlength, value);

                SetProperty(ref _unitAbstandOTlength, value);
            }
        }

        private double? _abstandOTlength;

        public double? AbstandOTlength
        {
            get => _abstandOTlength;
            set { SetProperty(ref _abstandOTlength, value); }
        }

        private double? _vorherSteuerzeit;

        public double? VorherSteuerzeit
        {
            get => _vorherSteuerzeit;
            set
            {
                SetProperty(ref _vorherSteuerzeit, value);
                Refresh_Unterschied();
            }
        }

        private double? _nachherSteuerzeit;

        public double? NachherSteuerzeit
        {
            get => _nachherSteuerzeit;
            set
            {
                SetProperty(ref _nachherSteuerzeit, value);
                Refresh_Unterschied();
            }
        }

        private double? _vorherSteuerwinkelOeffnet;

        public double? VorherSteuerwinkelOeffnet
        {
            get => _vorherSteuerwinkelOeffnet;
            set { SetProperty(ref _vorherSteuerwinkelOeffnet, value); }
        }

        private double? _nachherSteuerwinkelOeffnet;

        public double? NachherSteuerwinkelOeffnet
        {
            get => _nachherSteuerwinkelOeffnet;
            set { SetProperty(ref _nachherSteuerwinkelOeffnet, value); }
        }

        private double? _vorherSteuerwinkelSchließt;

        public double? VorherSteuerwinkelSchließt
        {
            get => _vorherSteuerwinkelSchließt;
            set { SetProperty(ref _vorherSteuerwinkelSchließt, value); }
        }

        private double? _nachherSteuerwinkelSchließt;

        public double? NachherSteuerwinkelSchließt
        {
            get => _nachherSteuerwinkelSchließt;
            set { SetProperty(ref _nachherSteuerwinkelSchließt, value); }
        }

        private bool _kolbenunterkante_checked;

        public bool Kolbenunterkante_checked
        {
            get => _kolbenunterkante_checked;
            set
            {
                SetProperty(ref _kolbenunterkante_checked, value);
                Refresh_Unterschied();
            }
        }

        private bool _kolbenoberkante_checked;

        public bool Kolbenoberkante_checked
        {
            get => _kolbenoberkante_checked;
            set
            {
                SetProperty(ref _kolbenoberkante_checked, value);
                Refresh_Unterschied();
            }
        }

        private double? _unterschied_grad;

        public double? Unterschied_grad
        {
            get => _unterschied_grad;
            set { SetProperty(ref _unterschied_grad, value); }
        }

        private double? _unterschied_mm;

        public double? Unterschied_mm
        {
            get => _unterschied_mm;
            set { SetProperty(ref _unterschied_mm, value); }
        }

        #endregion Values
    }
}