// project=SimTuning.Forms.WPFCore, file=Modifier.cs, creation=2020:8:4
// Copyright (c) 2020 tuke productions. All rights reserved.
using System.Windows;
using System.Windows.Controls;

namespace SimTuning.Forms.WPFCore.Menu
{
    public class Modifier : ModifierBase
    {
        public DependencyProperty Property { get; set; }
        public object Value { get; set; }
        public string TemplatePartName { get; set; }

        public override void Apply(DependencyObject target)
        {
            if (target is FrameworkElement element && Property is DependencyProperty property)
            {
                if (target.GetValue(Control.TemplateProperty) is ControlTemplate template &&
                    template.FindName(TemplatePartName, element) is DependencyObject templatePart)
                {
                    templatePart.SetCurrentValue(property, Value);
                }
                else if (element.FindName(TemplatePartName) is DependencyObject childElement)
                {
                    childElement.SetValue(property, Value);
                }
            }
        }
    }
}