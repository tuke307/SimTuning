using SimTuning.WPF.ViewModels;
using SimTuning.WPF.ViewModels.Dyno;
using System.Windows.Controls;

namespace SimTuning.WPF.Views.Dyno
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