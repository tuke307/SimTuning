using MvvmCross.Commands;
using System.IO;
using Xamarin.Forms;

namespace SimTuning.Forms.UI.ViewModels.Auslass
{
    public class AuslassAnwendungViewModel : SimTuning.Core.ViewModels.Auslass.AnwendungViewModel
    {
        public AuslassAnwendungViewModel()
        {
            //override command
            CalculateCommand = new MvxCommand(Calculate);
        }

        protected new void Calculate()
        {
            Stream stream = base.Calculate();
            Auspuff = ImageSource.FromStream(() => stream);
        }

        private ImageSource _auspuff;

        public ImageSource Auspuff
        {
            get => _auspuff;
            private set => SetProperty(ref _auspuff, value);
        }
    }
}