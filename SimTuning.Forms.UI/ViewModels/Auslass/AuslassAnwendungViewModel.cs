using MvvmCross.Commands;
using System.IO;
using Xamarin.Forms;

namespace SimTuning.Forms.UI.ViewModels.Auslass
{
    public class AuslassAnwendungViewModel : SimTuning.Core.ViewModels.Auslass.AnwendungViewModel
    {
        public AuslassAnwendungViewModel()
        {
            CalculateCommand = new MvxCommand(Calculate);
            DiffusorStageCommand = new MvxCommand<string>(DiffusorStage);

            InsertDataCommand = new MvxCommand(InsertData);
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