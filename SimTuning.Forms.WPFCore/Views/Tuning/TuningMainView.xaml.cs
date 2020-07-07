using MvvmCross.Platforms.Wpf.Views;
using SimTuning.WPFCore.ViewModels;
using SimTuning.WPFCore.ViewModels.Tuning;
using System.Windows.Controls;

namespace SimTuning.Forms.WPFCore.Views.Tuning
{
    public partial class TuningMainView : MvxWpfView<TuningMainViewModel>
    {
        public TuningMainView(/*MainWindowViewModel mainWindowViewModel*/)
        {
            InitializeComponent();

            //DataContext = new TuningMainViewModel(mainWindowViewModel);
        }
    }
}