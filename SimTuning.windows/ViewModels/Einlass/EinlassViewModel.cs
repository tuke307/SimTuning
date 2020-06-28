using SimTuning.ViewModels;
using SimTuning.windows.Business;
using SimTuning.windows.Views.Einlass;
using System.Windows.Input;

namespace SimTuning.windows.ViewModels.Einlass
{
    public class EinlassViewModel : BaseViewModel
    {
        public EinlassViewModel()
        {
            NewContentCommand = new ActionCommand(NewContent);
            NewContent("Kanal");
        }

        public object EinlassContent
        {
            get => Get<object>();
            set => Set(value);
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