using OxyPlot;
using System;
using XF.Material.Forms;

namespace SimTuning.mobile.Business
{
    public class ApplicationChanges
    {
        //private SettingsModel settings;

        public void LoadColors()
        {
            //SettingsModel settings = SettingsJson.JSONDeserilaize();

            //Uri primary = new Uri($"pack://application:,,,/MaterialDesignColors;component/Themes/Recommended/Primary/MaterialDesignColor." + settings.PrimaryColor + ".xaml");
            //NewResourceDictionary(0, primary);

            //Uri accent = new Uri($"pack://application:,,,/MaterialDesignColors;component/Themes/Recommended/Accent/MaterialDesignColor." + settings.SecondaryColor + ".xaml");
            //NewResourceDictionary(1, accent);

            //Uri basetheme = new Uri($"pack://application:,,,/MaterialDesignThemes.Wpf;component/Themes/MaterialDesignTheme." + StringBaseTheme(settings.DarkMode) + ".xaml");
            //NewResourceDictionary(2, basetheme);
        }

        public void SetPrimary(object primary_color)
        {
            //Uri primary = new Uri($"pack://application:,,,/MaterialDesignColors;component/Themes/Recommended/Primary/MaterialDesignColor." + primary_color.ToString() + ".xaml");
            //NewResourceDictionary(0, primary);

            //settings.PrimaryColor = primary_color.ToString();

            //SettingsJson.JSONSerialize(settings);
        }

        public void SetAccent(object acccent_color)
        {
            //Uri accent = new Uri($"pack://application:,,,/MaterialDesignColors;component/Themes/Recommended/Accent/MaterialDesignColor." + acccent_color.ToString() + ".xaml");
            //NewResourceDictionary(1, accent);

            //settings.SecondaryColor = acccent_color.ToString();

            //SettingsJson.JSONSerialize(settings);
        }

        public void SetBaseTheme(object base_color)
        {
            //Uri basetheme = new Uri($"pack://application:,,,/MaterialDesignThemes.Wpf;component/Themes/MaterialDesignTheme." + StringBaseTheme((bool)base_color) + ".xaml");
            //NewResourceDictionary(2, basetheme);

            //settings.DarkMode = (bool)base_color;

            //SettingsJson.JSONSerialize(settings);
        }

        private void NewResourceDictionary(int position, Uri newvalue)
        {
            //Application.Current.Resources.MergedDictionaries.RemoveAt(position);
            //Application.Current.Resources.MergedDictionaries.Insert(position, new ResourceDictionary() { Source = newvalue });
        }

        private string StringBaseTheme(bool value)
        {
            if (value)
                return "Dark";
            else
                return "Light";
        }
    }
}