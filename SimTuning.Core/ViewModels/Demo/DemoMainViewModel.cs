// project=SimTuning.Core, file=DemoMainViewModel.cs, creation=2020:7:31
// Copyright (c) 2020 tuke productions. All rights reserved.
using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System.Threading.Tasks;

namespace SimTuning.Core.ViewModels.Demo
{
    public class DemoMainViewModel : MvxNavigationViewModel
    {
        public DemoMainViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService) : base(logProvider, navigationService)
        {
        }

        public IMvxCommand OpenWebsiteCommand { get; set; }

        /// <summary>
        /// Prepares this instance.
        /// called after construction.
        /// </summary>
        public override void Prepare()
        {
        }

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        /// <returns>Initilisierung.</returns>
        public override Task Initialize()
        {
            return base.Initialize();
        }

        #region Commands

        protected virtual void OpenWebsite()
        {
            //Business.Functions.GoToSite("https://www.tuke-productions.de");
        }

        #endregion Commands
    }
}