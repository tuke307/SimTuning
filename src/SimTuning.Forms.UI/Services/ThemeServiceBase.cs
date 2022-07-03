// Copyright (c) 2021 tuke productions. All rights reserved.
using SimTuning.Forms.UI.Themes;
using SimTuning.Forms.UI.Themes.Base;
using System.Linq;
using Xamarin.Forms;

namespace SimTuning.Forms.UI.Services
{
    public class ThemeServiceBase : IThemeService
    {
        public BaseTheme CurrentRuntimeTheme { get; private set; }

        public virtual void UpdateTheme(BaseTheme themeMode)
        {
            switch (ColorSettings.Theme)
            {
                case (int)BaseTheme.Inherit:
                    if (themeMode == BaseTheme.Dark)
                        goto case BaseTheme.Dark;
                    else
                        goto case BaseTheme.Light;
                case (int)BaseTheme.Dark:
                    SetTheme(BaseTheme.Dark);
                    break;

                case (int)BaseTheme.Light:
                    SetTheme(BaseTheme.Light);
                    break;

                default:
                    break;
            }
        }

        private void SetTheme(BaseTheme themeMode)
        {
            if (CurrentRuntimeTheme == themeMode)
            {
                return;
            }

            CurrentRuntimeTheme = themeMode;
        }
    }
}