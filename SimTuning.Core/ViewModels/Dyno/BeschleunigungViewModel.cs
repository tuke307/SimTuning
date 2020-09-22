using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.Plugin.Messenger;
using MvvmCross.ViewModels;
using OxyPlot;
using SimTuning.Core.ModuleLogic;
using System;
using System.Resources;
using System.Threading.Tasks;

namespace SimTuning.Core.ViewModels.Dyno
{
    public class BeschleunigungViewModel : MvxNavigationViewModel
    {
        protected readonly ResourceManager rm;

        public PlotModel PlotBeschleunigung
        {
            get => DynoLogic.PlotBeschleunigung;
        }

        public MvxAsyncCommand RefreshPlotCommand { get; set; }

        public MvxAsyncCommand ShowAusrollenCommand { get; set; }

        public BeschleunigungViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService, IMvxMessenger messenger)
             : base(logProvider, navigationService)
        {
            this.rm = new ResourceManager(typeof(SimTuning.Core.resources));
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
            base.Prepare();
        }

        protected virtual Task RefreshPlot()
        {
            throw new NotImplementedException();

            //DynoLogic.CalculateStrengthPlot(this.Dyno, out List<DynoPsModel> ps/*, out List<DynoNmModel> nm*/);
            //this.Dyno.DynoPS = ps;
            ////this.Dyno.DynoNm = nm;

            //using (var db = new DatabaseContext())
            //{
            //    // in Datenbank einfügen
            //    db.Dyno.Attach(this.Dyno);
            //    db.SaveChanges();
            //}

            this.RaisePropertyChanged(() => this.PlotBeschleunigung);
        }
    }
}