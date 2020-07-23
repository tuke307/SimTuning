using MvvmCross.Platforms.Wpf.Views;

namespace SimTuning.Forms.WPF.Views.Auslass
{
    public partial class AuslassMainView : MvxWpfView/*<AuslassMainViewModel>*/
    {
        public AuslassMainView()
        {
            InitializeComponent();

            //DataContext = new SimTuning.WPFCore.ViewModels.Auslass.AuslassMainViewModel();
        }
    }
}