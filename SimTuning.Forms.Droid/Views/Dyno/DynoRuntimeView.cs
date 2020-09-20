using Android.OS;
using Android.Runtime;
using Android.Views;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using MvvmCross.Platforms.Android.Views.Fragments;
using SimTuning.Forms.UI.ViewModels;
using SimTuning.Forms.UI.ViewModels.Dyno;

namespace SimTuning.Droid.Views.Dyno
{
    [MvxDialogFragmentPresentation/*(typeof(MenuViewModel), Resource.Id.content_frame)*/]
    [Register(nameof(DynoRuntimeView))]
    public class DynoRuntimeView : MvxFragment<DynoRuntimeViewModel>
    {
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment return
            // inflater.Inflate(Resource.Layout.YourFragment, container, false);

            return base.OnCreateView(inflater, container, savedInstanceState);
        }
    }
}