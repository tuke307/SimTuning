using Android.App;
using Android.App;
using Android.Support.V7.App;
using MvvmCross;
using MvvmCross.Platforms.Android;
using SimTuning.Forms.UI;
using SimTuning.Forms.UI.Services;
using SimTuning.Forms.UI.Themes;

namespace SimTuning.Forms.Droid
{
    /// <summary>
    /// ThemeService.
    /// </summary>
    /// <seealso cref="SimTuning.Forms.UI.Services.ThemeServiceBase" />
    public class ThemeService : ThemeServiceBase
    {
        Activity Activity => Mvx.IoCProvider.Resolve<IMvxAndroidCurrentTopActivity>().Activity;

        public override void UpdateTheme(BaseTheme themeMode)
        {
            base.UpdateTheme(themeMode);

            bool changed = false;

            switch (ColorSettings.Theme)
            {
                case (int)BaseTheme.Inherit:
                    if (AppCompatDelegate.DefaultNightMode != AppCompatDelegate.ModeNightFollowSystem)
                    {
                        AppCompatDelegate.DefaultNightMode = AppCompatDelegate.ModeNightFollowSystem;
                        changed = true;
                    }
                    break;

                case (int)BaseTheme.Dark:
                    if (AppCompatDelegate.DefaultNightMode != AppCompatDelegate.ModeNightYes)
                    {
                        AppCompatDelegate.DefaultNightMode = AppCompatDelegate.ModeNightYes;
                        changed = true;
                    }
                    break;

                case (int)BaseTheme.Light:
                    if (AppCompatDelegate.DefaultNightMode != AppCompatDelegate.ModeNightNo)
                    {
                        AppCompatDelegate.DefaultNightMode = AppCompatDelegate.ModeNightNo;
                        changed = true;
                    }
                    break;

                default:
                    break;
            }

            if (this.Activity?.Theme != null && changed)
            {
                (this.Activity as MainActivity)?.Delegate.ApplyDayNight();
                this.Activity.Recreate();
            }
        }
    }
}