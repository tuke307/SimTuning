using MvvmCross.Core;
using MvvmCross.Platforms.Wpf.Core;
using MvvmCross.Platforms.Wpf.Views;
using System.IO;

namespace SimTuning.Forms.WPFCore
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>

    public partial class App : MvxApplication
    {
        //public App()
        //{
        //    SimTuning.Core.Constants.Platform = Plugin.DeviceInfo.Abstractions.Platform.Windows;

        //    Data.Constants.DatabasePath = Path.Combine(SimTuning.Core.Constants.FileDirectory, Data.Constants.DatabaseName);

        //    if (!Directory.Exists(SimTuning.Core.Constants.FileDirectory))
        //        Directory.CreateDirectory(SimTuning.Core.Constants.FileDirectory);
        //}

        protected override void RegisterSetup()
        {
            this.RegisterSetupType<MvxWpfSetup<SimTuning.WPFCore.App>>();
        }
    }
}