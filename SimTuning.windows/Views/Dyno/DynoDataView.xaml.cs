using SimTuning.windows.ViewModels;
using SimTuning.windows.ViewModels.Dyno;
using System.Windows.Controls;

namespace SimTuning.windows.Views.Dyno
{
    /// <summary>
    /// Interaction logic for Data.xaml
    /// </summary>
    public partial class DynoDataView : UserControl
    {
        public DynoDataView(MainWindowViewModel mainWindowViewModel)
        {
            InitializeComponent();

            DataContext = new DynoDataViewModel(mainWindowViewModel);
        }
    }
}