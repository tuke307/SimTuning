using MvvmCross.Logging;
using MvvmCross.Navigation;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SimTuning.Forms.WPFCore.ViewModels.Dyno
{
    public class DynoMainViewModel : SimTuning.Core.ViewModels.Dyno.MainViewModel
    {
        private readonly IMvxNavigationService _navigationService;

        public DynoMainViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService) : base(logProvider, navigationService)
        {
            _navigationService = navigationService;
        }

        public override void Prepare(SimTuning.Core.Models.UserModel _user)
        {
            base.Prepare(_user);
        }

        public override Task Initialize()
        {
            return base.Initialize();
        }

        public override void ViewAppearing()
        {
            _navigationService.Navigate<DynoDataViewModel>();
            _navigationService.Navigate<DynoAudioViewModel>();
            _navigationService.Navigate<DynoSpectrogramViewModel>();
            _navigationService.Navigate<DynoDiagnosisViewModel>();

            DynoTabIndex = 0;
        }
    }
}