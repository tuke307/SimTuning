using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SimTuning.Forms.UI.ViewModels.Dyno
{
    public class DynoMainViewModel : MvxViewModel
    {
        private readonly IMvxNavigationService _navigationService;
        private bool _firstTime = true;

        public DynoMainViewModel(IMvxNavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        private Task ShowInitialViewModels()
        {
            var tasks = new List<Task>
            {
                _navigationService.Navigate<DynoDataViewModel>(),
                _navigationService.Navigate<DynoAudioViewModel>(),
                _navigationService.Navigate<DynoSpectrogramViewModel>(),
                _navigationService.Navigate<DynoDiagnosisViewModel>()
            };
            return Task.WhenAll(tasks);
        }

        public override void ViewAppearing()
        {
            if (_firstTime)
            {
                ShowInitialViewModels();
                _firstTime = false;
            }
        }
    }
}