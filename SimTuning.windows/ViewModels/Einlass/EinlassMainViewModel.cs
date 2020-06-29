using SimTuning.ViewModels;
using SimTuning.windows.Business;
using SimTuning.windows.Views.Einlass;
using System.Windows.Input;
using MvvmCross.ViewModels;

namespace SimTuning.windows.ViewModels.Einlass
{
    public class EinlassMainViewModel : MvxViewModel
    {
        public EinlassMainViewModel()
        {
            NewContentCommand = new ActionCommand(NewContent);
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