using SimTuning.WPFCore.ViewModels.Motor;
using System.Windows.Controls;
using MvvmCross.Platforms.Wpf.Views;

namespace SimTuning.Forms.WPFCore.Views.Motor
{
    /// <summary>
    /// Interaktionslogik für Motor.xaml
    /// </summary>
    public partial class MotorMainView : MvxWpfView<MotorMainViewModel>
    {
        public MotorMainView()
        {
            InitializeComponent();

            //DataContext = new MotorMainViewModel();
        }
    }
}