using SimTuning.windows.Business;
using SimTuning.windows.Views.Auslass;
using System.Windows.Input;

namespace SimTuning.ViewModels
{
    public class AuslassViewModel : BaseViewModel
    {
        public AuslassViewModel()
        {
            NewContentCommand = new ActionCommand(NewContent);
            NewContent("Theorie");
        }

        public object AuslassContent
        {
            get => Get<object>();
            set => Set(value);
        }

        public ICommand NewContentCommand { get; set; }

        public void NewContent(object parameter)
        {
            switch (parameter)
            {
                case "Theorie":
                    AuslassContent = new AuslassTheorieView();
                    break;

                case "Anwendung":
                    AuslassContent = new AuslassAnwendungView();
                    break;

                default:
                    break;
            }
        }
    }
}