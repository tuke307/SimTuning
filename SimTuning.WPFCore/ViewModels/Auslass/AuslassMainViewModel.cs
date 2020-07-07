using MvvmCross.Commands;
using MvvmCross.ViewModels;
using System.Windows.Input;

namespace SimTuning.WPFCore.ViewModels.Auslass
{
    public class AuslassMainViewModel : SimTuning.Core.ViewModels.Auslass.MainViewModel
    {
        public AuslassMainViewModel()
        {
            //NewContentCommand = new MvxCommand<string>(NewContent);
            //NewContent("Theorie");
        }

        private object _auslassContent;

        public object AuslassContent
        {
            get => _auslassContent;
            set => SetProperty(ref _auslassContent, value);
        }

        public ICommand NewContentCommand { get; set; }

        //public void NewContent(object parameter)
        //{
        //    switch (parameter)
        //    {
        //        case "Theorie":
        //            AuslassContent = new AuslassTheorieView();
        //            break;

        //        case "Anwendung":
        //            AuslassContent = new AuslassAnwendungView();
        //            break;

        //        default:
        //            break;
        //    }
        //}
    }
}