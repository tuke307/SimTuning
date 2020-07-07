using SimTuning.WPFCore.ViewModels;
using SimTuning.WPFCore.ViewModels.Tuning;
using System.Windows.Controls;
using MvvmCross.Platforms.Wpf.Views;

namespace SimTuning.Forms.WPFCore.Views.Tuning
{
    public partial class TuningInputView : MvxWpfView<TuningInputViewModel>
    {
        public TuningInputView(/*MainWindowViewModel mainWindowViewModel*/)
        {
            InitializeComponent();

            //DataContext = new TuningInputViewModel(mainWindowViewModel);
        }
    }
}