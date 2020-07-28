using MvvmCross.Core;
using SimTuning.WPFCore.Region;

namespace SimTuning.Forms.WPFCore
{
    public partial class App
    {
        protected override void RegisterSetup()
        {
            base.RegisterSetup();
            this.RegisterSetupType<MvxWpfSetup<SimTuning.WPFCore.App>>();
        }
    }
}