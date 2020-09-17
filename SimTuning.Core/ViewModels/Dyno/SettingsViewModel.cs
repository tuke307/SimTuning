namespace SimTuning.Core.ViewModels.Dyno
{
    using Data;
    using Data.Models;
    using MvvmCross.Commands;
    using MvvmCross.Logging;
    using MvvmCross.Navigation;
    using MvvmCross.Plugin.Messenger;
    using MvvmCross.ViewModels;
    using SimTuning.Core.Models;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    /// SettingsViewModel.
    /// </summary>
    /// <seealso cref="MvvmCross.ViewModels.MvxNavigationViewModel" />
    public class SettingsViewModel : MvxNavigationViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SettingsViewModel" /> class.
        /// </summary>
        /// <param name="logProvider">The log provider.</param>
        /// <param name="navigationService">The navigation service.</param>
        public SettingsViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService, IMvxMessenger messenger)
                                    : base(logProvider, navigationService)
        {
            this._token = messenger.Subscribe<MvxReloaderMessage>(this.ReloadData);
        }

        #region Methods

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
            base.Prepare();
        }

        /// <summary>
        /// Reloads the data.
        /// </summary>
        /// <param name="mvxReloaderMessage">The MVX reloader message.</param>
        public virtual void ReloadData(MvxReloaderMessage mvxReloaderMessage = null)
        {
            try
            {
                using (var db = new DatabaseContext())
                {
                    this.Dyno = db.Dyno.Single(d => d.Active == true);
                }

                this.RaisePropertyChanged(() => this.EndAcceleration);
            }
            catch (Exception exc)
            {
                this.Log.ErrorException("Fehler beim Laden des Dyno-Datensatz: ", exc);
            }
        }

        #endregion Methods

        #region Values

        private readonly MvxSubscriptionToken _token;
        private DynoModel _dyno;

        /// <summary>
        /// Gets or sets the dyno.
        /// </summary>
        /// <value>The dyno.</value>
        public DynoModel Dyno
        {
            get => _dyno;
            set => SetProperty(ref _dyno, value);
        }

        /// <summary>
        /// Gets or sets the end acceleration.
        /// </summary>
        /// <value>The end acceleration.</value>
        public int? EndAcceleration
        {
            get => this.Dyno?.EndAcceleration;
            set
            {
                if (this.Dyno == null)
                {
                    return;
                }

                this.Dyno.EndAcceleration = value;
            }
        }

        /// <summary>
        /// Gets or sets the show acceleration command.
        /// </summary>
        /// <value>The show acceleration command.</value>
        public IMvxAsyncCommand ShowAccelerationCommand { get; protected set; }

        #endregion Values
    }
}