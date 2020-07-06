using SimTuning.WPF.ViewModels;
using SimTuning.WPF.ViewModels.Tuning;
using System.Windows.Controls;

namespace SimTuning.WPF.Views.Tuning
{
    /// <summary>
    /// Interaktionslogik für Tuning_Durchlauf.xaml
    /// </summary>
    public partial class TuningInputView : UserControl
    {
        public TuningInputView(MainWindowViewModel mainWindowViewModel)
        {
            InitializeComponent();

            DataContext = new TuningInputViewModel(mainWindowViewModel);
        }
    }
}