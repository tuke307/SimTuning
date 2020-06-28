using SimTuning.windows.Business;
using System.IO;
using System.Windows.Media.Imaging;

namespace SimTuning.windows.ViewModels.Auslass
{
    public class Auslass_AnwendungViewModel : SimTuning.ViewModels.Auslass.AnwendungViewModel
    {
        public Auslass_AnwendungViewModel()
        {
            CalculateCommand = new ActionCommand(Calculate);
            DiffusorStageCommand = new ActionCommand(DiffusorStage);
            InsertDataCommand = new ActionCommand(InsertData);
        }


        protected void Calculate(object parameter)
        {
            Stream stream = base.Calculate();   
            PngBitmapDecoder decoder = new PngBitmapDecoder(stream, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.Default);
            Auspuff = decoder.Frames[0];
        }

     
        public BitmapSource Auspuff
        {
            get => Get<BitmapSource>();
            set => Set(value);
        }
    }
}