using SimTuning.WPF.ViewModels;
using SimTuning.WPF.ViewModels.Dyno;
using System.Windows.Controls;

namespace SimTuning.WPF.Views.Dyno
{
    public partial class DynoMainView : UserControl
    {
        public DynoMainView(MainWindowViewModel mainWindowViewModel)
        {
            InitializeComponent();

            DataContext = new DynoMainViewModel(mainWindowViewModel);
        }
    }
}