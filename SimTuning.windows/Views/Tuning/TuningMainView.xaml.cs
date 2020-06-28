using SimTuning.windows.ViewModels;
using SimTuning.windows.ViewModels.Tuning;
using System.Windows.Controls;

namespace SimTuning.windows.Views.Tuning

{
    /// <summary>
    /// Interaktionslogik für Divers.xaml
    /// </summary>
    public partial class TuningMainView : UserControl
    {
        public TuningMainView(MainWindowViewModel mainWindowViewModel)
        {
            InitializeComponent();

            DataContext = new TuningViewModel(mainWindowViewModel);
        }
    }
}