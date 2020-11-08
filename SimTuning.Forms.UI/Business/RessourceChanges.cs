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
        /// TODO: add ResourceDictionary to the mergedDictionaries did not work.
        /// </summary>
        /// <param name="primaryColor">Color of the primary.</param>
        /// <param name="secondaryColor">Color of the secondary.</param>
        /// <param name="baseTheme">The base theme.</param>
        public void Colors(PrimaryColor? primaryColor = null, SecondaryColor? secondaryColor = null, BaseTheme? baseTheme = null)
        {
            //Uri changes = null;

            //var mergedDictionaries = Application.Current.Resources.MergedDictionaries;

            //mergedDictionaries.Clear();

            //changes = new Uri($"Themes/Primary/" + primaryColor + ".xaml", UriKind.Relative);
            //mergedDictionaries.Add(new ResourceDictionary() { Source = changes });
            //// mergedDictionaries.ElementAt(0).Source = changes;

            //changes = new Uri($"Themes/Accent/" + secondaryColor + ".xaml", UriKind.Relative);
            //mergedDictionaries.Add(new ResourceDictionary() { Source = changes });
            //// mergedDictionaries.ElementAt(1).Source = changes;

            //changes = new Uri($"Themes/Base/" + baseTheme + ".xaml", UriKind.Relative);
            //mergedDictionaries.Add(new ResourceDictionary() { Source = changes });
            //// mergedDictionaries.ElementAt(2).Source = changes;
        }
    }
}