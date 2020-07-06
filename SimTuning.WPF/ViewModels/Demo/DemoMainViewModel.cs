﻿using MvvmCross.Commands;
using SimTuning.WPF.Business;

namespace SimTuning.WPF.ViewModels.Demo
{
    public class DemoMainViewModel : SimTuning.Core.ViewModels.Demo.BuyProViewModel
    {
        public DemoMainViewModel()
        {
            OpenWebsiteCommand = new MvxCommand(OpenWebsite);
        }

        protected override void OpenWebsite()
        {
            Functions.GoToSite("https://www.tuke-productions.de");
        }
    }
}