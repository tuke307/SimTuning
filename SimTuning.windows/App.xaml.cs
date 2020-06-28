using Data;
using System;
using System.IO;
using System.Windows;

namespace SimTuning.windows
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            SimTuning.Constants.Platform = Plugin.DeviceInfo.Abstractions.Platform.Windows;

            Data.Constants.DatabasePath = Path.Combine(SimTuning.Constants.FileDirectory, Data.Constants.DatabaseName);

            if (!Directory.Exists(SimTuning.Constants.FileDirectory))
                Directory.CreateDirectory(SimTuning.Constants.FileDirectory);
        }
    }
}