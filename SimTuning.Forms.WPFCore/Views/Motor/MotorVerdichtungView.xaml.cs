using SimTuning.WPFCore.ViewModels.Motor;
using System.Windows.Controls;
using MvvmCross.Platforms.Wpf.Views;

namespace SimTuning.Forms.WPFCore.Views.Motor
{
    /// <summary>
    /// Interaktionslogik für Motor_Verdichtung.xaml
    /// </summary>
    public partial class MotorVerdichtungView : MvxWpfView<MotorVerdichtungViewModel>
    {
        public MotorVerdichtungView()
        {
            InitializeComponent();

            //DataContext = new MotorVerdichtungViewModel();
        }
    }
}