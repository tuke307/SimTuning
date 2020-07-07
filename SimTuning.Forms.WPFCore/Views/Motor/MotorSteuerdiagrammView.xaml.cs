using SimTuning.WPFCore.ViewModels.Motor;
using System.Windows.Controls;
using MvvmCross.Platforms.Wpf.Views;

namespace SimTuning.Forms.WPFCore.Views.Motor
{
    public partial class MotorSteuerdiagrammView : MvxWpfView<MotorSteuerdiagrammViewModel>
    {
        public MotorSteuerdiagrammView()
        {
            InitializeComponent();

            //DataContext = new MotorSteuerdiagrammViewModel();
        }
    }
}