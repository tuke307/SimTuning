// project=SimTuning.Core, file=GeschwindigkeitViewModel.cs, creation=2020:12:14 Copyright
// (c) 2021 tuke productions. All rights reserved.
namespace SimTuning.Core.ViewModels.Dyno
{
    using Data;
    using Data.Models;
    using Microsoft.Extensions.Logging;
    using MvvmCross.Commands;
    using MvvmCross.Navigation;
    using MvvmCross.Plugin.Messenger;
    using MvvmCross.ViewModels;
    using OxyPlot;
    using SimTuning.Core.ModuleLogic;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    /// BeschleunigungViewModel.
    /// </summary>
    public class GeschwindigkeitViewModel : MvxNavigationViewModel
    {
        /// <summary>
        /// BeschleunigungViewModel.
        /// </summary>
        /// <param name="logFactory"></param>
        /// <param name="navigationService"></param>
        /// <param name="messenger"></param>
        public GeschwindigkeitViewModel(ILoggerFactory logFactory, IMvxNavigationService navigationService, IMvxMessenger messenger)
             : base(logFactory, navigationService)
        {
            this.RefreshPlotCommand = new MvxAsyncCommand(this.RefreshPlot);
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

        public PlotModel PlotGeschwindigkeit
        {
            get => DynoLogic.PlotGeschwindigkeit;
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
                    db.Entry(this.Dyno).Collection(d => d.Geschwindigkeit).Load();
                }
            }
            catch (Exception exc)
            {
                this.Log.LogError("Fehler bei ReloadData: ", exc);
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
                DynoLogic.GetGeschwindigkeitsGraphFitted(this.Dyno?.Geschwindigkeit.ToList());

                await this.RaisePropertyChanged(() => PlotGeschwindigkeit).ConfigureAwait(true);
            }
            catch (Exception exc)
            {
                this.Log.LogError("Fehler bei RefreshPlot: ", exc);
            }
        }

        #endregion Methods
    }
}