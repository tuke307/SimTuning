// project=SimTuning.Core, file=VergaserViewModel.cs, creation=2020:7:31 Copyright (c)
// 2020 tuke productions. All rights reserved.
namespace SimTuning.Core.ViewModels.Einlass
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
    /// VergaserViewModel.
    /// </summary>
    /// <seealso cref="MvvmCross.ViewModels.MvxNavigationViewModel" />
    public class VergaserViewModel : MvxNavigationViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VergaserViewModel" /> class.
        /// </summary>
        /// <param name="logProvider">The log provider.</param>
        /// <param name="navigationService">The navigation service.</param>
        public VergaserViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService) : base(logProvider, navigationService)
        {
            VolumeQuantityUnits = new VolumeQuantity();
            LengthQuantityUnits = new LengthQuantity();

            UnitHubvolumen = VolumeQuantityUnits.Where(x => x.UnitEnumValue.Equals(VolumeUnit.CubicMillimeter)).First();
            UnitVergasergroeße = LengthQuantityUnits.Where(x => x.UnitEnumValue.Equals(LengthUnit.Millimeter)).First();
            UnitHauptdueseD = LengthQuantityUnits.Where(x => x.UnitEnumValue.Equals(LengthUnit.Micrometer)).First();

            using (var db = new DatabaseContext())
            {
                IList<VehiclesModel> vehicList = db.Vehicles
                    .Include(vehicles => vehicles.Motor)
                    .ToList();

                HelperVehicles = new ObservableCollection<VehiclesModel>(vehicList);
            }

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

        public void InsertData()
        {
            if (HelperVehicle.Motor.HubraumV.HasValue)
                Hubvolumen = HelperVehicle.Motor.HubraumV;

            if (HelperVehicle.Motor.ResonanzU.HasValue)
                Resonanzdrehzahl = HelperVehicle.Motor.ResonanzU;
        }

        private void Refresh_Hauptduesendurchmesser()
        {
            if (Vergasergroeße.HasValue)
            {
                HauptdueseD = EinlassLogic.GetVergaserHauptduesenDurchmesser(
                    UnitsNet.UnitConverter.Convert(Vergasergroeße.Value,
                    UnitVergasergroeße.UnitEnumValue,
                    LengthUnit.Millimeter));
            }
        }

        private void Refresh_Vergasergroeße()
        {
            if (Hubvolumen.HasValue && Resonanzdrehzahl.HasValue)
            {
                Vergasergroeße = EinlassLogic.GetVergaserDurchmesser(
                    UnitsNet.UnitConverter.Convert(Hubvolumen.Value,
                    UnitHubvolumen.UnitEnumValue,
                    VolumeUnit.CubicCentimeter),
                    Resonanzdrehzahl.Value);
            }
        }

        #endregion Commands

        #region Values

        private double? _hauptdueseD;

        private VehiclesModel _helperVehicle;

        private ObservableCollection<VehiclesModel> _helperVehicles;

        private double? _hubvolumen;

        private double? _resonanzdrehzahl;

        private UnitListItem _unitHauptdueseD;

        private UnitListItem _unitHubvolumen;

        private UnitListItem _unitVergasergroeße;

        private double? _vergasergroeße;

        public double? HauptdueseD
        {
            get => _hauptdueseD;
            set { SetProperty(ref _hauptdueseD, value); }
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

        public double? Hubvolumen
        {
            get => _hubvolumen;
            set
            {
                SetProperty(ref _hubvolumen, value);
                Refresh_Vergasergroeße();
            }
        }

        public IMvxCommand InsertDataCommand { get; set; }

        public ObservableCollection<UnitListItem> LengthQuantityUnits { get; }

        public double? Resonanzdrehzahl
        {
            get => _resonanzdrehzahl;
            set
            {
                SetProperty(ref _resonanzdrehzahl, value);
                Refresh_Vergasergroeße();
            }
        }

        public UnitListItem UnitHauptdueseD
        {
            get => _unitHauptdueseD;
            set
            {
                HauptdueseD = Business.Functions.UpdateValue(HauptdueseD, UnitHauptdueseD, value);

                SetProperty(ref _unitHauptdueseD, value);
            }
        }

        public UnitListItem UnitHubvolumen
        {
            get => _unitHubvolumen;
            set
            {
                Hubvolumen = Business.Functions.UpdateValue(Hubvolumen, UnitHubvolumen, value);

                SetProperty(ref _unitHubvolumen, value);
            }
        }

        public UnitListItem UnitVergasergroeße
        {
            get => _unitVergasergroeße;
            set
            {
                Vergasergroeße = Business.Functions.UpdateValue(Vergasergroeße, UnitVergasergroeße, value);

                SetProperty(ref _unitVergasergroeße, value);
            }
        }

        public double? Vergasergroeße
        {
            get => _vergasergroeße;
            set
            {
                SetProperty(ref _vergasergroeße, value);
                Refresh_Hauptduesendurchmesser();
            }
        }

        public ObservableCollection<UnitListItem> VolumeQuantityUnits { get; }

        #endregion Values
    }
}