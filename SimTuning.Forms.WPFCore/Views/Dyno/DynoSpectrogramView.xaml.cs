using SimTuning.WPFCore.ViewModels;
using SimTuning.WPFCore.ViewModels.Dyno;
using System.Windows.Controls;
using MvvmCross.Platforms.Wpf.Views;

namespace SimTuning.Forms.WPFCore.Views.Dyno
{
    public partial class DynoSpectrogramView : MvxWpfView<DynoSpectrogramViewModel>
    {
        public DynoSpectrogramView(/*MainWindowViewModel mainWindowViewModel*/)
        {
            InitializeComponent();

            //DataContext = new DynoSpectrogramViewModel(mainWindowViewModel);
        }
    }
}