using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SimTuning.WPF.Base.ViewModels.Auslass
{
    public class AuslassMainViewModel : SimTuning.Core.ViewModels.Auslass.MainViewModel
    {
        private readonly IMvxNavigationService _navigationService;
        private bool _firstTime = true;

        public AuslassMainViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService) : base(logProvider, navigationService)
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

        private Task ShowInitialViewModels()
        {
            var tasks = new List<Task>();
            tasks.Add(_navigationService.Navigate<AuslassTheorieViewModel>());
            tasks.Add(_navigationService.Navigate<AuslassAnwendungViewModel>());
            return Task.WhenAll(tasks);
        }

        public override void ViewAppearing()
        {
            if (_firstTime)
            {
                ShowInitialViewModels();
                _firstTime = false;

                AuslassTabIndex = 0;
            }
        }
    }
}