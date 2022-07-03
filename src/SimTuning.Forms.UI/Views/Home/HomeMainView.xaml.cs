// Copyright (c) 2021 tuke productions. All rights reserved.
using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using SimTuning.Forms.UI.ViewModels.Home;

namespace SimTuning.Forms.UI.Views.Home
{
    [MvxMasterDetailPagePresentation(MasterDetailPosition.Detail, WrapInNavigationPage = true, NoHistory = true)]
    public partial class HomeMainView : MvxContentPage<HomeMainViewModel>
    {
        public HomeMainView()
        {
            InitializeComponent();
        }
    }
}