using MvvmCross.Platforms.Wpf.Presenters.Attributes;
using MvvmCross.Platforms.Wpf.Views;
using System.Windows;
using System.Windows.Input;

namespace SimTuning.Forms.WPFCore.Views
{
    //[MvxWindowPresentation(Identifier = nameof(MainWindow), Modal = true)]
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }
    }
}