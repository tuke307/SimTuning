using Data;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using SimTuning.Models;
using SimTuning.ModuleLogic;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using UnitsNet.Units;

namespace SimTuning.ViewModels.Einlass
{
    public class KanalViewModel : BaseViewModel
    {
        private readonly EinlassLogic einlass = new EinlassLogic();
        public ObservableCollection<UnitListItem> AreaQuantityUnits { get; }
        public ObservableCollection<UnitListItem> VolumeQuantityUnits { get; }
        public ObservableCollection<UnitListItem> LengthQuantityUnits { get; }

        public KanalViewModel()
        {
            //InsertDataCommand = new ActionCommand(InsertData);

            AreaQuantityUnits = new AreaQuantity();
            VolumeQuantityUnits = new VolumeQuantity();
            LengthQuantityUnits = new LengthQuantity();

            //Vorausgewählte Einheiten
            UnitEinlassA = AreaQuantityUnits.Where(x => x.UnitEnumValue.Equals(AreaUnit.SquareMillimeter)).First();
            UnitKurbelgehauseV = VolumeQuantityUnits.Where(x => x.UnitEnumValue.Equals(VolumeUnit.CubicCentimeter)).First();
            UnitAnsaugleitungD = LengthQuantityUnits.Where(x => x.UnitEnumValue.Equals(LengthUnit.Centimeter)).First();
            UnitResonanzlaenge = LengthQuantityUnits.Where(x => x.UnitEnumValue.Equals(LengthUnit.Centimeter)).First();

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

        public void InsertData(object obj)
        {
            if (Vehicle.Motor.Einlass.FlaecheA.HasValue)
                EinlassA = Vehicle.Motor.Einlass.FlaecheA;

            if (Vehicle.Motor.Einlass.SteuerzeitSZ.HasValue)
                Einlasssteuerwinkel = Vehicle.Motor.Einlass.SteuerzeitSZ;

            if (Vehicle.Motor.ResonanzU.HasValue)
                Resonanzdrehzahl = Vehicle.Motor.ResonanzU;

            if (Vehicle.Motor.KurbelgehaeuseV.HasValue)
                KurbelgehauseV = Vehicle.Motor.KurbelgehaeuseV;

            if (Vehicle.Motor.Einlass.LaengeL.HasValue)
                AnsaugleitungD = Vehicle.Motor.Einlass.LaengeL;
        }

        public ICommand InsertDataCommand { get; set; }

        private void Refresh_resonanzlaenge()
        {
            if (EinlassA.HasValue && Einlasssteuerwinkel.HasValue && KurbelgehauseV.HasValue && Resonanzdrehzahl.HasValue && AnsaugleitungD.HasValue)
            {
                Resonanzlaenge = einlass.Get_Resonanzlaenge(
                    UnitsNet.UnitConverter.Convert(EinlassA.Value, UnitEinlassA.UnitEnumValue, AreaUnit.SquareCentimeter),
                    Einlasssteuerwinkel.Value,
                    UnitsNet.UnitConverter.Convert(KurbelgehauseV.Value, UnitKurbelgehauseV.UnitEnumValue, VolumeUnit.CubicCentimeter),
                    Resonanzdrehzahl.Value,
                    UnitsNet.UnitConverter.Convert(AnsaugleitungD.Value, UnitAnsaugleitungD.UnitEnumValue, LengthUnit.Centimeter));
            }
        }

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

        public UnitListItem UnitResonanzlaenge
        {
            get => Get<UnitListItem>();
            set
            {
                Resonanzlaenge = Business.Functions.UpdateValue(Resonanzlaenge, UnitResonanzlaenge, value);

                Set(value);
            }
        }

        public double? Resonanzlaenge
        {
            get => Get<double?>();
            set => Set(value);
        }

        public UnitListItem UnitEinlassA
        {
            get => Get<UnitListItem>();
            set
            {
                EinlassA = Business.Functions.UpdateValue(EinlassA, UnitEinlassA, value);

                Set(value);
            }
        }

        public double? EinlassA
        {
            get => Get<double?>();
            set { Set(value); Refresh_resonanzlaenge(); }
        }

        public double? Einlasssteuerwinkel
        {
            get => Get<double?>();
            set { Set(value); Refresh_resonanzlaenge(); }
        }

        public double? Resonanzdrehzahl
        {
            get => Get<double?>();
            set { Set(value); Refresh_resonanzlaenge(); }
        }

        public UnitListItem UnitKurbelgehauseV
        {
            get => Get<UnitListItem>();
            set
            {
                KurbelgehauseV = Business.Functions.UpdateValue(KurbelgehauseV, UnitKurbelgehauseV, value);

                Set(value);
            }
        }

        public double? KurbelgehauseV
        {
            get => Get<double?>();
            set { Set(value); Refresh_resonanzlaenge(); }
        }

        public UnitListItem UnitAnsaugleitungD
        {
            get => Get<UnitListItem>();
            set
            {
                AnsaugleitungD = Business.Functions.UpdateValue(AnsaugleitungD, UnitAnsaugleitungD, value);

                Set(value);
            }
        }

        public double? AnsaugleitungD
        {
            get => Get<double?>();
            set { Set(value); Refresh_resonanzlaenge(); }
        }
    }
}