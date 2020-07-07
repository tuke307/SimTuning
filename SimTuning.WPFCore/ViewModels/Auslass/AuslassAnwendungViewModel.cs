using MvvmCross.Commands;
using System.IO;
using System.Windows.Media.Imaging;

namespace SimTuning.WPFCore.ViewModels.Auslass
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
            PngBitmapDecoder decoder = new PngBitmapDecoder(stream, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.Default);
            Auspuff = decoder.Frames[0];
        }

        private BitmapSource _auspuff;

        public BitmapSource Auspuff
        {
            get => _auspuff;
            set => SetProperty(ref _auspuff, value);
        }
    }
}