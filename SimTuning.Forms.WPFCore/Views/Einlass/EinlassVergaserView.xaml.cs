// project=SimTuning.Forms.WPFCore, file=EinlassVergaserView.xaml.cs, creation=2020:7:31
// Copyright (c) 2020 tuke productions. All rights reserved.
using MvvmCross.Platforms.Wpf.Presenters.Attributes;
using MvvmCross.Platforms.Wpf.Views;
using SimTuning.Forms.WPFCore.Region;
using SimTuning.Forms.WPFCore.ViewModels.Einlass;

namespace SimTuning.Forms.WPFCore.Views.Einlass
{
    [MvxWpfPresenter("EinlassRegion", mvxViewPosition.NewOrExsist)]
    //[MvxContentPresentation(WindowIdentifier = nameof(MainWindow), StackNavigation = false)]
    public partial class EinlassVergaserView : MvxWpfView<EinlassVergaserViewModel>
    {
        public EinlassVergaserView()
        {
            InitializeComponent();
        }
    }
}