using SimTuning.WPF.ViewModels;
using SimTuning.WPF.ViewModels.Tuning;
using System.Windows.Controls;

namespace SimTuning.WPF.Views.Tuning

{
    /// <summary>
    /// Interaktionslogik für Divers.xaml
    /// </summary>
    public partial class TuningMainView : UserControl
    {
        public TuningMainView(MainWindowViewModel mainWindowViewModel)
        {
            InitializeComponent();

            DataContext = new TuningMainViewModel(mainWindowViewModel);
        }
    }
}