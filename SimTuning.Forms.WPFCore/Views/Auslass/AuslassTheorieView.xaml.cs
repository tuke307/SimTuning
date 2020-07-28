using MvvmCross.Platforms.Wpf.Presenters.Attributes;
using MvvmCross.Platforms.Wpf.Views;
using SimTuning.WPFCore.Region;
using SimTuning.WPFCore.ViewModels.Auslass;

namespace SimTuning.Forms.WPFCore.Views.Auslass
{
    [MvxWpfPresenter("AuslassRegion", mvxViewPosition.NewOrExsist)]
    //[MvxContentPresentation(WindowIdentifier = nameof(MainWindow), StackNavigation = false)]
    public partial class AuslassTheorieView : MvxWpfView<AuslassTheorieViewModel>
    {
        public AuslassTheorieView()
        {
            InitializeComponent();
        }
    }
}