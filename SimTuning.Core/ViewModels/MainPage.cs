using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using SimTuning.Core.Models;
using System.Threading.Tasks;

namespace SimTuning.Core.ViewModels
{
    /// <summary>
    /// MainPage-ViewModel.
    /// </summary>
    /// <seealso cref="MvvmCross.ViewModels.MvxNavigationViewModel" />
    public class MainPage : MvxNavigationViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MainPage"/> class.
        /// </summary>
        /// <param name="logProvider">The log provider.</param>
        /// <param name="navigationService">The navigation service.</param>
        public MainPage(IMvxLogProvider logProvider, IMvxNavigationService navigationService)
            : base(logProvider, navigationService)
        {
        }

        /// <summary>
        /// Gets or sets the show home view model command.
        /// </summary>
        /// <value>
        /// The show home view model command.
        /// </value>
        public IMvxAsyncCommand ShowHomeViewModelCommand { get; protected set; }

        /// <summary>
        /// Gets or sets the show menu view model command.
        /// </summary>
        /// <value>
        /// The show menu view model command.
        /// </value>
        public IMvxAsyncCommand ShowMenuViewModelCommand { get; protected set; }

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
        /// <returns>Initialisierung.</returns>
        public override Task Initialize()
        {
            return base.Initialize();
        }
    }
}