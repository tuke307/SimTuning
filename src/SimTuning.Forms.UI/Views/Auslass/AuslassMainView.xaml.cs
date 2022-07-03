// Copyright (c) 2021 tuke productions. All rights reserved.
using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;

namespace SimTuning.Forms.UI.Views.Auslass
{
    [MvxMasterDetailPagePresentation(MasterDetailPosition.Detail, WrapInNavigationPage = true, NoHistory = true)]
    public partial class AuslassMainView : MvxContentPage<SimTuning.Forms.UI.ViewModels.Auslass.AuslassMainViewModel>
    {
        public AuslassMainView()
        {
            InitializeComponent();
        }
    }
}