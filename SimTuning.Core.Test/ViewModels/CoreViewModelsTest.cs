using MvvmCross.Tests;
using NUnit.Framework;
using SimTuning.Core.ViewModels;
using SimTuning.Core.ViewModels.Auslass;
using SimTuning.Core.ViewModels.Demo;
using SimTuning.Core.ViewModels.Dyno;
using SimTuning.Core.ViewModels.Einlass;
using SimTuning.Core.ViewModels.Einstellungen;
using SimTuning.Core.ViewModels.Home;
using SimTuning.Core.ViewModels.Motor;
using SimTuning.Core.ViewModels.Tuning;

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
        public void DynoAudioPlayerViewModelTest()
        {
            var vm = MvvmCross.IoC.MvxIoCProvider.Instance.IoCConstruct<Core.ViewModels.Dyno.AudioPlayerViewModel>();
        }

        [Test]
        public void DynoAudioViewModelTest()
        {
            var vm = MvvmCross.IoC.MvxIoCProvider.Instance.IoCConstruct<Core.ViewModels.Dyno.AudioViewModel>();
        }

        [Test]
        public void DynoAusrollenViewModelTest()
        {
            var vm = MvvmCross.IoC.MvxIoCProvider.Instance.IoCConstruct<Core.ViewModels.Dyno.AusrollenViewModel>();
        }

        [Test]
        public void DynoBeschleunigungViewModelTest()
        {
            var vm = MvvmCross.IoC.MvxIoCProvider.Instance.IoCConstruct<Core.ViewModels.Dyno.BeschleunigungViewModel>();
        }

        [Test]
        public void DynoDataViewModelTest()
        {
            var vm = MvvmCross.IoC.MvxIoCProvider.Instance.IoCConstruct<Core.ViewModels.Dyno.DataViewModel>();
        }

        [Test]
        public void DynoDiagnosisViewModelTest()
        {
            var vm = MvvmCross.IoC.MvxIoCProvider.Instance.IoCConstruct<Core.ViewModels.Dyno.DiagnosisViewModel>();
        }

        [Test]
        public void DynoMainViewModelTest()
        {
            var vm = MvvmCross.IoC.MvxIoCProvider.Instance.IoCConstruct<Core.ViewModels.Dyno.MainViewModel>();
        }

        [Test]
        public void DynoRuntimeViewModelTest()
        {
            var vm = MvvmCross.IoC.MvxIoCProvider.Instance.IoCConstruct<Core.ViewModels.Dyno.RuntimeViewModel>();
        }

        [Test]
        public void DynoSettingsViewModelTest()
        {
            var vm = MvvmCross.IoC.MvxIoCProvider.Instance.IoCConstruct<Core.ViewModels.Dyno.SettingsViewModel>();
        }

        [Test]
        public void DynoSpectrogramViewModelTest()
        {
            var vm = MvvmCross.IoC.MvxIoCProvider.Instance.IoCConstruct<Core.ViewModels.Dyno.SpectrogramViewModel>();
        }

        [Test]
        public void EinlassKanalViewModelTest()
        {
            var vm = MvvmCross.IoC.MvxIoCProvider.Instance.IoCConstruct<Core.ViewModels.Einlass.KanalViewModel>();
        }

        [Test]
        public void EinlassMainViewModelTest()
        {
            var vm = MvvmCross.IoC.MvxIoCProvider.Instance.IoCConstruct<Core.ViewModels.Einlass.MainViewModel>();
        }

        [Test]
        public void EinlassVergaserViewModelTest()
        {
            var vm = MvvmCross.IoC.MvxIoCProvider.Instance.IoCConstruct<Core.ViewModels.Einlass.VergaserViewModel>();
        }

        [Test]
        public void EinstellungenApplicationViewModelTest()
        {
            var vm = MvvmCross.IoC.MvxIoCProvider.Instance.IoCConstruct<Core.ViewModels.Einstellungen.ApplicationViewModel>();
        }

        [Test]
        public void EinstellungenAussehenViewModelTest()
        {
            var vm = MvvmCross.IoC.MvxIoCProvider.Instance.IoCConstruct<Core.ViewModels.Einstellungen.AussehenViewModel>();
        }

        [Test]
        public void EinstellungenKontoViewModelTest()
        {
            var vm = MvvmCross.IoC.MvxIoCProvider.Instance.IoCConstruct<Core.ViewModels.Einstellungen.KontoViewModel>();
        }

        [Test]
        public void EinstellungenMainViewModelTest()
        {
            var vm = MvvmCross.IoC.MvxIoCProvider.Instance.IoCConstruct<Core.ViewModels.Einstellungen.MainViewModel>();
        }

        [Test]
        public void EinstellungenVehiclesViewModelTest()
        {
            var vm = MvvmCross.IoC.MvxIoCProvider.Instance.IoCConstruct<Core.ViewModels.Einstellungen.VehiclesViewModel>();
        }

        [Test]
        public void HomeHomeViewModelViewModelTest()
        {
            var vm = MvvmCross.IoC.MvxIoCProvider.Instance.IoCConstruct<Core.ViewModels.Home.HomeViewModel>();
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

        [Test]
        public void MotorHubraumViewModelTest()
        {
            var vm = MvvmCross.IoC.MvxIoCProvider.Instance.IoCConstruct<Core.ViewModels.Motor.HubraumViewModel>();
        }

        [Test]
        public void MotorMainViewModelTest()
        {
            var vm = MvvmCross.IoC.MvxIoCProvider.Instance.IoCConstruct<Core.ViewModels.Motor.MainViewModel>();
        }

        [Test]
        public void MotorSteuerdiagrammViewModelTest()
        {
            var vm = MvvmCross.IoC.MvxIoCProvider.Instance.IoCConstruct<Core.ViewModels.Motor.SteuerdiagrammViewModel>();
        }

        [Test]
        public void MotorUmrechnungViewModelTest()
        {
            var vm = MvvmCross.IoC.MvxIoCProvider.Instance.IoCConstruct<Core.ViewModels.Motor.UmrechnungViewModel>();
        }

        [Test]
        public void MotorVerdichtungViewModelTest()
        {
            var vm = MvvmCross.IoC.MvxIoCProvider.Instance.IoCConstruct<Core.ViewModels.Motor.VerdichtungViewModel>();
        }
    }
}