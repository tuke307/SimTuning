// Copyright (c) 2021 tuke productions. All rights reserved.
namespace SimTuning.WPF.UI.ViewModels.Tuning
{
    using Microsoft.Extensions.Logging;
    using MvvmCross.Commands;
    using MvvmCross.Navigation;
    using MvvmCross.Plugin.Messenger;
    using SimTuning.WPF.UI.Business;
    using SimTuning.WPF.UI.Messages;
    using System.Threading.Tasks;

    /// <summary>
    /// WPF-spezifisches Tuning-Data-ViewModel.
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
                _messenger.Publish(new ShowSnackbarMessage(this, "Fehler beim löschen"));
            }
        }

        /// <inheritdoc />
        protected override void NewTuning()
        {
            try
            {
                base.NewTuning();
            }
            catch
            {
                _messenger.Publish(new ShowSnackbarMessage(this, "Fehler beim erstellen"));
            }
        }

        /// <inheritdoc cref="Core.ViewModels.Tuning.DataViewModel.SaveTuning" />
        protected new void SaveTuning()
        {
            if (!base.SaveTuning())
            {
                _messenger.Publish(
                    new ShowSnackbarMessage(
                        this,
                        "Fehler beim speichern"));
            }
        }

        #endregion Methods

        #region Values

        private readonly ILogger<TuningDataViewModel> _logger;

        #endregion Values
    }
}