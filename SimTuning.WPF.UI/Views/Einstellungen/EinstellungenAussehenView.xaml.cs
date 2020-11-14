﻿// project=SimTuning.WPF.UI, file=EinstellungenAussehenView.xaml.cs, creation=2020:7:7
// Copyright (c) 2020 tuke productions. All rights reserved.
using MvvmCross.Platforms.Wpf.Presenters.Attributes;
using MvvmCross.Platforms.Wpf.Views;
using SimTuning.WPF.UI.Region;
using SimTuning.WPF.UI.ViewModels.Einstellungen;

namespace SimTuning.WPF.UI.Views.Einstellungen
{
    [MvxWpfPresenter("PageContent", mvxViewPosition.NewOrExsist)]
    public partial class EinstellungenAussehenView : MvxWpfView<EinstellungenAussehenViewModel>
    {
        public EinstellungenAussehenView()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            this.ViewModel.ShowSnackbar();
        }

        private void ComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            this.ViewModel.ShowSnackbar();
        }
    }
}