﻿using MvvmCross.Platforms.Wpf.Presenters.Attributes;
using MvvmCross.Platforms.Wpf.Views;
using SimTuning.Forms.WPFCore.Region;
using SimTuning.Forms.WPFCore.ViewModels.Auslass;

namespace SimTuning.Forms.WPFCore.Views.Auslass
{
    [MvxWpfPresenter("AuslassRegion", mvxViewPosition.NewOrExsist)]
    //[MvxContentPresentation(WindowIdentifier = nameof(MainWindow), StackNavigation = false)]
    public partial class AuslassAnwendungView : MvxWpfView<AuslassAnwendungViewModel>
    {
        public AuslassAnwendungView()
        {
            InitializeComponent();
        }
    }
}