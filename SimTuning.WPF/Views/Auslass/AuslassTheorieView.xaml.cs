using SimTuning.WPF.ViewModels.Auslass;
using System.Windows.Controls;

namespace SimTuning.WPF.Views.Auslass
{
    public partial class AuslassTheorieView : UserControl
    {
        public AuslassTheorieView()
        {
            InitializeComponent();

            DataContext = new AuslassTheorieViewModel();
        }
    }
}