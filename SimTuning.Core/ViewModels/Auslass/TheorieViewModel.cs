﻿using Data;
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

namespace SimTuning.Core.ViewModels.Auslass
{
    public class TheorieViewModel : MvxNavigationViewModel
    {
        public ObservableCollection<UnitListItem> AreaQuantityUnits { get; }
        public ObservableCollection<UnitListItem> LengthQuantityUnits { get; }
        public ObservableCollection<UnitListItem> SpeedQuantityUnits { get; }

        public TheorieViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService) : base(logProvider, navigationService)
        {
            this.AreaQuantityUnits = new AreaQuantity();
            this.LengthQuantityUnits = new LengthQuantity();
            this.SpeedQuantityUnits = new SpeedQuantity();

            this.UnitAbgasV = this.SpeedQuantityUnits.Where(x => x.UnitEnumValue.Equals(SpeedUnit.MeterPerSecond)).First();
            this.UnitAuslassA = this.AreaQuantityUnits.Where(x => x.UnitEnumValue.Equals(AreaUnit.SquareCentimeter)).First();
            this.UnitKruemmerD = this.LengthQuantityUnits.Where(x => x.UnitEnumValue.Equals(LengthUnit.Millimeter)).First();
            this.UnitKruemmerL = this.LengthQuantityUnits.Where(x => x.UnitEnumValue.Equals(LengthUnit.Centimeter)).First();
            this.UnitResonanzL = this.LengthQuantityUnits.Where(x => x.UnitEnumValue.Equals(LengthUnit.Centimeter)).First();

            using (var db = new DatabaseContext())
            {
                IList<VehiclesModel> vehicList = db.Vehicles
                    .Include(vehicles => vehicles.Motor)
                    .Include(vehicles => vehicles.Motor.Auslass)
                    .ToList();

                HelperVehicles = new ObservableCollection<VehiclesModel>(vehicList);
            }

            // Commands
            this.InsertDataCommand = new MvxCommand(this.InsertData);
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

        protected virtual void InsertData()
        {
            if (this.HelperVehicle.Motor.Auslass.FlaecheA.HasValue)
            {
                this.AuslassA = this.HelperVehicle.Motor.Auslass.FlaecheA;
            }

            if (this.HelperVehicle.Motor.ResonanzU.HasValue)
            {
                this.ResonanzDrehzahl = this.HelperVehicle.Motor.ResonanzU;
            }

            if (this.HelperVehicle.Motor.Auslass.SteuerzeitSZ.HasValue)
            {
                this.AusslassSteuerwinkel = this.HelperVehicle.Motor.Auslass.SteuerzeitSZ;
            }
        }

        private void Refresh_KruemmerL()
        {
            if (this.KruemmerD.HasValue)
            {
                if (!this.DrehmomentF.HasValue)
                {
                    this.DrehmomentF = 9;
                }

                this.KruemmerL = AuslassLogic.GetManifoldLength(
                     UnitsNet.UnitConverter.Convert(this.KruemmerD.Value,
                     this.UnitKruemmerD.UnitEnumValue,
                     LengthUnit.Millimeter),

                     this.DrehmomentF.Value,

                     0);
            }
        }

        private void Refresh_KruemmerD()
        {
            if (this.AuslassA.HasValue)
                KruemmerSpannneD =
                    AuslassLogic.GetManifoldDiameter(
                    UnitsNet.UnitConverter.Convert(AuslassA.Value,
                    UnitAuslassA.UnitEnumValue,
                    AreaUnit.SquareCentimeter), 10)
                    +
                    " - "
                    +
                    AuslassLogic.GetManifoldDiameter(
                    UnitsNet.UnitConverter.Convert(AuslassA.Value,
                    UnitAuslassA.UnitEnumValue,
                    AreaUnit.SquareCentimeter), 20);
        }

        private void Refresh_AuspuffGeschwindigkeit()
        {
            if (this.ModAbgasT.HasValue)
            {
                this.AbgasV = AuslassLogic.GetGasVelocity(this.ModAbgasT.Value);
            }
        }

        private void Refresh_Resonanzlaenge()
        {
            if (this.AusslassSteuerwinkel.HasValue && this.AbgasT.HasValue && this.ResonanzDrehzahl.HasValue)
            {
                this.ResonanzL = AuslassLogic.GetResonanceLength(this.AusslassSteuerwinkel.Value, this.AbgasT.Value, this.ResonanzDrehzahl.Value);
            }
        }

        #endregion Commands

        #region Values

        #region Hilfsdaten

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

        #endregion Hilfsdaten

        private string _kruemmerSpannneD;

        public string KruemmerSpannneD
        {
            get => _kruemmerSpannneD;
            set { SetProperty(ref _kruemmerSpannneD, value); }
        }

        private UnitListItem _unitAuslassA;

        public UnitListItem UnitAuslassA
        {
            get => _unitAuslassA;
            set
            {
                AuslassA = Business.Functions.UpdateValue(AuslassA, _unitAuslassA, value);

                SetProperty(ref _unitAuslassA, value);
            }
        }

        private double? _auslassA;

        public double? AuslassA
        {
            get => _auslassA;
            set { SetProperty(ref _auslassA, value); }
        }

        private UnitListItem _unitKruemmerD;

        public UnitListItem UnitKruemmerD
        {
            get => _unitKruemmerD;
            set
            {
                KruemmerD = Business.Functions.UpdateValue(KruemmerD, _unitKruemmerD, value);

                SetProperty(ref _unitKruemmerD, value);
            }
        }

        private double? _kruemmerD;

        public double? KruemmerD
        {
            get => _kruemmerD;
            set
            {
                SetProperty(ref _kruemmerD, value);
                Refresh_KruemmerL();
            }
        }

        private double? _drehmomentF;

        public double? DrehmomentF
        {
            get => _drehmomentF;
            set
            {
                SetProperty(ref _drehmomentF, value);
                Refresh_KruemmerL();
            }
        }

        private UnitListItem _unitKruemmerL;

        public UnitListItem UnitKruemmerL
        {
            get => _unitKruemmerL;
            set
            {
                KruemmerL = Business.Functions.UpdateValue(KruemmerL, _unitKruemmerL, value);

                SetProperty(ref _unitKruemmerL, value);
            }
        }

        private double? _kruemmerL;

        public double? KruemmerL
        {
            get => _kruemmerL;
            set { SetProperty(ref _kruemmerL, value); }
        }

        private double? _modAbgasT;

        public double? ModAbgasT
        {
            get => _modAbgasT;
            set
            {
                SetProperty(ref _modAbgasT, value);
                Refresh_AuspuffGeschwindigkeit();
            }
        }

        private UnitListItem _unitAbgasV;

        public UnitListItem UnitAbgasV
        {
            get => _unitAbgasV;
            set
            {
                AbgasV = Business.Functions.UpdateValue(AbgasV, _unitAbgasV, value);

                SetProperty(ref _unitAbgasV, value);
            }
        }

        private double? _abgasV;

        public double? AbgasV
        {
            get => _abgasV;
            set { SetProperty(ref _abgasV, value); }
        }

        private double? _abgasT;

        public double? AbgasT
        {
            get => _abgasT;
            set
            {
                SetProperty(ref _abgasT, value);
                Refresh_Resonanzlaenge();
            }
        }

        private double? _ausslassSteuerwinkel;

        public double? AusslassSteuerwinkel
        {
            get => _ausslassSteuerwinkel;
            set
            {
                SetProperty(ref _ausslassSteuerwinkel, value);
                Refresh_Resonanzlaenge();
            }
        }

        private double? _resonanzDrehzahl;

        public double? ResonanzDrehzahl
        {
            get => _resonanzDrehzahl;
            set
            {
                SetProperty(ref _resonanzDrehzahl, value);
                Refresh_Resonanzlaenge();
            }
        }

        private UnitListItem _unitResonanzL;

        public UnitListItem UnitResonanzL
        {
            get => _unitAuslassA;
            set
            {
                ResonanzL = Business.Functions.UpdateValue(ResonanzL, _unitResonanzL, value);

                SetProperty(ref _unitResonanzL, value);
            }
        }

        private double? _resonanzL;

        public double? ResonanzL
        {
            get => _resonanzL;
            set { SetProperty(ref _resonanzL, value); }
        }

        #endregion Values
    }
}