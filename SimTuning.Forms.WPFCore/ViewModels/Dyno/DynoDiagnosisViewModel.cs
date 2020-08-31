// project=SimTuning.Forms.WPFCore, file=DynoDiagnosisViewModel.cs, creation=2020:7:30
// Copyright (c) 2020 tuke productions. All rights reserved.
namespace SimTuning.Forms.WPFCore.ViewModels.Dyno
{
    using MaterialDesignThemes.Wpf;
    using MvvmCross.Commands;
    using MvvmCross.Logging;
    using MvvmCross.Navigation;
    using MvvmCross.Plugin.Messenger;
    using SimTuning.Core.Models;
    using SimTuning.Forms.WPFCore.Business;
    using SimTuning.Forms.WPFCore.Views.Dialog;
    using System.Globalization;
    using System.Threading.Tasks;

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

            await DialogHost.Show(new DialogLoadingView(), "DialogLoading", async delegate (object sender, DialogOpenedEventArgs args)
            {
                base.RefreshPlot();

                args.Session.Close();
            }).ConfigureAwait(true);
        }

        private bool CheckDynoData()
        {
            if (Dyno == null)
            {
                Functions.ShowSnackbarDialog(rm.GetString("ERR_NODATA", CultureInfo.CurrentCulture));

                return false;
            }
            else { return true; }
        }

        #endregion Methods
    }
}