﻿// Copyright (c) 2021 tuke productions. All rights reserved.
using SimTuning.Core.ViewModels.Einstellungen;
using CommunityToolkit.Mvvm.DependencyInjection;

namespace SimTuning.Maui.UI.Views.Einstellungen
{
    public partial class EinstellungenMenuView : ContentPage
    {
        public EinstellungenMenuView()
        {
            InitializeComponent();

            BindingContext = Ioc.Default.GetRequiredService<MenuViewModel>();
        }

        public MenuViewModel ViewModel => (MenuViewModel)BindingContext;
    }
}