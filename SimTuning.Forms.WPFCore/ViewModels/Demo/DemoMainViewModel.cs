// project=SimTuning.Forms.WPFCore, file=DemoMainViewModel.cs, creation=2020:7:31
// Copyright (c) 2020 tuke productions. All rights reserved.
using System.Threading.Tasks;
using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using SimTuning.Forms.WPFCore.Business;

namespace SimTuning.Forms.WPFCore.ViewModels.Demo
{
    public class DemoMainViewModel : SimTuning.Core.ViewModels.Demo.DemoMainViewModel
    {
        public DemoMainViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService) : base(logProvider, navigationService)
        {
            //override commands
            OpenWebsiteCommand = new MvxCommand(OpenWebsite);
        }

        /// <summary>
        /// Prepares this instance.
        /// called after construction.
        /// </summary>
        public override void Prepare()
        {
            base.Prepare();
        }

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        /// <returns>Initilisierung.</returns>
        public override Task Initialize()
        {
            return base.Initialize();
        }

        protected override void OpenWebsite()
        {
            Functions.GoToSite("https://www.tuke-productions.de");
        }
    }
}