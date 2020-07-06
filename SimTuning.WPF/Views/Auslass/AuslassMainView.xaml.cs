using System.Windows.Controls;

namespace SimTuning.WPF.Views.Auslass
{
    public partial class AuslassMainView : UserControl
    {
        public AuslassMainView()
        {
            InitializeComponent();

            DataContext = new SimTuning.WPF.ViewModels.Auslass.AuslassMainViewModel();
        }
    }
}