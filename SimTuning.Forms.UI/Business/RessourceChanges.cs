using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace SimTuning.Forms.UI.Business
{
    public class RessourceChanges
    {
        /// <summary>
        /// Colorses the specified primary color.
        /// </summary>
        /// <param name="primaryColor">Color of the primary.</param>
        /// <param name="secondaryColor">Color of the secondary.</param>
        /// <param name="baseTheme">The base theme.</param>
        public void Colors(PrimaryColor? primaryColor = null, SecondaryColor? secondaryColor = null, BaseTheme? baseTheme = null)
        {
            Uri changes = null;

            //if (primaryColor != null)
            //{
            //    changes = new Uri($"Themes/Primary/" + primaryColor + ".xaml");
            //}

            //if (secondaryColor != null)
            //{
            //    changes = new Uri($"Themes/Accent/" + secondaryColor + ".xaml");
            //}

            if (baseTheme != null)
            {
                changes = new Uri($"Themes/Base/" + baseTheme + ".xaml");
            }

            if (changes != null)
            {
                ICollection<ResourceDictionary> mergedDictionaries = Application.Current.Resources.MergedDictionaries;
                mergedDictionaries.Remove(mergedDictionaries.Last());
                mergedDictionaries.Add(new ResourceDictionary() { Source = changes });

                XF.Material.Forms.Material.Use("Material.Configuration");
            }
        }
    }
}