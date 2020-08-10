// project=SimTuning.Forms.UI, file=AuslassAnwendungView.xaml.cs, creation=2020:6:30
// Copyright (c) 2020 tuke productions. All rights reserved.
using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;

namespace SimTuning.Forms.UI.Views.Auslass
{
    [MvxTabbedPagePresentation(TabbedPosition.Tab)]
    public partial class AuslassAnwendungView : MvxContentPage<SimTuning.Forms.UI.ViewModels.Auslass.AuslassAnwendungViewModel>
    {
        public AuslassAnwendungView()
        {
            InitializeComponent();

            //BindingContext = new AuslassAnwendungViewModel();
        }
    }
}