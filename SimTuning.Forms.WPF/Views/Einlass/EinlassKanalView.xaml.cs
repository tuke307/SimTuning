using MvvmCross.Platforms.Wpf.Views;

namespace SimTuning.Forms.WPF.Views.Einlass
{
    public partial class EinlassKanalView : MvxWpfView/*<EinlassKanalViewModel>*/
    {
        public EinlassKanalView()
        {
            InitializeComponent();

            //DataContext = new EinlassKanalViewModel();
        }
    }
}