// project=SimTuning.Forms.WPFCore, file=TreeHelpers.cs, creation=2020:8:4
// Copyright (c) 2020 tuke productions. All rights reserved.
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace SimTuning.Forms.WPFCore.Menu
{
    public class TreeHelpers
    {
        public static readonly DependencyProperty ModifiersProperty = DependencyProperty.RegisterAttached(
            "Modifiers", typeof(ModifierCollection), typeof(TreeHelpers), new PropertyMetadata(default(ModifierCollection), PropertyChangedCallback));

        private static void PropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            if (dependencyObject is FrameworkElement element && !element.IsLoaded)
            {
                element.Loaded += ElementOnLoaded;
            }
            else
            {
                ApplyModifiers(e.NewValue as IEnumerable<ModifierBase>);
            }

            void ApplyModifiers(IEnumerable<ModifierBase> modifiers)
            {
                foreach (var modifier in modifiers ?? Enumerable.Empty<ModifierBase>())
                {
                    modifier.Apply(dependencyObject);
                }
            }

            void ElementOnLoaded(object sender, RoutedEventArgs routedEventArgs)
            {
                ((FrameworkElement)sender).Loaded -= ElementOnLoaded;
                ApplyModifiers(GetModifiers((FrameworkElement)sender));
            }
        }

        public static void SetModifiers(DependencyObject element, ModifierCollection value)
        {
            element.SetValue(ModifiersProperty, value);
        }

        public static ModifierCollection GetModifiers(DependencyObject element)
        {
            return (ModifierCollection)element.GetValue(ModifiersProperty);
        }
    }
}