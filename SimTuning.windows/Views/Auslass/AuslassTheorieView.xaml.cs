using SimTuning.ViewModels.Auslass;
using System.Windows.Controls;

namespace SimTuning.windows.Views.Auslass
{
    /// <summary>
    /// Interaction logic for Auslass_Theorie.xaml
    /// </summary>
    public partial class AuslassTheorieView : UserControl
    {
        public AuslassTheorieView()
        {
            InitializeComponent();

            DataContext = new Auslass_TheorieViewModel();
        }
    }
}