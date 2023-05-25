﻿using CommunityToolkit.Mvvm.DependencyInjection;
using Serilog;
using SimTuning.Core.Services;
using SimTuning.Data;
using SimTuning.Maui.UI.Services;
using SimTuning.Maui.UI.ViewModels;

namespace SimTuning.Maui.App
{
    public partial class App : Application
    {
        private bool _initialized;

        public App()
        {
            InitializeComponent();

            // Register services
            if (!_initialized)
            {
                _initialized = true;
                Ioc.Default.ConfigureServices(
                    new ServiceCollection()
                    .AddLogging(logging => logging.AddSerilog(dispose: true))

                    .AddSingleton<DatabaseContext, DatabaseContext>()
                    .AddSingleton<IVehicleService, VehicleService>()
                    .AddSingleton<IBrowserService, BrowserService>()
                    .AddSingleton<INavigationService, NavigationService>()

                    .AddTransient<MainPageViewModel>()
                    .AddTransient<VehiclesViewModel>()
                    .AddTransient<HomeViewModel>()
                    .AddTransient<PortTimingViewModel>()
                    .AddTransient<VehiclesViewModelBase>()

                    .AddTransient<AuslassMainViewModel>()
                    .AddTransient<AuslassAnwendungViewModel>()
                    .AddTransient<AuslassTheorieViewModel>()

                    .AddTransient<EinlassMainViewModel>()
                    .AddTransient<EinlassKanalViewModel>()
                    .AddTransient<EinlassVergaserViewModel>()

                    .AddTransient<MotorMainViewModel>()
                    .AddTransient<MotorHubraumViewModel>()
                    .AddTransient<MotorSteuerdiagrammViewModel>()
                    .AddTransient<MotorUmrechnungViewModel>()
                    .AddTransient<MotorVerdichtungViewModel>()

                    .AddTransient<EinstellungenViewModel>()

                    .AddTransient<VehiclesViewModel>()

                    .AddTransient<DynoMainViewModel>()
                    //.AddTransient<DynoAudioPlayerViewModel>()
                    .AddTransient<DynoAusrollenViewModel>()
                    .AddTransient<DynoDataViewModel>()
                    .AddTransient<DynoDiagnosisViewModel>()
                    .AddTransient<DynoGeschwindigkeitViewModel>()
                    .AddTransient<DynoRuntimeViewModel>()
                    .AddTransient<DynoAudioViewModel>()

                    .BuildServiceProvider());
            }

            MainPage = new SimTuning.Maui.UI.Views.MainPage();
        }
    }
}