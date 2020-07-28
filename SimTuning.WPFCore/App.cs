using System.IO;
using MvvmCross.ViewModels;
using SimTuning.Core.ViewModels.Auslass;

namespace SimTuning.WPFCore
{
    public class App : MvxApplication
    {
        public override void Initialize()
        {
            SimTuning.Core.Constants.Platform = Plugin.DeviceInfo.Abstractions.Platform.Windows;

            Data.Constants.DatabasePath = Path.Combine(SimTuning.Core.Constants.FileDirectory, Data.Constants.DatabaseName);

            if (!Directory.Exists(SimTuning.Core.Constants.FileDirectory))
                Directory.CreateDirectory(SimTuning.Core.Constants.FileDirectory);

            RegisterAppStart<SimTuning.WPFCore.ViewModels.MainViewModel>();

            base.Initialize();
        }
    }
}