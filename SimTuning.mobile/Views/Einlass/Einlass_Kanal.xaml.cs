//using SimTuning.mobile.ViewModels.Einlass;
//using System.Windows.Controls;
using SimTuning.mobile.ViewModels.Einlass;
using Xamarin.Forms;

namespace SimTuning.mobile.Views.Einlass
{
    /// <summary>
    /// Interaktionslogik für Einlass_Kanal.xaml
    /// </summary>
    public partial class Einlass_Kanal : ContentPage
    {
        public Einlass_Kanal()
        {
            InitializeComponent();

            BindingContext = new Einlass_KanalViewModel();
        }
    }
}