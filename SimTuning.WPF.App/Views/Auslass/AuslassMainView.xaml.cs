// project=SimTuning.WPF.App, file=AuslassMainView.xaml.cs, creation=2020:9:2 Copyright
// (c) 2020 tuke productions. All rights reserved.
using MvvmCross.Platforms.Wpf.Views;
using SimTuning.WPF.UI.Region;
using SimTuning.WPF.UI.ViewModels.Auslass;

namespace SimTuning.WPF.App.Views.Auslass
{
    [MvxWpfPresenter("PageContent", mvxViewPosition.NewOrExsist)]
    public partial class AuslassMainView : MvxWpfView<AuslassMainViewModel>
    {
        public AuslassMainView()
        {
            this.InitializeComponent();
        }
    }
}