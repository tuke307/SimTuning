using SimTuning.WPF.ViewModels;
using SimTuning.WPF.ViewModels.Tuning;
using System.Windows.Controls;

namespace SimTuning.WPF.Views.Tuning
{
    /// <summary>
    /// Interaction logic for Data.xaml
    /// </summary>
    public partial class TuningDataView : UserControl
    {
        public TuningDataView(MainWindowViewModel mainWindowViewModel)
        {
            InitializeComponent();

            DataContext = new TuningDataViewModel(mainWindowViewModel);
        }
    }
}