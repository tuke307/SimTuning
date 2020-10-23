// project=SimTuning.Core, file=AussehenViewModel.cs, creation=2020:7:31 Copyright (c)
// 2020 tuke productions. All rights reserved.
namespace SimTuning.Core.ViewModels.Einstellungen
{
    using MvvmCross.Commands;
    using MvvmCross.Logging;
    using MvvmCross.Navigation;
    using MvvmCross.ViewModels;
    using SimTuning.Core.Models;
    using System.Resources;
    using System.Threading.Tasks;

    /// <summary>
    /// AussehenViewModel.
    /// </summary>

    public class AussehenViewModel : MvxNavigationViewModel
    {
        protected ResourceManager rm;

        public IMvxCommand ApplyAccentCommand { get; set; }

        public IMvxCommand ApplyPrimaryCommand { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AussehenViewModel" /> class.
        /// </summary>
        /// <param name="logProvider">The log provider.</param>
        /// <param name="navigationService">The navigation service.</param>
        public AussehenViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService)
            : base(logProvider, navigationService)
        {
            this.rm = new ResourceManager(typeof(SimTuning.Core.resources));
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
            base.Prepare();
        }
    }
}