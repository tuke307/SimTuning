using SimTuning.windows.ViewModels;
using SimTuning.windows.ViewModels.Tuning;
using System.Windows.Controls;

namespace SimTuning.windows.Views.Tuning
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