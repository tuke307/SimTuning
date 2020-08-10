// project=SimTuning.Forms.WPFCore, file=AuslassAnwendungViewModel.cs, creation=2020:7:31
// Copyright (c) 2020 tuke productions. All rights reserved.
using MaterialDesignThemes.Wpf;
using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using SimTuning.Forms.WPFCore.Views.Dialog;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace SimTuning.Forms.WPFCore.ViewModels.Auslass
{
    public class AuslassAnwendungViewModel : SimTuning.Core.ViewModels.Auslass.AnwendungViewModel
    {
        public AuslassAnwendungViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService) : base(logProvider, navigationService)
        {
        }

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        /// <returns>Initilisierung.</returns>
        public override Task Initialize()
        {
            //override command
            CalculateCommand = new MvxAsyncCommand(CalculateAsync);

            return base.Initialize();
        }

        protected new async Task CalculateAsync()
        {
            await DialogHost.Show(new DialogLoadingView(), "DialogLoading", delegate (object sender, DialogOpenedEventArgs args)
            {
                Stream stream = base.Calculate();
                PngBitmapDecoder decoder = new PngBitmapDecoder(stream, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.Default);
                Auspuff = decoder.Frames[0];

                args.Session.Close();
            }).ConfigureAwait(true);
        }

        private BitmapSource _auspuff;

        public BitmapSource Auspuff
        {
            get => _auspuff;
            set => SetProperty(ref _auspuff, value);
        }
    }
}