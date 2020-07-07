using SimTuning.WPFCore.ViewModels.Auslass;
using System.Windows.Controls;
using MvvmCross.Platforms.Wpf.Views;

namespace SimTuning.Forms.WPFCore.Views.Auslass
{
    public partial class AuslassTheorieView : MvxWpfView<AuslassTheorieViewModel>
    {
        public AuslassTheorieView()
        {
            InitializeComponent();

            //DataContext = new AuslassTheorieViewModel();
        }
    }
}