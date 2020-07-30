using MvvmCross.Core;
using SimTuning.Forms.WPFCore.Region;

namespace SimTuning.Forms.WPFCore
{
    public partial class App
    {
        protected override void RegisterSetup()
        {
            base.RegisterSetup();
            this.RegisterSetupType<MvxWpfSetup<MvxApp>>();
        }
    }
}