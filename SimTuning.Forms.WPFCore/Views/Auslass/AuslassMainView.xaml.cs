using MvvmCross.Platforms.Wpf.Views;
using SimTuning.WPFCore.ViewModels.Auslass;
using System.Windows.Controls;

namespace SimTuning.Forms.WPFCore.Views.Auslass
{
    public partial class AuslassMainView : MvxWpfView<AuslassMainViewModel>
    {
        public AuslassMainView()
        {
            InitializeComponent();

            //DataContext = new SimTuning.WPFCore.ViewModels.Auslass.AuslassMainViewModel();
        }
    }
}