using Data;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using SimTuning.Models;
using SimTuning.ModuleLogic;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using UnitsNet.Units;

namespace SimTuning.ViewModels.Motor
{
    public class UmrechnungViewModel : BaseViewModel
    {
        public ObservableCollection<UnitListItem> VolumeQuantityUnits { get; }
        public ObservableCollection<UnitListItem> LengthQuantityUnits { get; }

        public UmrechnungViewModel()
        {
            //InsertDataCommand = new ActionCommand(InsertData);

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

                Vehicles = new ObservableCollection<VehiclesModel>(vehicList);
            }
        }

        private EngineLogic steuerzeit = new EngineLogic();

        public ObservableCollection<VehiclesModel> Vehicles
        {
            get => Get<ObservableCollection<VehiclesModel>>();
            set => Set(value);
        }

        public VehiclesModel Vehicle
        {
            get => Get<VehiclesModel>();
            set => Set(value);
        }

        protected void InsertData(object parameter)
        {
            if (Vehicle.Motor.HubL.HasValue)
                Hub = Vehicle.Motor.HubL;

            if (Vehicle.Motor.PleulL.HasValue)
                PleulL = Vehicle.Motor.PleulL;

            if (Vehicle.Motor.DeachsierungL.HasValue)
                Deachsierung = Vehicle.Motor.DeachsierungL;
        }

        public UnitListItem UnitHub
        {
            get => Get<UnitListItem>();
            set
            {
                Hub = Business.Functions.UpdateValue(Hub, UnitHub, value);

                Set(value);
            }
        }

        public double? Hub
        {
            get => Get<double?>();
            set { Set(value); Refresh_hubradius(); }
        }

        public UnitListItem UnitPleulL
        {
            get => Get<UnitListItem>();
            set
            {
                PleulL = Business.Functions.UpdateValue(PleulL, UnitPleulL, value);

                Set(value);
            }
        }

        public double? PleulL
        {
            get => Get<double?>();
            set { Set(value); Refresh_hubradius(); }
        }

        public UnitListItem UnitDeachsierung
        {
            get => Get<UnitListItem>();
            set
            {
                Deachsierung = Business.Functions.UpdateValue(Deachsierung, UnitDeachsierung, value);

                Set(value);
            }
        }

        public double? Deachsierung
        {
            get => Get<double?>();
            set { Set(value); Refresh_hubradius(); }
        }

        private void Refresh_hubradius()
        {
            if (Hub.HasValue && PleulL.HasValue && Deachsierung.HasValue)
                HubR = steuerzeit.Get_hubradius(
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

        public UnitListItem UnitHubR
        {
            get => Get<UnitListItem>();
            set
            {
                HubR = Business.Functions.UpdateValue(HubR, UnitHubR, value);

                Set(value);
            }
        }

        public double? HubR
        {
            get => Get<double?>();
            set => Set(value);
        }

        public double? Steuerzeit
        {
            get => Get<double?>();
            set { Set(value); Refresh_kwgrad(); }
        }

        public UnitListItem UnitAbstandOTlength
        {
            get => Get<UnitListItem>();
            set
            {
                AbstandOTlength = Business.Functions.UpdateValue(AbstandOTlength, UnitAbstandOTlength, value);

                Set(value);
            }
        }

        public double? AbstandOTlength
        {
            get => Get<double?>();
            set => Set(value);
        }

        private void Refresh_kwgrad()
        {
            if (PleulL.HasValue && HubR.HasValue && Deachsierung.HasValue && Steuerzeit.HasValue)
                AbstandOTlength = steuerzeit.Get_mmvorot(
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

        public ICommand InsertDataCommand { get; set; }

        //erhöhen
        public double? vorher_steuerzeit
        {
            get => Get<double?>();
            set { Set(value); Refresh_Unterschied(); }
        }

        public double? nachher_steuerzeit
        {
            get => Get<double?>();
            set { Set(value); Refresh_Unterschied(); }
        }

        public double? vorher_steuerwinkel_oeffnet
        {
            get => Get<double?>();
            set => Set(value);
        }

        public double? nachher_steuerwinkel_oeffnet
        {
            get => Get<double?>();
            set => Set(value);
        }

        public double? vorher_steuerwinkel_schließt
        {
            get => Get<double?>();
            set => Set(value);
        }

        public double? nachher_steuerwinkel_schließt
        {
            get => Get<double?>();
            set => Set(value);
        }

        public bool kolbenunterkante_checked
        {
            get => Get<bool>();
            set { Set(value); Refresh_Unterschied(); }
        }

        public bool kolbenoberkante_checked
        {
            get => Get<bool>();
            set { Set(value); Refresh_Unterschied(); }
        }

        public double? unterschied_grad
        {
            get => Get<double?>();
            set => Set(value);
        }

        public double? unterschied_mm
        {
            get => Get<double?>();
            set => Set(value);
        }

        private void Refresh_Unterschied()
        {
            if (vorher_steuerzeit.HasValue && nachher_steuerzeit.HasValue && PleulL.HasValue && HubR.HasValue && Deachsierung.HasValue && (kolbenoberkante_checked || kolbenunterkante_checked))
            {
                List<double> steuerwinkel = new List<double>();
                steuerwinkel.AddRange(steuerzeit.Get_steuerwinkel(vorher_steuerzeit.Value, nachher_steuerzeit.Value, kolbenoberkante_checked, kolbenunterkante_checked));

                vorher_steuerwinkel_oeffnet = steuerwinkel[0];
                vorher_steuerwinkel_schließt = steuerwinkel[1];
                nachher_steuerwinkel_oeffnet = steuerwinkel[2];
                nachher_steuerwinkel_schließt = steuerwinkel[3];

                unterschied_grad = steuerzeit.Get_Unterschied_grad(vorher_steuerzeit.Value, nachher_steuerzeit.Value);

                //verbessern und durschnitt aus öffnen und schließen bilden
                unterschied_mm = steuerzeit.Get_Unterschied_mm(
                    vorher_steuerwinkel_oeffnet.Value,
                    nachher_steuerwinkel_oeffnet.Value,

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
    }
}