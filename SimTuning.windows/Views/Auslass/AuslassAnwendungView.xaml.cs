using SimTuning.windows.ViewModels.Auslass;
using System.Windows.Controls;

namespace SimTuning.windows.Views.Auslass
{
    /// <summary>
    /// Interaction logic for Auslass_Anwendung.xaml
    /// </summary>
    public partial class AuslassAnwendungView : UserControl
    {
        public AuslassAnwendungView()
        {
            InitializeComponent();

            DataContext = new Auslass_AnwendungViewModel();
        }
    }
}