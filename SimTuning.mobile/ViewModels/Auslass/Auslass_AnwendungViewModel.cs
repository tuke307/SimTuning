using Data.Models;
using System.IO;
using Xamarin.Forms;

namespace SimTuning.mobile.ViewModels.Auslass
{
    public class Auslass_AnwendungViewModel : SimTuning.ViewModels.Auslass.AnwendungViewModel
    {
        public Auslass_AnwendungViewModel()
        {
            //auslass = new AuslassLogic();
            CalculateCommand = new Command(Calculate);
            DiffusorStageCommand = new Command(DiffusorStage);

            InsertDataCommand = new Command(InsertData);
        }

        protected void Calculate(object parameter)
        {
            Stream stream = base.Calculate();
            Auspuff = ImageSource.FromStream(() => stream);
        }
       

        public ImageSource Auspuff
        {
            get => Get<ImageSource>();
            private set => Set(value);
        }
    }
}