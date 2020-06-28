using SimTuning.windows.ViewModels;
using SimTuning.windows.ViewModels.Dyno;
using System.Windows.Controls;

namespace SimTuning.windows.Views.Dyno
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