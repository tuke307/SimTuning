using MaterialDesignThemes.Wpf;

namespace SimTuning.WPF.UI.Models
{
    public interface IThemeService
    {
        void UpdatePrimary(MaterialDesignColors.PrimaryColor primaryColor);

        void UpdateSecondary(MaterialDesignColors.SecondaryColor secondaryColor);

        void UpdateTheme(MaterialDesignThemes.Wpf.BaseTheme themeMode);
    }
}