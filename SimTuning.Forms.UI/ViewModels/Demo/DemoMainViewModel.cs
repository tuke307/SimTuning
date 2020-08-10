// project=SimTuning.Forms.UI, file=DemoMainViewModel.cs, creation=2020:6:30
// Copyright (c) 2020 tuke productions. All rights reserved.
using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using System;
using Xamarin.Essentials;

namespace SimTuning.Forms.UI.ViewModels.Demo
{
    public class DemoMainViewModel : SimTuning.Core.ViewModels.Demo.DemoMainViewModel
    {
        public DemoMainViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService) : base(logProvider, navigationService)
        {
            //override commands
            OpenWebsiteCommand = new MvxCommand(OpenWebsite);
        }

        protected override void OpenWebsite()
        {
            Launcher.OpenAsync(new Uri("https://tuke-productions.de"));
        }
    }
}