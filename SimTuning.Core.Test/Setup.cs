using MvvmCross.Tests;
using NUnit.Framework;
using System;
using System.IO;

namespace SimTuning.Test
{
    [SetUpFixture]
    public class Setup : MvxIoCSupportingTest
    {
        [OneTimeSetUp]
        public void RunBeforeAnyTests()
        {
            if (Directory.Exists(Constants.Directory))
            {
                Directory.Delete(Constants.Directory);
            }

            Directory.CreateDirectory(Constants.Directory);
        }
    }
}