// project=SimTuning.Forms.WPFCore, file=DynoDiagnosisViewModel.cs, creation=2020:7:31
// Copyright (c) 2020 tuke productions. All rights reserved.
namespace SimTuning.Forms.WPFCore.ViewModels.Dyno
{
    using System.Globalization;
    using System.Threading.Tasks;
    using MaterialDesignThemes.Wpf;
    using MvvmCross.Commands;
    using MvvmCross.Logging;
    using MvvmCross.Navigation;
    using SimTuning.Forms.WPFCore.Business;
    using SimTuning.Forms.WPFCore.Views.Dialog;

    /// <summary>
    ///  WPF-spezifisches Dyno-Diagnose-ViewModel.
    /// </summary>
    /// <seealso cref="SimTuning.Core.ViewModels.Dyno.DiagnosisViewModel" />
    public class DynoDiagnosisViewModel : SimTuning.Core.ViewModels.Dyno.DiagnosisViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DynoDiagnosisViewModel"/> class.
        /// </summary>
        /// <param name="logProvider">The log provider.</param>
        /// <param name="navigationService">The navigation service.</param>
        public DynoDiagnosisViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService)
            : base(logProvider, navigationService)
        {
            this.RefreshPlotCommand = new MvxAsyncCommand(RefreshPlot);

            // datensatz checken
            // CheckDynoData();
        }

        #region Methods

        private bool CheckDynoData()
        {
            if (Dyno == null)
            {
                Functions.ShowSnackbarDialog(rm.GetString("ERR_NODATA", CultureInfo.CurrentCulture));

                return false;
            }
            else { return true; }
        }

        protected new async Task RefreshPlot()
        {
            if (!this.CheckDynoData())
            {
                return;
            }

            await DialogHost.Show(new DialogLoadingView(), "DialogLoading", async delegate (object sender, DialogOpenedEventArgs args)
            {
                await base.RefreshPlot().ConfigureAwait(true);

                args.Session.Close();
            }).ConfigureAwait(true);
        }

        #endregion Methods
    }
}