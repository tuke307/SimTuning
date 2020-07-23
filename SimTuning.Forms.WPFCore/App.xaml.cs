using MvvmCross.Core;
using MvvmCross.Platforms.Wpf.Views;
using SimTuning.Forms.WPFCore.Views;
using System.Windows.Threading;

namespace SimTuning.Forms.WPFCore
{
    public partial class App : MvxApplication
    {
        protected override void RegisterSetup()
        {
            this.RegisterSetupType<SimTuning.Forms.WPFCore.WPFSetup>();
        }

        //public virtual void ApplicationInitialized()
        //{
        //    if (MainWindow == null) return;

        //    MvxWpfSetupSingleton.EnsureSingletonAvailable(Dispatcher, MainWindow).EnsureInitialized();

        //    RunAppStart();
        //}
    }

    //public partial class App
    //{
    //    /// <summary>
    //    /// Setup complete indicator.
    //    /// </summary>
    //    private bool setupComplete;

    //    /// <summary>
    //    /// Does the setup.
    //    /// </summary>
    //    private void DoSetup()
    //    {
    //        var presenter = new CustomViewPresenter(MainWindow);

    //        var setup = MvxWpfSetupSingleton.EnsureSingletonAvailable(Dispatcher, MainWindow);
    //        setup.EnsureInitialized();

    //        SimTuning.Forms.WPFCore.WPFSetup setup2 = new SimTuning.Forms.WPFCore.WPFSetup(Dispatcher, presenter);
    //        //setup2.EnsureInitialized();

    //        IMvxAppStart start = Mvx.IoCProvider.Resolve<IMvxAppStart>();
    //        start.Start();

    //        this.setupComplete = true;
    //    }

    //    /// <summary>
    //    /// Raises the <see cref="E:System.Windows.Application.Activated" /> event.
    //    /// </summary>
    //    /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
    //    protected override void OnActivated(EventArgs e)
    //    {
    //        if (!this.setupComplete)
    //        {
    //            this.DoSetup();
    //        }

    //        base.OnActivated(e);
    //    }
    //}
}