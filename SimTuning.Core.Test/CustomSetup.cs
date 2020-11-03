namespace SimTuning.Test
{
    using MvvmCross.Base;
    using MvvmCross.Commands;
    using MvvmCross.Tests;
    using MvvmCross.Views;
    using NUnit.Framework;
    using System.IO;

    /// <summary>
    /// CustomSetup.
    /// </summary>
    /// <seealso cref="MvvmCross.Tests.MvxIoCSupportingTest" />
    [SetUpFixture]
    public class CustomSetup : MvxIoCSupportingTest
    {
        protected MockDispatcher MockDispatcher
        {
            get;
            private set;
        }

        /// <summary>
        /// Runs the after any tests.
        /// </summary>
        [OneTimeTearDown]
        public void RunAfterAnyTests()
        {
            // ...
        }

        /// <summary>
        /// Runs the before any tests.
        /// </summary>
        [OneTimeSetUp]
        // [SetUp]
        public void RunBeforeAnyTests()
        {
            base.Setup(); // from MvxIoCSupportingTest

            if (Directory.Exists(Constants.Directory))
            {
                Directory.Delete(Constants.Directory, true);
            }

            Directory.CreateDirectory(Constants.Directory);
        }

        /// <summary>
        /// Additionals the setup.
        /// </summary>
        protected override void AdditionalSetup()
        {
            this.Ioc.RegisterSingleton(Plugin.Settings.CrossSettings.Current);
            this.Ioc.RegisterSingleton(MediaManager.CrossMediaManager.Current);

            this.MockDispatcher = new MockDispatcher();
            this.Ioc.RegisterSingleton<IMvxViewDispatcher>(MockDispatcher);
            this.Ioc.RegisterSingleton<IMvxMainThreadDispatcher>(MockDispatcher);

            // MvvmCross.Core.MvxSingletonCache.Instance.Settings.AlwaysRaiseInpcOnUserInterfaceThread
            // = false;

            var helper = new MvxUnitTestCommandHelper();
            this.Ioc.RegisterSingleton<IMvxCommandHelper>(helper);
        }
    }
}