using SimTuning.WPF.ViewModels.Einlass;
using System.Windows.Controls;

namespace SimTuning.WPF.Views.Einlass
{
    /// <summary>
    /// Interaktionslogik für Einlass_Kanal.xaml
    /// </summary>
    public partial class EinlassKanalView : UserControl
    {
        public EinlassKanalView()
        {
            InitializeComponent();

            DataContext = new EinlassKanalViewModel();
        }
    }
}