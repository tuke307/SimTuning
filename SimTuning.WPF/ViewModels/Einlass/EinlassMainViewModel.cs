using MvvmCross.Commands;
using MvvmCross.ViewModels;
using SimTuning.WPF.Views.Einlass;
using System.Windows.Input;

namespace SimTuning.WPF.ViewModels.Einlass
{
    public class EinlassMainViewModel : MvxViewModel
    {
        public EinlassMainViewModel()
        {
            NewContentCommand = new MvxCommand<string>(NewContent);
            NewContent("Kanal");
        }

        private object _einlassContent;

        public object EinlassContent
        {
            get => _einlassContent;
            set => SetProperty(ref _einlassContent, value);
        }

        public ICommand NewContentCommand { get; set; }

        public void NewContent(object parameter)
        {
            switch (parameter)
            {
                case "Kanal":
                    EinlassContent = new EinlassKanalView();
                    break;

                case "Vergaser":
                    EinlassContent = new EinlassVergaserView();
                    break;

                default:
                    break;
            }
        }
    }
}