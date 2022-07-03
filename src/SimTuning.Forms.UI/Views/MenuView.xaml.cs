// Copyright (c) 2021 tuke productions. All rights reserved.
using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using SimTuning.Forms.UI.ViewModels;

namespace SimTuning.Forms.UI.Views
{
    [MvxMasterDetailPagePresentation(MasterDetailPosition.Master)]
    public partial class MenuView : MvxContentPage<MenuViewModel>
    {
        public MenuView()
        {
            InitializeComponent();
        }
    }
}