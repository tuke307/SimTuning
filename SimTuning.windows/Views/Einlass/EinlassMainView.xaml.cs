using SimTuning.windows.ViewModels.Einlass;
using System.Windows.Controls;

namespace SimTuning.windows.Views.Einlass
{
    /// <summary>
    /// Interaktionslogik für Einlass.xaml
    /// </summary>
    public partial class EinlassMainView : UserControl
    {
        public EinlassMainView()
        {
            InitializeComponent();

            DataContext = new EinlassMainViewModel();
        }
    }
}