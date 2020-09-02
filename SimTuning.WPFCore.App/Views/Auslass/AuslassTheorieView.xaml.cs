// project=SimTuning.WPF.UI, file=AuslassTheorieView.xaml.cs, creation=2020:7:7 Copyright
// (c) 2020 tuke productions. All rights reserved.
using MvvmCross.Platforms.Wpf.Views;
using SimTuning.WPF.UI.Region;
using SimTuning.WPF.UI.ViewModels.Auslass;

namespace SimTuning.WPFCore.App.Views.Auslass
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