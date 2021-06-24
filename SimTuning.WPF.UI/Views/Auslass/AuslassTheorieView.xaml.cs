// project=SimTuning.WPF.UI, file=AuslassTheorieView.xaml.cs, creation=2020:9:7 Copyright
// (c) 2021 tuke productions. All rights reserved.
using MvvmCross.Platforms.Wpf.Views;
using Mvx.Wpf.ItemsPresenter;
using SimTuning.WPF.UI.ViewModels.Auslass;

namespace SimTuning.WPF.UI.Views.Auslass
{
    [MvxWpfPresenter("AuslassRegion", mvxViewPosition.NewOrExsist)]
    public partial class AuslassTheorieView : MvxWpfView<AuslassTheorieViewModel>
    {
        public AuslassTheorieView()
        {
            this.InitializeComponent();
        }
    }
}