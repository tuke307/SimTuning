using SimTuning.ViewModels;
using System.Windows.Controls;

namespace SimTuning.windows.Views.Auslass
{
    /// <summary>
    /// Interaktionslogik für Auslass.xaml
    /// </summary>
    public partial class AuslassMainView : UserControl
    {
        public AuslassMainView()
        {
            InitializeComponent();

            DataContext = new AuslassMainViewModel();
        }
    }
}