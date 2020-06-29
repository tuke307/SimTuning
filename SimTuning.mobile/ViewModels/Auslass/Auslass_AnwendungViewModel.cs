using Data.Models;
using System.IO;
using Xamarin.Forms;

namespace SimTuning.mobile.ViewModels.Auslass
{
    public class Auslass_AnwendungViewModel : SimTuning.ViewModels.Auslass.AnwendungViewModel
    {
        public Auslass_AnwendungViewModel()
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