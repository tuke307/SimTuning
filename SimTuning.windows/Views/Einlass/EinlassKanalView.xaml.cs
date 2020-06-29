using SimTuning.windows.ViewModels.Einlass;
using System;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace SimTuning.windows.Views.Einlass
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