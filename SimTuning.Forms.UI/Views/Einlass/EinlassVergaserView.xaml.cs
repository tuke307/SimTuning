// project=SimTuning.Forms.UI, file=EinlassVergaserView.xaml.cs, creation=2020:6:30
// Copyright (c) 2021 tuke productions. All rights reserved.
using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using SimTuning.Forms.UI.ViewModels.Einlass;

namespace SimTuning.Forms.UI.Views.Einlass
{
    [MvxTabbedPagePresentation(TabbedPosition.Tab)]
    public partial class EinlassVergaserView : MvxContentPage<EinlassVergaserViewModel>
    {
        public EinlassVergaserView()
        {
            InitializeComponent();

            //BindingContext = new EinlassVergaserViewModel();
        }
    }
}