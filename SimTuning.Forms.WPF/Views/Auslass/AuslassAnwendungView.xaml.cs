using SimTuning.WPFCore.ViewModels.Auslass;
using System.Windows.Controls;
using MvvmCross.Platforms.Wpf.Views;

using SimTuning.WPFCore.ViewModels.Auslass;

namespace SimTuning.Forms.WPF.Views.Auslass
{
    public partial class AuslassAnwendungView : MvxWpfView<AuslassAnwendungViewModel>
    {
        public AuslassAnwendungView()
        {
            InitializeComponent();

            //DataContext = new AuslassAnwendungViewModel();
        }
    }
}