using MvvmCross.Tests;
using NUnit.Framework;
using SimTuning.Core.ViewModels;
using SimTuning.Core.ViewModels.Auslass;

namespace SimTuning.Test
{
    public class CoreViewModelsTest : MvxTestFixture
    {
        [Test]
        public void MainPageViewModelCanExecuteTest()
        {
            var mainPage = MvvmCross.IoC.MvxIoCProvider.Instance.IoCConstruct<MainPage>();
            mainPage.ShowHomeViewModelCommand.ListenForRaiseCanExecuteChanged();
            mainPage.ShowMenuViewModelCommand.ListenForRaiseCanExecuteChanged();
        }

        [Test]
        public void MenuViewModelCanExecuteTest()
        {
            var menu = MvvmCross.IoC.MvxIoCProvider.Instance.IoCConstruct<Menu>();
            menu.ButtonCloseMenu.ListenForRaiseCanExecuteChanged();
            menu.ButtonOpenMenu.ListenForRaiseCanExecuteChanged();
        }
    }
}