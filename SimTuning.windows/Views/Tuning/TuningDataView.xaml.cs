using SimTuning.windows.ViewModels;
using SimTuning.windows.ViewModels.Tuning;
using System.Windows.Controls;

namespace SimTuning.windows.Views.Tuning
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