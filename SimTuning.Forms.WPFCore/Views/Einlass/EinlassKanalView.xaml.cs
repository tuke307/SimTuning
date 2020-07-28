using MvvmCross.Platforms.Wpf.Presenters.Attributes;
using MvvmCross.Platforms.Wpf.Views;
using SimTuning.WPFCore.Region;
using SimTuning.WPFCore.ViewModels.Einlass;

namespace SimTuning.Forms.WPFCore.Views.Einlass
{
    [MvxWpfPresenter("EinlassRegion", mvxViewPosition.NewOrExsist)]
    //[MvxContentPresentation(WindowIdentifier = nameof(MainWindow), StackNavigation = false)]
    public partial class EinlassKanalView : MvxWpfView<EinlassKanalViewModel>
    {
        public EinlassKanalView()
        {
            InitializeComponent();
        }
    }
}