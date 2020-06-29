using Data.Models;
using System.IO;
using Xamarin.Forms;

namespace SimTuning.mobile.ViewModels.Auslass
{
    public class AuslassAnwendungViewModel : SimTuning.ViewModels.Auslass.AnwendungViewModel
    {
        public AuslassAnwendungViewModel()
        {
            CalculateCommand = new Command(Calculate);
            DiffusorStageCommand = new Command(DiffusorStage);

            InsertDataCommand = new Command(InsertData);
        }

        protected void Calculate(object parameter)
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