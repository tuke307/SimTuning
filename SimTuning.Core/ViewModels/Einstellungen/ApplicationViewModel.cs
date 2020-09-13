// project=SimTuning.Core, file=ApplicationViewModel.cs, creation=2020:9:2 Copyright (c)
// 2020 tuke productions. All rights reserved.
namespace SimTuning.Core.ViewModels.Einstellungen
{
    using MvvmCross.Logging;
    using MvvmCross.Navigation;
    using MvvmCross.ViewModels;
    using SimTuning.Core.Models;
    using System.Threading.Tasks;

    /// <summary>
    /// ApplicationViewModel.
    /// </summary>

    public class ApplicationViewModel : MvxNavigationViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationViewModel" /> class.
        /// </summary>
        /// <param name="logProvider">The log provider.</param>
        /// <param name="navigationService">The navigation service.</param>
        public ApplicationViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService)
            : base(logProvider, navigationService)
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

        public override void Prepare()
        {
        }
    }
}