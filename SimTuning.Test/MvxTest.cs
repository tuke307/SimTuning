// project=SimTuning.Test, file=MvxTest.cs, creation=2021:6:23 Copyright (c) 2021 tuke
// productions. All rights reserved.
namespace SimTuning.Test
{
    using MvvmCross.Base;
    using MvvmCross.Commands;
    using MvvmCross.Tests;
    using MvvmCross.Views;
    using NUnit.Framework;
    using System.IO;

    /// <summary>
    /// MvxTest.
    /// </summary>
    /// <seealso cref="MvvmCross.Tests.MvxIoCSupportingTest" />
    public class MvxTest : MvxIoCSupportingTest
    {
        protected MvxMockViewDispatcher MockDispatcher
        {
            get;
            private set;
        }

        /// <summary>
        /// Runs the before any tests.
        /// </summary>
        [SetUp]
        public virtual void SetupTest()
        {
            Setup();

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
            base.AdditionalSetup();

            MockDispatcher = new MvxMockViewDispatcher();
            Ioc.RegisterSingleton<IMvxMainThreadDispatcher>(MockDispatcher);
            Ioc.RegisterSingleton<IMvxViewDispatcher>(MockDispatcher);

            //this.Ioc.RegisterSingleton(Plugin.Settings.CrossSettings.Current);
            //this.Ioc.RegisterSingleton(MediaManager.CrossMediaManager.Current);
        }
    }
}