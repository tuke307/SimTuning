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

namespace SimTuning.ViewModels.Auslass
{
    public class TheorieViewModel : BaseViewModel
    {
        private readonly AuslassLogic auslass;
        public ObservableCollection<UnitListItem> AreaQuantityUnits { get; }
        public ObservableCollection<UnitListItem> LengthQuantityUnits { get; }
        public ObservableCollection<UnitListItem> SpeedQuantityUnits { get; }

        public TheorieViewModel()
        {
            auslass = new AuslassLogic();

            //InsertDataCommand = new ActionCommand(InsertData);

            AreaQuantityUnits = new AreaQuantity();
            LengthQuantityUnits = new LengthQuantity();
            SpeedQuantityUnits = new SpeedQuantity();

            UnitAbgasV = SpeedQuantityUnits.Where(x => x.UnitEnumValue.Equals(SpeedUnit.MeterPerSecond)).First();
            UnitAuslassA = AreaQuantityUnits.Where(x => x.UnitEnumValue.Equals(AreaUnit.SquareCentimeter)).First();
            UnitKruemmerD = LengthQuantityUnits.Where(x => x.UnitEnumValue.Equals(LengthUnit.Millimeter)).First();
            UnitKruemmerL = LengthQuantityUnits.Where(x => x.UnitEnumValue.Equals(LengthUnit.Centimeter)).First();
            UnitResonanzL = LengthQuantityUnits.Where(x => x.UnitEnumValue.Equals(LengthUnit.Centimeter)).First();

            using (var db = new DatabaseContext())
            {
                IList<VehiclesModel> vehicList = db.Vehicles
                    .Include(vehicles => vehicles.Motor)
                    .Include(vehicles => vehicles.Motor.Auslass)
                    .ToList();

                Vehicles = new ObservableCollection<VehiclesModel>(vehicList);
            }
        }

        protected virtual void InsertData(object obj)
        {
            if (Vehicle.Motor.Auslass.FlaecheA.HasValue)
                AuslassA = Vehicle.Motor.Auslass.FlaecheA;

            if (Vehicle.Motor.ResonanzU.HasValue)
                ResonanzDrehzahl = Vehicle.Motor.ResonanzU;

            if (Vehicle.Motor.Auslass.SteuerzeitSZ.HasValue)
                AusslassSteuerwinkel = Vehicle.Motor.Auslass.SteuerzeitSZ;
        }

        public ICommand InsertDataCommand { get; set; }

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

        public string KruemmerSpannneD
        {
            get => Get<string>();
            set => Set(value);
        }

        public UnitListItem UnitAuslassA
        {
            get => Get<UnitListItem>();
            set
            {
                AuslassA = Business.Functions.UpdateValue(AuslassA, UnitAuslassA, value);

                Set(value);
            }
        }

        public double? AuslassA
        {
            get => Get<double?>();
            set { Set(value); Refresh_KruemmerD(); }
        }

        public UnitListItem UnitKruemmerD
        {
            get => Get<UnitListItem>();
            set
            {
                KruemmerD = Business.Functions.UpdateValue(KruemmerD, UnitKruemmerD, value);

                Set(value);
            }
        }

        public double? KruemmerD
        {
            get => Get<double?>();
            set { Set(value); Refresh_KruemmerL(); }
        }

        public double? DrehmomentF
        {
            get => Get<double?>();
            set { Set(value); Refresh_KruemmerL(); }
        }

        public UnitListItem UnitKruemmerL
        {
            get => Get<UnitListItem>();
            set
            {
                KruemmerL = Business.Functions.UpdateValue(KruemmerL, UnitKruemmerL, value);

                Set(value);
            }
        }

        public double? KruemmerL
        {
            get => Get<double?>();
            set => Set(value);
        }

        public double? ModAbgasT
        {
            get => Get<double?>();
            set { Set(value); Refresh_AuspuffGeschwindigkeit(); }
        }

        public UnitListItem UnitAbgasV
        {
            get => Get<UnitListItem>();
            set
            {
                AbgasV = Business.Functions.UpdateValue(AbgasV, UnitAbgasV, value);

                Set(value);
            }
        }

        public double? AbgasV
        {
            get => Get<double?>();
            set => Set(value);
        }

        public double? AbgasT
        {
            get => Get<double?>();
            set { Set(value); Refresh_Resonanzlaenge(); }
        }

        public double? AusslassSteuerwinkel
        {
            get => Get<double?>();
            set { Set(value); Refresh_Resonanzlaenge(); }
        }

        public double? ResonanzDrehzahl
        {
            get => Get<double?>();
            set { Set(value); Refresh_Resonanzlaenge(); }
        }

        public UnitListItem UnitResonanzL
        {
            get => Get<UnitListItem>();
            set
            {
                ResonanzL = Business.Functions.UpdateValue(ResonanzL, UnitResonanzL, value);

                Set(value);
            }
        }

        public double? ResonanzL
        {
            get => Get<double?>();
            set => Set(value);
        }

        private void Refresh_KruemmerL()
        {
            if (KruemmerD.HasValue)
            {
                if (!DrehmomentF.HasValue)
                    DrehmomentF = 9;

                KruemmerL = auslass.Get_Kruemmerlaenge(
                     UnitsNet.UnitConverter.Convert(KruemmerD.Value,
                     UnitKruemmerD.UnitEnumValue,
                     LengthUnit.Millimeter),

                     DrehmomentF.Value,

                     0);
            }
        }

        private void Refresh_KruemmerD()
        {
            if (AuslassA.HasValue)
                KruemmerSpannneD =
                    auslass.Get_KruemmerDurchmesser(
                    UnitsNet.UnitConverter.Convert(AuslassA.Value,
                    UnitAuslassA.UnitEnumValue,
                    AreaUnit.SquareCentimeter), 10)
                    +
                    " - "
                    +
                    auslass.Get_KruemmerDurchmesser(
                    UnitsNet.UnitConverter.Convert(AuslassA.Value,
                    UnitAuslassA.UnitEnumValue,
                    AreaUnit.SquareCentimeter), 20);
        }

        private void Refresh_AuspuffGeschwindigkeit()
        {
            if (ModAbgasT.HasValue)
                AbgasV = auslass.Get_Abgasgeschwindigkeit(ModAbgasT.Value);
        }

        private void Refresh_Resonanzlaenge()
        {
            if (AusslassSteuerwinkel.HasValue && AbgasT.HasValue && ResonanzDrehzahl.HasValue)
                ResonanzL = auslass.Get_Resonanzlaenge(AusslassSteuerwinkel.Value, AbgasT.Value, ResonanzDrehzahl.Value);
        }
    }
}