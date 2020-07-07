using SimTuning.WPFCore.ViewModels.Einlass;
using System.Windows.Controls;
using MvvmCross.Platforms.Wpf.Views;

namespace SimTuning.Forms.WPF.Views.Einlass
{
    public partial class EinlassMainView : MvxWpfView<EinlassMainViewModel>
    {
        public EinlassMainView()
        {
            InitializeComponent();

            //DataContext = new EinlassMainViewModel();
        }
    }
}