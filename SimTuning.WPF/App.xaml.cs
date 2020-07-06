using System.IO;
using System.Windows;

namespace SimTuning.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            SimTuning.Core.Constants.Platform = Plugin.DeviceInfo.Abstractions.Platform.Windows;

            Data.Constants.DatabasePath = Path.Combine(SimTuning.Core.Constants.FileDirectory, Data.Constants.DatabaseName);

            if (!Directory.Exists(SimTuning.Core.Constants.FileDirectory))
                Directory.CreateDirectory(SimTuning.Core.Constants.FileDirectory);
        }
    }
}