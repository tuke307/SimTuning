// Copyright (c) 2021 tuke productions. All rights reserved.
using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using SimTuning.Forms.UI.ViewModels.Einlass;

namespace SimTuning.Forms.UI.Views.Einlass
{
    [MvxMasterDetailPagePresentation(MasterDetailPosition.Detail, WrapInNavigationPage = true, NoHistory = true)]
    public partial class EinlassMainView : MvxContentPage<EinlassMainViewModel>
    {
        public EinlassMainView()
        {
            InitializeComponent();
        }
    }
}