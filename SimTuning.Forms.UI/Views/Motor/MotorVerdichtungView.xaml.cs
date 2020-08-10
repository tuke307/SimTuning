// project=SimTuning.Forms.UI, file=MotorVerdichtungView.xaml.cs, creation=2020:6:30
// Copyright (c) 2020 tuke productions. All rights reserved.
using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using SimTuning.Forms.UI.ViewModels.Motor;

namespace SimTuning.Forms.UI.Views.Motor
{
    [MvxTabbedPagePresentation(TabbedPosition.Tab)]
    public partial class MotorVerdichtungView : MvxContentPage<MotorVerdichtungViewModel>
    {
        public MotorVerdichtungView()
        {
            InitializeComponent();

            //BindingContext = new MotorVerdichtungViewModel();
        }
    }
}