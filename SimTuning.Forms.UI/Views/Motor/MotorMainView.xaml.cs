// project=SimTuning.Forms.UI, file=MotorMainView.xaml.cs, creation=2020:6:30
// Copyright (c) 2020 tuke productions. All rights reserved.
using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using SimTuning.Forms.UI.ViewModels.Motor;

namespace SimTuning.Forms.UI.Views.Motor
{
    [MvxTabbedPagePresentation(TabbedPosition.Root)]
    [MvxMasterDetailPagePresentation(MasterDetailPosition.Detail, WrapInNavigationPage = true, NoHistory = true)]
    public partial class MotorMainView : MvxTabbedPage<MotorMainViewModel>
    {
        public MotorMainView()
        {
            InitializeComponent();

            //BindingContext = new MotorMainViewModel();
        }
    }
}