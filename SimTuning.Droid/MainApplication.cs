using Android.App;
using Android.Runtime;
using MvvmCross.Platforms.Android.Core;
using MvvmCross.Platforms.Android.Views;
using SimTuning.Forms.UI;
using System;

namespace SimTuning.Droid
{
    [Application]
    public class MainApplication : MvxAndroidApplication<MvxAndroidSetup<App>, App>
    {
        public MainApplication(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
        }
    }
}