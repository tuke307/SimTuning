// project=SimTuning.WPF.UI, file=EinlassKanalView.xaml.cs, creation=2020:7:7 Copyright
// (c) 2020 tuke productions. All rights reserved.
using MvvmCross.Platforms.Wpf.Presenters.Attributes;
using MvvmCross.Platforms.Wpf.Views;
using SimTuning.WPF.UI.Region;
using SimTuning.WPF.UI.ViewModels.Einlass;

namespace SimTuning.WPFCore.App.Views.Einlass
{
    [MvxWpfPresenter("EinlassRegion", mvxViewPosition.NewOrExsist)]
    public partial class EinlassKanalView : MvxWpfView<EinlassKanalViewModel>
    {
        public EinlassKanalView()
        {
            InitializeComponent();
        }
    }
}