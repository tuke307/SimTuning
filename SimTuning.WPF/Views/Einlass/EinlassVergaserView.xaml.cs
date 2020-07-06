using SimTuning.WPF.ViewModels.Einlass;
using System.Windows.Controls;

namespace SimTuning.WPF.Views.Einlass
{
    /// <summary>
    /// Interaktionslogik für Einlass_Vergaser.xaml
    /// </summary>
    public partial class EinlassVergaserView : UserControl
    {
        public EinlassVergaserView()
        {
            InitializeComponent();

            DataContext = new EinlassVergaserViewModel();
        }
    }
}