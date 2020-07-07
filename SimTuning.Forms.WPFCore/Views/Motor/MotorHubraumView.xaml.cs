using MvvmCross.Platforms.Wpf.Views;
using SimTuning.WPFCore.ViewModels.Motor;
using System.Windows.Controls;

namespace SimTuning.Forms.WPFCore.Views.Motor
{
    public partial class MotorHubraumView : MvxWpfView<MotorHubraumViewModel>
    {
        public MotorHubraumView()
        {
            InitializeComponent();

            //DataContext = new MotorHubraumViewModel();
        }
    }
}