using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace SimTuning.WPF.Base.ViewModels.Auslass
{
    public class AuslassAnwendungViewModel : SimTuning.Core.ViewModels.Auslass.AnwendungViewModel
    {
        public AuslassAnwendungViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService) : base(logProvider, navigationService)
        {
        }

        public override Task Initialize()
        {
            //override command
            CalculateCommand = new MvxCommand(Calculate);

            return base.Initialize();
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