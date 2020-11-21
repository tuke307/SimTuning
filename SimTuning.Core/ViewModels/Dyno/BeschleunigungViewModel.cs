namespace SimTuning.Core.ViewModels.Dyno
{
    using Data;
    using Data.Models;
    using MvvmCross.Commands;
    using MvvmCross.Logging;
    using MvvmCross.Navigation;
    using MvvmCross.Plugin.Messenger;
    using MvvmCross.ViewModels;
    using OxyPlot;
    using SimTuning.Core.ModuleLogic;
    using System;
    using System.Linq;
    using System.Resources;
    using System.Threading.Tasks;

    /// <summary>
    /// BeschleunigungViewModel.
    /// </summary>
    public class BeschleunigungViewModel : MvxNavigationViewModel
    {
        /// <summary>
        /// BeschleunigungViewModel.
        /// </summary>
        /// <param name="logProvider"></param>
        /// <param name="navigationService"></param>
        /// <param name="messenger"></param>
        public BeschleunigungViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService, IMvxMessenger messenger)
             : base(logProvider, navigationService)
        {
        }

        #region Values

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

        public PlotModel PlotBeschleunigung
        {
            get => DynoLogic.PlotBeschleunigung;
        }

        public MvxAsyncCommand RefreshPlotCommand { get; set; }

        public MvxAsyncCommand ShowAusrollenCommand { get; set; }

        #endregion Values

        #region Methods

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        /// <returns>Initilisierung.</returns>
        public override Task Initialize()
        {
            this.ReloadData();

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
        public void ReloadData()
        {
            try
            {
                using (var db = new DatabaseContext())
                {
                    this.Dyno = db.Dyno.Single(d => d.Active == true);
                    db.Entry(this.Dyno).Collection(d => d.Beschleunigung).Load();
                }
            }
            catch (Exception exc)
            {
                this.Log.ErrorException("Fehler bei ReloadData: ", exc);
            }
        }

        /// <summary>
        /// Aktualisiert den Beschleunigungs-Graphen.
        /// </summary>
        /// <returns></returns>
        protected virtual async Task RefreshPlot()
        {
            try
            {
                DynoLogic.GetBeschleunigungsGraphFitted(this.Dyno?.Beschleunigung.ToList());

                await this.RaisePropertyChanged(() => PlotBeschleunigung).ConfigureAwait(true);
            }
            catch (Exception exc)
            {
                this.Log.ErrorException("Fehler bei RefreshPlot: ", exc);
            }
        }

        #endregion Methods
    }
}