using MvvmCross.Tests;
using NUnit.Framework;
using SimTuning.Core.ViewModels;

namespace SimTuning.Test
{
    public class CoreViewModelsTest : MvxTestFixture
    {
        [Test]
        public void AuslassAnwendungViewModelTest()
        {
            var vm = MvvmCross.IoC.MvxIoCProvider.Instance.IoCConstruct<Core.ViewModels.Auslass.AnwendungViewModel>();
        }

        [Test]
        public void AuslassMainViewModelTest()
        {
            var vm = MvvmCross.IoC.MvxIoCProvider.Instance.IoCConstruct<Core.ViewModels.Auslass.MainViewModel>();
        }

        [Test]
        public void AuslassTheorieViewModelTest()
        {
            var vm = MvvmCross.IoC.MvxIoCProvider.Instance.IoCConstruct<Core.ViewModels.Auslass.TheorieViewModel>();
        }

        [Test]
        public void DemoMainViewModelTest()
        {
            var vm = MvvmCross.IoC.MvxIoCProvider.Instance.IoCConstruct<Core.ViewModels.Demo.DemoMainViewModel>();
        }

        [Test]
        public void MainPageViewModelTest()
        {
            var vm = MvvmCross.IoC.MvxIoCProvider.Instance.IoCConstruct<MainPage>();
            vm.ShowHomeViewModelCommand.ListenForRaiseCanExecuteChanged();
            vm.ShowMenuViewModelCommand.ListenForRaiseCanExecuteChanged();
        }

        [Test]
        public void MenuViewModelTest()
        {
            var vm = MvvmCross.IoC.MvxIoCProvider.Instance.IoCConstruct<Menu>();
            vm.ButtonCloseMenu.ListenForRaiseCanExecuteChanged();
            vm.ButtonOpenMenu.ListenForRaiseCanExecuteChanged();
        }
    }
}