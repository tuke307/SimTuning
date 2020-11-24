namespace SimTuning.Test
{
    using MvvmCross.Tests;
    using NUnit.Framework;

    /// <summary>
    /// CoreViewModelsTest.
    /// </summary>
    /// <seealso cref="MvvmCross.Tests.MvxTestFixture" />
    public class CoreViewModelsTest : MvxTestFixture
    {
        /// <summary>
        /// AuslassAnwendungViewModelTest.
        /// </summary>
        [Test]
        public void AuslassAnwendungViewModelTest()
        {
            var vm = MvvmCross.IoC.MvxIoCProvider.Instance.IoCConstruct<Core.ViewModels.Auslass.AnwendungViewModel>();
            vm.CalculateCommand.ListenForRaiseCanExecuteChanged();
            vm.DiffusorStageCommand.ListenForRaiseCanExecuteChanged();
            vm.InsertDataCommand.ListenForRaiseCanExecuteChanged();
        }

        /// <summary>
        /// AuslassMainViewModelTest.
        /// </summary>
        [Test]
        public void AuslassMainViewModelTest()
        {
            var vm = MvvmCross.IoC.MvxIoCProvider.Instance.IoCConstruct<Core.ViewModels.Auslass.MainViewModel>();
        }

        /// <summary>
        /// Auslasses the theorie view model test.
        /// </summary>
        [Test]
        public void AuslassTheorieViewModelTest()
        {
            var vm = MvvmCross.IoC.MvxIoCProvider.Instance.IoCConstruct<Core.ViewModels.Auslass.TheorieViewModel>();
            vm.InsertDataCommand.ListenForRaiseCanExecuteChanged();
        }

        /// <summary>
        /// Demoes the main view model test.
        /// </summary>
        [Test]
        public void DemoMainViewModelTest()
        {
            var vm = MvvmCross.IoC.MvxIoCProvider.Instance.IoCConstruct<Core.ViewModels.Demo.DemoMainViewModel>();
            vm.OpenWebsiteCommand.ListenForRaiseCanExecuteChanged();
        }

        /// <summary>
        /// Dynoes the audio player view model test.
        /// </summary>
        [Test]
        public void DynoAudioPlayerViewModelTest()
        {
            var vm = MvvmCross.IoC.MvxIoCProvider.Instance.IoCConstruct<Core.ViewModels.Dyno.AudioPlayerViewModel>();
            vm.CutBeginnCommand.ListenForRaiseCanExecuteChanged();
            vm.CutEndCommand.ListenForRaiseCanExecuteChanged();
            vm.DragCompletedCommand.ListenForRaiseCanExecuteChanged();
            vm.DragStartedCommand.ListenForRaiseCanExecuteChanged();
            vm.PlayPauseCommand.ListenForRaiseCanExecuteChanged();
            vm.SkipForwardCommand.ListenForRaiseCanExecuteChanged();
            vm.SkipBackwardsCommand.ListenForRaiseCanExecuteChanged();
        }

        [Test]
        public void DynoAusrollenViewModelTest()
        {
            var vm = MvvmCross.IoC.MvxIoCProvider.Instance.IoCConstruct<Core.ViewModels.Dyno.AusrollenViewModel>();
            vm.RefreshPlotCommand.ListenForRaiseCanExecuteChanged();
            vm.ShowDiagnosisCommand.ListenForRaiseCanExecuteChanged();
        }

        /// <summary>
        /// Dynoes the beschleunigung view model test.
        /// </summary>
        [Test]
        public void DynoBeschleunigungViewModelTest()
        {
            var vm = MvvmCross.IoC.MvxIoCProvider.Instance.IoCConstruct<Core.ViewModels.Dyno.GeschwindigkeitViewModel>();
            vm.RefreshPlotCommand.ListenForRaiseCanExecuteChanged();
            vm.ShowAusrollenCommand.ListenForRaiseCanExecuteChanged();
        }

        /// <summary>
        /// Dynoes the data view model test.
        /// </summary>
        [Test]
        public void DynoDataViewModelTest()
        {
            var vm = MvvmCross.IoC.MvxIoCProvider.Instance.IoCConstruct<Core.ViewModels.Dyno.DataViewModel>();
            vm.DeleteDynoCommand.ListenForRaiseCanExecuteChanged();
            vm.ExportDynoCommand.ListenForRaiseCanExecuteChanged();
            vm.NewDynoCommand.ListenForRaiseCanExecuteChanged();
            vm.SaveDynoCommand.ListenForRaiseCanExecuteChanged();
            vm.ShowSaveButtonCommand.ListenForRaiseCanExecuteChanged();
        }

        /// <summary>
        /// Dynoes the diagnosis view model test.
        /// </summary>
        [Test]
        public void DynoDiagnosisViewModelTest()
        {
            var vm = MvvmCross.IoC.MvxIoCProvider.Instance.IoCConstruct<Core.ViewModels.Dyno.DiagnosisViewModel>();
            vm.InsertVehicleCommand.ListenForRaiseCanExecuteChanged();
            vm.RefreshPlotCommand.ListenForRaiseCanExecuteChanged();
            vm.ShowSaveCommand.ListenForRaiseCanExecuteChanged();
        }

        /// <summary>
        /// Dynoes the main view model test.
        /// </summary>
        [Test]
        public void DynoMainViewModelTest()
        {
            var vm = MvvmCross.IoC.MvxIoCProvider.Instance.IoCConstruct<Core.ViewModels.Dyno.MainViewModel>();
        }

        /// <summary>
        /// Dynoes the runtime view model test.
        /// </summary>
        [Test]
        public void DynoRuntimeViewModelTest()
        {
            var vm = MvvmCross.IoC.MvxIoCProvider.Instance.IoCConstruct<Core.ViewModels.Dyno.RuntimeViewModel>();
            vm.ResetAccelerationCommand.ListenForRaiseCanExecuteChanged();
            vm.ShowSpectrogramCommand.ListenForRaiseCanExecuteChanged();
            vm.StartAccelerationCommand.ListenForRaiseCanExecuteChanged();
            vm.StopAccelerationCommand.ListenForRaiseCanExecuteChanged();
        }

        /// <summary>
        /// Dynoes the spectrogram view model test.
        /// </summary>
        [Test]
        public void DynoSpectrogramViewModelTest()
        {
            var vm = MvvmCross.IoC.MvxIoCProvider.Instance.IoCConstruct<Core.ViewModels.Dyno.SpectrogramViewModel>();
            vm.FilterPlotCommand.ListenForRaiseCanExecuteChanged();
            vm.RefreshAudioFileCommand.ListenForRaiseCanExecuteChanged();
            vm.RefreshPlotCommand.ListenForRaiseCanExecuteChanged();
            vm.RefreshSpectrogramCommand.ListenForRaiseCanExecuteChanged();
            vm.SpecificGraphCommand.ListenForRaiseCanExecuteChanged();
        }

        /// <summary>
        /// Einlasses the kanal view model test.
        /// </summary>
        [Test]
        public void EinlassKanalViewModelTest()
        {
            var vm = MvvmCross.IoC.MvxIoCProvider.Instance.IoCConstruct<Core.ViewModels.Einlass.KanalViewModel>();
            vm.InsertDataCommand.ListenForRaiseCanExecuteChanged();
        }

        /// <summary>
        /// Einlasses the main view model test.
        /// </summary>
        [Test]
        public void EinlassMainViewModelTest()
        {
            var vm = MvvmCross.IoC.MvxIoCProvider.Instance.IoCConstruct<Core.ViewModels.Einlass.MainViewModel>();
        }

        /// <summary>
        /// Einlasses the vergaser view model test.
        /// </summary>
        [Test]
        public void EinlassVergaserViewModelTest()
        {
            var vm = MvvmCross.IoC.MvxIoCProvider.Instance.IoCConstruct<Core.ViewModels.Einlass.VergaserViewModel>();
            vm.InsertDataCommand.ListenForRaiseCanExecuteChanged();
        }

        /// <summary>
        /// Einstellungens the application view model test.
        /// </summary>
        [Test]
        public void EinstellungenApplicationViewModelTest()
        {
            var vm = MvvmCross.IoC.MvxIoCProvider.Instance.IoCConstruct<Core.ViewModels.Einstellungen.ApplicationViewModel>();
        }

        /// <summary>
        /// Einstellungens the aussehen view model test.
        /// </summary>
        [Test]
        public void EinstellungenAussehenViewModelTest()
        {
            var vm = MvvmCross.IoC.MvxIoCProvider.Instance.IoCConstruct<Core.ViewModels.Einstellungen.AussehenViewModel>();
        }

        /// <summary>
        /// Einstellungens the konto view model test.
        /// </summary>
        [Test]
        public void EinstellungenKontoViewModelTest()
        {
            var vm = MvvmCross.IoC.MvxIoCProvider.Instance.IoCConstruct<Core.ViewModels.Einstellungen.KontoViewModel>();
            vm.RegisterSiteCommand.ListenForRaiseCanExecuteChanged();
            vm.ConnectUserCommand.ListenForRaiseCanExecuteChanged();
            vm.PasswordChangedCommand.ListenForRaiseCanExecuteChanged();
        }

        /// <summary>
        /// Einstellungens the menu view model test.
        /// </summary>
        [Test]
        public void EinstellungenMenuViewModelTest()
        {
            var vm = MvvmCross.IoC.MvxIoCProvider.Instance.IoCConstruct<Core.ViewModels.Einstellungen.MenuViewModel>();
        }

        /// <summary>
        /// Einstellungens the vehicles view model test.
        /// </summary>
        [Test]
        public void EinstellungenVehiclesViewModelTest()
        {
            var vm = MvvmCross.IoC.MvxIoCProvider.Instance.IoCConstruct<Core.ViewModels.Einstellungen.VehiclesViewModel>();
            vm.DeleteVehicleCommand.ListenForRaiseCanExecuteChanged();
            vm.NewVehicleCommand.ListenForRaiseCanExecuteChanged();
            vm.SaveVehicleCommand.ListenForRaiseCanExecuteChanged();
            vm.ShowSaveButtonCommand.ListenForRaiseCanExecuteChanged();
        }

        /// <summary>
        /// Homes the home view model view model test.
        /// </summary>
        [Test]
        public void HomeHomeViewModelViewModelTest()
        {
            var vm = MvvmCross.IoC.MvxIoCProvider.Instance.IoCConstruct<Core.ViewModels.Home.HomeViewModel>();
            vm.OpenDonateCommand.ListenForRaiseCanExecuteChanged();
            vm.OpenEmailCommand.ListenForRaiseCanExecuteChanged();
            vm.OpenInstagramCommand.ListenForRaiseCanExecuteChanged();
            vm.OpenTutorialCommand.ListenForRaiseCanExecuteChanged();
            vm.OpenTwitterCommand.ListenForRaiseCanExecuteChanged();
            vm.OpenWebsiteCommand.ListenForRaiseCanExecuteChanged();
        }

        /// <summary>
        /// Mains the page view model test.
        /// </summary>
        [Test]
        public void MainPageViewModelTest()
        {
            var vm = MvvmCross.IoC.MvxIoCProvider.Instance.IoCConstruct<SimTuning.Core.ViewModels.MainPage>();
            vm.ShowHomeViewModelCommand.ListenForRaiseCanExecuteChanged();
            vm.ShowMenuViewModelCommand.ListenForRaiseCanExecuteChanged();
        }

        /// <summary>
        /// Menus the view model test.
        /// </summary>
        [Test]
        public void MenuViewModelTest()
        {
            var vm = MvvmCross.IoC.MvxIoCProvider.Instance.IoCConstruct<SimTuning.Core.ViewModels.Menu>();
            vm.ButtonCloseMenu.ListenForRaiseCanExecuteChanged();
            vm.ButtonOpenMenu.ListenForRaiseCanExecuteChanged();
        }

        /// <summary>
        /// Motors the hubraum view model test.
        /// </summary>
        [Test]
        public void MotorHubraumViewModelTest()
        {
            var vm = MvvmCross.IoC.MvxIoCProvider.Instance.IoCConstruct<Core.ViewModels.Motor.HubraumViewModel>();
        }

        /// <summary>
        /// Motors the main view model test.
        /// </summary>
        [Test]
        public void MotorMainViewModelTest()
        {
            var vm = MvvmCross.IoC.MvxIoCProvider.Instance.IoCConstruct<Core.ViewModels.Motor.MainViewModel>();
        }

        /// <summary>
        /// Motors the steuerdiagramm view model test.
        /// </summary>
        [Test]
        public void MotorSteuerdiagrammViewModelTest()
        {
            var vm = MvvmCross.IoC.MvxIoCProvider.Instance.IoCConstruct<Core.ViewModels.Motor.SteuerdiagrammViewModel>();
            vm.InsertReferenceCommand.ListenForRaiseCanExecuteChanged();
            vm.InsertVehicleCommand.ListenForRaiseCanExecuteChanged();
        }

        /// <summary>
        /// Motors the umrechnung view model test.
        /// </summary>
        [Test]
        public void MotorUmrechnungViewModelTest()
        {
            var vm = MvvmCross.IoC.MvxIoCProvider.Instance.IoCConstruct<Core.ViewModels.Motor.UmrechnungViewModel>();
            vm.InsertDataCommand.ListenForRaiseCanExecuteChanged();
        }

        /// <summary>
        /// Motors the verdichtung view model test.
        /// </summary>
        [Test]
        public void MotorVerdichtungViewModelTest()
        {
            var vm = MvvmCross.IoC.MvxIoCProvider.Instance.IoCConstruct<Core.ViewModels.Motor.VerdichtungViewModel>();
            vm.InsertDataCommand.ListenForRaiseCanExecuteChanged();
        }
    }
}