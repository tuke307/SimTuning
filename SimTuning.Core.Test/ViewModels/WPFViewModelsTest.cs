namespace SimTuning.Test
{
    using MvvmCross.Tests;
    using NUnit.Framework;

    /// <summary>
    /// WPFViewModelsTest.
    /// </summary>
    /// <seealso cref="MvvmCross.Tests.MvxTestFixture" />
    public class WPFViewModelsTest : MvxTestFixture
    {
        /// <summary>
        /// AuslassAnwendungViewModelTest.
        /// </summary>
        [Test]
        public void AuslassAnwendungViewModelTest()
        {
            var vm = MvvmCross.IoC.MvxIoCProvider.Instance.IoCConstruct<WPF.UI.ViewModels.Auslass.AuslassAnwendungViewModel>();
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
            var vm = MvvmCross.IoC.MvxIoCProvider.Instance.IoCConstruct<WPF.UI.ViewModels.Auslass.AuslassMainViewModel>();
        }

        /// <summary>
        /// Auslasses the theorie view model test.
        /// </summary>
        [Test]
        public void AuslassTheorieViewModelTest()
        {
            var vm = MvvmCross.IoC.MvxIoCProvider.Instance.IoCConstruct<WPF.UI.ViewModels.Auslass.AuslassTheorieViewModel>();
            vm.InsertDataCommand.ListenForRaiseCanExecuteChanged();
        }

        /// <summary>
        /// Demoes the main view model test.
        /// </summary>
        [Test]
        public void DemoMainViewModelTest()
        {
            var vm = MvvmCross.IoC.MvxIoCProvider.Instance.IoCConstruct<WPF.UI.ViewModels.Demo.DemoMainViewModel>();
            vm.OpenWebsiteCommand.ListenForRaiseCanExecuteChanged();
        }

        /// <summary>
        /// Dynoes the audio player view model test.
        /// </summary>
        [Test]
        public void DynoAudioPlayerViewModelTest()
        {
            var vm = MvvmCross.IoC.MvxIoCProvider.Instance.IoCConstruct<WPF.UI.ViewModels.Dyno.DynoAudioPlayerViewModel>();
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
            var vm = MvvmCross.IoC.MvxIoCProvider.Instance.IoCConstruct<WPF.UI.ViewModels.Dyno.DynoAusrollenViewModel>();
            vm.RefreshPlotCommand.ListenForRaiseCanExecuteChanged();
            vm.ShowDiagnosisCommand.ListenForRaiseCanExecuteChanged();
        }

        /// <summary>
        /// Dynoes the beschleunigung view model test.
        /// </summary>
        [Test]
        public void DynoBeschleunigungViewModelTest()
        {
            var vm = MvvmCross.IoC.MvxIoCProvider.Instance.IoCConstruct<WPF.UI.ViewModels.Dyno.DynoBeschleunigungViewModel>();
            vm.RefreshPlotCommand.ListenForRaiseCanExecuteChanged();
            vm.ShowAusrollenCommand.ListenForRaiseCanExecuteChanged();
        }

        /// <summary>
        /// Dynoes the data view model test.
        /// </summary>
        [Test]
        public void DynoDataViewModelTest()
        {
            var vm = MvvmCross.IoC.MvxIoCProvider.Instance.IoCConstruct<WPF.UI.ViewModels.Dyno.DynoDataViewModel>();
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
            var vm = MvvmCross.IoC.MvxIoCProvider.Instance.IoCConstruct<WPF.UI.ViewModels.Dyno.DynoDiagnosisViewModel>();
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
            var vm = MvvmCross.IoC.MvxIoCProvider.Instance.IoCConstruct<WPF.UI.ViewModels.Dyno.DynoMainViewModel>();
        }

        /// <summary>
        /// Dynoes the spectrogram view model test.
        /// </summary>
        [Test]
        public void DynoSpectrogramViewModelTest()
        {
            var vm = MvvmCross.IoC.MvxIoCProvider.Instance.IoCConstruct<WPF.UI.ViewModels.Dyno.DynoSpectrogramViewModel>();
            vm.FilterPlotCommand.ListenForRaiseCanExecuteChanged();
            vm.OpenFileCommand.ListenForRaiseCanExecuteChanged();
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
            var vm = MvvmCross.IoC.MvxIoCProvider.Instance.IoCConstruct<WPF.UI.ViewModels.Einlass.EinlassKanalViewModel>();
            vm.InsertDataCommand.ListenForRaiseCanExecuteChanged();
        }

        /// <summary>
        /// Einlasses the main view model test.
        /// </summary>
        [Test]
        public void EinlassMainViewModelTest()
        {
            var vm = MvvmCross.IoC.MvxIoCProvider.Instance.IoCConstruct<WPF.UI.ViewModels.Einlass.EinlassMainViewModel>();
        }

        /// <summary>
        /// Einlasses the vergaser view model test.
        /// </summary>
        [Test]
        public void EinlassVergaserViewModelTest()
        {
            var vm = MvvmCross.IoC.MvxIoCProvider.Instance.IoCConstruct<WPF.UI.ViewModels.Einlass.EinlassVergaserViewModel>();
            vm.InsertDataCommand.ListenForRaiseCanExecuteChanged();
        }

        /// <summary>
        /// Einstellungens the application view model test.
        /// </summary>
        [Test]
        public void EinstellungenApplicationViewModelTest()
        {
            var vm = MvvmCross.IoC.MvxIoCProvider.Instance.IoCConstruct<WPF.UI.ViewModels.Einstellungen.EinstellungenApplicationViewModel>();
        }

        /// <summary>
        /// Einstellungens the aussehen view model test.
        /// </summary>
        [Test]
        public void EinstellungenAussehenViewModelTest()
        {
            var vm = MvvmCross.IoC.MvxIoCProvider.Instance.IoCConstruct<WPF.UI.ViewModels.Einstellungen.EinstellungenAussehenViewModel>();
            vm.ApplyAccentCommand.ListenForRaiseCanExecuteChanged();
            vm.ApplyPrimaryCommand.ListenForRaiseCanExecuteChanged();
        }

        /// <summary>
        /// Einstellungens the konto view model test.
        /// </summary>
        [Test]
        public void EinstellungenKontoViewModelTest()
        {
            var vm = MvvmCross.IoC.MvxIoCProvider.Instance.IoCConstruct<WPF.UI.ViewModels.Einstellungen.EinstellungenKontoViewModel>();
            vm.RegisterSiteCommand.ListenForRaiseCanExecuteChanged();
            vm.ConnectUserCommand.ListenForRaiseCanExecuteChanged();
            vm.PasswordChangedCommand.ListenForRaiseCanExecuteChanged();
        }

        /// <summary>
        /// Einstellungens the main view model test.
        /// </summary>
        [Test]
        public void EinstellungenMenuViewModelTest()
        {
            var vm = MvvmCross.IoC.MvxIoCProvider.Instance.IoCConstruct<WPF.UI.ViewModels.Einstellungen.EinstellungenMenuViewModel>();
        }

        /// <summary>
        /// Einstellungens the vehicles view model test.
        /// </summary>
        [Test]
        public void EinstellungenVehiclesViewModelTest()
        {
            var vm = MvvmCross.IoC.MvxIoCProvider.Instance.IoCConstruct<WPF.UI.ViewModels.Einstellungen.EinstellungenVehiclesViewModel>();
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
            var vm = MvvmCross.IoC.MvxIoCProvider.Instance.IoCConstruct<WPF.UI.ViewModels.Home.HomeMainViewModel>();
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
            var vm = MvvmCross.IoC.MvxIoCProvider.Instance.IoCConstruct<WPF.UI.ViewModels.MainViewModel>();
            vm.ShowHomeViewModelCommand.ListenForRaiseCanExecuteChanged();
            vm.ShowMenuViewModelCommand.ListenForRaiseCanExecuteChanged();
        }

        /// <summary>
        /// Menus the view model test.
        /// </summary>
        [Test]
        public void MenuViewModelTest()
        {
            var vm = MvvmCross.IoC.MvxIoCProvider.Instance.IoCConstruct<WPF.UI.ViewModels.MenuViewModel>();
            vm.ButtonCloseMenu.ListenForRaiseCanExecuteChanged();
            vm.ButtonOpenMenu.ListenForRaiseCanExecuteChanged();
        }

        /// <summary>
        /// Motors the hubraum view model test.
        /// </summary>
        [Test]
        public void MotorHubraumViewModelTest()
        {
            var vm = MvvmCross.IoC.MvxIoCProvider.Instance.IoCConstruct<WPF.UI.ViewModels.Motor.MotorHubraumViewModel>();
        }

        /// <summary>
        /// Motors the main view model test.
        /// </summary>
        [Test]
        public void MotorMainViewModelTest()
        {
            var vm = MvvmCross.IoC.MvxIoCProvider.Instance.IoCConstruct<WPF.UI.ViewModels.Motor.MotorMainViewModel>();
        }

        /// <summary>
        /// Motors the steuerdiagramm view model test.
        /// </summary>
        [Test]
        public void MotorSteuerdiagrammViewModelTest()
        {
            var vm = MvvmCross.IoC.MvxIoCProvider.Instance.IoCConstruct<WPF.UI.ViewModels.Motor.MotorSteuerdiagrammViewModel>();
            vm.InsertReferenceCommand.ListenForRaiseCanExecuteChanged();
            vm.InsertVehicleCommand.ListenForRaiseCanExecuteChanged();
        }

        /// <summary>
        /// Motors the umrechnung view model test.
        /// </summary>
        [Test]
        public void MotorUmrechnungViewModelTest()
        {
            var vm = MvvmCross.IoC.MvxIoCProvider.Instance.IoCConstruct<WPF.UI.ViewModels.Motor.MotorUmrechnungViewModel>();
            vm.InsertDataCommand.ListenForRaiseCanExecuteChanged();
        }

        /// <summary>
        /// Motors the verdichtung view model test.
        /// </summary>
        [Test]
        public void MotorVerdichtungViewModelTest()
        {
            var vm = MvvmCross.IoC.MvxIoCProvider.Instance.IoCConstruct<WPF.UI.ViewModels.Motor.MotorVerdichtungViewModel>();
            vm.InsertDataCommand.ListenForRaiseCanExecuteChanged();
        }
    }
}