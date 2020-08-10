// project=SimTuning.Core, file=HomeViewModel.cs, creation=2020:7:31
// Copyright (c) 2020 tuke productions. All rights reserved.
using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System.Threading.Tasks;

namespace SimTuning.Core.ViewModels.Home
{
    public class HomeViewModel : MvxNavigationViewModel
    {
        public HomeViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService) : base(logProvider, navigationService)
        {
        }

        public IMvxCommand OpenInstagramCommand { get; set; }
        public IMvxCommand OpenWebsiteCommand { get; set; }
        public IMvxCommand OpenTwitterCommand { get; set; }
        public IMvxCommand OpenEmailCommand { get; set; }
        public IMvxCommand OpenDonateCommand { get; set; }
        public MvxCommand OpenTutorialCommand { get; set; }

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

        protected virtual void OpenInstagram()
        {
        }

        protected virtual void OpenWebsite()
        {
        }

        protected virtual void OpenTwitter()
        {
        }

        protected virtual void OpenEmail()
        {
        }

        protected virtual void OpenDonate()
        {
        }

        protected virtual void OpenTutorial()
        {
        }

        #endregion Commands
    }
}