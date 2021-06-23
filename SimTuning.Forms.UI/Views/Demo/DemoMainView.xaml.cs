// project=SimTuning.Forms.UI, file=DemoMainView.xaml.cs, creation=2020:6:30 Copyright (c)
// 2021 tuke productions. All rights reserved.
using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using SimTuning.Forms.UI.ViewModels.Demo;

namespace SimTuning.Forms.UI.Views.Demo
{
    [MvxMasterDetailPagePresentation(MasterDetailPosition.Detail, WrapInNavigationPage = true, NoHistory = true)]
    public partial class DemoMainView : MvxContentPage<DemoMainViewModel>
    {
        public DemoMainView()
        {
            InitializeComponent();

            //BindingContext = new DemoMainViewModel();
        }
    }
}