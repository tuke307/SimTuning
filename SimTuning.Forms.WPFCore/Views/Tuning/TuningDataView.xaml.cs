using MvvmCross.Platforms.Wpf.Views;
using SimTuning.WPFCore.ViewModels;
using SimTuning.WPFCore.ViewModels.Tuning;
using System.Windows.Controls;

namespace SimTuning.Forms.WPFCore.Views.Tuning
{
    public partial class TuningDataView : MvxWpfView<TuningDataViewModel>
    {
        public TuningDataView(/*MainWindowViewModel mainWindowViewModel*/)
        {
            InitializeComponent();

            //DataContext = new TuningDataViewModel(mainWindowViewModel);
        }
    }
}