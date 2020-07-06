using MvvmCross.Commands;
using MvvmCross.ViewModels;
using SimTuning.WPF.Views.Auslass;
using System.Windows.Input;

namespace SimTuning.WPF.ViewModels.Auslass
{
    public class AuslassMainViewModel : MvxViewModel
    {
        public AuslassMainViewModel()
        {
            NewContentCommand = new MvxCommand<string>(NewContent);
            NewContent("Theorie");
        }

        private object _auslassContent;

        public object AuslassContent
        {
            get => _auslassContent;
            set => SetProperty(ref _auslassContent, value);
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