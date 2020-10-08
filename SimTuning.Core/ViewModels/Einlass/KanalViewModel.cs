// project=SimTuning.Core, file=KanalViewModel.cs, creation=2020:7:31 Copyright (c) 2020
// tuke productions. All rights reserved.
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
    /// KanalViewModel.
    /// </summary>
    /// <seealso cref="MvvmCross.ViewModels.MvxNavigationViewModel" />
    public class KanalViewModel : MvxNavigationViewModel
    {
        /// <summary>
        /// Gets the area quantity units.
        /// </summary>
        /// <value>The area quantity units.</value>
        public ObservableCollection<UnitListItem> AreaQuantityUnits { get; }

        /// <summary>
        /// Gets or sets the insert data command.
        /// </summary>
        /// <value>The insert data command.</value>
        public IMvxCommand InsertDataCommand { get; set; }

        /// <summary>
        /// Gets the length quantity units.
        /// </summary>
        /// <value>The length quantity units.</value>
        public ObservableCollection<UnitListItem> LengthQuantityUnits { get; }

        /// <summary>
        /// Gets the volume quantity units.
        /// </summary>
        /// <value>The volume quantity units.</value>
        public ObservableCollection<UnitListItem> VolumeQuantityUnits { get; }

        public KanalViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService)
            : base(logProvider, navigationService)
        {
            this.AreaQuantityUnits = new AreaQuantity();
            this.VolumeQuantityUnits = new VolumeQuantity();
            this.LengthQuantityUnits = new LengthQuantity();

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

            this.InsertDataCommand = new MvxCommand(this.InsertData);
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

        /// <summary>
        /// Inserts the data.
        /// </summary>
        private void InsertData()
        {
            if (this.HelperVehicle.Motor.Einlass.FlaecheA.HasValue)
            {
                this.EinlassA = this.HelperVehicle.Motor.Einlass.FlaecheA;
            }

            if (this.HelperVehicle.Motor.Einlass.SteuerzeitSZ.HasValue)
            {
                this.Einlasssteuerwinkel = this.HelperVehicle.Motor.Einlass.SteuerzeitSZ;
            }

            if (this.HelperVehicle.Motor.ResonanzU.HasValue)
            {
                this.Resonanzdrehzahl = this.HelperVehicle.Motor.ResonanzU;
            }

            if (this.HelperVehicle.Motor.KurbelgehaeuseV.HasValue)
            {
                this.KurbelgehauseV = this.HelperVehicle.Motor.KurbelgehaeuseV;
            }

            if (this.HelperVehicle.Motor.Einlass.LaengeL.HasValue)
            {
                this.AnsaugleitungD = this.HelperVehicle.Motor.Einlass.LaengeL;
            }
        }

        private void RefreshResonanzlaenge()
        {
            if (this.EinlassA.HasValue && Einlasssteuerwinkel.HasValue && KurbelgehauseV.HasValue && Resonanzdrehzahl.HasValue && AnsaugleitungD.HasValue)
            {
                Resonanzlaenge = EinlassLogic.GetResonanzLaenge(
                    UnitsNet.UnitConverter.Convert(EinlassA.Value, UnitEinlassA.UnitEnumValue, AreaUnit.SquareCentimeter),
                    Einlasssteuerwinkel.Value,
                    UnitsNet.UnitConverter.Convert(KurbelgehauseV.Value, UnitKurbelgehauseV.UnitEnumValue, VolumeUnit.CubicCentimeter),
                    Resonanzdrehzahl.Value,
                    UnitsNet.UnitConverter.Convert(AnsaugleitungD.Value, UnitAnsaugleitungD.UnitEnumValue, LengthUnit.Centimeter));
            }
        }

        #endregion Commands

        #region Values

        private double? _ansaugleitungD;
        private double? _einlassA;
        private double? _einlasssteuerwinkel;
        private VehiclesModel _helperVehicle;
        private ObservableCollection<VehiclesModel> _helperVehicles;

        private double? _kurbelgehauseV;

        private double? _resonanzdrehzahl;

        private double? _resonanzlaenge;

        private UnitListItem _unitAnsaugleitungD;

        private UnitListItem _unitEinlassA;

        private UnitListItem _unitKurbelgehauseV;

        private UnitListItem _unitResonanzlaenge;

        public double? AnsaugleitungD
        {
            get => _ansaugleitungD;
            set
            {
                SetProperty(ref _ansaugleitungD, value);
                RefreshResonanzlaenge();
            }
        }

        public double? EinlassA
        {
            get => _einlassA;
            set
            {
                SetProperty(ref _einlassA, value);
                RefreshResonanzlaenge();
            }
        }

        public double? Einlasssteuerwinkel
        {
            get => _einlasssteuerwinkel;
            set
            {
                SetProperty(ref _einlasssteuerwinkel, value);
                RefreshResonanzlaenge();
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

        public double? KurbelgehauseV
        {
            get => _kurbelgehauseV;
            set
            {
                SetProperty(ref _kurbelgehauseV, value);
                RefreshResonanzlaenge();
            }
        }

        public double? Resonanzdrehzahl
        {
            get => _resonanzdrehzahl;
            set
            {
                SetProperty(ref _resonanzdrehzahl, value);
                RefreshResonanzlaenge();
            }
        }

        public double? Resonanzlaenge
        {
            get => _resonanzlaenge;
            set { SetProperty(ref _resonanzlaenge, value); }
        }

        public UnitListItem UnitAnsaugleitungD
        {
            get => _unitAnsaugleitungD;
            set
            {
                AnsaugleitungD = Business.Functions.UpdateValue(AnsaugleitungD, _unitAnsaugleitungD, value);

                SetProperty(ref _unitAnsaugleitungD, value);
            }
        }

        public UnitListItem UnitEinlassA
        {
            get => _unitEinlassA;
            set
            {
                EinlassA = Business.Functions.UpdateValue(EinlassA, UnitEinlassA, value);

                SetProperty(ref _unitEinlassA, value);
            }
        }

        public UnitListItem UnitKurbelgehauseV
        {
            get => _unitKurbelgehauseV;
            set
            {
                KurbelgehauseV = Business.Functions.UpdateValue(KurbelgehauseV, _unitKurbelgehauseV, value);

                SetProperty(ref _unitKurbelgehauseV, value);
            }
        }

        public UnitListItem UnitResonanzlaenge
        {
            get => _unitResonanzlaenge;
            set
            {
                Resonanzlaenge = Business.Functions.UpdateValue(Resonanzlaenge, _unitResonanzlaenge, value);

                SetProperty(ref _unitResonanzlaenge, value);
            }
        }

        #endregion Values
    }
}