﻿using SimTuning.WPFCore.ViewModels;
using SimTuning.WPFCore.ViewModels.Dyno;
using System.Windows.Controls;
using MvvmCross.Platforms.Wpf.Views;

namespace SimTuning.Forms.WPFCore.Views.Dyno
{
    public partial class DynoDiagnosisView : MvxWpfView<DynoDiagnosisViewModel>
    {
        public DynoDiagnosisView(/*MainWindowViewModel mainWindowViewModel*/)
        {
            InitializeComponent();

            //DataContext = new DynoDiagnosisViewModel(mainWindowViewModel);
        }
    }
}