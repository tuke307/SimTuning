﻿// Copyright (c) 2021 tuke productions. All rights reserved.
using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.DependencyInjection;
using SimTuning.Data.Models;
using SimTuning.Maui.UI.ViewModels;
using SimTuning.Maui.UI.Views.Popups;

namespace SimTuning.Maui.UI.Views.Motor
{
    public partial class MotorVerdichtungView : ContentView
    {
        public MotorVerdichtungViewModel ViewModel => (MotorVerdichtungViewModel)BindingContext;

        public MotorVerdichtungView()
        {
            InitializeComponent();

            BindingContext = Ioc.Default.GetRequiredService<MotorVerdichtungViewModel>();
        }

        private async void HelperVehiclesButton_Clicked(object sender, EventArgs e)
        {
            var task = Application.Current.MainPage.ShowPopupAsync(new VehiclePopup());
            var result = await task;
            if (result != null)
            {
                ViewModel.InsertHelperVehicle(result as VehiclesModel);
            }
        }
    }
}