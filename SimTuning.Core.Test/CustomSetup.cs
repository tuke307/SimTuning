using MvvmCross.Base;
using MvvmCross.Commands;
using MvvmCross.Tests;
using MvvmCross.Views;
using NUnit.Framework;
using System.IO;

namespace SimTuning.Test
{
    [SetUpFixture]
    public class CustomSetup : MvxIoCSupportingTest
    {
        protected MockDispatcher MockDispatcher
        {
            get;
            private set;
        }

        [OneTimeTearDown]
        public void RunAfterAnyTests()
        {
            // ...
        }

        [OneTimeSetUp]
        //[SetUp]
        public void RunBeforeAnyTests()
        {
            base.Setup(); // from MvxIoCSupportingTest

            if (Directory.Exists(Constants.Directory))
            {
                Directory.Delete(Constants.Directory, true);
            }

            Directory.CreateDirectory(Constants.Directory);
        }

        protected override void AdditionalSetup()
        {
            Ioc.RegisterSingleton(Plugin.Settings.CrossSettings.Current);
            Ioc.RegisterSingleton(MediaManager.CrossMediaManager.Current);

            MockDispatcher = new MockDispatcher();
            Ioc.RegisterSingleton<IMvxViewDispatcher>(MockDispatcher);
            Ioc.RegisterSingleton<IMvxMainThreadDispatcher>(MockDispatcher);

            // MvvmCross.Core.MvxSingletonCache.Instance.Settings.AlwaysRaiseInpcOnUserInterfaceThread
            // = false;

            var helper = new MvxUnitTestCommandHelper();
            Ioc.RegisterSingleton<IMvxCommandHelper>(helper);
        }
    }
}