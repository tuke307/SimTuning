// project=SimTuning.Forms.WPFCore, file=DynoDiagnosisViewModel.cs, creation=2020:7:31
// Copyright (c) 2020 tuke productions. All rights reserved.
using MaterialDesignThemes.Wpf;
using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using SimTuning.Forms.WPFCore.Business;
using SimTuning.Forms.WPFCore.Views.Dialog;
using System.Globalization;
using System.Threading.Tasks;

namespace SimTuning.Forms.WPFCore.ViewModels.Dyno
{
    public class DynoDiagnosisViewModel : SimTuning.Core.ViewModels.Dyno.DiagnosisViewModel
    {
        public DynoDiagnosisViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService) : base(logProvider, navigationService)
        {
            RefreshPlotCommand = new MvxAsyncCommand(RefreshPlot);

            //datensatz checken
            //CheckDynoData();
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
    }
}