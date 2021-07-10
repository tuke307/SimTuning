// project=SimTuning.Core, file=MainPage.cs, creation=2020:7:31 Copyright (c) 2021 tuke
// productions. All rights reserved.
namespace SimTuning.Core.ViewModels
{
    using Microsoft.Extensions.Logging;
    using MvvmCross.Commands;
    using MvvmCross.Navigation;
    using MvvmCross.ViewModels;
    using System.Threading.Tasks;

    /// <summary>
    /// MainPage-ViewModel.
    /// </summary>
    /// <seealso cref="MvvmCross.ViewModels.MvxNavigationViewModel" />
    public class MainPage : MvxNavigationViewModel
    {
        /// <summary>
        /// Gets or sets the show home view model command.
        /// </summary>
        /// <value>The show home view model command.</value>
        public IMvxAsyncCommand ShowHomeViewModelCommand { get; protected set; }

        /// <summary>
        /// Gets or sets the show menu view model command.
        /// </summary>
        /// <value>The show menu view model command.</value>
        public IMvxAsyncCommand ShowMenuViewModelCommand { get; protected set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MainPage" /> class.
        /// </summary>
        /// <param name="logFactory">The log provider.</param>
        /// <param name="navigationService">The navigation service.</param>
        public MainPage(ILoggerFactory logFactory, IMvxNavigationService navigationService)
            : base(logFactory, navigationService)
        {
        }

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        /// <returns>Initialisierung.</returns>
        public override Task Initialize()
        {
            return base.Initialize();
        }

        /// <summary>
        /// Prepares this instance. called after construction.
        /// </summary>
        public override void Prepare()
        {
            base.Prepare();
        }
    }
}