﻿// Copyright (c) 2021 tuke productions. All rights reserved.
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using SimTuning.Core.Models;
using SimTuning.Core.ModuleLogic;
using SimTuning.Core.Services;
using SimTuning.Data;
using SimTuning.Data.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using UnitsNet.Units;

namespace SimTuning.Core.ViewModels.Motor
{
    public class HubraumViewModel : MvxViewModel
    {
        /// <summary> Initializes a new instance of the <see cref="HubraumViewModel"/>
        /// class. </summary> <param name="logger"><inheritdoc cref="ILogger"
        /// path="/summary/node()" /></param> <param name="navigationService"><inheritdoc
        /// cref="IMvxNavigationService" path="/summary/node()" /></param
        public HubraumViewModel(
            ILogger<HubraumViewModel> logger,
            IMvxNavigationService navigationService,
            IVehicleService vehicleService)
        {
            this._logger = logger;
            this._vehicleService = vehicleService;

            VolumeQuantityUnits = new VolumeQuantity();
            LengthQuantityUnits = new LengthQuantity();
        }

        #region Commands

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        /// <returns>Initilisierung.</returns>
        public override Task Initialize()
        {
            UnitEinbauspiel = LengthQuantityUnits.Where(x => x.UnitEnumValue.Equals(LengthUnit.Millimeter)).First();
            UnitHub = LengthQuantityUnits.Where(x => x.UnitEnumValue.Equals(LengthUnit.Centimeter)).First();
            UnitHubraumV = VolumeQuantityUnits.Where(x => x.UnitEnumValue.Equals(VolumeUnit.CubicCentimeter)).First();
            UnitKolbenD = LengthQuantityUnits.Where(x => x.UnitEnumValue.Equals(LengthUnit.Centimeter)).First();
            UnitBohrungD = LengthQuantityUnits.Where(x => x.UnitEnumValue.Equals(LengthUnit.Centimeter)).First();

            HelperVehicles = new ObservableCollection<VehiclesModel>(_vehicleService.RetrieveVehicles());

            return base.Initialize();
        }

        /// <summary>
        /// Prepares this instance. called after construction.
        /// </summary>
        public override void Prepare()
        {
        }

        private void Refresh_all()
        {
            if (Hub.HasValue && HubraumV.HasValue)
            {
                if (!Einbauspiel.HasValue)
                    Einbauspiel = 0.03;

                BohrungD = EngineLogic.GetCylinderHoleDiameter(
                    UnitsNet.UnitConverter.Convert(
                    HubraumV.Value,
                    UnitHubraumV.UnitEnumValue,
                    VolumeUnit.CubicCentimeter),

                    UnitsNet.UnitConverter.Convert(
                    Hub.Value,
                    UnitHub.UnitEnumValue,
                    LengthUnit.Centimeter));

                KolbenD = EngineLogic.GetKolbenDurchmesser(
                    UnitsNet.UnitConverter.Convert(
                    BohrungD.Value,
                    UnitBohrungD.UnitEnumValue,
                    LengthUnit.Centimeter),

                    UnitsNet.UnitConverter.Convert(
                    Einbauspiel.Value,
                    UnitEinbauspiel.UnitEnumValue,
                    LengthUnit.Millimeter));
            }
        }

        #endregion Commands

        #region Values

        private readonly ILogger<HubraumViewModel> _logger;

        private readonly IVehicleService _vehicleService;

        private double? _bohrungD;

        private double? _einbauspiel;

        private GrindingDiametersModel _grindingDiameters;

        private VehiclesModel _helperVehicle;

        private ObservableCollection<VehiclesModel> _helperVehicles;

        private double? _hub;

        private double? _hubraumV;

        private double? _kolbenD;

        private UnitListItem _unitBohrungD;

        private UnitListItem _unitEinbauspiel;

        private UnitListItem _unitHub;

        private UnitListItem _unitHubraumV;

        private UnitListItem _unitKolbenD;

        public double? BohrungD
        {
            get => _bohrungD;
            set { SetProperty(ref _bohrungD, value); }
        }

        public double? Einbauspiel
        {
            get => _einbauspiel;
            set
            {
                SetProperty(ref _einbauspiel, value);
                Refresh_all();
            }
        }

        public GrindingDiametersModel GrindingDiameters
        {
            get => _grindingDiameters;
            set { SetProperty(ref _grindingDiameters, value); }
        }

        public VehiclesModel HelperVehicle
        {
            get => _helperVehicle;
            set
            {
                SetProperty(ref _helperVehicle, value);

                if (value.Motor.BohrungD.HasValue)
                    GrindingDiameters = EngineLogic.GetGrindingDiameters(value.Motor.BohrungD.Value);
            }
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
                Refresh_all();
            }
        }

        public double? HubraumV
        {
            get => _hubraumV;
            set
            {
                SetProperty(ref _hubraumV, value);
                Refresh_all();
            }
        }

        public double? KolbenD
        {
            get => _kolbenD;
            set { SetProperty(ref _kolbenD, value); }
        }

        public ObservableCollection<UnitListItem> LengthQuantityUnits { get; }

        public UnitListItem UnitBohrungD
        {
            get => _unitBohrungD;
            set
            {
                BohrungD = Helpers.Functions.UpdateValue(BohrungD, _unitBohrungD, value);

                SetProperty(ref _unitBohrungD, value);
            }
        }

        public UnitListItem UnitEinbauspiel
        {
            get => _unitEinbauspiel;
            set
            {
                Einbauspiel = Helpers.Functions.UpdateValue(Einbauspiel, _unitEinbauspiel, value);

                SetProperty(ref _unitEinbauspiel, value);
            }
        }

        public UnitListItem UnitHub
        {
            get => _unitHub;
            set
            {
                Hub = Helpers.Functions.UpdateValue(Hub, UnitHub, value);

                SetProperty(ref _unitHub, value);
            }
        }

        public UnitListItem UnitHubraumV
        {
            get => _unitHubraumV;
            set
            {
                HubraumV = Helpers.Functions.UpdateValue(HubraumV, UnitHubraumV, value);

                SetProperty(ref _unitHubraumV, value);
            }
        }

        public UnitListItem UnitKolbenD
        {
            get => _unitKolbenD;
            set
            {
                KolbenD = Helpers.Functions.UpdateValue(KolbenD, UnitKolbenD, value);

                SetProperty(ref _unitKolbenD, value);
            }
        }

        public ObservableCollection<UnitListItem> VolumeQuantityUnits { get; }

        #endregion Values
    }
}