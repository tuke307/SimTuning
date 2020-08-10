// project=SimTuning.Forms.UI, file=EinstellungenMainView.xaml.cs, creation=2020:6:30
// Copyright (c) 2020 tuke productions. All rights reserved.
using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using SimTuning.Forms.UI.ViewModels.Einstellungen;

namespace SimTuning.Forms.UI.Views.Einstellungen
{
    [MvxTabbedPagePresentation(TabbedPosition.Root)]
    [MvxMasterDetailPagePresentation(MasterDetailPosition.Detail, WrapInNavigationPage = true, NoHistory = true)]
    public partial class EinstellungenMainView : MvxTabbedPage<EinstellungenMainViewModel>
    {
        public EinstellungenMainView()
        {
            InitializeComponent();
        }
    }
}