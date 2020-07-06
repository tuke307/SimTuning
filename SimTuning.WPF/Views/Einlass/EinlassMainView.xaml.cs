using SimTuning.WPF.ViewModels.Einlass;
using System.Windows.Controls;

namespace SimTuning.WPF.Views.Einlass
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