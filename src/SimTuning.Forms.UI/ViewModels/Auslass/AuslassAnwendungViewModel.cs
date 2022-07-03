// Copyright (c) 2021 tuke productions. All rights reserved.
namespace SimTuning.Forms.UI.ViewModels.Auslass
{
    using Microsoft.Extensions.Logging;
    using MvvmCross.Commands;
    using MvvmCross.Navigation;
    using SimTuning.Core.Services;
    using System.IO;
    using System.Threading.Tasks;
    using Xamarin.Forms;

    /// <summary>
    /// AuslassAnwendungViewModel.
    /// </summary>
    /// <seealso cref="SimTuning.Core.ViewModels.Auslass.AnwendungViewModel" />
    public class AuslassAnwendungViewModel : SimTuning.Core.ViewModels.Auslass.AnwendungViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AuslassAnwendungViewModel" />
        /// class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="navigationService">The navigation service.</param>
        public AuslassAnwendungViewModel(
            ILogger<AuslassAnwendungViewModel> logger,
            IMvxNavigationService navigationService,
            IVehicleService vehicleService)
            : base(logger, navigationService, vehicleService)
        {
            this._logger = logger;
        }

        #region Methods

        /// <inheritdoc />
        public override Task Initialize()
        {
            // override command
            this.CalculateCommand = new MvxAsyncCommand(this.Calculate);

            return base.Initialize();
        }

        /// <inheritdoc />
        public override void Prepare()
        {
            base.Prepare();
        }

        /// <inheritdoc cref="Core.ViewModels.Auslass.AnwendungViewModel.Calculate" />
        protected new async Task Calculate()
        {
            Stream stream = base.Calculate();
            Auspuff = ImageSource.FromStream(() => stream);
        }

        #endregion Methods

        #region Values

        private readonly ILogger<AuslassAnwendungViewModel> _logger;
        private ImageSource _auspuff;

        public ImageSource Auspuff
        {
            get => _auspuff;
            private set => SetProperty(ref _auspuff, value);
        }

        #endregion Values
    }
}