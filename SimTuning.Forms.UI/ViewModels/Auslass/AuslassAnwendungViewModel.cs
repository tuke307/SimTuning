// project=SimTuning.Forms.UI, file=AuslassAnwendungViewModel.cs, creation=2020:6:30
// Copyright (c) 2020 tuke productions. All rights reserved.
namespace SimTuning.Forms.UI.ViewModels.Auslass
{
    using MvvmCross.Commands;
    using MvvmCross.Logging;
    using MvvmCross.Navigation;
    using System.IO;
    using System.Threading.Tasks;
    using Xamarin.Forms;

    /// <summary>
    /// </summary>
    /// <seealso cref="SimTuning.Core.ViewModels.Auslass.AnwendungViewModel" />
    public class AuslassAnwendungViewModel : SimTuning.Core.ViewModels.Auslass.AnwendungViewModel
    {
        private ImageSource _auspuff;

        public ImageSource Auspuff
        {
            get => _auspuff;
            private set => SetProperty(ref _auspuff, value);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AuslassAnwendungViewModel" />
        /// class.
        /// </summary>
        /// <param name="logProvider">The log provider.</param>
        /// <param name="navigationService">The navigation service.</param>
        public AuslassAnwendungViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService)
            : base(logProvider, navigationService)
        {
            //override command
            this.CalculateCommand = new MvxAsyncCommand(this.Calculate);
        }

        /// <summary>
        /// Calculates this instance.
        /// </summary>
        protected new async Task Calculate()
        {
            Stream stream = base.Calculate();
            Auspuff = ImageSource.FromStream(() => stream);
        }
    }
}