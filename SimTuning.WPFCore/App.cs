using MvvmCross;
using MvvmCross.ViewModels;
using SimTuning.WPFCore.ViewModels;
using SimTuning.WPFCore.ViewModels.Home;
using System.IO;

namespace SimTuning.WPFCore
{
    public class App : MvvmCross.ViewModels.MvxApplication
    {
        public override void Initialize()
        {
            SimTuning.Core.Constants.Platform = Plugin.DeviceInfo.Abstractions.Platform.Windows;

            Data.Constants.DatabasePath = Path.Combine(SimTuning.Core.Constants.FileDirectory, Data.Constants.DatabaseName);

            if (!Directory.Exists(SimTuning.Core.Constants.FileDirectory))
                Directory.CreateDirectory(SimTuning.Core.Constants.FileDirectory);

            RegisterAppStart<MainWindowViewModel>();
        }
    }
}