using MvvmCross.Commands;
using System;
using Xamarin.Essentials;

namespace SimTuning.Forms.UI.ViewModels.Demo
{
    public class DemoMainViewModel : SimTuning.Core.ViewModels.Demo.DemoMainViewModel
    {
        public DemoMainViewModel()
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