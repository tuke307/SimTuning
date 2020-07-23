using MvvmCross.Platforms.Wpf.Views;
using MvvmCross.ViewModels;

namespace SimTuning.Forms.WPF.Views.Auslass
{
    public partial class AuslassAnwendungView : MvxWpfView/*<AuslassAnwendungViewModel>*/
    {
        public AuslassAnwendungView()
        {
            InitializeComponent();

            //DataContext = new AuslassAnwendungViewModel();
        }
    }
}