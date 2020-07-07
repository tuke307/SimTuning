using SimTuning.WPFCore.ViewModels.Einlass;
using System.Windows.Controls;
using MvvmCross.Platforms.Wpf.Views;

namespace SimTuning.Forms.WPFCore.Views.Einlass
{
    public partial class EinlassKanalView : MvxWpfView<EinlassKanalViewModel>
    {
        public EinlassKanalView()
        {
            InitializeComponent();

            //DataContext = new EinlassKanalViewModel();
        }
    }
}