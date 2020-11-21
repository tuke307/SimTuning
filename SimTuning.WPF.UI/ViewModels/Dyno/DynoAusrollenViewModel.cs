// project=SimTuning.WPF.UI, file=DynoAusrollenViewModel.cs, creation=2020:10:5 Copyright
// (c) 2020 tuke productions. All rights reserved.
using MaterialDesignThemes.Wpf;
using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.Plugin.Messenger;
using SimTuning.Core.ViewModels.Dyno;
using SimTuning.WPF.UI.Dialog;
using System.Globalization;
using System.Threading.Tasks;
using System.Windows;

namespace SimTuning.WPF.UI.ViewModels.Dyno
{
    public class DynoAusrollenViewModel : AusrollenViewModel
    {
        public DynoAusrollenViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService, IMvxMessenger messenger)
              : base(logProvider, navigationService, messenger)
        {
            this.RefreshPlotCommand = new MvxAsyncCommand(this.RefreshPlot);
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

        /// <summary>
        /// Refreshes the plot.
        /// </summary>
        protected new async Task RefreshPlot()
        {
            if (!this.CheckDynoData())
            {
                return;
            }
            await DialogHost.Show(new DialogLoadingView(), "DialogLoading", (object sender, DialogOpenedEventArgs args) =>
            {
                Task.Run(async () =>
                {
                    await base.RefreshPlot().ConfigureAwait(true);

                    Application.Current.Dispatcher.Invoke(() => args.Session.Close());
                });
            }).ConfigureAwait(true);
        }

        private bool CheckDynoData()
        {
            if (this.Dyno == null)
            {
                WPF.UI.Business.Functions.ShowSnackbarDialog(SimTuning.Core.Business.Functions.GetLocalisedRes(typeof(SimTuning.Core.resources), "ERR_NODATA"));

                return false;
            }

            if (this.Dyno.Beschleunigung == null)
            {
                WPF.UI.Business.Functions.ShowSnackbarDialog(SimTuning.Core.Business.Functions.GetLocalisedRes(typeof(SimTuning.Core.resources), "ERR_NODATA"));

                return false;
            }

            return true;
        }
    }
}