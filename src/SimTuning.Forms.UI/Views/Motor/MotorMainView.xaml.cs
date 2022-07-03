// Copyright (c) 2021 tuke productions. All rights reserved.
using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using SimTuning.Forms.UI.ViewModels.Motor;

namespace SimTuning.Forms.UI.Views.Motor
{
    [MvxMasterDetailPagePresentation(MasterDetailPosition.Detail, WrapInNavigationPage = true, NoHistory = true)]
    public partial class MotorMainView : MvxContentPage<MotorMainViewModel>
    {
        public MotorMainView()
        {
            InitializeComponent();
        }
    }
}