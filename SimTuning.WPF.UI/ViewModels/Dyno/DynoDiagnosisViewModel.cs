// project=SimTuning.WPF.UI, file=DynoDiagnosisViewModel.cs, creation=2020:9:2 Copyright
// (c) 2020 tuke productions. All rights reserved.
namespace SimTuning.WPF.UI.ViewModels.Dyno
{
    using MaterialDesignThemes.Wpf;
    using MvvmCross.Commands;
    using MvvmCross.Logging;
    using MvvmCross.Navigation;
    using MvvmCross.Plugin.Messenger;
    using SimTuning.Core.Models;
    using SimTuning.WPF.UI.Business;
    using SimTuning.WPF.UI.Dialog;
    using System.Globalization;
    using System.Threading.Tasks;
    using System.Windows;

    /// <summary>
    /// WPF-spezifisches Dyno-Diagnose-ViewModel.
    /// </summary>
    /// <seealso cref="SimTuning.Core.ViewModels.Dyno.DiagnosisViewModel" />
    public class DynoDiagnosisViewModel : SimTuning.Core.ViewModels.Dyno.DiagnosisViewModel
    {
        private readonly MvxSubscriptionToken _token;

        /// <summary>
        /// Initializes a new instance of the <see cref="DynoDiagnosisViewModel" /> class.
        /// </summary>
        /// <param name="logProvider">The log provider.</param>
        /// <param name="navigationService">The navigation service.</param>
        public DynoDiagnosisViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService, IMvxMessenger messenger)
            : base(logProvider, navigationService, messenger)
        {
            this.RefreshPlotCommand = new MvxAsyncCommand(RefreshPlot);

            _token = messenger.Subscribe<MvxReloaderMessage>(this.ReloadData);
            // datensatz checken CheckDynoData();
        }

        #region Methods

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
                    base.RefreshPlot();

                    Application.Current.Dispatcher.Invoke(() => args.Session.Close());
                });
            }).ConfigureAwait(true);
        }

        private bool CheckDynoData()
        {
            if (Dyno == null)
            {
                Functions.ShowSnackbarDialog(SimTuning.Core.Business.Functions.GetLocalisedRes(typeof(SimTuning.Core.resources), "ERR_NODATA"));

                return false;
            }
            else { return true; }
        }

        #endregion Methods
    }
}