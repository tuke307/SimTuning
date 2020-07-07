using SimTuning.WPFCore.ViewModels.Auslass;
using System.Windows.Controls;
using MvvmCross.Platforms.Wpf.Views;

namespace SimTuning.Forms.WPF.Views.Auslass
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