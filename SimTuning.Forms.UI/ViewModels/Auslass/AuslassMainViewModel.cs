// project=SimTuning.Forms.UI, file=AuslassMainViewModel.cs, creation=2020:6:30 Copyright
// (c) 2020 tuke productions. All rights reserved.
namespace SimTuning.Forms.UI.ViewModels.Auslass
{
    using Microsoft.Extensions.Logging;
    using MvvmCross.Navigation;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// AuslassMainViewModel.
    /// </summary>
    /// <seealso cref="SimTuning.Core.ViewModels.Auslass.MainViewModel" />
    public class AuslassMainViewModel : SimTuning.Core.ViewModels.Auslass.MainViewModel
    {
        private readonly IMvxNavigationService _navigationService;
        private bool _firstTime = true;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuslassMainViewModel" /> class.
        /// </summary>
        /// <param name="logFactory">The log provider.</param>
        /// <param name="navigationService">The navigation service.</param>
        public AuslassMainViewModel(ILoggerFactory logFactory, IMvxNavigationService navigationService)
            : base(logFactory, navigationService)
        {
            this._navigationService = navigationService;
        }

        /// <summary>
        /// Views the appearing.
        /// </summary>
        public override void ViewAppearing()
        {
            if (this._firstTime)
            {
                this.ShowInitialViewModels();
                this._firstTime = false;
            }
        }

        /// <summary>
        /// Shows the initial view models.
        /// </summary>
        /// <returns></returns>
        private Task ShowInitialViewModels()
        {
            var tasks = new List<Task>
            {
                this._navigationService.Navigate<AuslassTheorieViewModel>(),
                this._navigationService.Navigate<AuslassAnwendungViewModel>()
            };
            return Task.WhenAll(tasks);
        }
    }
}