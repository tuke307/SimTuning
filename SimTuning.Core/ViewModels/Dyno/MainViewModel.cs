﻿using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System.Threading.Tasks;

namespace SimTuning.Core.ViewModels.Dyno
{
    public class MainViewModel : MvxNavigationViewModel<SimTuning.Core.Models.UserModel>
    {
        public SimTuning.Core.Models.UserModel User { get; protected set; }

        public MainViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService) : base(logProvider, navigationService)
        {
        }

        public override void Prepare(SimTuning.Core.Models.UserModel _user)
        {
            User = _user;
        }

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        /// <returns>Initilisierung.</returns>
        public override Task Initialize()
        {
            return base.Initialize();
        }

        private int _dynoTabIndex;

        public int DynoTabIndex
        {
            get => _dynoTabIndex;
            set { SetProperty(ref _dynoTabIndex, value); }
        }
    }
}