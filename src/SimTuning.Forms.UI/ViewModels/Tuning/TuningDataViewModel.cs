// Copyright (c) 2021 tuke productions. All rights reserved.
namespace SimTuning.Forms.UI.ViewModels.Tuning
{
    using Microsoft.Extensions.Logging;
    using MvvmCross.Commands;
    using MvvmCross.Navigation;
    using MvvmCross.Plugin.Messenger;
    using SimTuning.Forms.UI.Helpers;
    using System;
    using System.Threading.Tasks;

    /// <summary>
    /// TuningDataViewModel.
    /// </summary>
    /// <seealso cref="SimTuning.Core.ViewModels.Tuning.DataViewModel" />
    public class TuningDataViewModel : SimTuning.Core.ViewModels.Tuning.DataViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TuningDataViewModel" /> class.
        /// </summary>
        /// <param name="logger">The log provider.</param>
        /// <param name="navigationService">The navigation service.</param>
        public TuningDataViewModel(
            ILogger<TuningDataViewModel> logger,
            IMvxNavigationService navigationService,
            IMvxMessenger messenger)
            : base(logger, navigationService, messenger)
        {
            this._logger = logger;
        }

        #region Methods

        /// <inheritdoc />
        public override Task Initialize()
        {
            // Commands
            this.NewTuningCommand = new MvxCommand(this.NewTuning);
            this.DeleteTuningCommand = new MvxCommand(this.DeleteTuning);
            this.SaveTuningCommand = new MvxCommand(this.SaveTuning);

            return base.Initialize();
        }

        /// <inheritdoc />
        public override void Prepare()
        {
            base.Prepare();
        }

        /// <inheritdoc cref="Core.ViewModels.Tuning.DataViewModel.DeleteTuning" />
        protected new void DeleteTuning()
        {
            if (!base.DeleteTuning())
            {
                Functions.ShowSnackbarDialog("Fehler beim löschen");
            }
        }

        /// <inheritdoc />
        protected override void NewTuning()
        {
            try
            {
                base.NewTuning();
            }
            catch (Exception)
            {
                Functions.ShowSnackbarDialog("Fehler beim erstellen");
            }
        }

        /// <inheritdoc cref="Core.ViewModels.Tuning.DataViewModel.SaveTuning" />
        protected new void SaveTuning()
        {
            if (!base.SaveTuning())
            {
                Functions.ShowSnackbarDialog("Fehler beim speichern");
            }
        }

        #endregion Methods

        #region Values

        private readonly ILogger<TuningDataViewModel> _logger;

        #endregion Values
    }
}