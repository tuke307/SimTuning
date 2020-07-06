using SimTuning.WPF.ViewModels.Auslass;
using System.Windows.Controls;

namespace SimTuning.WPF.Views.Auslass
{
    public partial class AuslassAnwendungView : UserControl
    {
        public AuslassAnwendungView()
        {
            InitializeComponent();

            DataContext = new AuslassAnwendungViewModel();
        }
    }
}