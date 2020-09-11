// project=SimTuning.Droid, file=MainApplication.cs, creation=2020:7:1 Copyright (c) 2020
// tuke productions. All rights reserved.
namespace SimTuning.Droid
{
    using Android.App;
    using Android.Runtime;
    using MvvmCross.Platforms.Android.Core;
    using MvvmCross.Platforms.Android.Views;
    using SimTuning.Forms.UI;
    using System;

    /// <summary>
    /// MainApplication.
    /// </summary>
    /// <seealso cref="MvvmCross.Platforms.Android.Views.MvxAndroidApplication{MvvmCross.Platforms.Android.Core.MvxAndroidSetup{SimTuning.Forms.UI.App}, SimTuning.Forms.UI.App}" />
    [Application]
    public class MainApplication : MvxAndroidApplication<MvxAndroidSetup<App>, App>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MainApplication" /> class.
        /// </summary>
        /// <param name="javaReference">The java reference.</param>
        /// <param name="transfer">The transfer.</param>
        public MainApplication(IntPtr javaReference, JniHandleOwnership transfer)
            : base(javaReference, transfer)
        {
        }
    }
}